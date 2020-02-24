using Auto.Mapping;
using Auto.Mapping.DependencyInjection;
using Lavyn.Domain.Dtos;
using Lavyn.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Lavyn.Business.Mapping
{
    public static class Injector
    {
        public static IServiceCollection AddMappers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapping();
            serviceCollection.AddAutoMapper<IMapper<User, UserDto>, UserToUserDtoMapping>();
    
            return serviceCollection;
        }
    }
}