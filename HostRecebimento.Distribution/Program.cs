using HostRecebimento.Application.Interfaces;
using HostRecebimento.Application.Services;
using HostRecebimento.Distribution;
using HostRecebimento.Infra.Database;
using HostRecebimento.Infra.Messaging;
using HostRecebimento.Infra.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(config =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                // RabbitMQ
                services.Configure<RabbitSettings>(
                    context.Configuration.GetSection("RabbitSettings"));

                // Database repository
                services.AddSingleton<IPessoaRepository, PessoaRepository>();

                // RabbitMQ Consumer
                services.AddSingleton<IRabbitMQConsumer, RabbitMQConsumer>();

                // Serviço que usa o consumer
                services.AddSingleton<IMessageConsumerService, MessageConsumerService>();

                // Hosted service de inicialização
                services.AddHostedService<Startup>();
            })
            .Build();

        await host.RunAsync();
    }
}