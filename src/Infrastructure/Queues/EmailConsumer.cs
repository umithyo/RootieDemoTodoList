using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Queues;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Queues
{
    public class EmailConsumer : IEmailConsumer
    {
        private readonly ILogger<EmailConsumer> _logger;
        private readonly IQueueService _queueService;
        private readonly IEmailService _emailService;

        public EmailConsumer(
            ILogger<EmailConsumer> logger,
            IQueueService queueService,
            IEmailService emailService)
        {
            _logger = logger;
            _queueService = queueService;
            _emailService = emailService;
        }
        public void Consume()
        {
            _queueService.Consume(new QueueOptions { Name = "MailQueue" },
                consumed =>
                {
                    _logger.Log(LogLevel.Information, "[Consume] MailQueue consumed. Sending email");
                    _emailService.Send(options => options.EmailAddress = consumed);
                });
        }
    }
}