using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using NHibernate.Proxy;

namespace FluentNHibernate.AspNetCore.Identity.Entities {

    public class IdentityUser : IdentityUser<string> {
        public virtual ICollection<IdentityRole> IdentityRoles { get; protected set; }
        public virtual ICollection<IdentityUserLogin> IdentityUserLogins { get; protected set; }
        public virtual ICollection<IdentityUserToken> IdentityUserTokens { get; protected set; }
        public virtual ICollection<IdentityUserClaim> IdentityUserClaims { get; protected set; }

        public IdentityUser()
        {
            
        }

        public virtual void AddToken(IdentityUserToken<string> token)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));
            EnsureTokensCollection();
            var modelToken = token as IdentityUserToken ?? new IdentityUserToken(token);
            AddToCollection(this.IdentityUserTokens, modelToken);
        }

        public virtual bool RemoveToken(IdentityUserToken<string> identityUserToken)
        {
            if (identityUserToken == null)
                throw new ArgumentNullException(nameof(identityUserToken));
            var modelToken = identityUserToken as IdentityUserToken ?? new IdentityUserToken(identityUserToken);
            return RemoveFromCollection(this.IdentityUserTokens, modelToken);
        }

        public virtual void AddRole(IdentityRole identityRole)
        {
            EnsureRolesCollection();
            AddToCollection(this.IdentityRoles, identityRole);
        }

        public virtual bool RemoveRole(IdentityRole identityRole)
        {
            return RemoveFromCollection(this.IdentityRoles, identityRole);
        }

        public virtual void AddLogin(IdentityUserLogin identityUserLogin)
        {
            EnsureLoginsCollection();
            AddToCollection(this.IdentityUserLogins, identityUserLogin);
        }

        public virtual bool RemoveLogin(IdentityUserLogin identityUserLogin)
        {
            return RemoveFromCollection(this.IdentityUserLogins, identityUserLogin);
        }

        public virtual void AddClaim(IdentityUserClaim identityUserClaim)
        {
            EnsureClaimsCollection();
            AddToCollection(this.IdentityUserClaims, identityUserClaim);
        }

        public virtual bool RemoveClaim(IdentityUserClaim identityUserClaim)
        {
            return RemoveFromCollection(this.IdentityUserClaims, identityUserClaim);
        }

        private void EnsureTokensCollection()
        {
            if (this.IdentityUserTokens == null)
                this.IdentityUserTokens = new List<IdentityUserToken>();
        }

        private void EnsureLoginsCollection()
        {
            if (this.IdentityUserLogins == null)
                this.IdentityUserLogins = new List<IdentityUserLogin>();
        }

        private void EnsureRolesCollection()
        {
            if (this.IdentityRoles == null)
                this.IdentityRoles = new List<IdentityRole>();
        }

        private void EnsureClaimsCollection()
        {
            if (this.IdentityUserClaims == null)
                this.IdentityUserClaims = new List<IdentityUserClaim>();
        }

        private void AddToCollection<T>(ICollection<T> collection, T item)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (!collection.Contains(item))
                collection.Add(item);
        }

        private bool RemoveFromCollection<T>(ICollection<T> collection, T item)
            where T : class
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var toRemove = collection?.SingleOrDefault(i => i.Equals(item));
            return toRemove != null && (collection?.Remove(toRemove)).GetValueOrDefault();
        }

        public virtual bool Equals(IdentityUser other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            if (!this.GetType().IsUnproxiedTypeEqual(other.GetType()))
                return false;
            if (this.Id == null && other.Id == null)
                return StringComparer.OrdinalIgnoreCase.Equals(this.UserName, other.UserName);

            return StringComparer.OrdinalIgnoreCase.Equals(this.Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as IdentityUser);
        }

        public override int GetHashCode()
        {
            return this.Id?.GetHashCode() ?? UserName?.GetHashCode() ?? 0;
        }
    }

}
