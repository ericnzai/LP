using System;
using System.Globalization;

namespace LP.Model.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToStringWithCulture(this DateTime dateTime, CultureInfo ci = null)
        {
            if (ci != null)
            {
                return dateTime.ToString("dd MMM yyyy", ci);
            }
            else
            {
                return dateTime.ToString("dd MMM yyyy");
            }
        }
    }
}
