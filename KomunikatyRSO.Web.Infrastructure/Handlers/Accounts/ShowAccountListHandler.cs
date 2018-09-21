using KomunikatyRSO.Shared.Commands;
using KomunikatyRSO.Shared.Commands.Accounts;
using KomunikatyRSO.Web.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KomunikatyRSO.Web.Infrastructure.Handlers.Accounts
{
    public class ShowAccountListHandler : ICommandHandler<ShowAccountList>
    {
        private readonly AccountService accountService;

        public ShowAccountListHandler(AccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task HandleAsync(ShowAccountList command)
        {
            await accountService.GetUsers();
        }

        public async Task<List<string>> HandleAsync2(ShowAccountList command)
        {
            return await accountService.GetUsers();
        }
    }
}
