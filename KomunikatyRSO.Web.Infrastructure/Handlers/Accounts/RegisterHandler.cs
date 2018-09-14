using KomunikatyRSO.Web.Infrastructure.Services;
using KomunikatyRSO.Shared.Commands;
using KomunikatyRSO.Shared.Commands.Accounts;
using System.Threading.Tasks;

namespace KomunikatyRSO.Web.Infrastructure.Handlers.Accounts
{
    public class RegisterHandler : ICommandHandler<Register>
    {
        private readonly AccountService accountService;

        public RegisterHandler(AccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task HandleAsync(Register command)
        {
            await accountService.RegisterAsync(command.UserId, command.Password);
        }
    }
}
