using DotnetCoreRabbitMqSample.Api.Contracts.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using DotnetCoreRabbitMqSample.Api.Contracts.Model;
using DotnetCoreRabbitMqSample.Api.Contracts.Responses;

namespace DotnetCoreRabbitMqSample.Api.Consumers
{
    public class MembershipStartedEventConsumer : IConsumer<MembershipStartedEvent>
    {
        ILogger<MembershipStartedEventConsumer> _logger;

        public MembershipStartedEventConsumer(ILogger<MembershipStartedEventConsumer> logger)
        {
            _logger = logger;
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
            throw new Exception("Exception test");


            await SendEmail(emailModel);
        }

        public async Task SendEmail(EmailModel emailModel)
        {
            //Send email
            _logger.LogInformation("Email: " + JsonSerializer.Serialize(emailModel));
        }
    }   
}
