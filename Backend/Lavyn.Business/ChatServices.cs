using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Auto.Mapping.DependencyInjection;
using Lavyn.Business.Consumers;
using Lavyn.Core.Extensions;
using Lavyn.Domain.Dtos;
using Lavyn.Domain.Entities;
using Lavyn.Domain.Entities.Enums;
using Lavyn.Persistence.Repository;
using Microsoft.Extensions.Logging;

namespace Lavyn.Business
{
    public class ChatServices
    {
        private User authenticatedUser;
        private UserRepository userRepository;
        private RoomRepository roomRepository;
        private MessageRepository messageRepository;
        private FirebaseMessagingConsumer firebaseConsumer;
        private ILogger<ChatServices> logger;
        private IMapResolver mapResolver;
        
        public ChatServices(
            User authenticatedUser,
            UserRepository userRepository,
            RoomRepository roomRepository,
            MessageRepository messageRepository,
            FirebaseMessagingConsumer firebaseConsumer,
            ILogger<ChatServices> logger,
            IMapResolver mapResolver)
        {
            this.authenticatedUser = authenticatedUser;
            this.userRepository = userRepository;
            this.roomRepository = roomRepository;
            this.messageRepository = messageRepository;
            this.firebaseConsumer = firebaseConsumer;
            this.logger = logger;
            this.mapResolver = mapResolver;
        }

        public IObservable<UserDto> SetAuthenticatedUserOnline(bool isOnline)
        {
            logger.LogInformation($"{authenticatedUser.Name} is setting IsOnline to {isOnline}");
            authenticatedUser.IsOnline = isOnline;
            return userRepository.UpdateAsync(authenticatedUser).Select(user => mapResolver.Map<User, UserDto>(user));
        }

        public IObservable<Message> SendMessageAsync(ChatMessageDto messageDto)
        {
            var room = roomRepository.GetRoomByKey(messageDto.RoomKey);
            
            var message = new Message()
            {
                RoomId = room.Id,
                SenderId = authenticatedUser.Id,
                Date = DateTime.Now,
                Content = messageDto.Message
            };

            messageRepository.CreateAsync(message);
            
            var saveOnDatabase = messageRepository.CreateAsync(message);


            var tokens = room.UserHasRoom.Select(x => x.User)
                .Where(x => x.Id != authenticatedUser.Id)
                .SelectMany(x => x.Tokens)
                .ToList();

            var requests = new List<IObservable<FirebaseMessagingResponseDto>>();
            
            tokens.Where(x => x.Type == TokenType.FirebaseMessaging)
                .ToList()
                .ForEach(token => requests.Add(firebaseConsumer.Send(new FirebaseMessagingRequestDto(token.Value, new FirebaseNotificationDto
                {
                    Body = message.Content,
                    Title = "You received a new Message"
                })))
            );

            var responses = requests.Zip();
            var result = saveOnDatabase.Zip(responses, (msg, response) => msg);
            result.Select(msg =>
            {
                room.LastMessageDate = msg.Date;
                roomRepository.Update(room);
                return msg;
            });

            return result;
        }
    }
}