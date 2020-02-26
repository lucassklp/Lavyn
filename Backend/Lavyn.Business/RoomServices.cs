using Lavyn.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Auto.Mapping.DependencyInjection;
using Lavyn.Business.Exceptions;
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
        public RoomServices(RoomRepository roomRepository, User authenticatedUser)
            : base(roomRepository)
        {
            this.roomRepository = roomRepository;
            this.authenticatedUser = authenticatedUser;
        }

        private string GenerateKey(params long[] ids)
        {
            Array.Sort(ids);
            return string.Concat(ids, '_').ToSha256();
        }

        public long GetChatId(long userId)
        {
            var room = this.roomRepository.GetRoomByUsers(RoomType.Individual, GenerateKey(this.authenticatedUser.Id, userId));

            if(room == null)
            {
                room = new Room()
                {
                    Type = RoomType.Individual,
                    Key = GenerateKey(this.authenticatedUser.Id, userId),
                    Users = new System.Collections.Generic.List<UserHasRoom>()
                    {
                        new UserHasRoom()
                        {
                            UserId = authenticatedUser.Id
                        },
                        new UserHasRoom()
                        {
                            UserId = userId
                        }
                    }
                };

                roomRepository.Create(room);

                return room.Id;
            }

            return room.Id;
        }
    }
}
