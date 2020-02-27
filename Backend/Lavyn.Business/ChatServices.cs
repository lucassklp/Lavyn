using System;
using Lavyn.Business.Consumers;
using Lavyn.Domain.Dtos;
using Lavyn.Domain.Entities;
using Lavyn.Domain.Entities.Enums;
using Lavyn.Persistence.Repository;

namespace Lavyn.Business
{
    public class ChatServices
    {
        private User authenticatedUser;
        private UserRepository userRepository;
        private RoomRepository roomRepository;
        private MessageRepository messageRepository;
        private FirebaseMessagingConsumer firebaseConsumer;
        
        public ChatServices(
            User authenticatedUser,
            UserRepository userRepository,
            RoomRepository roomRepository,
            MessageRepository messageRepository,
            FirebaseMessagingConsumer firebaseConsumer)
        {
            this.authenticatedUser = authenticatedUser;
            this.userRepository = userRepository;
            this.roomRepository = roomRepository;
            this.messageRepository = messageRepository;
            this.firebaseConsumer = firebaseConsumer;
        }

        public IObservable<Message> SendMessageAsync(ChatMessageDto messageDto)
        {
            var message = new Message()
            {
                Date = DateTime.Now,
                Content = messageDto.Message,
                RoomId = messageDto.RoomId,
                SenderId = authenticatedUser.Id
            };
            
            //var token = null;
            var saveOnDatabase = messageRepository.CreateAsync(message);

            // if(token != null)
            // {
            //     var sendFirebaseNotification = firebaseConsumer.Send(new FirebaseMessagingRequestDto(token.Value, new FirebaseNotificationDto()
            //     {
            //         Body = message?.Content?.Substring(0, 20),
            //         Title = "You received a new Message"
            //     }));               

            //     return Observable.Zip(saveOnDatabase, sendFirebaseNotification, (messageSaved, messageSent) => {
            //         return messageSaved;
            //     });
            // }
            // else
            return saveOnDatabase;

        }
    }
}