namespace Acerola.Infrastructure.Modules
{
    using Autofac;
    using Acerola.Application.Queries;
    using Acerola.Infrastructure.Queries;
    using System;
    using Acerola.Infrastructure.AutoMapper;

    public class AutoMapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DomainConverter>()
                .As<DomainConverter>()
                .SingleInstance();
        }
    }
}
