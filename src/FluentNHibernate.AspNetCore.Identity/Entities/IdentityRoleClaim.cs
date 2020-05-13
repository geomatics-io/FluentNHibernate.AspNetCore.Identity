using Microsoft.AspNetCore.Identity;

namespace FluentNHibernate.AspNetCore.Identity.Entities {

    public class IdentityRoleClaim : IdentityRoleClaim<string>
    {
        public virtual IdentityRole IdentityRole { get; set; }
    }

}
