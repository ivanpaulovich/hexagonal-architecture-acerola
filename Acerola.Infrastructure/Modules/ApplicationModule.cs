using Autofac;
using Acerola.Infrastructure.DataAccess;
using Acerola.Infrastructure.DataAccess.Repositories.Customers;
using Acerola.Domain.Customers;
using Acerola.Domain.Accounts;
using Acerola.Infrastructure.DataAccess.Repositories.Accounts;

namespace Acerola.Infrastructure.Modules
{
    public class ApplicationModule : Module
    {
        public readonly string connectionString;
        public readonly string databaseName;

        public ApplicationModule(string connectionString, string databaseName)
        {
            this.connectionString = connectionString;
            this.databaseName = databaseName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MongoContext>()
                .As<MongoContext>()
                .WithParameter("connectionString", connectionString)
                .WithParameter("databaseName", databaseName)
                .SingleInstance();

            builder.RegisterType<CustomerReadOnlyRepository>()
                .As<ICustomerReadOnlyRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AccountReadOnlyRepository>()
                .As<IAccountReadOnlyRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
