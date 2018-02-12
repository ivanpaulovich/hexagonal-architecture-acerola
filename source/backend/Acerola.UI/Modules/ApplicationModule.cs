namespace Acerola.UI.Modules
{
    using Acerola.Application.Commands.Register;
    using Autofac;

    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //
            // Register all Types in Manga.Application
            //
            builder.RegisterAssemblyTypes(typeof(RegisterHandler).Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
