namespace Acerola.Infrastructure.Modules
{
    using Autofac;
    using Acerola.Infrastructure.Mappings;
    using Acerola.Application.Mappers;

    public class AutoMapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DTOMapper>()
               .As<IDTOMapper>()
               .InstancePerLifetimeScope();
        }
    }
}
