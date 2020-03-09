using System.Collections.Generic;
using System.Linq;
using Auto.Mapping.DependencyInjection;
using Auto.Mapping.Mappers;
using Lavyn.Domain.Dtos;
using Lavyn.Domain.Entities;

namespace Lavyn.Business.Mapping
{
    public class RoomToRoomDtoMapping : LightMapper<Room, RoomDto>
    {
        private IMapResolver mapResolver;

        public RoomToRoomDtoMapping(IMapResolver mapResolver)
        {
            this.mapResolver = mapResolver;
        }
        
        public override RoomDto Map(Room input)
        {
            return new RoomDto
            {
                Key = input.Key,
                Name = input.Name,
                Messages = input.Messages?.Select(message => mapResolver.Map<Message, MessageDto>(message))?.ToList() ?? new List<MessageDto>(),
                Participants = input.UserHasRoom
                    .Select(x => x.User)
                    .Select(user => mapResolver.Map<User, UserDto>(user)).ToList(),
                LastViews = input.UserHasRoom.Select(userHasRoom => mapResolver.Map<UserHasRoom, ViewedMessageDto>(userHasRoom)).ToList()
            };
        }
    }
}