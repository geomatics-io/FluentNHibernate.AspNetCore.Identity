using FluentNHibernate.AspNetCore.Identity.Entities;
using FluentNHibernate.Mapping;
using NHibernate;

namespace FluentNHibernate.AspNetCore.Identity.Mappings
{
    public class IdentityUserClassMap : ClassMap<IdentityUser>
    {
        public IdentityUserClassMap()
        {
            Table("AspNetUsers");
            LazyLoad();

            Id(i => i.Id)
                .GeneratedBy.UuidHex("D")
                .Column("Id")
                .Length(450)
                .Not.Nullable();

            Map(i => i.UserName)
                .Column("UserName")
                .Length(256)
                .Unique()
                .Nullable();
            
            Map(i => i.NormalizedUserName)
                .Column("NormalizedUserName")
                .Length(256)
                .Nullable();

            Map(i => i.Email)
                .Column("Email")
                .Length(256)
                .Index("EmailIndex")
                .Nullable();

            Map(i => i.NormalizedEmail)
                .Column("NormalizedEmail")
                .Length(256)
                .Nullable();

            Map(i => i.EmailConfirmed)
                .Column("EmailConfirmed")
                .Not.Nullable();

            Map(i => i.PasswordHash )
                .Column("PasswordHash")
                .Nullable();

            Map(i => i.SecurityStamp )
                .Column("SecurityStamp")
                .Nullable();

            Map(i => i.ConcurrencyStamp )
                .Column("ConcurrencyStamp")
                .Nullable();

            Map(i => i.PhoneNumber )
                .Column("PhoneNumber")
                .Nullable();

            Map(i => i.PhoneNumberConfirmed )
                .Column("PhoneNumberConfirmed")
                .Not.Nullable();

            Map(i => i.TwoFactorEnabled )
                .Column("TwoFactorEnabled")
                .Not.Nullable();

            Map(i => i.LockoutEnd )
                .Column("LockoutEnd")
                .CustomType(NHibernateUtil.DateTime.GetType())
                .Nullable();
            

            Map(i => i.LockoutEnabled )
                .Column("LockoutEnabled")
                .Not.Nullable();

            Map(i => i.AccessFailedCount )
                .Column("AccessFailedCount")
                .Not.Nullable();

            HasManyToMany(i => i.IdentityRoles)
                .ParentKeyColumn("UserId")
                .ChildKeyColumn("RoleId")
                .Cascade.All()
                .Table("AspNetUserRoles");

            HasMany(i => i.IdentityUserLogins)
                .KeyColumns.Add("UserId")
                .Cascade.All();

            HasMany(i => i.IdentityUserClaims)
                .KeyColumns.Add("UserId")
                .Cascade.All();
        }
    }
}