using HostEnvio.Application.Interfaces;
using HostEnvio.Domain.Entities;

namespace HostEnvio
{
    public class Startup
    {
        private readonly IMessagePublisherService _publisher;

        public Startup(IMessagePublisherService publisher)
        {
            _publisher = publisher;
        }

        public void Inicializar()
        {
            EnviarMensagem().Wait();
        }

        public async Task EnviarMensagem()
        {
            var message = new PessoaMensagem
            {
                Id = 1,
                Nome = "Vanessa",
                Email = "vanessa@email.com"
            };

            _publisher.Publish(message);
        }
    }
}
