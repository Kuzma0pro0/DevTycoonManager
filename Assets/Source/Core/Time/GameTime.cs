using System;

namespace DevIdle.Core
{
    public struct GameTime
    {
        public const int SecondsInHour = MinutesInHour * SecondsInMinute;
        public const int SecondsInDay = SecondsInHour * HoursInDay;
        public const int SecondsInWeek = SecondsInDay * DaysInWeek;
        public const int SecondsInMonth = SecondsInWeek * WeeksInMonth;
        public const int SecondsInYear = SecondsInMonth * MonthsInYear;

        public const int DaysInMonth = DaysInWeek * WeeksInMonth;

        public const int MonthsInYear = 12;
        public const int WeeksInMonth = 4;
        public const int DaysInWeek = 7;
        public const int HoursInDay = 24;
        public const int MinutesInHour = 60;
        public const int SecondsInMinute = 60;

        public int TotalMonths
        { get { return (int)(time / SecondsInMonth); } }
        public int TotalWeeks
        { get { return (int)(time / SecondsInWeek); } }
        public int TotalDays
        { get { return (int)(time / SecondsInDay); } }
        public int TotalHours
        { get { return (int)(time / SecondsInHour); } }
        public int TotalMinutes
        { get { return (int)(time / SecondsInMinute); } }

        public int Year
        { get { return (int)(time / SecondsInYear); } }
        public int Month
        { get { return 1 + (TotalMonths % MonthsInYear); } }
        public int Week
        { get { return 1 + (TotalWeeks % WeeksInMonth); } }
        public int WeekDay
        { get { return 1 + (TotalDays % DaysInWeek); } }
        public int Day
        { get { return 1 + (TotalDays % DaysInMonth); } }
        public int Hour
        { get { return TotalHours % HoursInDay; } }
        public int Minute
        { get { return TotalMinutes % MinutesInHour; } }
        public int Second
        { get { return (int)time % SecondsInMinute; } }

        public double Time
        { get { return time; } }

        private double time;

        public GameTime(double time)
        {
            this.time = time;
        }

        public string ToString(string format)
        {
            switch (format)
            {
                case "f":
                    return $"day {TotalDays} {Hour:00}:{Minute:00}:{Second:00}";
                case "s":
                    if (TotalDays > 0)
                    {
                        return $"{TotalDays}d {Hour:00}:{Minute:00}:{Second:00}";
                    }
                    else
                    {
                        return $"{Hour:00}:{Minute:00}:{Second:00}";
                    }
                default:
                    return ToString();
            }
        }

        public override string ToString()
        {
            return $"{Time:0}";
        }
    }
}