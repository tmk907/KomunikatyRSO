using System;

namespace KomunikatyRSO.Shared.Api
{
    public static class DateTimeExtensions
    {
        public static DateTime ToDateTime(this string value)
        {
            return DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
