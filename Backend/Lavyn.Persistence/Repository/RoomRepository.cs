using Microsoft.EntityFrameworkCore;
using Lavyn.Domain.Entities;
using Lavyn.Domain.Entities.Enums;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Lavyn.Persistence.Repository
{
    public class RoomRepository : AbstractRepository<Room>
    {
        public RoomRepository(DbContext daoContext) : base(daoContext)
        {
        }

        public Room GetRoomByUsers(RoomType roomType, string key)
        {
            return Query()
                .Where(x => x.Type == roomType && x.Key == key)
                .FirstOrDefault();
        }

        public List<string> GetGroupsIdByUser(long userId)
        {
            return Query<UserHasRoom>().Where(x => x.UserId == userId)
                .Select(x => x.Room.Id.ToString())
                .ToList();
        }
    }
}
