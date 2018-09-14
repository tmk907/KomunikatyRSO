using System.Threading.Tasks;

namespace KomunikatyRSO.Shared.Commands
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<T>(T command) where T : ICommand;
    }
}
