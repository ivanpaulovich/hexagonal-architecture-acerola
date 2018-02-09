namespace Acerola.Infrastructure.Modules
{
    using Autofac;
    using Acerola.Infrastructure.DataAccess;
    using Acerola.Infrastructure.DataAccess.Repositories.Customers;
    using Acerola.Domain.Customers;
    using Acerola.Domain.Accounts;
    using Acerola.Infrastructure.DataAccess.Repositories.Accounts;
    using Acerola.Application.UseCases;
    using Acerola.Application.Boundary;

    public class ApplicationModule : Module
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AccountBalanceContext>()
                .As<AccountBalanceContext>()
                .WithParameter("connectionString", ConnectionString)
                .WithParameter("databaseName", DatabaseName)
                .InstancePerLifetimeScope();

            builder.RegisterType<CustomerReadOnlyRepository>()
                .As<ICustomerReadOnlyRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CustomerWriteOnlyRepository>()
                .As<ICustomerWriteOnlyRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AccountReadOnlyRepository>()
                .As<IAccountReadOnlyRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AccountWriteOnlyRepository>()
                .As<IAccountWriteOnlyRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<Register>()
                .As<IRegister>()
                .InstancePerLifetimeScope();

            builder.RegisterType<Deposit>()
                .As<IDeposit>()
                .InstancePerLifetimeScope();

            builder.RegisterType<Withdraw>()
                .As<IWithdraw>()
                .InstancePerLifetimeScope();

            builder.RegisterType<Close>()
                .As<IClose>()
                .InstancePerLifetimeScope();
        }
    }
}
