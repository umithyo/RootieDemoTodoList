using System;
using Domain.Enums;

namespace Domain.Entities
{
    public class Todo
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string Email { get; set; }
        public DateTime? Date { get; set; }
        public string TimeZone { get; set; }
        public string CronExpression { get; set; }
        public ScheduleType ScheduleType { get; set; }
    }
}