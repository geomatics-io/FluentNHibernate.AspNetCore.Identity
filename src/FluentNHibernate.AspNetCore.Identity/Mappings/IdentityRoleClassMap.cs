using FluentNHibernate.AspNetCore.Identity.Entities;
using FluentNHibernate.Mapping;

namespace FluentNHibernate.AspNetCore.Identity.Mappings
{
    public class IdentityRoleClassMap : ClassMap<IdentityRole>
    {
        public IdentityRoleClassMap()
        {
            Table("AspNetRoles");
            LazyLoad();

            Id(i => i.Id)
                .GeneratedBy.Assigned()
                .Length(450)
                .Not.Nullable();

            Map(i => i.Name)
                .Column("Name")
                .Nullable()
                .Unique()
                .Length(256);

            Map(i => i.NormalizedName)
                .Column("NormalizedName")
                .Nullable()
                .Length(256);

            Map(i => i.ConcurrencyStamp)
                .Column("ConcurrencyStamp")
                .Nullable();

            HasManyToMany(i => i.IdentityUsers)
                .ParentKeyColumn("RoleId")
                .ChildKeyColumn("UserId")
                .Cascade.All()
                .Inverse()
                .Table("AspNetUserRoles");

            HasMany(i => i.IdentityRoleClaims)
                .Cascade.All()
                .KeyColumn("RoleId");

        }
    }
}