using System;

namespace Lavyn.Domain.Entities
{
    public class UserHasRoom : Identifiable<long>
    {
        public long Id { get; set; }        
        public long RoomId { get; set; }
        public Room Room { get; set; }
        
        public long UserId { get; set; }
        public User User { get; set; }
        
        public DateTime LastSeen { get; set; }
    }
}