using FluentNHibernate.AspNetCore.Identity.Entities;
using FluentNHibernate.Mapping;

namespace FluentNHibernate.AspNetCore.Identity.Mappings
{
    public class IdentityUserLoginClassMap : ClassMap<IdentityUserLogin>
    {
        public IdentityUserLoginClassMap()
        {
            Table("AspNetUserLogins");
            LazyLoad();

            CompositeId()
                .KeyProperty(i => i.LoginProvider, "LoginProvider")
                .KeyProperty(i => i.ProviderKey, "ProviderKey");

            Map(i => i.ProviderDisplayName)
                .Nullable();

            References(i => i.IdentityUser)
                .Column("UserId")
                .Not.Nullable();
        }
    }
}