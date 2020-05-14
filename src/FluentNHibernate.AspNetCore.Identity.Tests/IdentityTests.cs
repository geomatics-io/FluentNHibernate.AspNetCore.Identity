using System.Data;
using System.Linq;
using FluentNHibernate.AspNetCore.Identity.Entities;
using NHibernate;
using NHibernate.AdoNet;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Context;
using NHibernate.Event;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NUnit.Framework;
using Microsoft.AspNetCore.Identity;
using System;

namespace FluentNHibernate.AspNetCore.Identity.Tests
{
    [TestFixture]
    public class IdentityTests
    {
        private static ISessionFactory _sessionFactory;

        [SetUp]
        public void SetUp()
        {
            var _connectionString = "Server=127.0.0.1;Port=5432;Database=omnifeed;User Id=omnifeed;Password=omnifeed;";

            var connection = PostgreSQLConfiguration
                    .Standard
                    .ConnectionString(_connectionString)
                    .IsolationLevel(IsolationLevel.ReadCommitted);

            var _fluentConfiguration = Fluently.Configure()
                .Database(connection)
                .Mappings(mapper =>
                {
                    mapper.FluentMappings.AddFromAssemblyOf<FluentNHibernate.AspNetCore.Identity.Entities.IdentityUser>();

                })
                .CurrentSessionContext<ThreadStaticSessionContext>()
                .ExposeConfiguration(Config);

            _sessionFactory = _fluentConfiguration.BuildSessionFactory();
        }

        private void Config(Configuration cfg)
        {
            cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionProvider, "NHibernate.Connection.DriverConnectionProvider");
            cfg.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver, "NHibernate.Driver.NpgsqlDriver");
            cfg.SetProperty(NHibernate.Cfg.Environment.Dialect, "NHibernate.Dialect.PostgreSQLDialect");

            BuildSchema(cfg);
        }

        private void BuildSchema(Configuration cfg)
        {
            new SchemaExport(cfg).Create(true, true);
            // new SchemaUpdate(cfg).Execute(true, true);
        }

        [Test]
        public async System.Threading.Tasks.Task IsPrime_InputIs1_ReturnFalseAsync()
        {
            
            using(ISession session = _sessionFactory.OpenSession())
            using(var tx = session.BeginTransaction())
            {
                try
                {


                    FluentNHibernate.AspNetCore.Identity.UserStore<FluentNHibernate.AspNetCore.Identity.Entities.IdentityUser, FluentNHibernate.AspNetCore.Identity.Entities.IdentityRole> userStore = new UserStore<Entities.IdentityUser, Entities.IdentityRole>(session);
                    UserManager<FluentNHibernate.AspNetCore.Identity.Entities.IdentityUser> userManager = new UserManager<FluentNHibernate.AspNetCore.Identity.Entities.IdentityUser>(userStore, null, null, null, null, null, null, null, null);

                    userManager.PasswordHasher = new PasswordHasher<FluentNHibernate.AspNetCore.Identity.Entities.IdentityUser>();

                    string password = @"6yd%Ss*vCv&QA6";

                    var user = new FluentNHibernate.AspNetCore.Identity.Entities.IdentityUser
                    {
                        UserName = "username",
                        Email = "user.name@email.com",
                        EmailConfirmed = true,
                        LockoutEnabled = false
                    };

                    user.PasswordHash = userManager.PasswordHasher.HashPassword(user, password);

                    var result = await userManager.CreateAsync(user, password);

                    tx.Commit();
                }
                catch(Exception ex)
                {
                    tx.Rollback();
                }
            }
        }
    }
}