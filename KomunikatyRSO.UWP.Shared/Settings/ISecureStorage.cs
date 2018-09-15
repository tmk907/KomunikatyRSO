using System;

namespace KomunikatyRSO.UWP.Shared.Settings
{
    public interface ISecureStorage
    {
        Guid UserId { get; set; }
        string Password { get; set; }
        string Token { get; set; }
        DateTime TokenExpiration { get; set; }
    }
}