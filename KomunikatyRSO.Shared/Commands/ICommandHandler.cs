using System.Threading.Tasks;

namespace KomunikatyRSO.Shared.Commands
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}
