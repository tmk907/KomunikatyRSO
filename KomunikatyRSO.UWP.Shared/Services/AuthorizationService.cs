using KomunikatyRSO.Shared.Commands.Accounts;
using KomunikatyRSO.UWP.Shared.Settings;
using System;
using System.Threading.Tasks;

namespace KomunikatyRSO.UWP.Shared.Services
{
    public class AuthorizationService
    {
        public AuthorizationService()
        {
            secureStorage = new SecureStorage();
            client = new PushNotificationsClient();
        }

        private readonly ISecureStorage secureStorage;
        private readonly PushNotificationsClient client;

        public bool IsRegistered()
        {
            return secureStorage.UserId == Guid.Empty;
        }

        public async Task RegisterAsync()
        {
            var userId = Guid.NewGuid();
            string password = GeneratePassword();
            var command = new Register()
            {
                Password = password,
                UserId = userId,
            };
            try
            {
                var result = await client.RegisterAsync(command);
                if (result)
                {
                    secureStorage.UserId = userId;
                    secureStorage.Password = password;
                }
            }
            catch(Exception ex)
            {

            }
        }

        public async Task RequestTokenIfNeededAsync()
        {
            if (String.IsNullOrEmpty(secureStorage.Token) || secureStorage.TokenExpiration < DateTime.Now)
            {
                var command = new CreateToken()
                {
                    Password = secureStorage.Password,
                    UserId = secureStorage.UserId
                };
                try
                {
                    var jwtToken = await client.RequestTokenAsync(command);
                    secureStorage.Token = jwtToken.Token;
                    secureStorage.TokenExpiration = jwtToken.Expires;
                }
                catch (Exception ex)
                {
                }
            }
        }

        private string GeneratePassword()
        {
            return "password";
        }
    }
}
