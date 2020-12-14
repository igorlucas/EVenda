using System.Threading.Tasks;

namespace EstoqueService.Services.AzureServiceBus.Queues.Consumers
{
    public interface IQueueMessageConsumer
    {
        public Task CloseQueueAsync();
        public void RegisterMessageHandler();
    }
}
