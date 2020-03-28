using System;
using System.Collections.Generic;
using System.Text;

namespace FileBagWebApi.Utilities.NetCore.Interfaces
{
    public enum InMemoryCacheOffset
    {
        OneSecond,
        TenSeconds,
        ThirtySeconds,
        OneMinute,
        FiveMinutes,
        FiftyMinutes,
        ThirtyMinutes,
        OneHour,
        TwoHours,
        SixHours,
        TwelveHours,
        OneDay,
        NoLimit
    }
}
