namespace Acerola.Application.Commands.Deposit
{
    using System.Threading.Tasks;

    public interface IDepositHandler
    {
        Task<DepositResult> Handle(DepositCommand command);
    }
}
