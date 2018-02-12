namespace Acerola.Application.Commands.Close
{
    using System.Threading.Tasks;

    public interface ICloseHandler
    {
        Task Handle(CloseCommand command);
    }
}
