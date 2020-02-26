using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Updatedge.net.Entities.V1;
using Updatedge.net.Services.V1;
using Flurl.Http.Testing;
using Updatedge.net.Exceptions;
using Udatedge.Common.Models.Availability;
using Udatedge.Common;

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
            var start = DateTimeOffset.Now;
            var end = DateTimeOffset.Now.AddSeconds(-1);
                       
            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(() =>
                    _availabilityService.GetAvailabilityDailyAsync(
                        start,
                        end,
                        1,
                       _userIdList));

            Assert.True(ex.ExceptionDetails.Errors.ContainsKey("start"));
            var startError = ex.ExceptionDetails.Errors["start"];
            var endUtcFormatted = $"({end.ToString()})";
            var startUtcFormatted = $"({start.ToString()})";
            Assert.True(startError.Contains(string.Format(Constants.ErrorMessages.XMustBeBeforeY, startUtcFormatted, endUtcFormatted)));
        }
        
        [Test]
        public void GetAvailabilityPerDailyInterval_XMustBeWithinYHoursOfZ()
        {
            // Arrange           
            var start = DateTimeOffset.Now;
            var end = DateTimeOffset.Now.AddHours(24).AddSeconds(1);

            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(() =>
                    _availabilityService.GetAvailabilityDailyAsync(
                        start,
                        end,
                        1,
                       _userIdList));

            Assert.True(ex.ExceptionDetails.Errors.ContainsKey("end"));
            var endError = ex.ExceptionDetails.Errors["end"];
            var endUtcFormatted = $"({end.ToString()})";
            var startUtcFormatted = $"({start.ToString()})";
            Assert.True(endError.Contains(string.Format(Constants.ErrorMessages.XMustBeWithinYHoursOfZ, endUtcFormatted, 24, startUtcFormatted)));
        }

        [Test]
        public void GetAvailabilityPerDailyInterval_NoWorkerIdsSpecified()
        {            
            // Arrange           
            var start = DateTimeOffset.Now;
            var end = DateTimeOffset.Now.AddHours(23).AddMinutes(59).AddSeconds(59);

            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(async () =>
            {
                var result = await _availabilityService.GetAvailabilityDailyAsync(start, end, 1, new List<string>());                
            });
          
            Assert.True(ex.ExceptionDetails.Errors.ContainsKey("workerids"));
            var workerIdsErrors = ex.ExceptionDetails.Errors["workerids"];
            Assert.True(workerIdsErrors.Contains(Constants.ErrorMessages.NoWorkerIdsSpecified));
        }
        #endregion

        #region GetTotalAvailability tests
        [Test]
        public async Task GetTotalAvailability_OkResult()
        {
            // Arrange
            _httpTest.RespondWithJson(_expectedOkResult);

            var result = await _availabilityService.GetTotalAvailability(
                new WorkersIntervalsRequest 
                { 
                    WorkerIds = _userIdList, 
                    Intervals = new List<BaseInterval>
                    {
                        new BaseInterval { Start = DateTimeOffset.Now, End = DateTimeOffset.Now.AddHours(7)}
                    } 
                });

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