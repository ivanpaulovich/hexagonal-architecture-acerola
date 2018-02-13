namespace Acerola.Application.Commands.Register
{
    using System.Threading.Tasks;

    public interface IRegisterHandler
    {
        Task<RegisterResult> Handle(RegisterCommand message);
    }
}
