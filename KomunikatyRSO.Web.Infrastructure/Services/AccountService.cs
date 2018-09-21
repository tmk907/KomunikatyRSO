using KomunikatyRSO.Web.Infrastructure.EF;
using KomunikatyRSO.Web.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KomunikatyRSO.Web.Infrastructure.Services
{
    public class AccountService
    {
        private readonly NotificationsDbContext dbContext;
        private readonly IEncrypter encrypter;

        public AccountService(NotificationsDbContext dbContext, IEncrypter encrypter)
        {
            this.dbContext = dbContext;
            this.encrypter = encrypter;
        }

        public async Task RegisterAsync(Guid userId, string password)
        {
            var passwordHash = encrypter.GetHash(password);
            var token = encrypter.GetToken(userId, passwordHash);
            var user = new User(userId, passwordHash, token);
            dbContext.Add(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsTokenValid(Guid userId, string token)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user != null)
            {
                if (user.Token == token)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> AreCredentialsValid(Guid userId, string password)
        {
            var passwordHash = encrypter.GetHash(password);
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user!= null)
            {
                if (user.PasswordHash == passwordHash)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<List<string>> GetUsers()
        {
            return await dbContext.Users.Select(s=>s.UserId.ToString()).ToListAsync();
        }
    }
}
