using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace FluentNHibernate.AspNetCore.Identity.Entities
{
    public class IdentityRole : IdentityRole<string>
    {
        public virtual ICollection<IdentityUser> IdentityUsers { get; protected set; }
        public virtual ICollection<IdentityRoleClaim> IdentityRoleClaims { get; protected set; }

        public IdentityRole()
        {
        }
    }
}