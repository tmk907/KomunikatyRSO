using KomunikatyRSO.Web.Infrastructure.Extensions;
using KomunikatyRSO.Web.Infrastructure.Services;
using KomunikatyRSO.Shared.Commands;
using KomunikatyRSO.Shared.Commands.Accounts;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace KomunikatyRSO.Web.Infrastructure.Handlers.Accounts
{
    public class CreateTokenHandler : ICommandHandler<CreateToken>
    {
        private readonly AccountService accountService;
        private readonly JwtService jwtService;
        private readonly IMemoryCache cache;

        public CreateTokenHandler(AccountService accountService, JwtService jwtService, IMemoryCache cache)
        {
            this.accountService = accountService;
            this.jwtService = jwtService;
            this.cache = cache;
        }

        public async Task HandleAsync(CreateToken command)
        {
            bool areCredentialsValid = await accountService.AreCredentialsValid(command.UserId, command.Password);
            if (areCredentialsValid)
            {
                var jwt = jwtService.CreateToken(command.UserId);
                cache.SetJwt(command.UserId, jwt);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
