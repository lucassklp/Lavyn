﻿namespace Lavyn.Domain.Entities
{
    public class UserHasRole
    {
        public long UserId { get; set; }
        public User User { get; set; }
        public long RoleId { get; set; }
        public Role Role { get; set; }
    }
}
