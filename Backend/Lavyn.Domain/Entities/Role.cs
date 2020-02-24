using System.Collections.Generic;

namespace Lavyn.Domain.Entities
{
    public class Role : Identifiable<long>
    {
        public long Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UserHasRole> UserRoles { get; set; }
    }
}