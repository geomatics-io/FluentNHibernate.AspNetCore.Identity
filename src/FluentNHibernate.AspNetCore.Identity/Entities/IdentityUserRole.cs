using Microsoft.AspNetCore.Identity;

namespace FluentNHibernate.AspNetCore.Identity.Entities {

    public class IdentityUserRole : IdentityUserRole<string>
    {
        public virtual IdentityUser IdentityUser { get; set; }
        public virtual IdentityRole IdentityRole { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as IdentityUserRole;

            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return this.IdentityUser == other.IdentityUser &&
                   this.IdentityRole == other.IdentityRole;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = GetType().GetHashCode();
                hash = (hash * 31) ^ IdentityUser.GetHashCode();
                hash = (hash * 31) ^ IdentityRole.GetHashCode();

                return hash;
            }
        }
    }

}
