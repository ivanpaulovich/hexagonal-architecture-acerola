namespace Manga.Infrastructure.Mappings
{
    using AutoMapper;
    using Acerola.Application.Results;
    using Acerola.Domain.Customers.Accounts;

    public class AccountsProfile : Profile
    {
        public AccountsProfile()
        {
            CreateMap<Account, AccountResult>()
                .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CurrentBalance, opt => opt.MapFrom(src => src.CurrentBalance.Value));

            CreateMap<Debit, TransactionResult>()
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount.Value))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.TransactionDate, opt => opt.MapFrom(src => src.TransactionDate));

            CreateMap<Credit, TransactionResult>()
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount.Value))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.TransactionDate, opt => opt.MapFrom(src => src.TransactionDate));
        }
    }
}
