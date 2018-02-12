namespace Acerola.Application.Commands.Close
{
    using System.Threading.Tasks;
    using Acerola.Domain.Customers;

    public class CloseHandler : ICloseHandler
    {
        private readonly ICustomerReadOnlyRepository customerReadOnlyRepository;
        private readonly ICustomerWriteOnlyRepository customerWriteOnlyRepository;

        public CloseHandler(
            ICustomerReadOnlyRepository customerReadOnlyRepository,
            ICustomerWriteOnlyRepository customerWriteOnlyRepository)
        {
            this.customerReadOnlyRepository = customerReadOnlyRepository;
            this.customerWriteOnlyRepository = customerWriteOnlyRepository;
        }

        public async Task Handle(CloseCommand command)
        {
            Customer customer = await customerReadOnlyRepository.GetByAccount(command.AccountId);
            customer.RemoveAccount(command.AccountId);
            await customerWriteOnlyRepository.Update(customer);
        }
    }
}
