using System;

namespace Lavyn.Domain.Entities
{
    public class Message : Identifiable<long>
    {
        public long Id { get; set; }
        
        public long SenderId { get; set; }
        public User Sender { get; set; }

        public long RoomId { get; set; }
        public Room Room { get; set; }
        
        public DateTime Date { get; set; }
        
        public string Content { get; set; }

    }
}