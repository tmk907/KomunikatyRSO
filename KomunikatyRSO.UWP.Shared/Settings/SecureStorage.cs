using System;

namespace KomunikatyRSO.UWP.Shared.Settings
{
    public class SecureStorage : ISecureStorage
    {
        SettingsHelper helper;

        public SecureStorage()
        {
            helper = new SettingsHelper();
        }

        public Guid UserId
        {
            get { return helper.Read<Guid>(nameof(UserId), Guid.Empty); }
            set { helper.Save(nameof(UserId), value); }
        }

        public string Password
        {
            get { return helper.Read<string>(nameof(Password), ""); }
            set { helper.Save(nameof(Password), value); }
        }

        public string Token
        {
            get { return helper.Read<string>(nameof(Token), ""); }
            set { helper.Save(nameof(Token), value); }
        }

        public DateTime TokenExpiration
        {
            get { return helper.Read<DateTime>(nameof(TokenExpiration), DateTime.MinValue); }
            set { helper.Save(nameof(TokenExpiration), value); }
        }
    }
}
