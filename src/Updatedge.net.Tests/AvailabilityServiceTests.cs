using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Updatedge.net.Entities.V1;
using Updatedge.net.Entities.V1.Availability;
using Updatedge.net.Services.V1;
using Flurl.Http.Testing;
using Updatedge.net.Exceptions;

namespace Updatedge.net.Tests
{
    public class AvailabilityServiceTests
    {
        private HttpTest _httpTest;
        private AvailabilityService _availabilityService;
        private OkApiResult<List<WorkerAvailabilityIntervals>> _expectedOkResult;
        private List<string> _userIdList;
        
        [SetUp]
        public void Setup()
        {
            // Put Flurl into test mode
            _httpTest = new HttpTest();

            // instantiate the Availabiity service
            _availabilityService = new AvailabilityService("https://localhost/", "1234567890");

            _userIdList = new List<string> { "UserId1", "UserId2" };
            // create our expected result
            var workerAvailabilityList = new List<WorkerAvailabilityIntervals>
            {
                new WorkerAvailabilityIntervals(),                
                new WorkerAvailabilityIntervals()                
            };

            _expectedOkResult = new OkApiResult<List<WorkerAvailabilityIntervals>>
            {
                Data = workerAvailabilityList
            };
        }

        [Test]
        public async Task GetAvailabilityPerDailyInterval_OkResult()
        {
            // Arrange
            _httpTest.RespondWithJson(_expectedOkResult);
            
            var result = await _availabilityService.GetAvailabilityDailyAsync(
                    DateTime.Now, 
                    DateTime.Now.AddHours(23).AddMinutes(59).AddSeconds(59), 
                    1, 
                    _userIdList);

            // Assert
            Assert.AreEqual(_expectedOkResult.Data.Count, result.Data.Count);            
        }

        [Test]
        public void GetAvailabilityPerDailyInterval_401Result()
        {
            // Arrange
            _httpTest.RespondWithJson(new { message = "Unauthorized" }, 401);

            // Assert
            Assert.ThrowsAsync<UnauthorizedApiRequestException>(() => 
                    _availabilityService.GetAvailabilityDailyAsync(
                        DateTime.Now, 
                        DateTime.Now.AddHours(23).AddMinutes(59).AddSeconds(59), 
                        1, 
                       _userIdList));            
        }

        [Test]
        public void GetAvailabilityPerDailyInterval_400Result()
        {
            // Arrange
            _httpTest.RespondWithJson(new { message = "Bad Request" }, 400);
            
            // Assert
            Assert.ThrowsAsync<InvalidApiRequestException>(() => 
                    _availabilityService.GetAvailabilityDailyAsync(
                        DateTime.Now,
                        DateTime.Now.AddHours(23).AddMinutes(59).AddSeconds(59),
                        1, 
                        _userIdList));            
        }

        [Test]
        public async Task GetTotalAvailability_OkResult()
        {
            // Arrange
            _httpTest.RespondWithJson(_expectedOkResult);

            var result = await _availabilityService.GetTotalAvailability(new TotalAvailabilityRequest());

            // Assert
            Assert.AreEqual(_expectedOkResult.Data.Count, result.Data.Count);
        }

        [TearDown]
        public void DisposeHttpTest()
        {
            _httpTest.Dispose();
        }
    }
}