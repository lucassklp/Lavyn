using System.Collections.Generic;

namespace Lavyn.Domain.Dtos
{
    public class RoomDto
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public List<UserDto> Participants { get; set; }
        public List<MessageDto> Messages { get; set; }
        public List<ViewedMessageDto> LastViews { get; set; }
    }
}