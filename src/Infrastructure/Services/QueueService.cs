using System;
using System.Text;
using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure.Services
{
    public class QueueService : IQueueService
    {
        private readonly ILogger<QueueService> _logger;
        public QueueService(ILogger<QueueService> logger)
        {
            _logger = logger;
        }
        public void Queue(Action<QueueOptions> options, string message)
        {
            var queueOptions = new QueueOptions();
            options(queueOptions);
            Send(queueOptions, message);
        }

        public void Queue(QueueOptions options, string message)
        {
            Send(options, message);
        }

        public void Consume(QueueOptions queueOptions, Action<string> consumed)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            var q = channel.QueueDeclare(queue: queueOptions.Name,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _logger.Log(LogLevel.Information, $"[Queue] {queueOptions.Name} has been received");
                consumed(message);
            };
            channel.BasicConsume(queue: queueOptions.Name,
                                 autoAck: true,
                                 consumer: consumer);
        }

        public void Send(QueueOptions queueOptions, string message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var q = channel.QueueDeclare(queue: queueOptions.Name,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: queueOptions.Name,
                                     basicProperties: null,
                                     body: body);
                _logger.Log(LogLevel.Information, $"[Queue] {queueOptions.Name} has been published");
            }
        }
    }
}