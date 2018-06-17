namespace Acerola.Infrastructure.Modules
{
    using Autofac;
    using Acerola.Application;

    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //
            // Register all Types in Acerola.Application
            //
            builder.RegisterAssemblyTypes(typeof(ApplicationException).Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
