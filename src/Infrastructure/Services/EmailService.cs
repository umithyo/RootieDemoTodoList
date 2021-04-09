using System;
using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }
        public void Send(Action<EmailOptions> options)
        {
            var _options = new EmailOptions();
            options(_options);
            _logger.Log(LogLevel.Information, $"[MOCK] Email has been sent to {_options.EmailAddress}");
        }
    }
}