using System;

namespace Lavyn.Domain.Dtos
{
    public class MessageDto
    {
        public long SenderId { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}