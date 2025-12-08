

using HostEnvio.Domain.Entities;

namespace HostEnvio.Infra.Messaging
{
    public interface IRabbitMQProducer
    {
        void Publish(PessoaMensagem mensagem);
    }
}
