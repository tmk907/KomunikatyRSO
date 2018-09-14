using System;

namespace KomunikatyRSO.Shared.Commands.Accounts
{
    public class Register : ICommand
    {
        public Guid UserId { get; set; }
        public string Password { get; set; }
    }
}
