using System.Threading.Tasks;
using VendaService.Services.AzureServiceBus.Queues;

namespace VendaService.Services.AzureServiceBus
{
    public class ServiceBusMessageSender 
    {
        private ProdutoVendidoMessageSender _produtoVendidoMessageSender;

        public ServiceBusMessageSender(ProdutoVendidoMessageSender produtoVendidoMessageSender)
        {
            _produtoVendidoMessageSender = produtoVendidoMessageSender;
        }
        public async Task SendProdutoVendidoMessage<T>(T payload)
        {
            await _produtoVendidoMessageSender.SendMessage(payload);
        }
    }
}
