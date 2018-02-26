namespace Acerola.Application.Commands.Deposit
{
    using System.Threading.Tasks;

    public interface IDepositService
    {
        Task<DepositResult> Process(DepositCommand command);
    }
}
