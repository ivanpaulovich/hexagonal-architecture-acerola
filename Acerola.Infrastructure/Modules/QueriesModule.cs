using Autofac;
using Acerola.Application.Queries;
using Acerola.Infrastructure.Queries;
using System;

namespace Acerola.Infrastructure.Modules
{
    public class QueriesModule : Module
    {
        public readonly string connectionString;
        public readonly string databaseName;

        public QueriesModule(string connectionString, string databaseName)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            if (string.IsNullOrWhiteSpace(databaseName))
                throw new ArgumentNullException(nameof(databaseName));

            this.connectionString = connectionString;
            this.databaseName = databaseName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomersQueries>()
                .As<ICustomersQueries>()
                .WithParameter("connectionString", connectionString)
                .WithParameter("databaseName", databaseName)
                .SingleInstance();

            builder.RegisterType<AccountsQueries>()
                .As<IAccountsQueries>()
                .WithParameter("connectionString", connectionString)
                .WithParameter("databaseName", databaseName)
                .SingleInstance();
        }
    }
}
