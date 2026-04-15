using System;

namespace _Project.Scripts.Services.Logging
{
    [Flags]
    public enum LogModule
    {
        None = 0,
        Saving = 1,
        Analytics = 2,
        All = Saving | Analytics
    }
}