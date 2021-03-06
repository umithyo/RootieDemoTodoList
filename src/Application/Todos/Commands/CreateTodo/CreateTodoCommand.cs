using Application.Common.Interfaces;
using Application.Common.Queues;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Todos.Commands.CreateTodo
{
    public class CreateTodoCommand : IRequest<string>
    {
        /// <summary>
        /// The content that will be emailed
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// The email address
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// The date (if specific) that email will be sent on
        /// </summary>
        public DateTime? Date { get; set; }
        /// <summary>
        /// The TimeZoneId the schedule will be based on
        /// </summary>
        public string TimeZone { get; set; }
        /// <summary>
        /// The Cron Expression (if ScheduleType is Cron) the schedule will be based on
        /// </summary>
        public string CronExpression { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        /// <summary>
        /// The Schedule Type for which the program will schedule the process
        /// </summary>
        public ScheduleType ScheduleType { get; set; }
    }

    public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly IBackgroundWorkerService _backgroundWorkerService;
        private readonly IQueueService _queueService;

        public CreateTodoCommandHandler(
            IApplicationDbContext context,
            IBackgroundWorkerService backgroundWorkerService,
            IQueueService queueService)
        {
            _context = context;
            _backgroundWorkerService = backgroundWorkerService;
            _queueService = queueService;
        }

        public async Task<string> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var entity = new Todo
            {
                Id = Guid.NewGuid().ToString(),
                Content = request.Content,
                Email = request.Email,
                Date = request.Date,
                TimeZone = request.TimeZone,
                CronExpression = request.CronExpression,
                ScheduleType = request.ScheduleType
            };

            if (request.ScheduleType == ScheduleType.SpecificDate)
            {
                _backgroundWorkerService.Schedule<IEmailQueue>(job =>
                                        job.Send(request.Email),
                                        TimeZoneInfo.ConvertTimeFromUtc(request.Date.Value, TimeZoneInfo.FindSystemTimeZoneById(request.TimeZone)).TimeOfDay);
            }
            else
            {
                var cronExpression = request.ScheduleType switch
                {
                    ScheduleType.Daily => "0 12 * * *",
                    ScheduleType.Weekly => "0 12 * * 1",
                    ScheduleType.Monthly => "0 12 1 * *",
                    _ => request.CronExpression
                };

                _backgroundWorkerService.Cron<IEmailQueue>(job =>
                    job.Send(request.Email),
                    cronExpression,
                    TimeZoneInfo.FindSystemTimeZoneById(request.TimeZone));
            }

            _context.Todos.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
