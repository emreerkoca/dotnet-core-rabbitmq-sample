namespace DotnetCoreRabbitMqSample.Api.Contracts.Requests
{
    public class PostMembershipRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
