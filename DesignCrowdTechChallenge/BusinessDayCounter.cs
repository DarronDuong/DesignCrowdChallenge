using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignCrowdTechChallenge
{
    public class BusinessDayCounter
    {
        #region Task 1

        /// <summary>
        /// Calculate number of weekdays between 2 specific dates
        /// STEP 1: Get total days between 2 dates (incl. 2nd date by default)
        /// STEP 2: Get number of weekend day from the full week(s) (not include if outside the full week(s))
        /// STEP 3: Check if 1st date & 2nd date are the same day of week
        /// ==== if yes => go to STEP 4,
        /// ==== if no, calculate weekends in the following 3 scenarios
        /// ==== 3.1: scenario 1 - 1st day is weekday
        /// ==== 3.2: scenario 2 - 1st day is Saturday
        /// ==== 3.3: scenario 3 - 1st day is Sunday
        /// STEP 4: Subtract weekends, subtract 2nd date from total days if 2nd date is weekday
        /// </summary>
        /// <param name="firstDate"></param>
        /// <param name="secondDate"></param>
        /// <returns>Number of weekdays</returns>
        public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            var totalWeekDays = 0;

            if (!IsValidDate(firstDate) || !IsValidDate(secondDate)) return 0;

            if (firstDate >= secondDate) return 0;

            //STEP 1:
            var timeSpan = secondDate - firstDate;

            var totalDays = timeSpan.Days;

            //STEP 2:
            var dayOfWeekends = totalDays / 7 * 2;

            //STEP 3:
            if (firstDate.DayOfWeek != secondDate.DayOfWeek)
            {
                var firstDayOfWeek = firstDate.DayOfWeek == DayOfWeek.Sunday ? (byte) 7 : (byte) firstDate.DayOfWeek;
                var lastDayOfWeek = secondDate.DayOfWeek == DayOfWeek.Sunday ? (byte) 7 : (byte) secondDate.DayOfWeek;

                if (lastDayOfWeek < firstDayOfWeek)
                    lastDayOfWeek += 7; // to ensure lastDayOfWeek always > firstDayOfWeek for comparison purpose

                if (firstDayOfWeek < 6) // 3.1: 1st day = weekday
                {
                    if (lastDayOfWeek == 6) // 2nd day = Sat
                        dayOfWeekends += 1; // incl Sat

                    else if (lastDayOfWeek >= 7) // 2nd day = Sun or after
                        dayOfWeekends += 2; // incl Sat & Sun
                }
                else if (firstDayOfWeek == 6) //3.2: 1st day = Sat
                {
                    if (lastDayOfWeek >= 7) // 2nd day = Sun or after
                        dayOfWeekends += 1; //incl Sun
                }
                else if (firstDayOfWeek == 7) //3.3: 1st day = Sun
                {
                    if (lastDayOfWeek == 13) // 2nd day = Sat (not 6 because 6 < 7, so 6 was added 7 = 13)
                        dayOfWeekends += 1; //incl Sat
                }
            }

            //STEP 4:
            if (secondDate.DayOfWeek != DayOfWeek.Sunday && secondDate.DayOfWeek != DayOfWeek.Saturday)
                totalWeekDays = totalDays - dayOfWeekends - 1; // exclude weekends and the 2nd date.
            else
                totalWeekDays = totalDays - dayOfWeekends; // exclude weekends only because 2nd date is weekend

            return totalWeekDays;
        }

        #endregion

        #region Task 2

        /// <summary>
        /// Calculate number of business days between 2 specific dates with pre-defined list of holidays
        /// </summary>
        /// <param name="firstDate">Date</param>
        /// <param name="secondDate">Date</param>
        /// <param name="publicHolidays">List of Date</param>
        /// <returns>Number of business days</returns>
        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
        {
            if (firstDate >= secondDate) return 0;

            var businessDays = WeekdaysBetweenTwoDates(firstDate, secondDate);

            if (businessDays <= 0) return 0;

            foreach (var holiday in publicHolidays)
            {
                if (holiday > firstDate && holiday < secondDate)
                    businessDays--;
            }

            return businessDays;
        }

        #endregion

        #region Task 3

        /// <summary>
        /// Calculate number of business days between 2 specific dates
        /// </summary>
        /// <param name="firstDate"></param>
        /// <param name="secondDate"></param>
        /// <returns></returns>
        public int BusinessDaysBetweenTwoDatesExtended(DateTime firstDate, DateTime secondDate)
        {
            if (firstDate >= secondDate) return 0;

            var businessDays = WeekdaysBetweenTwoDates(firstDate, secondDate);

            if (businessDays <= 0) return 0;

            var holidays = new List<DateTime>();
            if (firstDate.Year == secondDate.Year)
            {
                var rules = new HolidayRules(firstDate.Year);
                holidays = rules.Holidays;
            }
            else
            {
                for (var i = firstDate.Year; i <= secondDate.Year; i++)
                {
                    var tempRules = new HolidayRules(i);
                    holidays = holidays.Concat(tempRules.Holidays).ToList();
                }
            }

            holidays.ForEach(holiday =>
            {
                if (holiday > firstDate && holiday < secondDate)
                    businessDays--;
            });

            return businessDays;
        }

        #endregion

        /// <summary>
        /// Validate if input is a valid date
        /// </summary>
        /// <param name="date"></param>
        /// <returns>A boolean value</returns>
        public bool IsValidDate(DateTime date)
        {
            try
            {
                DateTime temp;
                return DateTime.TryParse(date.ToString(), out temp);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public class HolidayRules
    {
        // Assumption: public holidays in NSW (Aus)
        // Not take in account Easters

        private readonly int _year;

        public HolidayRules(int year)
        {
            _year = year;
        }

        public DateTime NewYearDay
        {
            get { return AdjustWeekendHoliday(new DateTime(_year, 1, 1)); }
        }

        public DateTime AustralianDay
        {
            get { return AdjustWeekendHoliday(new DateTime(_year, 1, 26)); }
        }

        public DateTime AnzacDay
        {
            get { return AdjustWeekendHoliday(new DateTime(_year, 4, 25)); }
        }

        public DateTime QueensBirthday
        {
            get
            {   
                // Queen's Birthday - 2nd Monday of June
                var queensBirthday = new DateTime(_year, 6, 1);
                byte mondayIndex = 0;

                for (var i = 1; i <= 14; i++)
                {
                    var tempDate = new DateTime(_year, 6, i);

                    if (tempDate.DayOfWeek != DayOfWeek.Monday) continue;

                    mondayIndex++;

                    if (mondayIndex != 2) continue;

                    queensBirthday = tempDate;

                    break;
                }

                return queensBirthday;
            }
        }

        public DateTime LaborDay
        {
            get
            {
                //Labor Day - 1st Monday of October
                var laborDay = new DateTime(_year, 10, 1);
                var tempDayOfWeek = laborDay.DayOfWeek;
                while (tempDayOfWeek != DayOfWeek.Monday)
                {
                    laborDay = laborDay.AddDays(1);
                    tempDayOfWeek = laborDay.DayOfWeek;
                }

                return laborDay;
            }
        }

        public DateTime ChristmasDay
        {
            get { return AdjustWeekendHoliday(new DateTime(_year, 12, 25)); }
        }

        public DateTime BoxingDay
        {
            get { return AdjustWeekendHoliday(new DateTime(_year, 12, 26)); }
        }

        public List<DateTime> Holidays
        {
            get
            {
                return new List<DateTime>()
                {
                    NewYearDay, AustralianDay, AnzacDay, QueensBirthday, LaborDay, ChristmasDay, BoxingDay
                };
            }
        }

        private static DateTime AdjustWeekendHoliday(DateTime holiday)
        {
            switch (holiday.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    return holiday.AddDays(2);
                case DayOfWeek.Sunday:
                    return holiday.AddDays(1);
                default:
                    return holiday;
            }
        }
    }
}