using System;

namespace Services.Time
{
    public interface ITimeService
    {
        DateTime UtcNow { get; }
    }
}