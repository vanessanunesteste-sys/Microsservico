using HostRecebimento.Application.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HostRecebimento.Distribution
{
    public class Startup : IHostedService
    {
        private readonly ILogger<Startup> _logger;
        private readonly IMessageConsumerService _consumerService;

        public Startup(ILogger<Startup> logger, IMessageConsumerService consumerService)
        {
            _logger = logger;
            _consumerService = consumerService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Host Recebimento iniciado. Iniciando consumo...");
            _consumerService.Start(); // inicia o consumidor
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Host Recebimento finalizado.");
            return Task.CompletedTask;
        }
    }
}
