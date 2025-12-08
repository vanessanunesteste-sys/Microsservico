using HostEnvio.Application.Interfaces;
using HostEnvio.Application.Services;
using HostEnvio.Distribution;
using HostEnvio.Infra.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

internal class Program
{
        private static async Task Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            // 1. Carregar appsettings.json
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            // 2. Registrar configurações do Rabbit
            builder.Services.Configure<RabbitSettings>(
                builder.Configuration.GetSection("RabbitSettings"));

            // 3. Registrar serviços
            builder.Services.AddSingleton<IRabbitMQProducer, RabbitMQProducer>();
            builder.Services.AddSingleton<IMessagePublisherService, MessagePublisherService>();

            // 4. Worker responsável por enviar a mensagem no startup
            builder.Services.AddHostedService<Startup>();

            // 5. Logs
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            // 6. Construir e rodar
            var host = builder.Build();
            await host.RunAsync();
        }
    }