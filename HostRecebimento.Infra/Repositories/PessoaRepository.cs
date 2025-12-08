using Dapper;
using HostRecebimento.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HostRecebimento.Infra.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {

        private readonly string _connectionString;

        public PessoaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("ConexaoBanco")["ConnectionString"];

        }

        public async Task SalvarAsync(PessoaMensagem pessoa)
        {
            using var connection = new SqlConnection(_connectionString);

            var sql = @"INSERT INTO Pessoa (Nome, Email)
                        VALUES (@Nome, @Email)";

            await connection.ExecuteAsync(sql, pessoa);
        }
    }
}
