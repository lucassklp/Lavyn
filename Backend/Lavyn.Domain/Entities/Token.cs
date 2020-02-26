using Lavyn.Domain.Entities.Enums;

namespace Lavyn.Domain.Entities
{
    public class Token : Identifiable<long>
    {
        public long Id { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }

        public TokenType Type { get; set; }

        public string Value { get; set; }
    }
}
