using HostEnvio.Domain.Entities;

namespace HostEnvio.Application.Interfaces
{
    public interface IMessagePublisherService
    {
        Task Publish(PessoaMensagem mensagem);
    }
}
