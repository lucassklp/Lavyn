using Auto.Mapping.Mappers;
using Lavyn.Domain.Dtos;
using Lavyn.Domain.Entities;

namespace Lavyn.Business.Mapping
{
    public class UserHasRoomToViewedMessageDto : LightMapper<UserHasRoom, ViewedMessageDto>
    {
        public override ViewedMessageDto Map(UserHasRoom input)
        {
            return new ViewedMessageDto
            {
                LastSeen = input.LastSeen,
                RoomKey = input.Room.Key,
                UserId = input.UserId
            };
        }
    }
}