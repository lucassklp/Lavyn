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
            serviceCollection.AddTransient<IMapper<User, UserDto>, UserToUserDtoMapping>();
            serviceCollection.AddTransient<IMapper<Room, RoomDto>, RoomToRoomDtoMapping>();
            serviceCollection.AddTransient<IMapper<Message, MessageDto>, MessageToMessageDtoMapping>();
            serviceCollection.AddTransient<IMapper<UserHasRoom, ViewedMessageDto>, UserHasRoomToViewedMessageDto>();
            
            return serviceCollection;
        }
    }
}