using Microsoft.AspNetCore.Identity;

namespace FluentNHibernate.AspNetCore.Identity.Entities {

    public class IdentityUserLogin : IdentityUserLogin<string> {

        public virtual IdentityUser IdentityUser { get; set; }

        protected bool Equals(IdentityUserLogin other) {
            return LoginProvider == other.LoginProvider
                && ProviderKey == other.ProviderKey;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            if (obj.GetType() != GetType()) {
                return false;
            }
            return Equals((IdentityUserLogin)obj);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = 0;
                hashCode = LoginProvider.GetHashCode();
                hashCode = (hashCode * 397) ^ ProviderKey.GetHashCode();
                return hashCode;
            }
        }

    }

}
