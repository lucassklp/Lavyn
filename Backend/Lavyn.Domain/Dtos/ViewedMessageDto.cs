using System;

namespace Lavyn.Domain.Dtos
{
    public class ViewedMessageDto
    {
        public long UserId { get; set; }
        public string RoomKey { get; set; }
        public DateTime LastSeen { get; set; }
    }
}