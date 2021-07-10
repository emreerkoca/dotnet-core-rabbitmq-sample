using DotnetCoreRabbitMqSample.Api.Contracts.Events;
using DotnetCoreRabbitMqSample.Api.Data;
using DotnetCoreRabbitMqSample.Api.Model;
using MassTransit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreRabbitMqSample.Api.Services
{
    public class MessageBrokerService : IMessageBrokerService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IBusControl _busControl;

        public MessageBrokerService(AppDbContext appDbContext, IBusControl busControl)
        {
            _appDbContext = appDbContext;
            _busControl = busControl;
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

        public async Task PublishMissingMessages()
        {
            List<Message> missingMessageList = _appDbContext.Messages.Where(m => m.CreatedOn < DateTime.UtcNow.AddHours(-1) && !m.ProcessTime.HasValue).ToList();

            foreach (var message in missingMessageList)
            {
                await _busControl.Publish(JsonConvert.DeserializeObject<MembershipStartedEvent>(message.Content));
            }
        }
    }
}
