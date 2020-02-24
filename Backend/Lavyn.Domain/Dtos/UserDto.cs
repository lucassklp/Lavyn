using System;

namespace Lavyn.Domain.Dtos
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsOnline { get; set; }
        public DateTime LastActivity { get; set; }
    }
}
