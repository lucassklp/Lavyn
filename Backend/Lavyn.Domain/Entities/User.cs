using System;
using System.Collections.Generic;

namespace Lavyn.Domain.Entities
{
    public class User : Identifiable<long>, ICredential
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        public bool IsOnline { get; set; }
        
        public DateTime LastLogin { get; set; }
        
        
        public virtual ICollection<UserHasRole> UserRoles { get; set; }

        public virtual IList<UserHasRoom> UserRooms { get; set; }
        
        public virtual IList<Message> SentMessages { get; set; }
        
        #region ICredential Members
        public string Password { get; set; }
        public string Login => Email;
        #endregion
    }
}
