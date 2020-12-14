using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VendaService.Data;
using VendaService.Models;

namespace VendaService.Services.AzureServiceBus.Queues
{
    public class ProdutoEditadoMessageConsumer : IQueueMessageConsumer
    {
        private ILoggerFactory LoggerFactory { get; set; }
        private const string QueueName = "produtoeditado";
        private readonly QueueClient _queueClient;
        private readonly ILogger _logger;
        public ProdutoEditadoMessageConsumer(IConfiguration configuration)
        {
            LoggerFactory = new LoggerFactory();
            _logger = LoggerFactory.CreateLogger(nameof(ProdutoEditadoMessageConsumer));
            _queueClient = new QueueClient(configuration.GetConnectionString("ServiceBus"), QueueName);
        }
        public async Task CloseQueueAsync() => await _queueClient.CloseAsync();

        public void RegisterMessageHandler()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionMessageHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };
            _queueClient.RegisterMessageHandler(ProcessMessageAsync, messageHandlerOptions);
        }

        private async Task ProcessMessageAsync(Message message, CancellationToken token)
        {
            _logger.LogInformation($"Mensagem recebida - QueueName: {QueueName}");
            await ProcessProdutoAtualizadoQueue(message);
            await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private async Task ProcessProdutoAtualizadoQueue(Message message)
        {
            var options = new DbContextOptions<VendaServiceContext>();
            using (var _db = new VendaServiceContext(options))
            {
                try
                {
                    var produto = JsonConvert.DeserializeObject<Produto>(Encoding.UTF8.GetString(message.Body));
                    _db.Entry(produto).State = EntityState.Modified;
                    await _db.SaveChangesAsync();
                    _logger.LogInformation($"Produto atualizado - QueueName: {QueueName}");
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
        }
        private Task ExceptionMessageHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            _logger.LogError(exceptionReceivedEventArgs.Exception, "ServiceBusMessageHandler encontrou uma exceção");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            _logger.LogDebug($"- Endpoint: {context.Endpoint}");
            _logger.LogDebug($"- Entity Path: {context.EntityPath}");
            _logger.LogDebug($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }
    }
}