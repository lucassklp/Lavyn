using System;
using System.Collections.Generic;
using System.Linq;
using Lavyn.Domain.Dtos;
using Lavyn.Domain.Entities;

namespace Lavyn.Business
{
    public class CallServices
    {
        private static Dictionary<string, Dictionary<long, string>> _calls = new Dictionary<string, Dictionary<long, string>>();
        private static Dictionary<long, string> _userRoom = new Dictionary<long, string>();
        
        private User _authenticatedUser;
        public CallServices(User authenticatedUser)
        {
            _authenticatedUser = authenticatedUser;
        }
        
        public List<string> Connect(EnterCallDto enterDto)
        {
            if (!_calls.ContainsKey(enterDto.Key))
            {
                _calls.Add(enterDto.Key, new Dictionary<long, string>());
            }
            _userRoom.Add(_authenticatedUser.Id, enterDto.Key);
            var call = _calls[enterDto.Key];
            var otherIds = call.Values.ToList();
            call.Add(_authenticatedUser.Id, enterDto.PeerId);
            return otherIds;
        }

        public (string RoomKey, string PeerId) Disconnect()
        {
            if (!_userRoom.ContainsKey(_authenticatedUser.Id))
                return (null, null);
            
            var roomKey = _userRoom[_authenticatedUser.Id];
            var peerId = _calls[roomKey][_authenticatedUser.Id];
            _userRoom.Remove(_authenticatedUser.Id);
            _calls[roomKey].Remove(_authenticatedUser.Id);
            
            if (!_calls[roomKey].Any())
            {
                _calls.Remove(roomKey);
            }

            return (roomKey, peerId);
        }
    }
}