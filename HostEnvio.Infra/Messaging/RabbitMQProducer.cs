using System.Text;
using System.Text.Json;
using HostEnvio.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace HostEnvio.Infra.Messaging
{
    public class RabbitMQProducer : IRabbitMQProducer
    {
        private readonly ILogger<RabbitMQProducer> _logger;
        private readonly RabbitSettings _settings;

        public RabbitMQProducer(
            ILogger<RabbitMQProducer> logger,
            IOptions<RabbitSettings> options)
        {
            _logger = logger;
            _settings = options.Value;
        }

        public void Publish(PessoaMensagem mensagem)
        {
            var factory = new ConnectionFactory
            {
                HostName = _settings.Host,
                Port = _settings.Port,
                UserName = _settings.User,
                Password = _settings.Password
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: _settings.Queue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var json = JsonSerializer.Serialize(mensagem);
            var body = Encoding.UTF8.GetBytes(json);

            _logger.LogInformation("Enviando mensagem para a fila: {json}", json);

            channel.BasicPublish(
                exchange: "",
                routingKey: _settings.Queue,
                basicProperties: null,
                body: body);
        }
    }
}
