using System;

namespace KomunikatyRSO.Shared.Commands.Accounts
{
    public class CreateToken : ICommand
    {
        public Guid UserId { get; set; }
        public string Password { get; set; }
    }
}
