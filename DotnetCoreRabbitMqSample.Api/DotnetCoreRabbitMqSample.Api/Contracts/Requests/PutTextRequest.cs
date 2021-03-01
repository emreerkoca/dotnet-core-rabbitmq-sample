namespace DotnetCoreRabbitMqSample.Api.Contracts.Requests
{
    public class PutTextRequest
    {
        public long UserId { get; set; }
        public string Text { get; set; }
    }
}
