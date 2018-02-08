namespace Acerola.Infrastructure.AutoMapper
{
    using Acerola.Application.DTO;
    using Acerola.Domain.Accounts;
    using Acerola.Domain.Customers;
    using global::AutoMapper;

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
