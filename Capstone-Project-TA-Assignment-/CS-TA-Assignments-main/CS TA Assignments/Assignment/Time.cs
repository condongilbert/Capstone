using System;
using System.Collections.Generic;
using System.Text;

namespace cwu.cs.TaAssignments
{
    /// <summary>
    /// Represents available or required times of a student or course.
    /// </summary>
    struct Time
    {
        // The time of the first class each day (currently, 8 a.m.).
        public const int FirstClassHour = 8;

        // The number of time slots each day:
        // 8 a.m. until 5 p.m. 
        public const int SlotsPerDay = 10;

        // The number of days each week.
        // Currently, only Monday to Thursday
        public const int DaysPerWeek = 4;

        // List of day abbreviations.
        private static string[] dayAbbr = { "M", "T", "W", "TH", "F" };
        private static string[] dayStrs =
        {
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday"
        };


        // Each bit represents one time slot.
        // Least significant bit represents Monday 8am.
        private long timeSlots;

        public Time(long slots)
        {
            timeSlots = slots;
        }

        public Time(string days, string time)
        {
            timeSlots = 0;

            if (!time.Matches(@"\d?\d:00(A|P)M")) return;

            int hour = int.Parse(time.Substring(0, time.IndexOf(":")));
            if (time.EndsWith("PM") && hour < 12 /* 12pm is noon */) hour += 12;

            string[] dayLst = days.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string dayStr in dayLst)
            {
                int day = Array.IndexOf(dayAbbr, dayStr);

                if (day < 0)
                {
                    TextLog.WriteLine("wrong day format: '" + days + "'");
                    timeSlots = 0;
                    return;
                }

                int shift = day * SlotsPerDay + hour - FirstClassHour;
                timeSlots |= 1L << shift;
            }
        }

        public string GetLongDayString()
        {
            StringBuilder sb = new StringBuilder();

            for (long day = 0, slots = timeSlots; slots > 0; day++, slots >>= SlotsPerDay)
            {
                long dayTimes = (1L << SlotsPerDay) - 1;
                if ((slots & dayTimes) == 0) continue;

                string pre = sb.Length == 0 ? string.Empty : " / ";
                sb.Append(pre + dayStrs[day]);
            }

            return sb.ToString();
        }

        public string GetStartTimes()
        {
            StringBuilder sb = new StringBuilder();

            // Combine all days into one.
            long allInOne = 0;
            for (long slots = timeSlots; slots > 0; slots >>= SlotsPerDay)
            {
                long dayTimes = (1L << SlotsPerDay) - 1;
                allInOne |= slots & dayTimes;
            }

            for (int hour = FirstClassHour; allInOne > 0; hour++, allInOne >>= 1)
            {
                if ((allInOne & 1) == 0) continue;

                string pre = sb.Length == 0 ? string.Empty : " ";
                string ap = hour < 12 ? "AM" : "PM";

                int clockHour = hour <= 12 ? hour : hour - 12;
                sb.Append(pre + clockHour.ToString() + ":00" + ap);
            }

            return sb.ToString();
        }

        public string GetEndTimes()
        {
            StringBuilder sb = new StringBuilder();

            // Combine all days into one.
            long allInOne = 0;
            for (long slots = timeSlots; slots > 0; slots >>= SlotsPerDay)
            {
                long dayTimes = (1L << SlotsPerDay) - 1;
                allInOne |= slots & dayTimes;
            }

            for (int hour = FirstClassHour; allInOne > 0; hour++, allInOne >>= 1)
            {
                if ((allInOne & 1) == 0) continue;

                string pre = sb.Length == 0 ? string.Empty : " ";
                string ap = hour < 12 ? "AM" : "PM";

                int clockHour = hour <= 12 ? hour : hour - 12;
                sb.Append(pre + clockHour.ToString() + ":50" + ap);
            }

            return sb.ToString();
        }

        public static Time operator |(Time lhs, Time rhs)
        {
            return new Time(lhs.timeSlots | rhs.timeSlots);
        }

        public static bool operator <=(Time lhs, Time rhs)
        {
            return (lhs.timeSlots & rhs.timeSlots) == lhs.timeSlots;
        }

        public static bool operator >=(Time lhs, Time rhs)
        {
            return rhs <= lhs;
        }
    }
}
