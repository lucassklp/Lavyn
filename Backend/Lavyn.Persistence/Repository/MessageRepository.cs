using Microsoft.EntityFrameworkCore;
using Lavyn.Domain.Entities;

namespace Lavyn.Persistence.Repository
{
    public class MessageRepository : AbstractRepository<Message>
    {
        public MessageRepository(DbContext daoContext) : base(daoContext)
        {
        }
    }
}
