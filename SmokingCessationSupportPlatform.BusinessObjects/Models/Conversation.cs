using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokingCessationSupportPlatform.BusinessObjects.Models
{
    public class Conversation
    {
        public int ConversationId { get; set; } 

        public int UserId { get; set; } 
        public int CoachId { get; set; }

        public DateTime StartedAt { get; set; } 
        public DateTime? LastMessageAt { get; set; } 
        public virtual User User { get; set; } = null!;
        public virtual Coach Coach { get; set; } = null!;
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>(); // Danh sách các tin nhắn trong cuộc hội thoại này
    }
}
