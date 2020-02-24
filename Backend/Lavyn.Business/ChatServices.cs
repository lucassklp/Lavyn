using System;
using Lavyn.Domain.Dtos;
using Lavyn.Domain.Entities;
using Lavyn.Persistence.Repository;

namespace Lavyn.Business
{
    public class ChatServices
    {
        private Crud<long, Message> messageCrud;
        private User authenticatedUser;
        public ChatServices(Crud<long, Message> messageCrud, User authenticatedUser)
        {
            this.messageCrud = messageCrud;
            this.authenticatedUser = authenticatedUser;
        }

        public IObservable<Message> SaveMessageAsync(ChatMessageDto messageDto)
        {
            var message = new Message()
            {
                Date = DateTime.Now,
                Content = messageDto.Message,
                RoomId = messageDto.ChatId,
                SenderId = authenticatedUser.Id
            };
            
            return messageCrud.CreateAsync(message);
        }
    }
}