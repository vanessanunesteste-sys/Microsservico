using HostRecebimento.Domain.Entities;

namespace HostRecebimento.Infra.Repositories
{
    public interface IPessoaRepository
    {
        Task SalvarAsync(PessoaMensagem pessoa);
    }
}
