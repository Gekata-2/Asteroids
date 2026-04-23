using System;

namespace _Project.Scripts.Services.Logging
{
    [Flags]
    public enum LogModule
    {
        None = 0,
        Other = 1,
        Saving = 2,
        Analytics = 4,
        Ads = 8,
        All = Other | Saving | Analytics | Ads
    }
}