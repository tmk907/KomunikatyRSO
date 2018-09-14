using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KomunikatyRSO.Web.Infrastructure.Services
{
    public interface IEncrypter
    {
        string GetHash(string value);
        string GetToken(Guid userId, string passwordHash);
    }
}
