﻿using Darecali.Strategy;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darecali.Tests.Strategy
{
    [Category("Strategy")]
    public class EveryNthWeekOnSpecifiedDaysStrategyFixture
    {
        const int weekDayFlags = (int)DayOfWeekFlags.WeekDays;
        const int weekendDayFlags = (int)DayOfWeekFlags.WeekendDays;
        const int ThursdayFridaySaturdayFlags = (int)(DayOfWeekFlags.Thursday | DayOfWeekFlags.Friday | DayOfWeekFlags.Saturday);

        #region Out of range parameter tests

        [Test]
        public void ShouldThrowWhenDaysOfWeekLessThan1Test()
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<ArgumentException>(() =>
            {
                var sut = new EveryNthWeekOnSpecifiedDaysStrategy(0);
            });
        }

        [Test]
        public void ShouldThrowWhenDaysOfWeekGreaterThan127Test()
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<ArgumentException>(() =>
            {
                var sut = new EveryNthWeekOnSpecifiedDaysStrategy((DayOfWeekFlags)128);
            });
        }

        #endregion

        #region Nth week tests

        [Test]
        public void EveryOneWeekTest()
        {
            var sut = Factory.CreateController(DateTime.Today, "W1")
                .Take(21).ToList();
            for (int i = 0; i < 21; i++)
                sut[i].ShouldBe(DateTime.Today.AddDays(i), "should be today + " + i);
        }

        [Test]
        public void EveryTwoWeeksTest()
        {
            DateTime startDate = new DateTime(2016, 02, 22);
            var sut = Factory.CreateController(startDate, "W2")
                .Take(21).ToList();
            for (int i = 0; i < 7; i++)
                sut[i].ShouldBe(startDate.AddDays(i), "should be startDate + " + i);
            for (int i = 0; i < 7; i++)
                sut[7 + i].ShouldBe(startDate.AddDays(14 + i), "should be startDate + 2 weeks + " + i);
            for (int i = 0; i < 7; i++)
                sut[14 + i].ShouldBe(startDate.AddDays(28 + i), "should be startDate + 4 weeks + " + i);
        }

        [Test]
        public void EveryThreeWeeksTest()
        {
            DateTime startDate = new DateTime(2016, 02, 22);
            var sut = Factory.CreateController(startDate, "W3")
                .Take(21).ToList();
            for (int i = 0; i < 7; i++)
                sut[i].ShouldBe(startDate.AddDays(i), "should be startDate + " + i);
            for (int i = 0; i < 7; i++)
                sut[7 + i].ShouldBe(startDate.AddDays(21 + i), "should be startDate + 3 weeks + " + i);
            for (int i = 0; i < 7; i++)
                sut[14 + i].ShouldBe(startDate.AddDays(42 + i), "should be startDate + 6 weeks + " + i);
        }

        #endregion

        #region Weekday tests

        [Test]
        public void WeekdaysEveryWeekTest()
        {
            #region Expected Results

            List<DateTime> expectedResults = new List<DateTime>()
            {
                new DateTime(2016, 02, 22),
                new DateTime(2016, 02, 23),
                new DateTime(2016, 02, 24),
                new DateTime(2016, 02, 25),
                new DateTime(2016, 02, 26),

                new DateTime(2016, 02, 29),
                new DateTime(2016, 03, 01),
                new DateTime(2016, 03, 02),
                new DateTime(2016, 03, 03),
                new DateTime(2016, 03, 04),

                new DateTime(2016, 03, 07),
                new DateTime(2016, 03, 08),
                new DateTime(2016, 03, 09),
                new DateTime(2016, 03, 10),
                new DateTime(2016, 03, 11),
            };

            #endregion;

            DateTime startDate = new DateTime(2016, 02, 22);  //Monday
            var sut = Factory.CreateController(startDate, "W" + weekDayFlags + ",1")
                .Take(expectedResults.Count).ToList();

            for (int i = 0; i < expectedResults.Count; i++)
                sut[i].ShouldBe(expectedResults[i], string.Format("at {0}, should be {1}", i, expectedResults[i].ToString("D")));
        }

        [Test]
        public void WeekdaysEveryTwoWeeksTest()
        {
            #region Expected Results

            List<DateTime> expectedResults = new List<DateTime>()
            {
                new DateTime(2016, 02, 22),
                new DateTime(2016, 02, 23),
                new DateTime(2016, 02, 24),
                new DateTime(2016, 02, 25),
                new DateTime(2016, 02, 26),

                new DateTime(2016, 03, 07),
                new DateTime(2016, 03, 08),
                new DateTime(2016, 03, 09),
                new DateTime(2016, 03, 10),
                new DateTime(2016, 03, 11),

                new DateTime(2016, 03, 21),
                new DateTime(2016, 03, 22),
                new DateTime(2016, 03, 23),
                new DateTime(2016, 03, 24),
                new DateTime(2016, 03, 25),
            };

            #endregion;

            DateTime startDate = new DateTime(2016, 02, 22);  //Monday
            var sut = Factory.CreateController(startDate, "W" + weekDayFlags + ",2")
                .Take(expectedResults.Count).ToList();

            for (int i = 0; i < expectedResults.Count; i++)
                sut[i].ShouldBe(expectedResults[i], string.Format("at {0}, should be {1}", i, expectedResults[i].ToString("D")));
        }

        [Test]
        public void WeekdaysEveryThreeWeeksTest()
        {
            #region Expected Results

            List<DateTime> expectedResults = new List<DateTime>()
            {
                new DateTime(2016, 02, 22),
                new DateTime(2016, 02, 23),
                new DateTime(2016, 02, 24),
                new DateTime(2016, 02, 25),
                new DateTime(2016, 02, 26),

                new DateTime(2016, 03, 14),
                new DateTime(2016, 03, 15),
                new DateTime(2016, 03, 16),
                new DateTime(2016, 03, 17),
                new DateTime(2016, 03, 18),

                new DateTime(2016, 04, 04),
                new DateTime(2016, 04, 05),
                new DateTime(2016, 04, 06),
                new DateTime(2016, 04, 07),
                new DateTime(2016, 04, 08),
            };

            #endregion;

            DateTime startDate = new DateTime(2016, 02, 22);  //Monday
            var sut = Factory.CreateController(startDate, "W" + weekDayFlags + ",3")
                .Take(expectedResults.Count).ToList();

            for (int i = 0; i < expectedResults.Count; i++)
                sut[i].ShouldBe(expectedResults[i], string.Format("at {0}, should be {1}", i, expectedResults[i].ToString("D")));
        }

        #endregion

        #region Weekend day tests

        [Test]
        public void WeekendDaysEveryWeekTest()
        {
            #region Expected Results

            List<DateTime> expectedResults = new List<DateTime>()
            {
                new DateTime(2016, 02, 27),
                new DateTime(2016, 02, 28),

                new DateTime(2016, 03, 05),
                new DateTime(2016, 03, 06),

                new DateTime(2016, 03, 12),
                new DateTime(2016, 03, 13),
            };

            #endregion;

            DateTime startDate = new DateTime(2016, 02, 22);  //Monday
            var sut = Factory.CreateController(startDate, "W" + weekendDayFlags + ",1")
                .Take(expectedResults.Count).ToList();

            for (int i = 0; i < expectedResults.Count; i++)
                sut[i].ShouldBe(expectedResults[i], string.Format("at {0}, should be {1}", i, expectedResults[i].ToString("D")));
        }

        [Test]
        public void WeekendDaysEveryTwoWeeksTest()
        {
            #region Expected Results

            List<DateTime> expectedResults = new List<DateTime>()
            {
                new DateTime(2016, 02, 22),
                new DateTime(2016, 02, 23),
                new DateTime(2016, 02, 24),
                new DateTime(2016, 02, 25),
                new DateTime(2016, 02, 26),

                new DateTime(2016, 03, 07),
                new DateTime(2016, 03, 08),
                new DateTime(2016, 03, 09),
                new DateTime(2016, 03, 10),
                new DateTime(2016, 03, 11),

                new DateTime(2016, 03, 21),
                new DateTime(2016, 03, 22),
                new DateTime(2016, 03, 23),
                new DateTime(2016, 03, 24),
                new DateTime(2016, 03, 25),
            };

            #endregion;

            DateTime startDate = new DateTime(2016, 02, 22);  //Monday
            var sut = Factory.CreateController(startDate, "W" + weekDayFlags + ",2")
                .Take(expectedResults.Count).ToList();

            for (int i = 0; i < expectedResults.Count; i++)
                sut[i].ShouldBe(expectedResults[i], string.Format("at {0}, should be {1}", i, expectedResults[i].ToString("D")));
        }

        #endregion

        #region Non-matching start date tests

        [Test]
        public void EveryThursdayFridaySaturdayStartingTuesdayEveryWeekTest()
        {
            #region Expected Results

            List<DateTime> expectedResults = new List<DateTime>()
            {
                new DateTime(2016, 02, 25),
                new DateTime(2016, 02, 26),
                new DateTime(2016, 02, 27),

                new DateTime(2016, 03, 03),
                new DateTime(2016, 03, 04),
                new DateTime(2016, 03, 05),

                new DateTime(2016, 03, 10),
                new DateTime(2016, 03, 11),
                new DateTime(2016, 03, 12),
            };

            #endregion;

            DateTime startDate = new DateTime(2016, 02, 23);  //Tuesday
            var sut = Factory.CreateController(startDate, "W" + ThursdayFridaySaturdayFlags + ",1")
                .Take(expectedResults.Count).ToList();

            for (int i = 0; i < expectedResults.Count; i++)
                sut[i].ShouldBe(expectedResults[i], string.Format("at {0}, should be {1}", i, expectedResults[i].ToString("D")));
        }

        [Test]
        public void EveryThursdayFridaySaturdayStartingTuesdayEveryThirdWeekTest()
        {
            #region Expected Results

            List<DateTime> expectedResults = new List<DateTime>()
            {
                new DateTime(2016, 02, 25),
                new DateTime(2016, 02, 26),
                new DateTime(2016, 02, 27),

                new DateTime(2016, 03, 17),
                new DateTime(2016, 03, 18),
                new DateTime(2016, 03, 19),

                new DateTime(2016, 04, 07),
                new DateTime(2016, 04, 08),
                new DateTime(2016, 04, 09),
            };

            #endregion;

            DateTime startDate = new DateTime(2016, 02, 21);  //Sunday
            var sut = Factory.CreateController(startDate, "W" + ThursdayFridaySaturdayFlags + ",3")
                .Take(expectedResults.Count).ToList();

            for (int i = 0; i < expectedResults.Count; i++)
                sut[i].ShouldBe(expectedResults[i], string.Format("at {0}, should be {1}", i, expectedResults[i].ToString("D")));
        }

        #endregion
    }
}
