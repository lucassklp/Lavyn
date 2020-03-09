using Auto.Mapping.Mappers;
using Lavyn.Domain.Dtos;
using Lavyn.Domain.Entities;

namespace Lavyn.Business.Mapping
{
    public class MessageToMessageDtoMapping : LightMapper<Message, MessageDto>
    {
        public override MessageDto Map(Message input)
        {
            return new MessageDto
            {
                Message = input.Content,
                SenderId = input.SenderId,
                Date = input.Date
            };
        }
    }
}