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

            // instantiate the Availability service
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


        #region GetAvailabilityPerDailyInterval tests
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
            _httpTest.RespondWith("Unauthorized", 401);

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
            _httpTest.RespondWith("Bad Request", 400);

            // Arrange           
            var start = DateTime.Now;
            var end = DateTime.Now.AddHours(23).AddMinutes(59).AddSeconds(59);

            // Assert
            Assert.ThrowsAsync<InvalidApiRequestException>(() => 
                    _availabilityService.GetAvailabilityDailyAsync(
                        start,
                        end,
                        1, 
                        _userIdList));            
        }

        [Test]
        public void GetAvailabilityPerDailyInterval_403Result()
        {
            // Arrange
            _httpTest.RespondWith("No authorised workers", 403);

            // Arrange           
            var start = DateTime.Now;
            var end = DateTime.Now.AddHours(23).AddMinutes(59).AddSeconds(59);

            // Assert
            Assert.ThrowsAsync<ApiException>(() =>
                    _availabilityService.GetAvailabilityDailyAsync(
                        start,
                        end,
                        1,
                        _userIdList));
        }

        [Test]
        public void GetAvailabilityPerDailyInterval_500Result()
        {
            // Arrange
            _httpTest.RespondWith("Internal Server Error", 500);

            // Arrange           
            var start = DateTime.Now;
            var end = DateTime.Now.AddHours(23).AddMinutes(59).AddSeconds(59);

            // Assert
            Assert.ThrowsAsync<ApiException>(() =>
                    _availabilityService.GetAvailabilityDailyAsync(
                        start,
                        end,
                        1,
                        _userIdList));
        }

        [Test]
        public void GetAvailabilityPerDailyInterval_XMustBeBeforeY()
        {
            // Arrange           
            var start = DateTime.Now;
            var end = DateTime.Now.AddSeconds(-1);
                       
            // Assert            
            var result = Assert.ThrowsAsync<ApiWrapperException>(() =>
                    _availabilityService.GetAvailabilityDailyAsync(
                        start,
                        end,
                        1,
                       _userIdList));

            Assert.AreEqual(result.Message.ToString(), "Start date cannot be after end date");
        }
        
        [Test]
        public void GetAvailabilityPerDailyInterval_XMustBeWithinYHoursOfZ()
        {
            // Arrange           
            var start = DateTime.Now;
            var end = DateTime.Now.AddHours(24).AddSeconds(1);

            // Assert            
            var result = Assert.ThrowsAsync<ApiWrapperException>(() =>
                    _availabilityService.GetAvailabilityDailyAsync(
                        start,
                        end,
                        1,
                       _userIdList));

            Assert.AreEqual(result.Message.ToString(), "End date must be within 24 hours of Start date (inclusive).");
        }

        [Test]
        public void GetAvailabilityPerDailyInterval_NoWorkerIdsSpecified()
        {            
            // Arrange           
            var start = DateTime.Now;
            var end = DateTime.Now.AddHours(23).AddMinutes(59).AddSeconds(59);

            // Assert            
            var result = Assert.ThrowsAsync<ApiWrapperException>(() =>
                    _availabilityService.GetAvailabilityDailyAsync(
                        start,
                        end,
                        1,
                        new List<string>()));

            Assert.AreEqual(result.Message.ToString(), "You must supply at least one worker id.");
        }
        #endregion

        #region GetTotalAvailability tests
        [Test]
        public async Task GetTotalAvailability_OkResult()
        {
            // Arrange
            _httpTest.RespondWithJson(_expectedOkResult);

            var result = await _availabilityService.GetTotalAvailability(new TotalAvailabilityRequest { WorkerIds = _userIdList });

            // Assert
            Assert.AreEqual(_expectedOkResult.Data.Count, result.Data.Count);
        }
        #endregion

        [TearDown]
        public void DisposeHttpTest()
        {
            _httpTest.Dispose();
        }
    }
}