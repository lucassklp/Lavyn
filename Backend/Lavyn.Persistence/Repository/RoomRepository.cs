using Microsoft.EntityFrameworkCore;
using Lavyn.Domain.Entities;
using Lavyn.Domain.Entities.Enums;
using System.Linq;

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
    }
}
