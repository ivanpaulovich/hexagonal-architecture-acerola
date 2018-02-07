namespace Acerola.Infrastructure.AutoMapper
{
    using Acerola.Application.ViewModels;
    using Acerola.Domain.Accounts;
    using Acerola.Domain.Customers;
    using global::AutoMapper;

    public class DomainConverter
    {
        private readonly MapperConfiguration config;
        
        public DomainConverter()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Customer, CustomerVM>()
                    .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Personnummer, opt => opt.MapFrom(src => src.PIN.Text))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Text));

                cfg.CreateMap<Account, AccountVM>()
                    .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.CurrentBalance, opt => opt.MapFrom(src => src.CurrentBalance.Value))
                    .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId));

                cfg.CreateMap<Debit, TransactionVM>()
                    .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount.Value))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                    .ForMember(dest => dest.TransactionDate, opt => opt.MapFrom(src => src.TransactionDate));

                cfg.CreateMap<Credit, TransactionVM>()
                    .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount.Value))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                    .ForMember(dest => dest.TransactionDate, opt => opt.MapFrom(src => src.TransactionDate));
            });
        }

        public AccountVM Map(Account account)
        {
            AccountVM vm = Mapper.Map<AccountVM>(account);
            return vm;
        }

        public CustomerVM Map(Customer customer)
        {
            CustomerVM vm = Mapper.Map<CustomerVM>(customer);
            return vm;
        }
    }
}
