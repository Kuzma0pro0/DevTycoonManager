using System;

namespace DevIdle.Core
{
    public struct GameTime
    {
        public const int SecondsPerDay = 5;
        public const int DaysPerYear = 365;

        private static bool originSet;
        private static GameTime origin;
        public static GameTime Origin
        {
            get
            {
                // making origin field nullable and checking it for null makes il2cpp to fail with stackoverflow. dunno ¯\_(ツ)_/¯
                if (!originSet)
                {
                    origin = new GameTime(20, 12, 6);
                    originSet = true;
                }

                return origin;
            }
        }

        private static (int year, int month, int day) SecondsToDate(double time)
        {
            int[] DaysToMonth = { 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334, 365 };

            int n = (int)(time / SecondsPerDay);
            int year = n / DaysPerYear;
            n = n - (year * DaysPerYear);
            int m = (n >> 5) + 1;

            while (n >= DaysToMonth[m])
            {
                m++;
            }

            var month = m;
            var day = n - DaysToMonth[m - 1] + 1;

            return (year, month, day);
        }

        private static double DateToSeconds(int day, int month, int year)
        {
            int[] DaysToMonth = { 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334, 365 };

            if (year >= 0 && year <= 9999 && month >= 1 && month <= 12)
            {
                if (day >= 1 && day <= DaysToMonth[month] - DaysToMonth[month - 1])
                {
                    int y = year;
                    int n = y * 365 + DaysToMonth[month - 1] + day - 1;
                    return n * SecondsPerDay;
                }
            }

            throw new ArgumentException("Invalid date.");
        }

        public static GameTime FromDateString(string dateString)
        {
            var day = int.Parse(dateString.Substring(0, 2));
            var month = int.Parse(dateString.Substring(2, 2));
            var year = int.Parse(dateString.Substring(4, 2));

            return new GameTime(day, month, year);
        }

        public double TotalSeconds
        { get; private set; }

        // TODO: Make day/month/year lazy properties
        public readonly int Day;
        public readonly int Month;
        public readonly int Year;
        public readonly int TotalDays;

        public GameTime(int day, int month, int year)
        {
            TotalSeconds = DateToSeconds(day, month, year);
            Day = day;
            Month = month;
            Year = year;
            TotalDays = (int)(TotalSeconds / SecondsPerDay);
        }

        public GameTime(double time)
        {
            TotalSeconds = time;

            var date = SecondsToDate(time);
            Day = date.day;
            Month = date.month;
            Year = date.year;
            TotalDays = (int)(time / SecondsPerDay);
        }

        public override string ToString()
        {
            return $"{Day:00}/{Month:00}/{Year:00}";
        }
    }
}