namespace Acerola.Application.Commands.Register
{
    using System.Threading.Tasks;

    public interface IRegisterUseCase
    {
        Task<RegisterResult> Execute(string pin, string name, double initialAmount);
    }
}
