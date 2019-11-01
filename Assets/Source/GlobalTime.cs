using System;

namespace DevIdle
{
    public static class GlobalTime
    {
        public static DateTime Current
        {
            get
            {
                return DateTime.UtcNow;
            }
        }
    }
}
