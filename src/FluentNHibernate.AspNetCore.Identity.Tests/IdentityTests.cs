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

namespace FluentNHibernate.AspNetCore.Identity.Tests
{
    [TestFixture]
    public class IdentityTests
    {
        private static ISessionFactory _sessionFactory;

        [SetUp]
        public void SetUp()
        {
            var _connectionString = "Server=127.0.0.1;Port=5432;Database=pinboard;User Id=pinboard;Password=pinboard;";

            var connection = PostgreSQLConfiguration
                    .Standard
                    .ConnectionString(_connectionString)
                    .IsolationLevel(IsolationLevel.ReadCommitted);

            var _fluentConfiguration = Fluently.Configure()
                .Database(connection)
                .Mappings(mapper =>
                {
                    mapper.FluentMappings.AddFromAssemblyOf<IdentityUser>();

                })
                .CurrentSessionContext<ThreadStaticSessionContext>()
                .ExposeConfiguration(Config);

            _sessionFactory = _fluentConfiguration.BuildSessionFactory();
        }

        private void Config(Configuration cfg)
        {
            cfg.SetProperty(Environment.ConnectionProvider, "NHibernate.Connection.DriverConnectionProvider");
            cfg.SetProperty(Environment.ConnectionDriver, "NHibernate.Driver.NpgsqlDriver");
            cfg.SetProperty(Environment.Dialect, "NHibernate.Dialect.PostgreSQLDialect");

            BuildSchema(cfg);
        }

        private void BuildSchema(Configuration cfg)
        {
            new SchemaExport(cfg).Create(true, true);
            // new SchemaUpdate(cfg).Execute(true, true);
        }

        [Test]
        public void IsPrime_InputIs1_ReturnFalse()
        {
            var session = _sessionFactory.OpenSession();
            var results = session.Query<IdentityUser>().ToList();

            Assert.AreEqual(0, results.Count);
        }
    }
}