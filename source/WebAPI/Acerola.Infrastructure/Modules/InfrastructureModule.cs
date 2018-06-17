namespace Acerola.Infrastructure.Modules
{
    using Autofac;
    using Acerola.Infrastructure.MongoDataAccess;

    public class InfrastructureModule : Autofac.Module
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Context>()
                .As<Context>()
                .WithParameter("connectionString", ConnectionString)
                .WithParameter("databaseName", DatabaseName)
                .SingleInstance();

            //
            // Register all Types in Acerola.Infrastructure
            //
            builder.RegisterAssemblyTypes(typeof(InfrastructureException).Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
