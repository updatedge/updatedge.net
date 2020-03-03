using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Updatedge.net.Services.V1;
using Flurl.Http.Testing;
using Updatedge.net.Exceptions;
using Updatedge.Common.Models.Availability;
using Updatedge.Common;
using Updatedge.net.Entities.V1;
using AutoFixture;

namespace Updatedge.net.Tests
{
    public class AvailabilityServiceTests
    {
        private HttpTest _httpTest;        
        private AvailabilityService _availabilityService;
        private WorkersIntervalsRequest _workersIntervalsRequest;
                
        [SetUp]
        public void Setup()
        {
            // Put Flurl into test mode
            _httpTest = new HttpTest();            

            // register and create Availability service
            FixtureConfig.Fixture.Register(() => new AvailabilityService(FixtureConfig.Config));
            _availabilityService = FixtureConfig.Fixture.Create<AvailabilityService>();
            
            _workersIntervalsRequest = FixtureConfig.Fixture.Build<WorkersIntervalsRequest>()
                                            .With(o => o.Intervals, new List<BaseInterval> {
                                                new BaseInterval {
                                                    Start = DateTimeOffset.Now.AddSeconds(1),
                                                    End = DateTimeOffset.Now.AddHours(7)
                                                }
                                            })
                                            .Create();            
        }


        #region GetAvailabilityPerDailyInterval tests
        [Test]
        public async Task GetAvailabilityPerDailyInterval_OkResult()
        {
            // Arrange
            var okResult = FixtureConfig.Fixture.Create<OkApiResult<List<WorkerAvailabilityIntervals>>>();

            _httpTest.RespondWithJson(okResult);
            
            var result = await _availabilityService.GetAvailabilityDailyAsync(
                    DateTime.Now, 
                    DateTime.Now.AddHours(23).AddMinutes(59).AddSeconds(59), 
                    1, 
                    FixtureConfig.Fixture.Create<IEnumerable<string>>());

            // Assert
            Assert.AreEqual(okResult.Data.Count, result.Count);            
        }

        [Test]
        public void GetAvailabilityPerDailyInterval_401Result()
        {
            // Arrange          
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails401, 401);

            // Assert
            Assert.ThrowsAsync<UnauthorizedApiRequestException>(() => 
                    _availabilityService.GetAvailabilityDailyAsync(
                        DateTime.Now, 
                        DateTime.Now.AddHours(23).AddMinutes(59).AddSeconds(59), 
                        1,
                       FixtureConfig.Fixture.Create<IEnumerable<string>>()));            
        }

