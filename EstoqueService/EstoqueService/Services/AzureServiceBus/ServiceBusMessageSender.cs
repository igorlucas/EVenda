using EstoqueService.Services.AzureServiceBus.Queues.Senders;
using System.Threading.Tasks;

namespace EstoqueService.Services.AzureServiceBus
{
    public class ServiceBusMessageSender
    {

        private ProdutoCriadoMessageSender _produtoAdicionadoMessageSender;
        private ProdutoEditadoMessageSender _produtoAtualizadoMessageSender;

        public ServiceBusMessageSender(ProdutoCriadoMessageSender produtoAdicionadoMessageSender, 
            ProdutoEditadoMessageSender produtoAtualizadoMessageSender)
        {
            _produtoAdicionadoMessageSender = produtoAdicionadoMessageSender;
            _produtoAtualizadoMessageSender = produtoAtualizadoMessageSender;
        }
        public async Task SendProdutoAdicionadoMessage<T>(T payload) => await _produtoAdicionadoMessageSender.SendMessage(payload);

        public async Task SendProdutoAtualizadoMessage<T>(T payload) => await _produtoAtualizadoMessageSender.SendMessage(payload);
    }
}
