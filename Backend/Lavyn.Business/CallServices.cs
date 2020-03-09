using System.Collections.Generic;
using Lavyn.Domain.Dtos;
using Lavyn.Domain.Entities;

namespace Lavyn.Business
{
    public class CallServices
    {
        private static Dictionary<string, Dictionary<long, string>> _calls = new Dictionary<string, Dictionary<long, string>>();

        private User _authenticatedUser;
        public CallServices(User authenticatedUser)
        {
            _authenticatedUser = authenticatedUser;
        }
        
        public List<string> Connect(ConnectCallDto connectDto)
        {
            var dict = new Dictionary<long, string> {{_authenticatedUser.Id, connectDto.PeerId}};
            if (_calls.ContainsKey(connectDto.Key))
            {
                var call = _calls[connectDto.Key];
                
            }
            
            return _calls[connectDto.Key].
        }
    }
}