using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetCoreRabbitMqSample.Api.Model
{
    [Table("Message")]
    public class Message
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public DateTime? ProcessTime { get; set; }
    }
}
