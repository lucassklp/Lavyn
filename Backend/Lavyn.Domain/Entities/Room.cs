using System.Collections.Generic;
using Lavyn.Domain.Entities.Enums;

namespace Lavyn.Domain.Entities
{
    public class Room : Identifiable<long>
    {
        public long Id { get; set; }
        
        public string Key { get; set; }

        public RoomType Type { get; set; }
        
        public List<UserHasRoom> Users { get; set; }
        public List<Message> Messages { get; set; }
    }
}