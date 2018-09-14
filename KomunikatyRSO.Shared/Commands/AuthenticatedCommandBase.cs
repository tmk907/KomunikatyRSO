using System;

namespace KomunikatyRSO.Shared.Commands
{
    public class AuthenticatedCommandBase : IAuthenticatedCommand
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
    }
}
