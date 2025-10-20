using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Message
    {
        public long Id { get; set; }
        public long ConversationId { get; set; }
        public required string Content { get; set; }

    }
}