        [Test]
        public void GetAvailabilityPerDailyInterval_400Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails400, 400);

            // Arrange           
            var start = DateTime.Now;
            var end = DateTime.Now.AddHours(23).AddMinutes(59).AddSeconds(59);

            // Assert
            Assert.ThrowsAsync<InvalidApiRequestException>(() => 
                    _availabilityService.GetAvailabilityDailyAsync(
                        start,
                        end,
                        1,
                        FixtureConfig.Fixture.Create<IEnumerable<string>>()));            
        }

        [Test]
        public void GetAvailabilityPerDailyInterval_403Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails403, 403);

            // Arrange           
            var start = DateTime.Now;
            var end = DateTime.Now.AddHours(23).AddMinutes(59).AddSeconds(59);

            // Assert
            Assert.ThrowsAsync<ForbiddenApiRequestException>(() =>
                    _availabilityService.GetAvailabilityDailyAsync(
                        start,
                        end,
                        1,
                        FixtureConfig.Fixture.Create<IEnumerable<string>>()));
        }

        [Test]
        public void GetAvailabilityPerDailyInterval_500Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails500, 500);

            // Arrange           
            var start = DateTime.Now;
            var end = DateTime.Now.AddHours(23).AddMinutes(59).AddSeconds(59);

            // Assert
            Assert.ThrowsAsync<ApiException>(() =>
                    _availabilityService.GetAvailabilityDailyAsync(
                        start,
                        end,
                        1,
                        FixtureConfig.Fixture.Create<IEnumerable<string>>()));
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
                       FixtureConfig.Fixture.Create<IEnumerable<string>>()));

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
                       FixtureConfig.Fixture.Create<IEnumerable<string>>()));

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
            var okResult = FixtureConfig.Fixture.Create<OkApiResult<List<WorkerAvailabilityIntervals>>>();

            _httpTest.RespondWithJson(okResult);

            var result = await _availabilityService.GetTotalAvailability(_workersIntervalsRequest);

            // Assert
            Assert.AreEqual(okResult.Data.Count, result.Count);
        }

        [Test]
        public void GetTotalAvailability_401Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails401, 401);

            // Assert
            Assert.ThrowsAsync<UnauthorizedApiRequestException>(() =>
                    _availabilityService.GetTotalAvailability(_workersIntervalsRequest));
        }

        [Test]
        public void GetTotalAvailability_400Result()
        {
            // Arrange          
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails400, 400);

            // Assert
            Assert.ThrowsAsync<InvalidApiRequestException>(() =>
                    _availabilityService.GetTotalAvailability(_workersIntervalsRequest));
        }

        [Test]
        public void GetTotalAvailability_403Result()
        {
            // Arrange         
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails403, 403);

            // Assert
            Assert.ThrowsAsync<ForbiddenApiRequestException>(() =>
                    _availabilityService.GetTotalAvailability(_workersIntervalsRequest));
        }

        [Test]
        public void GetTotalAvailability_500Result()
        {
            // Arrange          
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails500, 500);

            // Assert
            Assert.ThrowsAsync<ApiException>(() =>
                    _availabilityService.GetTotalAvailability(_workersIntervalsRequest));
        }

        [Test]
        public void GetTotalAvailability_XMustBeBeforeY()
        {
            // Arrange           
            var start = DateTimeOffset.Now;
            var end = DateTimeOffset.Now.AddSeconds(-1);

            var workersIntervalRequest = FixtureConfig.Fixture.Build<WorkersIntervalsRequest>()
                    .With(o => o.Intervals, new List<BaseInterval> {
                            new BaseInterval { Start = start, End = end}
                        })
                    .Create();

            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(() =>
                    _availabilityService.GetTotalAvailability(workersIntervalRequest));

            Assert.True(ex.ExceptionDetails.Errors.ContainsKey("start"));
            var startError = ex.ExceptionDetails.Errors["start"];
            var endUtcFormatted = $"({end.ToString()})";
            var startUtcFormatted = $"({start.ToString()})";
            Assert.True(startError.Contains(string.Format(Constants.ErrorMessages.XMustBeBeforeY, startUtcFormatted, endUtcFormatted)));
        }

        [Test]
        public void GetTotalAvailability_XMustBeWithinYHoursOfZ()
        {
            // Arrange           
            var start = DateTimeOffset.Now;
            var end = DateTimeOffset.Now.AddHours(24).AddSeconds(1);

            var workersIntervalRequest = FixtureConfig.Fixture.Build<WorkersIntervalsRequest>()
                    .With(o => o.Intervals, new List<BaseInterval> {
                            new BaseInterval { Start = start, End = end}
                        })
                    .Create();

            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(() =>
                    _availabilityService.GetTotalAvailability(workersIntervalRequest));

            Assert.True(ex.ExceptionDetails.Errors.ContainsKey("end"));
            var endError = ex.ExceptionDetails.Errors["end"];
            var endUtcFormatted = $"({end.ToString()})";
            var startUtcFormatted = $"({start.ToString()})";
            Assert.True(endError.Contains(string.Format(Constants.ErrorMessages.XMustBeWithinYHoursOfZ, endUtcFormatted, 24, startUtcFormatted)));
        }

        [Test]
        public void GetTotalAvailability_NoWorkerIdsSpecified()
        {
            // Arrange           
            var start = DateTimeOffset.Now;
            var end = DateTimeOffset.Now.AddHours(23);

            var workersIntervalRequest = FixtureConfig.Fixture.Build<WorkersIntervalsRequest>()
                    .Without(o => o.WorkerIds)
                    .With(o => o.Intervals, new List<BaseInterval> {
                            new BaseInterval { Start = start, End = end}
                        })
                    .Create();

            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(() =>
                    _availabilityService.GetTotalAvailability(workersIntervalRequest));

            Assert.True(ex.ExceptionDetails.Errors.ContainsKey("workerids"));
            var workerIdsErrors = ex.ExceptionDetails.Errors["workerids"];
            Assert.True(workerIdsErrors.Contains(Constants.ErrorMessages.NoWorkerIdsSpecified));
        }

        [Test]
        public void GetTotalAvailability_NoIntervalsSpecified()
        {
            // Arrange           
            var start = DateTimeOffset.Now;
            var end = DateTimeOffset.Now.AddHours(23);

            var workersIntervalRequest = FixtureConfig.Fixture.Build<WorkersIntervalsRequest>()
                   .Without(o => o.WorkerIds)
                   .With(o => o.Intervals, new List<BaseInterval>())
                   .Create();

            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(() =>
                    _availabilityService.GetTotalAvailability(workersIntervalRequest));

            Assert.True(ex.ExceptionDetails.Errors.ContainsKey("intervals"));
            var intervalError = ex.ExceptionDetails.Errors["intervals"];
            Assert.True(intervalError.Contains(Constants.ErrorMessages.NoIntervalsSpecified));
        }

        [Test]
        public void GetTotalAvailability_IntervalsOverlap()
        {
            // Arrange           
            var start = DateTimeOffset.Now;
            var end = DateTimeOffset.Now.AddHours(23);

            var workersIntervalRequest = FixtureConfig.Fixture.Build<WorkersIntervalsRequest>()
                   .Without(o => o.WorkerIds)
                   .With(o => o.Intervals, new List<BaseInterval> {
                            new BaseInterval { Start = start, End = end},
                            new BaseInterval { Start = start.AddHours(22), End = end.AddHours(23)}
                       })
                   .Create();

            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(() =>
                    _availabilityService.GetTotalAvailability(workersIntervalRequest));

            Assert.True(ex.ExceptionDetails.Errors.ContainsKey("overlap"));
            var overlapError = ex.ExceptionDetails.Errors["overlap"];
            Assert.True(overlapError.Contains(Constants.ErrorMessages.IntervalsOverlap));
        }


        #endregion

        [TearDown]
        public void DisposeHttpTest()
        {
            _httpTest.Dispose();
        }
    }
}