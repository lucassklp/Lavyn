using Lavyn.Domain.Entities.Enums;

namespace Lavyn.Domain.Dtos
{
    public class CallDto
    {
        public CallType CallType { get; set; }
        public string Key { get; set; }
        public long CallerId { get; set; }
    }
}