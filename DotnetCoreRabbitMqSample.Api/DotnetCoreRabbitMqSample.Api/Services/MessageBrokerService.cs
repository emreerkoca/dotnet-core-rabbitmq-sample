using DotnetCoreRabbitMqSample.Api.Data;
using DotnetCoreRabbitMqSample.Api.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreRabbitMqSample.Api.Services
{
    public class MessageBrokerService : IMessageBrokerService
    {
        private readonly AppDbContext _appDbContext;

        public MessageBrokerService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task PutMessageAsProcessed(Guid id)
        {
            Message message = _appDbContext.Messages.FirstOrDefault(m => m.Id == id);

            if (message == null)
            {
                throw new Exception("Message not found!");
            }

            message.ProcessTime = DateTime.UtcNow;

            await _appDbContext.SaveChangesAsync();
        }
    }
}
