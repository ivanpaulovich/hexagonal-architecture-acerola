namespace Acerola.Application.Commands.Close
{
    using System.Threading.Tasks;
    using Acerola.Domain.Customers;

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

        public async Task<CloseResult> Handle(CloseCommand command)
        {
            Customer customer = await customerReadOnlyRepository.GetByAccount(command.AccountId);
            customer.RemoveAccount(command.AccountId);
            await customerWriteOnlyRepository.Update(customer);

            CloseResult response = resultConverter.Map<CloseResult>(customer);

            return response;
        }
    }
}
