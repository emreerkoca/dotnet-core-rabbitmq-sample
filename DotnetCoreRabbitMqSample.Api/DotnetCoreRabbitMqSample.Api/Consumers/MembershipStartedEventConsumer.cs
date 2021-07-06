using DotnetCoreRabbitMqSample.Api.Contracts.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using DotnetCoreRabbitMqSample.Api.Contracts.Model;
using DotnetCoreRabbitMqSample.Api.Contracts.Responses;
using DotnetCoreRabbitMqSample.Api.Services;

namespace DotnetCoreRabbitMqSample.Api.Consumers
{
    public class MembershipStartedEventConsumer : IConsumer<MembershipStartedEvent>
    {
        ILogger<MembershipStartedEventConsumer> _logger;
        IMessageBrokerService _messageBrokerService;
        

        public MembershipStartedEventConsumer(ILogger<MembershipStartedEventConsumer> logger, IMessageBrokerService messageBrokerService)
        {
            _logger = logger;
            _messageBrokerService = messageBrokerService;
        }

        public async Task Consume(ConsumeContext<MembershipStartedEvent> context)
        {
            MembershipResponse membershipResponse = context.Message.MembershipResponse;
            EmailModel emailModel = new EmailModel
            {
                EmailAddress = membershipResponse.Email,
                Subject = "Welcome to X Community",
                Body = "You're officially an X community member. Congrats :)"
            };

            _logger.LogInformation("DateTime: " + DateTime.UtcNow);
            //throw new Exception("Exception test");


            await SendEmail(emailModel);

            await _messageBrokerService.PutMessageAsProcessed(context.Message.Id);
        }

        public async Task SendEmail(EmailModel emailModel)
        {
            //Send email
            _logger.LogInformation("Email: " + JsonSerializer.Serialize(emailModel));
        }
    }   
}
