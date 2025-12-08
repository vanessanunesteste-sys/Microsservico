using System.Text;
using System.Text.Json;
using HostRecebimento.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HostRecebimento.Infra.Messaging
{
    public class RabbitMQConsumer : IRabbitMQConsumer
    {
        private readonly ILogger<RabbitMQConsumer> _logger;
        private readonly RabbitSettings _settings;

        public RabbitMQConsumer(
            ILogger<RabbitMQConsumer> logger,
            IOptions<RabbitSettings> options)
        {
            _logger = logger;
            _settings = options.Value;
        }

        public void Consume(Action<PessoaMensagem> onMessage)
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

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, args) =>
            {
                var body = args.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);

                _logger.LogInformation("Mensagem recebida: {json}", json);

                var pessoa = JsonSerializer.Deserialize<PessoaMensagem>(json);
                onMessage(pessoa);
            };

            channel.BasicConsume(
                queue: _settings.Queue,
                autoAck: true,
                consumer: consumer);

            _logger.LogInformation("Consumidor iniciado. Aguardando mensagens...");

            while (true)
            {
                Thread.Sleep(100);
            }
        }
    }
}
