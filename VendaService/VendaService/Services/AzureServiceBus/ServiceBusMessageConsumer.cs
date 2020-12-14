using Microsoft.Extensions.Configuration;
using VendaService.Services.AzureServiceBus.Queues;

namespace VendaService.Services.AzureServiceBus
{
    public class ServiceBusMessageConsumer
    {
        private readonly ProdutoCriadoMessageConsumer _produtoAdicionadoQueueHandler;
        private readonly ProdutoEditadoMessageConsumer _produtoAtualizadoQueueHandler;
        public ServiceBusMessageConsumer(IConfiguration configuration)
        {
            _produtoAdicionadoQueueHandler = new ProdutoCriadoMessageConsumer(configuration);
            _produtoAtualizadoQueueHandler = new ProdutoEditadoMessageConsumer(configuration);
        }

        public void RegisterAndWaitMessages()
        {
            _produtoAdicionadoQueueHandler.RegisterMessageHandler();
            _produtoAtualizadoQueueHandler.RegisterMessageHandler();
        }
    }
}