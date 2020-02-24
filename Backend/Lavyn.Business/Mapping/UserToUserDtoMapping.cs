using Auto.Mapping.Mappers;
using Lavyn.Domain.Dtos;
using Lavyn.Domain.Entities;

namespace Lavyn.Business.Mapping
{
    public class UserToUserDtoMapping : LightMapper<User, UserDto>
    {
        public override UserDto Map(User input) => new UserDto()
        {
            Id = input.Id,
            IsOnline = input.IsOnline,
            LastActivity = input.LastLogin,
            Name = input.Name
        };
    }
}