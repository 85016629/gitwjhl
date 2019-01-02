using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace fx.Infra.Utility
{
    public class DatetimeHelper
    {
        /// <summary>
        /// 日期转换为13位时间戳。
        /// 这里在太平洋时间加上了8个小时，即为中国时间。
        /// </summary>
        /// <param name="now"></param>
        /// <returns></returns>
        public static long DateTimeToTimespan(DateTime? now = null)
        {
            if (now == null)
            {
                now = DateTime.Now;
            }
            var dateStart = new DateTime(1970, 1, 1, 8, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((Convert.ToDateTime(now) - dateStart).TotalMilliseconds);//TotalSeconds
        }

        /// <summary>
        /// 时间戳转换为时间。根据不同的位数，统一的转换为当前时间。
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime TimestampToDate(long timestamp)
        {
            var dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

            var toNow = new TimeSpan();
            if (timestamp.ToString(CultureInfo.InvariantCulture).Length == 17)
                toNow = new TimeSpan(timestamp);

            if (timestamp.ToString(CultureInfo.InvariantCulture).Length == 10)
                toNow = new TimeSpan(timestamp * 10000000);

            if (timestamp.ToString(CultureInfo.InvariantCulture).Length == 13)
                toNow = new TimeSpan(timestamp * 10000);

            var dtR = dtStart.Add(toNow);

            return dtR;
        }
    }
}
