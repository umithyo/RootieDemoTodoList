using System;
using System.Linq.Expressions;

namespace Application.Common.Interfaces
{
    public interface IBackgroundWorkerService
    {
        string Schedule(Expression<Action> methodCall, TimeSpan delay);
        string Schedule<T>(Expression<Action<T>> methodCall, TimeSpan delay);
        void Cron(Expression<Action> methodCall, string cronExpression, TimeZoneInfo timeZoneInfo);
        void Cron<T>(Expression<Action<T>> methodCall, string cronExpression, TimeZoneInfo timeZoneInfo);
        void Enqueue(Expression<Action> methodCall);
        void Enqueue<T>(Expression<Action<T>> methodCall);
    }
}
