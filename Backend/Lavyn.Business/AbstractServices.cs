using System;
using Lavyn.Business.Consumers;
using Lavyn.Domain.Dtos;
using Lavyn.Domain.Entities;
using Lavyn.Persistence.Repository;

namespace Lavyn.Business
{
    public abstract class AbstractServices<TEntity>
        where TEntity: class, Identifiable<long>
    {
        public AbstractRepository<TEntity> Repository { get; private set; }

        public AbstractServices(AbstractRepository<TEntity> repository)
        {
            this.Repository = repository;
        }
    }
}