using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.BusinessObjects.Models
{
    public class Message
    {
        public int MessageId { get; set; } 

        public int ConversationId { get; set; }

        public int FromId { get; set; }
        public string FromType { get; set; } = null!; 

        public int ToId { get; set; }
        public string ToType { get; set; } = null!; 
        public string Content { get; set; } = null!; 

        public DateTime SentAt { get; set; } 

        public bool IsRead { get; set; } = false; 

        public virtual Conversation Conversation { get; set; } = null!;
    }
}
