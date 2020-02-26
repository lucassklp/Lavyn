using Lavyn.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lavyn.Persistence.Repository
{
    public abstract class AbstractRepository<TEntity> : Crud<long, TEntity>
        where TEntity: class, Identifiable<long>
    {
        protected DbContext Context { get; }
        public AbstractRepository(DbContext context) : base(context)
        {
            this.Context = context;
        }

        public DbSet<TEntity> Query()
        {
            return this.Context.Set<TEntity>();
        }

        public DbSet<T> Query<T>()
            where T: class
        {
            return this.Context.Set<T>();
        }
    }
}
