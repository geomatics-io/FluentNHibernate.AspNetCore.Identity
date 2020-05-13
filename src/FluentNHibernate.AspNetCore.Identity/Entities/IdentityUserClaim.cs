using Microsoft.AspNetCore.Identity;

namespace FluentNHibernate.AspNetCore.Identity.Entities {

    public class IdentityUserClaim : IdentityUserClaim<string>
    {
        public virtual IdentityUser IdentityUser { get; set; }
    }

}
