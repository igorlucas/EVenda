using System.Threading.Tasks;

namespace VendaService.Services.AzureServiceBus
{
    public interface IQueueMessageConsumer
    {
        public Task CloseQueueAsync();      
        public void RegisterMessageHandler();   
    }
}
