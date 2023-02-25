using Lavyn.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Auto.Mapping.DependencyInjection;
using Lavyn.Domain.Dtos;
using Lavyn.Domain.Entities;
using Lavyn.Persistence.Repository;
using Lavyn.Domain.Entities.Enums;

namespace Lavyn.Business
{
    public class RoomServices : AbstractServices<Room>
    {
        private RoomRepository roomRepository;
        private User authenticatedUser;
        private IMapResolver mapResolver;
        private Crud<long, UserHasRoom> crudUserhasRoom;
        public RoomServices(
            RoomRepository roomRepository, 
            User authenticatedUser,
            IMapResolver mapResolver,
            Crud<long, UserHasRoom> crudUserhasRoom)
            : base(roomRepository)
        {
            this.roomRepository = roomRepository;
            this.authenticatedUser = authenticatedUser;
            this.mapResolver = mapResolver;
            this.crudUserhasRoom = crudUserhasRoom;
        }

        private string GenerateKey(params long[] ids)
        {
            Array.Sort(ids);
            return string.Join('_', ids).ToSha256();
        }

        private RoomDto ConvertToDto(Room room)
        {
            var roomDto = mapResolver.Map<Room, RoomDto>(room);
            if (room.Type == RoomType.Individual)
            {
                roomDto.Name = room.UserHasRoom.Select(x => x.User)
                    .FirstOrDefault(x => x.Id != authenticatedUser.Id)?.Name;
            }

            return roomDto;
        }

        public List<RoomDto> GetRoomsAuthenticatedUser()
        {
            return roomRepository.GetRoomsByUserId(authenticatedUser.Id)
                .Select(room => ConvertToDto(room))
                .ToList();
        }
        
        public async Task<RoomDto> GetOrCreateRoom(string name = null, params long[] userIds)
        {
            var ids = userIds.ToList();
            ids.Add(authenticatedUser.Id);

            var key = GenerateKey(ids.ToArray());
            var room = roomRepository.GetRoomByKey(key);

            if (room != null) 
                return ConvertToDto(room);
            
            room = new Room()
            {
                Name = name,
                Type = ids.Count > 2 ? RoomType.Group : RoomType.Individual,
                Key = key,
                UserHasRoom = ids.Select(id => new UserHasRoom { UserId = id }).ToList()
            };

            await roomRepository.CreateAsync(room);
            
            return ConvertToDto(room);
        }

        public async Task<UserHasRoom> SetLastViewedRoom(string roomKey)
        {
            var userHasRoom = roomRepository.GetUserHasRoomByUserAndKey(authenticatedUser.Id, roomKey);
            userHasRoom.LastSeen = DateTime.Now;
            return await crudUserhasRoom.UpdateAsync(userHasRoom);
        }
    }
}
