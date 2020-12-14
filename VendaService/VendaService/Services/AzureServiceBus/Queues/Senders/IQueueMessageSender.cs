using System.Threading.Tasks;

namespace VendaService.Services.AzureServiceBus.Queues
{
    public interface IQueueMessageSender
    {
        Task SendMessage<T>(T payload);
    }
}
