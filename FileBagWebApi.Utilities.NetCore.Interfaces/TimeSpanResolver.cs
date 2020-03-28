using System;
using System.Collections.Generic;
using System.Text;

namespace FileBagWebApi.Utilities.NetCore.Interfaces
{
    public static class TimeSpanResolver
    {

        private static TimeSpan OneSecond { get { return TimeSpan.FromSeconds(1); } }

        private static TimeSpan TenSeconds { get { return TimeSpan.FromSeconds(10); } }

        private static TimeSpan ThirtySeconds { get { return TimeSpan.FromSeconds(30); } }

        private static TimeSpan OneMinute { get { return TimeSpan.FromSeconds(60); } }

        private static TimeSpan FiveMinutes { get { return TimeSpan.FromMinutes(5); } }

        private static TimeSpan FiftyMinutes { get { return TimeSpan.FromMinutes(15); } }

        private static TimeSpan ThirtyMinutes { get { return TimeSpan.FromMinutes(30); } }

        private static TimeSpan OneHour { get { return TimeSpan.FromMinutes(60); } }

        private static TimeSpan TwoHours { get { return TimeSpan.FromHours(2); } }

        private static TimeSpan SixHours { get { return TimeSpan.FromHours(6); } }

        private static TimeSpan TwelveHours { get { return TimeSpan.FromHours(12); } }

        private static TimeSpan OneDay { get { return TimeSpan.FromHours(24); } }

        public static TimeSpan Resolve(InMemoryCacheOffset offset)
        {
            switch (offset)
            {
                case InMemoryCacheOffset.OneSecond:
                    return OneSecond;
                case InMemoryCacheOffset.TenSeconds:
                    return TenSeconds;
                case InMemoryCacheOffset.ThirtySeconds:
                    return ThirtySeconds;
                case InMemoryCacheOffset.OneMinute:
                    return OneMinute;
                case InMemoryCacheOffset.FiveMinutes:
                    return FiveMinutes;
                case InMemoryCacheOffset.FiftyMinutes:
                    return FiftyMinutes;
                case InMemoryCacheOffset.ThirtyMinutes:
                    return ThirtyMinutes;
                case InMemoryCacheOffset.OneHour:
                    return OneHour;
                case InMemoryCacheOffset.TwoHours:
                    return TwoHours;
                case InMemoryCacheOffset.SixHours:
                    return SixHours;
                case InMemoryCacheOffset.TwelveHours:
                    return TwelveHours;
                case InMemoryCacheOffset.OneDay:
                    return OneDay;
            }

            return TwoHours;
        }
    }
}
