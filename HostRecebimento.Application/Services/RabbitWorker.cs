using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HostRecebimento.Application.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HostRecebimento.Application.Services
{
    public class RabbitWorker : BackgroundService
    {
        private readonly IMessageConsumerService _consumer;
        private readonly ILogger<RabbitWorker> _logger;

        public RabbitWorker(IMessageConsumerService consumer, ILogger<RabbitWorker> logger)
        {
            _consumer = consumer;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker iniciado. Aguardando mensagens...");
            _consumer.Start();
            return Task.CompletedTask;
        }
    }
}
