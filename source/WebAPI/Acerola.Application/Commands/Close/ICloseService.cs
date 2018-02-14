namespace Acerola.Application.Commands.Close
{
    using System.Threading.Tasks;

    public interface ICloseService
    {
        Task<CloseResult> Handle(CloseCommand command);
    }
}
