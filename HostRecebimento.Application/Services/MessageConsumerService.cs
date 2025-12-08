using System.Text.Json;
using HostRecebimento.Application.Interfaces;
using HostRecebimento.Domain.Entities;
using HostRecebimento.Infra.Messaging;
using HostRecebimento.Infra.Repositories;
using Microsoft.Extensions.Logging;

namespace HostRecebimento.Application.Services
{
    public class MessageConsumerService : IMessageConsumerService
    {
        private readonly IRabbitMQConsumer _consumer;
        private readonly IPessoaRepository _repository;
        private readonly ILogger<MessageConsumerService> _logger;


        public MessageConsumerService(
        IRabbitMQConsumer consumer,
        IPessoaRepository repository,
        ILogger<MessageConsumerService> logger)
        {
            _consumer = consumer;
            _repository = repository;
            _logger = logger;
        }


        public void Start()
        {
            _consumer.Consume(async (pessoa) =>
            {
                _logger.LogInformation("Mensagem recebida: {@pessoa}", pessoa);

                await _repository.SalvarAsync(pessoa); 
            });
        }
    }
}
