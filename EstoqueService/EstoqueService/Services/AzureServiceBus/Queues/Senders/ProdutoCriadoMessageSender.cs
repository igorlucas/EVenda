using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueService.Services.AzureServiceBus.Queues.Senders
{
    public class ProdutoCriadoMessageSender : IQueueMessageSender
    {
        private readonly QueueClient _queueClient;
        private const string QueueName = "produtocriado";

        public ProdutoCriadoMessageSender(IConfiguration configuration) 
            => _queueClient = new QueueClient(configuration.GetConnectionString("ServiceBus"), QueueName);

        public async Task SendMessage<T>(T payload)
        {
            string data = JsonConvert.SerializeObject(payload);
            Message message = new Message(Encoding.UTF8.GetBytes(data));
            await _queueClient.SendAsync(message);
        }
    }
}
