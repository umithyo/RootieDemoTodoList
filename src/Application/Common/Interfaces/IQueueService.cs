using System;
using Application.Common.Models;
using Microsoft.Extensions.Options;

namespace Application.Common.Interfaces
{
    public interface IQueueService
    {
        void Queue(Action<QueueOptions> options, string message);
        void Queue(QueueOptions options, string message);
        void Send(QueueOptions options, string message);
        void Consume(QueueOptions options, Action<string> consumed);
    }
}
