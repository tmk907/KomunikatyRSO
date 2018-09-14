using KomunikatyRSO.Web.Infrastructure.EF;
using System.Linq;
using System.Threading.Tasks;

namespace KomunikatyRSO.Web.Infrastructure.Services
{
    public class WnsTokenStorage
    {
        private readonly NotificationsDbContext dbContext;

        public WnsTokenStorage(NotificationsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string GetToken()
        {
            var token = dbContext.Tokens.FirstOrDefault();
            return token?.WNSToken ?? "";
        }

        public async Task SaveToken(string accessToken)
        {
            var token = dbContext.Tokens.FirstOrDefault();
            if (token == null)
            {
                dbContext.Tokens.Add(new Models.Token() { WNSToken = accessToken });
            }
            else
            {
                token.WNSToken = accessToken;
                dbContext.Tokens.Update(token);
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
