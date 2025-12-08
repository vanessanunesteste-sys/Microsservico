using HostRecebimento.Application.Interfaces;

namespace HostRecebimento
{
    public class Startup
    {
        private readonly IMessageConsumerService _consumer;

        public Startup(IMessageConsumerService consumer)
        {
            _consumer = consumer;
        }


        public void Start()
        {
            _consumer.Start();
        }
    }
}
