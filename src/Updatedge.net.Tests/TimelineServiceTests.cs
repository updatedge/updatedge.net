using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Updatedge.net.Services.V1;
using Flurl.Http.Testing;
using Updatedge.net.Exceptions;
using Udatedge.Common;
using Udatedge.Common.Models.TimelineEvents;
using Light.GuardClauses.Exceptions;
using AutoFixture;

namespace Updatedge.net.Tests
{
    public class TimelineServiceTests
    {
        private HttpTest _httpTest;        
        private TimelineService _timelineService;        
        
        
        [SetUp]
        public void Setup()
        {
            // Put Flurl into test mode
            _httpTest = new HttpTest();

            // register and create Timeline service
            FixtureConfig.Fixture.Register(() => new TimelineService(FixtureConfig.BaseUrl, FixtureConfig.ApiKey));
            _timelineService = FixtureConfig.Fixture.Create<TimelineService>();
        }


        #region GetEvents tests
        [Test]
        public async Task GetEvents_OkResult()
        {            
            // Arrange
            var okResult = FixtureConfig.Fixture.Create<List<TimelineEvent>>();
            
            _httpTest.RespondWithJson(okResult);
            
            var result = await _timelineService.GetEventsAsync(FixtureConfig.UserId1, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(1));

            // Assert
            Assert.True(okResult.Count == result.Count);            
        }

        [Test]
        public void GetEvents_401Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails401, 401);

            // Assert
            Assert.ThrowsAsync<UnauthorizedApiRequestException>(() => _timelineService.GetEventsAsync(FixtureConfig.UserId1, DateTime.Now.AddDays(-2), DateTime.Now.AddDays(1)));            
        }

        [Test]
        public void GetEvents_400Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails400, 400);
                                
            var start = DateTime.Now;
            var end = DateTime.Now.AddHours(23).AddMinutes(59).AddSeconds(59);

            // Assert
            Assert.ThrowsAsync<InvalidApiRequestException>(() => _timelineService.GetEventsAsync(FixtureConfig.UserId1, start, end));            
        }

        [Test]
        public void GetEvents_403Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails403, 403);
                        
            var start = DateTime.Now;
            var end = DateTime.Now.AddHours(23).AddMinutes(59).AddSeconds(59);

            // Assert
            Assert.ThrowsAsync<ForbiddenApiRequestException>(() => _timelineService.GetEventsAsync(FixtureConfig.UserId1, start, end));
        }

        [Test]
        public void GetEvents_500Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails500, 500);
                        
            var start = DateTime.Now;
            var end = DateTime.Now.AddHours(23).AddMinutes(59).AddSeconds(59);

            // Assert
            Assert.ThrowsAsync<ApiException>(() => _timelineService.GetEventsAsync(FixtureConfig.UserId1, start, end));
        }

        [Test]
        public void GetEvents_XMustBeBeforeY()
        {
            // Arrange           
            var start = DateTimeOffset.Now;
            var end = DateTimeOffset.Now.AddSeconds(-1);
                       
            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(() => _timelineService.GetEventsAsync(FixtureConfig.UserId1, start, end));

            Assert.True(ex.ExceptionDetails.Errors.ContainsKey("start"));
            var startError = ex.ExceptionDetails.Errors["start"];
            var endUtcFormatted = $"({end.ToString()})";
            var startUtcFormatted = $"({start.ToString()})";
            Assert.True(startError.Contains(string.Format(Constants.ErrorMessages.XMustBeBeforeY, startUtcFormatted, endUtcFormatted)));
        }
        
        [Test]
        public void GetEvents_XMustBeWithinYDaysOfZ()
        {
            // Arrange           
            var start = DateTimeOffset.Now;
            var end = DateTimeOffset.Now.AddDays(32).AddSeconds(1);

            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(() => _timelineService.GetEventsAsync(FixtureConfig.UserId1, start, end));

            Assert.True(ex.ExceptionDetails.Errors.ContainsKey("end"));
            var endError = ex.ExceptionDetails.Errors["end"];
            var endUtcFormatted = $"({end.ToString()})";
            var startUtcFormatted = $"({start.ToString()})";
            Assert.True(endError.Contains(string.Format(Constants.ErrorMessages.XMustBeWithinYDaysOfZ, endUtcFormatted, 32, startUtcFormatted)));
        }

        [Test]
        public async Task GetEvents_NoValidContent()
        {
            // Arrange            
            var start = DateTimeOffset.Now;
            var end = DateTimeOffset.Now.AddDays(2);
            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails204, 204);

            // act
            var result = await _timelineService.GetEventsAsync(FixtureConfig.UserId1, start, end);

            // Assert            
            Assert.True(result.Count == 0);
        }

        #endregion

        #region GetEvent tests
        [Test]
        public async Task GetEvent_OkResult()
        {
            // Arrange            
            var okResult = FixtureConfig.Fixture.Create<List<TimelineEvent>>();
            
            _httpTest.RespondWithJson(okResult);


            var result = await _timelineService.GetEventAsync(FixtureConfig.EventId1);

            // Assert
            Assert.AreEqual(okResult.Count, result.Count);
        }

        [Test]
        public void GetEvent_401Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails401, 401);

            // Assert
            Assert.ThrowsAsync<UnauthorizedApiRequestException>(() => _timelineService.GetEventAsync(FixtureConfig.EventId1));
        }

        [Test]
        public void GetEvent_400Result()
        {
            // Arrange           
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails400, 400);

            // Arrange           
            var start = DateTime.Now;
            var end = DateTime.Now.AddHours(23).AddMinutes(59).AddSeconds(59);

            // Assert
            Assert.ThrowsAsync<InvalidApiRequestException>(() => _timelineService.GetEventAsync(FixtureConfig.EventId1));
        }

        [Test]
        public void GetEvent_403Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails403, 403);
                                  
            var start = DateTime.Now;
            var end = DateTime.Now.AddHours(23).AddMinutes(59).AddSeconds(59);

            // Assert
            Assert.ThrowsAsync<ForbiddenApiRequestException>(() => _timelineService.GetEventAsync(FixtureConfig.EventId1));
        }

        [Test]
        public void GetEvent_500Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails500, 500);
                        
            var start = DateTime.Now;
            var end = DateTime.Now.AddHours(23).AddMinutes(59).AddSeconds(59);

            // Assert
            Assert.ThrowsAsync<ApiException>(() => _timelineService.GetEventAsync(FixtureConfig.EventId1));
        }
               
        [Test]
        public async Task GetEvent_NoValidContent()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails204, 204);
                        
            // act
            var result = await _timelineService.GetEventAsync(FixtureConfig.EventId0);

            // Assert            
            Assert.True(result.Count == 0);            
        }

        [Test]
        public async Task GetEvent_NoEventId()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails204, 204);

            // act
            var result = await _timelineService.GetEventAsync(FixtureConfig.EventId0);

            // Assert            
            Assert.ThrowsAsync<EmptyStringException>(() => _timelineService.GetEventAsync(string.Empty)); 
        }

        #endregion

        [TearDown]
        public void DisposeHttpTest()
        {
            _httpTest.Dispose();
        }
    }
}