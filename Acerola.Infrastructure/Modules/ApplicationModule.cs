using Autofac;
using MyAccountAPI.Domain.Model.Accounts;
using MyAccountAPI.Domain.Model.Customers;
using MyAccountAPI.Producer.Infrastructure.DataAccess;
using MyAccountAPI.Producer.Infrastructure.DataAccess.Repositories.Accounts;
using MyAccountAPI.Producer.Infrastructure.DataAccess.Repositories.Customers;

namespace MyAccountAPI.Producer.Infrastructure.Modules
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
