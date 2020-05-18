using System;
using FluentNHibernate.AspNetCore.Identity.Entities;
using FluentNHibernate.Mapping;

namespace FluentNHibernate.AspNetCore.Identity.Mappings
{
    public class IdentityUserTokenClassMap: ClassMap<IdentityUserToken>
    {
        public IdentityUserTokenClassMap()
        {
            Table("AspNetUserTokens");
            LazyLoad();

            CompositeId()
                .KeyProperty(i => i.UserId, "UserId")
                .KeyProperty(i => i.LoginProvider, "LoginProvider")
                .KeyProperty(i => i.Name, "Name");

            Map(i => i.Value)
                .Nullable();

            References(i => i.IdentityUser)
                .Column("UserId")
                .Not.Insert()
                .Not.Update();
        }
    }
}