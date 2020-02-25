using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Updatedge.net.Entities.V1;
using Updatedge.net.Entities.V1.Availability;
using Updatedge.net.Services.V1;
using Flurl.Http.Testing;

namespace Updatedge.net.Tests
{
    public class AvailabilityServiceTests
    {
        private HttpTest _httpTest;
        private AvailabilityService _availabilityService;
        private OkApiResult<List<WorkerAvailabilityIntervals>> _expected;

        [SetUp]
        public void Setup()
        {
            // Put Flurl into test mode
            _httpTest = new HttpTest();

            // instantiate the Availabiity service
            _availabilityService = new AvailabilityService("https://localhost/", "1234567890");

            // create our expected result
            var workerAvailabilityList = new List<WorkerAvailabilityIntervals>
            {
                new WorkerAvailabilityIntervals
                {
                    AvailableNowIntervals = new List<Interval>
                    {
                        new Interval { Start = DateTime.Now, End = DateTime.Now.AddHours(3), IntervalMinutes = 190 },
                        new Interval { Start = DateTime.Now.AddHours(3), End = DateTime.Now.AddHours(5), IntervalMinutes = 120 }
                    },
                    UnavailableIntervals = new List<Interval>
                    {
                        new Interval { Start = DateTime.Now.AddDays(1), End = DateTime.Now.AddDays(3), IntervalMinutes = 2880 },
                        new Interval { Start = DateTime.Now.AddDays(3), End = DateTime.Now.AddDays(5), IntervalMinutes = 2880 }
                    },
                    WorkerId = "UserId1"
                },
                new WorkerAvailabilityIntervals
                {
                    AvailableNowIntervals = new List<Interval>
                    {
                        new Interval { Start = DateTime.Now, End = DateTime.Now.AddHours(3), IntervalMinutes = 190 },
                        new Interval { Start = DateTime.Now.AddHours(3), End = DateTime.Now.AddHours(5), IntervalMinutes = 120 }
                    },
                    UnavailableIntervals = new List<Interval>
                    {
                        new Interval { Start = DateTime.Now.AddDays(1), End = DateTime.Now.AddDays(3), IntervalMinutes = 2880 },
                        new Interval { Start = DateTime.Now.AddDays(3), End = DateTime.Now.AddDays(5), IntervalMinutes = 2880 }
                    },
                    WorkerId = "UserId1"
                }
            };

            var expected = new OkApiResult<List<WorkerAvailabilityIntervals>>
            {
                Data = workerAvailabilityList
            };
        }

        [Test]
        public async Task GetAvailabilityPerDailyInterval_OkResult()
        {
            // Arrange
            _httpTest.RespondWithJson(_expected);
            
            var result = await _availabilityService.GetAvailabilityDailyAsync(DateTime.Now, DateTime.Now.AddDays(5), 1, new List<string> { "UserId1", "UserId2" });

            // Assert
            Assert.AreEqual(_expected.Data.Count, result.Data.Count);            
        }

        [TearDown]
        public void DisposeHttpTest()
        {
            _httpTest.Dispose();
        }
    }
}