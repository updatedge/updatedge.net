using System.Collections.Generic;
using System.Linq;
using Light.GuardClauses;
using System;
using Updatedge.Common;
using Updatedge.Common.Models.Availability;

namespace Updatedge.Common.Validation
{
    /// <summary>
    /// Defines useful interval validation routines in a fluent manner.
    /// </summary>
    public class IntervalValidations : BaseValidations
    {
        private readonly IEnumerable<BaseInterval> _intervals;
        private readonly string _paramName;

        /// <summary>
        /// Constructor
        /// </summary>
        public IntervalValidations(IEnumerable<BaseInterval> intervals, string paramName)
        {
            _intervals = intervals.MustNotBeNull(nameof(intervals));
            _paramName = paramName.MustNotBeNullOrEmpty(nameof(paramName));
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public IntervalValidations(BaseInterval interval) : base()
        {
            interval.MustNotBeNull(nameof(interval));
            _intervals = new List<BaseInterval>() { interval };
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="start">Start of interval</param>
        /// <param name="end">End of interval</param>
        public IntervalValidations(DateTimeOffset start, DateTimeOffset end) : base()
        {
            _intervals = new List<BaseInterval>() { new BaseInterval() { Start = start, End = end } };
        }

        /// <summary>
        /// Determines if an interval starts before it ends
        /// </summary>
        /// <returns>The error message or itself</returns>
        public IntervalValidations EndsAfterStart()
        {
            foreach (var interval in _intervals)
                if (interval.Start > interval.End)
                {
                    Add(nameof(interval.Start),
                        string.Format(Constants.ErrorMessages.XMustBeBeforeY,
                            $"({interval.Start.ToString()})",
                            $"({interval.End.ToString()})"));
                }

            return this;

        }

        /// <summary>
        /// Checks interval does not contain start/end with default utc times
        /// </summary>
        /// <returns></returns>
        public IntervalValidations StartEndSpecified()
        {
            foreach (var interval in _intervals)
            {
                if (interval.Start.UtcDateTime == new DateTimeOffset().UtcDateTime)
                {
                    Add(nameof(interval.Start), Constants.ErrorMessages.ValueNotSpecified);
                }

                if (interval.End.UtcDateTime == new DateTimeOffset().UtcDateTime)
                {
                    Add(nameof(interval.End), Constants.ErrorMessages.ValueNotSpecified);
                }
            }

            return this;

        }

        /// <summary>
        /// Determines if an interval is less than X hours
        /// </summary>
        /// <param name="hours">Maximum number of hours</param>
        /// <returns>Error message or itself</returns>
        public IntervalValidations LessThanXHours(int hours)
        {
            foreach (var interval in _intervals)
            {
                if (interval.End > interval.Start.AddHours(hours))
                {
                    Add(nameof(interval.End),
                        string.Format(Constants.ErrorMessages.XMustBeWithinYHoursOfZ,
                                    $"({interval.End.ToString()})",
                                    hours,
                                    $"({interval.Start.ToString()})"));
                }

            }
            return this;
        }

        /// <summary>
        /// Determines if an interval is less than X days
        /// </summary>
        /// <param name="days">Maximum number of days</param>
        /// <returns>Error message or itself</returns>
        public IntervalValidations LessThanXDays(int days)
        {
            foreach (var interval in _intervals)
            {
                if (interval.End > interval.Start.AddDays(days - 1))
                {
                    Add(nameof(interval.End),
                        string.Format(Constants.ErrorMessages.XMustBeWithinYDaysOfZ,
                                    $"({interval.End.ToString()})",
                                    days,
                                    $"({interval.Start.ToString()})"));
                }

            }
            return this;
        }

        /// <summary>
        /// Ensures an enumerable Interval collection contains at least one entry
        /// </summary>
        /// <returns>Error message or itself</returns>
        public IntervalValidations ContainsIntervals()
        {
            if (!_intervals.Any())
            {
                Add("intervals", Constants.ErrorMessages.NoIntervalsSpecified);
            }

            return this;
        }

        /// <summary>
        /// Ensures no intervals in a set overlap
        /// </summary>
        /// <returns></returns>
        public IntervalValidations ContainsNoOverlappingIntervals()
        {
            BaseInterval prevInterval = null;

            foreach (var interval in _intervals.OrderBy(i => i.Start))
            {
                if (prevInterval != null)
                {
                    if (interval.Start <= prevInterval.End)
                    {
                        Add("overlap", string.Format(Constants.ErrorMessages.IntervalsOverlap, nameof(interval.Start), nameof(interval.End)));
                    }
                }

                prevInterval = interval;
            }

            return this;
        }

        /// <summary>
        /// Validates an interval starts in the future
        /// </summary>
        /// <returns></returns>
        public IntervalValidations StartInFuture()
        {
            foreach (var interval in _intervals)
            {
                if (interval.Start <= DateTimeOffset.Now)
                    Add(_paramName, string.Format(Constants.ErrorMessages.MustStartInFuture, interval.Start));
            }

            return this;
        }
    }
}
