using DesignCrowdTechChallenge;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TaskOneTests
{
    [TestClass]
    public class BusinessDayCounterTests
    {
        private readonly List<DateTime> _publicHolidays = new List<DateTime>
        {
            new DateTime(2013, 12, 25),
            new DateTime(2013, 12, 26),
            new DateTime(2014, 1, 1)
        };

        #region Task 1 Tests

        #region Scenarios follow challenge paper

        [TestMethod]
        public void CalculateWeekDays_When7Oct2013To9Oct2013__ThenReturn1()
        {
            //Arrange
            var counter = new BusinessDayCounter();

            var firstDate = new DateTime(2013, 10, 7);
            var secondDate = new DateTime(2013, 10, 9);
            var expected = 1;

            //Act
            var actual = counter.WeekdaysBetweenTwoDates(firstDate, secondDate);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateWeekDays_When5Oct2013To14Oct2013__ThenReturn5()
        {
            //Arrange
            var counter = new BusinessDayCounter();

            var firstDate = new DateTime(2013, 10, 5);
            var secondDate = new DateTime(2013, 10, 14);
            var expected = 5;

            //Act
            var actual = counter.WeekdaysBetweenTwoDates(firstDate, secondDate);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateWeekDays_When7Oct2013To1Jan2014__ThenReturn61()
        {
            //Arrange
            var counter = new BusinessDayCounter();

            var firstDate = new DateTime(2013, 10, 7);
            var secondDate = new DateTime(2014, 1, 1);
            var expected = 61;

            //Act
            var actual = counter.WeekdaysBetweenTwoDates(firstDate, secondDate);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateWeekDays_When7Oct2013To5Oct2013__ThenReturn0()
        {
            //Arrange
            var counter = new BusinessDayCounter();

            var firstDate = new DateTime(2013, 10, 7);
            var secondDate = new DateTime(2013, 10, 5);
            var expected = 0;

            //Act
            var actual = counter.WeekdaysBetweenTwoDates(firstDate, secondDate);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region 1st date is weekday 

        [TestMethod]
        public void CalculateWeekDays_When5Nov2018To24Nov2018__ThenReturn14()
        {
            //Arrange
            var counter = new BusinessDayCounter();

            var firstDate = new DateTime(2018, 11, 5); //Mon
            var secondDate = new DateTime(2018, 11, 24); //Sat
            var expected = 14;

            //Act
            var actual = counter.WeekdaysBetweenTwoDates(firstDate, secondDate);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateWeekDays_When9Nov2018To24Nov2018__ThenReturn10()
        {
            //Arrange
            var counter = new BusinessDayCounter();

            var firstDate = new DateTime(2018, 11, 9); //Fri
            var secondDate = new DateTime(2018, 11, 24); //Sat
            var expected = 10;

            //Act
            var actual = counter.WeekdaysBetweenTwoDates(firstDate, secondDate);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateWeekDays_When6Nov2018To24Nov2018__ThenReturn13()
        {
            //Arrange
            var counter = new BusinessDayCounter();

            var firstDate = new DateTime(2018, 11, 6); //Tue
            var secondDate = new DateTime(2018, 11, 24); //Sat
            var expected = 13;

            //Act
            var actual = counter.WeekdaysBetweenTwoDates(firstDate, secondDate);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateWeekDays_When5Nov2018To25Nov2018__ThenReturn14()
        {
            //Arrange
            var counter = new BusinessDayCounter();

            var firstDate = new DateTime(2018, 11, 5); //Mon
            var secondDate = new DateTime(2018, 11, 25); //Sun
            var expected = 14;

            //Act
            var actual = counter.WeekdaysBetweenTwoDates(firstDate, secondDate);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateWeekDays_When9Nov2018To25Nov2018__ThenReturn10()
        {
            //Arrange
            var counter = new BusinessDayCounter();

            var firstDate = new DateTime(2018, 11, 9); //Fri
            var secondDate = new DateTime(2018, 11, 25); //Sun
            var expected = 10;

            //Act
            var actual = counter.WeekdaysBetweenTwoDates(firstDate, secondDate);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateWeekDays_When6Nov2018To25Nov2018__ThenReturn13()
        {
            //Arrange
            var counter = new BusinessDayCounter();

            var firstDate = new DateTime(2018, 11, 6); //Tue
            var secondDate = new DateTime(2018, 11, 24); //Sun
            var expected = 13;

            //Act
            var actual = counter.WeekdaysBetweenTwoDates(firstDate, secondDate);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateWeekDays_When6Nov2018To13Nov2018__ThenReturn4()
        {
            //Arrange
            var counter = new BusinessDayCounter();

            var firstDate = new DateTime(2018, 11, 6); //Tue
            var secondDate = new DateTime(2018, 11, 13); //Tue
            var expected = 4;

            //Act
            var actual = counter.WeekdaysBetweenTwoDates(firstDate, secondDate);

            //Assert
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void CalculateWeekDays_When1Nov2018To15Nov2018__ThenReturn9()
        {
            //Arrange
            var counter = new BusinessDayCounter();

            var firstDate = new DateTime(2018, 11, 1); //Thu
            var secondDate = new DateTime(2018, 11, 15); //Thu
            var expected = 9;

            //Act
            var actual = counter.WeekdaysBetweenTwoDates(firstDate, secondDate);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region 1st date is Saturday

        [TestMethod]
        public void CalculateWeekDays_When3Nov2018To12Nov2013__ThenReturn5()
        {
            //Arrange
            var counter = new BusinessDayCounter();

            var firstDate = new DateTime(2018, 11, 3); //Sat
            var secondDate = new DateTime(2018, 11, 12); //Mon
            var expected = 5;

            //Act
            var actual = counter.WeekdaysBetweenTwoDates(firstDate, secondDate);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateWeekDays_When3Nov2018To13Nov2013__ThenReturn6()
        {
            //Arrange
            var counter = new BusinessDayCounter();

            var firstDate = new DateTime(2018, 11, 3); //Sat
            var secondDate = new DateTime(2018, 11, 13); //Tue
            var expected = 6;

            //Act
            var actual = counter.WeekdaysBetweenTwoDates(firstDate, secondDate);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateWeekDays_When10Nov2018To23Nov2018__ThenReturn9()
        {
            //Arrange
            var counter = new BusinessDayCounter();

            var firstDate = new DateTime(2018, 11, 10); //Sat
            var secondDate = new DateTime(2018, 11, 23); //Fri
            var expected = 9;

            //Act
            var actual = counter.WeekdaysBetweenTwoDates(firstDate, secondDate);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateWeekDays_When3Nov2018To17Nov2018__ThenReturn10()
        {
            //Arrange
            var counter = new BusinessDayCounter();

            var firstDate = new DateTime(2018, 11, 3); //Sat
            var secondDate = new DateTime(2018, 11, 17); //Sat
            var expected = 10;

            //Act
            var actual = counter.WeekdaysBetweenTwoDates(firstDate, secondDate);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateWeekDays_When3Nov2018To18Nov2018__ThenReturn10()
        {
            //Arrange
            var counter = new BusinessDayCounter();

            var firstDate = new DateTime(2018, 11, 3); //Sat
            var secondDate = new DateTime(2018, 11, 18); //Sun
            var expected = 10;

            //Act
            var actual = counter.WeekdaysBetweenTwoDates(firstDate, secondDate);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region 1st date is Sunday

        [TestMethod]
        public void CalculateWeekDays_When4Nov2018To19Nov2018__ThenReturn10()
        {
            //Arrange
            var counter = new BusinessDayCounter();

            var firstDate = new DateTime(2018, 11, 4); //Sun
            var secondDate = new DateTime(2018, 11, 19); //Mon
            var expected = 10;

            //Act
            var actual = counter.WeekdaysBetweenTwoDates(firstDate, secondDate);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateWeekDays_When4Nov2018To20Nov2018__ThenReturn11()
        {
            //Arrange
            var counter = new BusinessDayCounter();

            var firstDate = new DateTime(2018, 11, 4); //Sun
            var secondDate = new DateTime(2018, 11, 20); //Tue
            var expected = 11;

            //Act
            var actual = counter.WeekdaysBetweenTwoDates(firstDate, secondDate);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateWeekDays_When4Nov2018To23Nov2018__ThenReturn14()
        {
            //Arrange
            var counter = new BusinessDayCounter();

            var firstDate = new DateTime(2018, 11, 4); //Sun
            var secondDate = new DateTime(2018, 11, 23); //Fri
            var expected = 14;

            //Act
            var actual = counter.WeekdaysBetweenTwoDates(firstDate, secondDate);

            //Assert
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void CalculateWeekDays_When4Nov2018To24Nov2018__ThenReturn15()
        {
            //Arrange
            var counter = new BusinessDayCounter();

            var firstDate = new DateTime(2018, 11, 4); //Sun
            var secondDate = new DateTime(2018, 11, 24); //Sat
            var expected = 15;

            //Act
            var actual = counter.WeekdaysBetweenTwoDates(firstDate, secondDate);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateWeekDays_When4Nov2018To18Nov2018__ThenReturn10()
        {
            //Arrange
            var counter = new BusinessDayCounter();

            var firstDate = new DateTime(2018, 11, 4); //Sun
            var secondDate = new DateTime(2018, 11, 18); //Sun
            var expected = 10;

            //Act
            var actual = counter.WeekdaysBetweenTwoDates(firstDate, secondDate);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #endregion

        #region Task 2 Tests

        [TestMethod]
        public void CalculateBusinessDays_When7Oct2013To9Oct2013__ThenReturn1()
        {
            //Arrange
            var counter = new BusinessDayCounter();

            var firstDate = new DateTime(2013, 10, 7);
            var secondDate = new DateTime(2013, 10, 9);
            var expected = 1;

            //Act
            var actual = counter.BusinessDaysBetweenTwoDates(firstDate, secondDate, _publicHolidays);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateBusinessDays_When24Dec2013To27Dec2013__ThenReturn0()
        {
            //Arrange
            var counter = new BusinessDayCounter();

            var firstDate = new DateTime(2013, 12, 24);
            var secondDate = new DateTime(2013, 12, 27);
            var expected = 0;

            //Act
            var actual = counter.BusinessDaysBetweenTwoDates(firstDate, secondDate, _publicHolidays);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateBusinessDays_When7Oct2013To1Jan2014__ThenReturn59()
        {
            //Arrange
            var counter = new BusinessDayCounter();

            var firstDate = new DateTime(2013, 10, 7);
            var secondDate = new DateTime(2014, 1, 1);
            var expected = 59;

            //Act
            var actual = counter.BusinessDaysBetweenTwoDates(firstDate, secondDate, _publicHolidays);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Task 3 Tests
        [TestMethod]
        public void CalculateBusinessDaysExtended_When7Oct2013To9Oct2013__ThenReturn1()
        {
            //Arrange
            var counter = new BusinessDayCounter();

            var firstDate = new DateTime(2013, 10, 7);
            var secondDate = new DateTime(2013, 10, 9);
            var expected = 1;

            //Act
            var actual = counter.BusinessDaysBetweenTwoDatesExtended(firstDate, secondDate);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateBusinessDaysExtended_When24Dec2013To27Dec2013__ThenReturn0()
        {
            //Arrange
            var counter = new BusinessDayCounter();

            var firstDate = new DateTime(2013, 12, 24);
            var secondDate = new DateTime(2013, 12, 27);
            var expected = 0;

            //Act
            var actual = counter.BusinessDaysBetweenTwoDatesExtended(firstDate, secondDate);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateBusinessDaysExtended_When7Oct2013To1Jan2014__ThenReturn59()
        {
            //Arrange
            var counter = new BusinessDayCounter();

            var firstDate = new DateTime(2013, 10, 7);
            var secondDate = new DateTime(2014, 1, 1);
            var expected = 59;

            //Act
            var actual = counter.BusinessDaysBetweenTwoDatesExtended(firstDate, secondDate);

            //Assert
            Assert.AreEqual(expected, actual);
        }
        #endregion
    }
}