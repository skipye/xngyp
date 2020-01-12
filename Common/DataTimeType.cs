using System;

namespace Common
{
    public enum DataTimeType
    {
        Week,
        Month,
        Season,
        Year
    }
    public class DataTimeManager
    {
        /// <summary>
        /// 获取开始时间
        /// </summary>
        /// <param name="type"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime GetTimeStartByType(DataTimeType type, DateTime time)
        {
            switch (type)
            {
                case DataTimeType.Week:
                    return time.AddDays(-(int)time.DayOfWeek + 1);
                case DataTimeType.Month:
                    return time.AddDays(-(int)time.Day + 1);
                case DataTimeType.Season:
                    var time1 = time.AddMonths(0 - ((time.Month - 1) % 3));
                    return time1.AddDays(-time1.Day + 1);
                case DataTimeType.Year:
                    return time.AddDays(-(int)time.DayOfYear + 1);
                default:
                    return time.AddDays(-(int)time.DayOfWeek + 1);
                    //return null;
            }
        }

        /// <summary>
        /// 获取结束时间
        /// </summary>
        /// <param name="DataTimeType">Week、Month、Season、Year</param>
        /// <param name="now"></param>
        /// <returns></returns>
        public static DateTime GetTimeEndByType(DataTimeType type, DateTime now)
        {
            switch (type)
            {
                case DataTimeType.Week:
                    return now.AddDays(7 - (int)now.DayOfWeek);
                case DataTimeType.Month:
                    return now.AddMonths(1).AddDays(-now.AddMonths(1).Day + 1).AddDays(-1);
                case DataTimeType.Season:
                    var time = now.AddMonths((3 - ((now.Month - 1) % 3) - 1));
                    return time.AddMonths(1).AddDays(-time.AddMonths(1).Day + 1).AddDays(-1);
                case DataTimeType.Year:
                    var time2 = now.AddYears(1);
                    return time2.AddDays(-time2.DayOfYear);
                default:
                    return now.AddDays(7 - (int)now.DayOfWeek);
            }
        }

    }
}
