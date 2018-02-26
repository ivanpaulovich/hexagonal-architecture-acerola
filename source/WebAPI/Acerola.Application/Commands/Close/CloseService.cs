namespace Acerola.Application.Commands.Close
{
    using System.Threading.Tasks;
    using Acerola.Domain.Customers;
    using Acerola.Domain.Customers.Accounts;

    public class CloseService : ICloseService
    {
        private readonly ICustomerReadOnlyRepository customerReadOnlyRepository;
        private readonly ICustomerWriteOnlyRepository customerWriteOnlyRepository;
        private readonly IResultConverter resultConverter;

        public CloseService(
            ICustomerReadOnlyRepository customerReadOnlyRepository,
            ICustomerWriteOnlyRepository customerWriteOnlyRepository,
            IResultConverter resultConverter)
        {
            this.customerReadOnlyRepository = customerReadOnlyRepository;
            this.customerWriteOnlyRepository = customerWriteOnlyRepository;
            this.resultConverter = resultConverter;
        }

        public async Task<CloseResult> Process(CloseCommand command)
        {
            Customer customer = await customerReadOnlyRepository.GetByAccount(command.AccountId);
            Account account = customer.FindAccount(command.AccountId);
            customer.RemoveAccount(command.AccountId);
            await customerWriteOnlyRepository.Update(customer);

            CloseResult response = resultConverter.Map<CloseResult>(account);

            return response;
        }
    }
}
