using HostEnvio.Application.Interfaces;
using HostEnvio.Domain.Entities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HostEnvio.Distribution
{
    public class Startup : IHostedService
    {
        private readonly IMessagePublisherService _publisher;
        private readonly ILogger<Startup> _logger;

        public Startup(IMessagePublisherService publisher, ILogger<Startup> logger)
        {
            _publisher = publisher;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Enviando mensagem ao subir o host...");

            var mensagem = new PessoaMensagem
            {
                Id = 2,
                Nome = "Vanessa Teste",
                Email = "vanessaTeste@email.com"
            };

            await _publisher.Publish(mensagem);

            _logger.LogInformation("Mensagem enviada com sucesso!");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
