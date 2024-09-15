using System;

namespace Services.Time
{
    public class DefaultTimeService : ITimeService
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}