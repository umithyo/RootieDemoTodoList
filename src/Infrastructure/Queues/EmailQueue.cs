using System;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Queues;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Queues
{
    public class EmailQueue : IEmailQueue
    {
        private readonly ILogger<EmailQueue> _logger;
        private readonly IQueueService _queueService;

        public EmailQueue(ILogger<EmailQueue> logger, IQueueService queueService)
        {
            _logger = logger;
            _queueService = queueService;
        }
        public void Send(string email)
        {
            _queueService.Queue(options => options.Name = "MailQueue", email);
        }
    }
}