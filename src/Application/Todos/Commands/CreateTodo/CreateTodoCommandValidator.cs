using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Enums;
using FluentValidation;

namespace Application.Todos.Commands.CreateTodo
{
    public class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
    {
        public CreateTodoCommandValidator()
        {
            RuleFor(v => v.Date)
                .Must(BePassedWithSpecificDateEnum).WithMessage("The specified DateTime is not valid");

            RuleFor(v => v.CronExpression)
                .Must(BePassedWithCronJobEnum).WithMessage("The specified DateTime is not valid");

            RuleFor(v => v.TimeZone)
                .NotEmpty().WithMessage("TimeZone argument is required.")
                .MinimumLength(1).WithMessage("TimeZone argument is required.")
                .Must(BeValidTimeZoneInfo).WithMessage("Invalid TimeZone");


        }

        public bool BePassedWithSpecificDateEnum(CreateTodoCommand model, DateTime? date)
        {
            if (model.ScheduleType == ScheduleType.SpecificDate)
            {
                return date.HasValue;
            }

            return true;
        }

        public bool BePassedWithCronJobEnum(CreateTodoCommand model, string cronExpression)
        {
            if (model.ScheduleType == ScheduleType.CronExpression)
            {
                return !string.IsNullOrEmpty(cronExpression);
            }

            return true;
        }

        public bool BeValidTimeZoneInfo(CreateTodoCommand model, string timeZone)
        {
            try
            {
                return TimeZoneInfo.FindSystemTimeZoneById(timeZone) != null;
            }
            catch
            {
                return false;
            }
        }
    }
}
