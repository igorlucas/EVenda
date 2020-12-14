using System.Threading.Tasks;

namespace EstoqueService.Services.AzureServiceBus.Queues.Senders
{
    public interface IQueueMessageSender
    {
        Task SendMessage<T>(T payload);
    }
}
