using System;
using System.Threading.Tasks;

namespace DotnetCoreRabbitMqSample.Api.Services
{
    public interface IMessageBrokerService
    {
        Task PutMessageAsProcessed(Guid id);
        Task PublishMissingMessages();
    }
}
