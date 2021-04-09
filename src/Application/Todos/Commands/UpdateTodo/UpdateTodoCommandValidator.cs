using System;
using Domain.Enums;
using FluentValidation;

namespace Application.Todos.Commands.UpdateTodo
{
    public class UpdateTodoCommandValidator : AbstractValidator<UpdateTodoCommand>
    {
        public UpdateTodoCommandValidator()
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

        public bool BePassedWithSpecificDateEnum(UpdateTodoCommand model, DateTime? date)
        {
            if (model.ScheduleType == ScheduleType.SpecificDate)
            {
                return date.HasValue;
            }

            return true;
        }

        public bool BePassedWithCronJobEnum(UpdateTodoCommand model, string cronExpression)
        {
            if (model.ScheduleType == ScheduleType.CronExpression)
            {
                return !string.IsNullOrEmpty(cronExpression);
            }

            return true;
        }

        public bool BeValidTimeZoneInfo(UpdateTodoCommand model, string timeZone)
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
