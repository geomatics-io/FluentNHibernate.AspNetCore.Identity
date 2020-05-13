using FluentNHibernate.AspNetCore.Identity.Entities;
using FluentNHibernate.Mapping;

namespace FluentNHibernate.AspNetCore.Identity.Mappings
{
    public class IdentityRoleClaimClassMap : ClassMap<IdentityRoleClaim>
    {
        public IdentityRoleClaimClassMap()
        {
            Table("AspNetRoleClaims");
            LazyLoad();

            Id(i => i.Id)
                .Column("Id")
                .Length(450)
                .Not.Nullable();

            Map(i => i.ClaimType)
                .Nullable();

            Map(i => i.ClaimValue)
                .Nullable();

            References(i => i.IdentityRole)
                .Column("RoleId");

        }
    }
}