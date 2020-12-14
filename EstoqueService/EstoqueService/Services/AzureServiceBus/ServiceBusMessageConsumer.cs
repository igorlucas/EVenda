using EstoqueService.Services.AzureServiceBus.Queues;
using Microsoft.Extensions.Configuration;

namespace EstoqueService.Services.AzureServiceBus
{
    public class ServiceBusMessageConsumer
    {
        private readonly ProdutoVendidoMessageConsumer _produtoVendidoMessageConsumer;
        public ServiceBusMessageConsumer(IConfiguration configuration)
        {
            _produtoVendidoMessageConsumer = new ProdutoVendidoMessageConsumer(configuration);
        }

        public void RegisterAndWaitMessages()
        {
            _produtoVendidoMessageConsumer.RegisterMessageHandler();
        }
    }
}
