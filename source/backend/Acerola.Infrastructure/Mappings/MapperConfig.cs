namespace Acerola.Infrastructure.Mappings
{
    using AutoMapper;

    public class MapperConfig
    {   
        public static void Register()
        {
            Mapper.Initialize(cfg => {
                cfg.AddProfile<AccountsProfile>();
                cfg.AddProfile<CustomersProfile>();
            });
        }
    }
}
