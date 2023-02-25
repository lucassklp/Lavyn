using Microsoft.EntityFrameworkCore;
using Lavyn.Domain.Entities;
using System.Linq;
using System.Collections.Generic;

namespace Lavyn.Persistence.Repository
{
    public class RoomRepository : AbstractRepository<Room>
    {
        public RoomRepository(DbContext daoContext) : base(daoContext)
        {
        }

        public Room GetRoomByKey(string key)
        {
            return Query()
                .Include(x => x.UserHasRoom)
                    .ThenInclude(x => x.User)
                        .ThenInclude(x => x.Tokens)
                .Include(x => x.UserHasRoom)
                    .ThenInclude(x => x.Room)
                .FirstOrDefault(x => x.Key == key);
        }
        
        public List<Room> GetRoomsByUserId(long userId)
        {
            return Query<UserHasRoom>().Where(x => x.UserId == userId)
                .Include(x => x.Room)
                    .ThenInclude(x => x.UserHasRoom)
                        .ThenInclude(x => x.User)
                .Include(x => x.Room.Messages)
                .Select(x => x.Room)
                .OrderByDescending(x => x.LastMessageDate)
                .ToList();
        }

        public UserHasRoom GetUserHasRoomByUserAndKey(long userId, string roomKey)
        {
            return Query<UserHasRoom>().SingleOrDefault(x => x.Room.Key == roomKey && x.UserId == userId);
        }
    }
}
