using System.Text.Json;
using HostEnvio.Application.Interfaces;
using HostEnvio.Domain.Entities;
using HostEnvio.Infra.Messaging;
using Microsoft.Extensions.Logging;

namespace HostEnvio.Application.Services
{
    public class MessagePublisherService : IMessagePublisherService
    {
        private readonly IRabbitMQProducer _producer;
        private readonly ILogger<MessagePublisherService> _logger;


        public MessagePublisherService(IRabbitMQProducer producer, ILogger<MessagePublisherService> logger)
        {
            _producer = producer;
            _logger = logger;
        }


        //public void Publish(PessoaMensagem mensagem)
        //{
            
        //}

        Task IMessagePublisherService.Publish(PessoaMensagem mensagem)
        {
            _logger.LogInformation("Publicando mensagem: {json}", JsonSerializer.Serialize(mensagem));

            _producer.Publish(mensagem); 

            return Task.CompletedTask;
        }
    }
}
