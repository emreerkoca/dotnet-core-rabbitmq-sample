using AutoMapper;
using DotnetCoreRabbitMqSample.Api.Contracts.Events;
using DotnetCoreRabbitMqSample.Api.Contracts.Requests;
using DotnetCoreRabbitMqSample.Api.Contracts.Responses;
using MassTransit;

namespace DotnetCoreRabbitMqSample.Api.Services
{
    public class TextService : ITextService
    {
        IBusControl _busControl;
        IMapper _mapper;

        public TextService(IBusControl busControl, IMapper mapper)
        {
            _busControl = busControl;
            _mapper = mapper;
        }
        public TextResponse PutText(PutTextRequest putTextRequest)
        {
            //We suppose text saved
            TextResponse textResponse = _mapper.Map<TextResponse>(putTextRequest);

            _busControl.Publish(new TextCreatedEvent
            {
                TextResponse = textResponse
            });

            return textResponse;
        }
    }
}
