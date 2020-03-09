using System;
using System.Collections.Generic;
using Lavyn.Domain.Entities.Enums;

namespace Lavyn.Domain.Entities
{
    public class Room : Identifiable<long>
    {
        public long Id { get; set; }
        
        public string Key { get; set; }

        public string Name { get; set; }

        public RoomType Type { get; set; }
        
        public DateTime LastMessageDate { get; set; }
        
        public List<UserHasRoom> UserHasRoom { get; set; }
        public List<Message> Messages { get; set; }
    }
}