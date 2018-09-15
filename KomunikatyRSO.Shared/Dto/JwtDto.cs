using System;

namespace KomunikatyRSO.Shared.Dto
{
    public class JwtDto
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
