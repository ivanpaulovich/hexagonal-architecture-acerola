namespace Acerola.Application.Commands.Deposit
{
    using System.Threading.Tasks;

    public interface IDepositService
    {
        Task<DepositResult> Handle(DepositCommand command);
    }
}
