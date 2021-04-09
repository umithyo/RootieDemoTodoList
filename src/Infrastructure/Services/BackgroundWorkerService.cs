using System;
using System.Linq.Expressions;
using Application.Common.Interfaces;
using Hangfire;

namespace Infrastructure.Services
{
    public class BackgroundWorkerService : IBackgroundWorkerService
    {
        public void Manage(string parameter)
        {

        }
        public string Schedule(Expression<Action> methodCall, TimeSpan delay)
        {
            return BackgroundJob.Schedule(methodCall, delay);
        }

        public string Schedule<T>(Expression<Action<T>> methodCall, TimeSpan delay)
        {
            return BackgroundJob.Schedule<T>(methodCall, delay);
        }

        public void Cron(Expression<Action> methodCall, string cronExpression, TimeZoneInfo timeZoneInfo)
        {
            RecurringJob.AddOrUpdate(methodCall, cronExpression, timeZoneInfo);
        }

        public void Cron<T>(Expression<Action<T>> methodCall, string cronExpression, TimeZoneInfo timeZoneInfo)
        {
            RecurringJob.AddOrUpdate<T>(methodCall, cronExpression, timeZoneInfo);
        }

        public void Enqueue(Expression<Action> methodCall)
        {
            BackgroundJob.Enqueue(methodCall);
        }

        public void Enqueue<T>(Expression<Action<T>> methodCall)
        {
            BackgroundJob.Enqueue<T>(methodCall);
        }
    }
}