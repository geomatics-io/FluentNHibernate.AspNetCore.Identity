using FluentNHibernate.AspNetCore.Identity.Entities;
using FluentNHibernate.Mapping;

namespace FluentNHibernate.AspNetCore.Identity.Mappings
{
    public class IdentityUserClaimClassMap : ClassMap<IdentityUserClaim>
    {
        public IdentityUserClaimClassMap()
        {
            Table("AspNetUserClaims");
            LazyLoad();

            Id(i => i.Id)
                .Column("Id")
                .Length(450)
                .Not.Nullable();
            
            Map(i => i.ClaimType)
                .Nullable();

            Map(i => i.ClaimValue)
                .Nullable();

            References(i => i.IdentityUser)
                .Column("UserId")
                .Not.Nullable();
        }
    }
}