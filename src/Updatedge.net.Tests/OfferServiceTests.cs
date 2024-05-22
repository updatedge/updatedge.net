using NUnit.Framework;
using System.Threading.Tasks;
using Updatedge.net.Services.V1;
using Flurl.Http.Testing;
using Updatedge.net.Exceptions;
using Updatedge.Common;
using AutoFixture;
using Updatedge.Common.Models.Offer;
using System.Collections.Generic;
using Updatedge.Common.Models.Availability;
using System;
using Updatedge.net.Entities.V1;
using System.Linq;

namespace Updatedge.net.Tests
{
    public class OfferServiceTests
    {
        private HttpTest _httpTest;
        private OfferService _offerService;        
        private CreateOffer _offer;
                
        [SetUp]
        public void Setup()
        {
            // Put Flurl into test mode
            _httpTest = new HttpTest();

            // register and create offer service            
            FixtureConfig.Fixture.Register(() => new OfferService(FixtureConfig.Config));
            _offerService = FixtureConfig.Fixture.Create<OfferService>();

            // build and create Offer object
            _offer = FixtureConfig.Fixture.Build<CreateOffer>()                    
                        .With(o => o.Events, new List<Interval> {
                                new Interval { 
                                    Start = DateTimeOffset.Now.AddDays(1), 
                                    End = DateTimeOffset.Now.AddDays(2)
                                }
                            })
                        .Create();
        }


        #region CreateOffer tests
        [Test]
        public async Task CreateOffer_OkResult()
        {
            // Arrange
            var okResult = new OkApiResult<string>
            {
                Data = FixtureConfig.OfferId1
            };

            _httpTest.RespondWith(FixtureConfig.OfferId1);
            
            var result = await _offerService.CreateOfferAsync(_offer);

            // Assert
            Assert.True(result == FixtureConfig.OfferId1);            
        }

        [Test]
        public void CreateOffer_401Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails401, 401);

            // Assert
            Assert.ThrowsAsync<UnauthorizedApiRequestException>(() => _offerService.CreateOfferAsync(_offer));            
        }

        [Test]
        public void CreateOffer_400Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails400, 400);
            
            // Assert
            Assert.ThrowsAsync<InvalidApiRequestException>(() => _offerService.CreateOfferAsync(_offer));            
        }

        [Test]
        public void CreateOffer_403Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails403, 403);
                                    
            // Assert
            Assert.ThrowsAsync<ForbiddenApiRequestException>(() => _offerService.CreateOfferAsync(_offer));
        }

        [Test]
        public void CreateOffer_500Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails500, 500);
                                    
            // Assert
            Assert.ThrowsAsync<ApiException>(() => _offerService.CreateOfferAsync(_offer));
        }

        [Test]
        public void CreateOffer_ValueNotSpecified()
        {
            var events = new List<BaseInterval>();
            
            // Arrange
            var offer = FixtureConfig.Fixture.Build<CreateOffer>()
                    .Without(o => o.Title)
                    .With(o => o.Events, new List<Interval> { 
                            new Interval { Start = DateTimeOffset.Now.AddDays(1), End = DateTimeOffset.Now.AddDays(2)}
                        })
                    .Create();

            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(() => _offerService.CreateOfferAsync(offer));

            Assert.True(ex.ExceptionDetails.Errors.ContainsKey("title"));
            var startError = ex.ExceptionDetails.Errors["title"];            
            Assert.True(startError.Contains(Constants.ErrorMessages.ValueNotSpecified));
        }

        [Test]
        public void CreateOffer_NoWorkerIdsSpecified()
        {
            // Arrange
            var offer = FixtureConfig.Fixture.Build<CreateOffer>()
                    .Without(o => o.WorkerIds)
                    .With(o => o.Events, new List<Interval> {
                            new Interval { Start = DateTimeOffset.Now.AddDays(1), End = DateTimeOffset.Now.AddDays(2)}
                        })
                    .Create();

            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(async () =>
            {
                var result = await _offerService.CreateOfferAsync(offer);
            });

            Assert.True(ex.ExceptionDetails.Errors.ContainsKey("workerids"));
            var workerIdsErrors = ex.ExceptionDetails.Errors["workerids"];
            Assert.True(workerIdsErrors.Contains(Constants.ErrorMessages.NoWorkerIdsSpecified));
        }

        [Test]
        public void CreateOffer_XMustBeBeforeY()
        {
            // Arrange           
            var start = DateTimeOffset.Now;
            var end = DateTimeOffset.Now.AddSeconds(-1);
                        
            var offer = FixtureConfig.Fixture.Build<CreateOffer>()                    
                    .With(o => o.Events, new List<Interval> {
                            new Interval { Start = start, End = end}
                        })
                    .Create();

            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(() => _offerService.CreateOfferAsync(offer));

            Assert.True(ex.ExceptionDetails.Errors.ContainsKey("start"));
            var startError = ex.ExceptionDetails.Errors["start"];
            var endUtcFormatted = $"({end.ToString()})";
            var startUtcFormatted = $"({start.ToString()})";
            Assert.True(startError.Contains(string.Format(Constants.ErrorMessages.XMustBeBeforeY, startUtcFormatted, endUtcFormatted)));
        }

        [Test]
        public void CreateOffer_MustStartInFuture()
        {
            // Arrange           
            var start = DateTimeOffset.Now.AddSeconds(-1);
            var end = DateTimeOffset.Now.AddHours(7);

            var offer = FixtureConfig.Fixture.Build<CreateOffer>()
                    .With(o => o.Events, new List<Interval> {
                            new Interval { Start = start, End = end}
                        })
                    .Create();

            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(() => _offerService.CreateOfferAsync(offer));

            Assert.True(ex.ExceptionDetails.Errors.ContainsKey("events"));
            var startError = ex.ExceptionDetails.Errors["events"];
            var endUtcFormatted = $"{end.ToString()}";
            var startUtcFormatted = $"{start.ToString()}";
            Assert.True(startError.Contains(string.Format(Constants.ErrorMessages.MustStartInFuture, startUtcFormatted, endUtcFormatted)));
        }

        #endregion

        #region GetOffer tests

        [Test]
        public async Task GetOffer_OkResult()
        {
            // Arrange
            var offer = FixtureConfig.Fixture.Create<Offer>();            
            _httpTest.RespondWithJson(offer, 200);

            var result = await _offerService.GetOfferAsync(FixtureConfig.OfferId1);

            // Assert
            Assert.True(result.CreatedAt == offer.CreatedAt);
            Assert.True(result.Title == offer.Title);
            Assert.True(result.Id == offer.Id);
            Assert.True(result.LocationPlaceId == offer.LocationPlaceId);
            Assert.True(result.Recipients.Count() == offer.Recipients.Count());
            Assert.True(result.Widthdrawn == offer.Widthdrawn);
        }

        [Test]
        public void GetOffer_401Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails401, 401);

            // Assert
            Assert.ThrowsAsync<UnauthorizedApiRequestException>(() => _offerService.GetOfferAsync(FixtureConfig.OfferId1));
        }
        
        [Test]
        public void GetOffer_403Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails403, 403);

            // Assert
            Assert.ThrowsAsync<ForbiddenApiRequestException>(() => _offerService.GetOfferAsync(FixtureConfig.OfferId1));
        }

        [Test]
        public void GetOffer_500Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails500, 500);

            // Assert
            Assert.ThrowsAsync<ApiException>(() => _offerService.GetOfferAsync(FixtureConfig.OfferId1));
        }

        [Test]
        public void GetOffer_ValueNotSpecified()
        {
            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(() => _offerService.GetOfferAsync(string.Empty));

            Assert.True(ex.ExceptionDetails.Errors.ContainsKey("id"));
            var startError = ex.ExceptionDetails.Errors["id"];
            Assert.True(startError.Contains(Constants.ErrorMessages.ValueNotSpecified));
        }


        #endregion

        #region WithdrawOffer tests

        [Test]
        public async Task WithdrawOffer_OkResult()
        {
            // Arrange            
            _httpTest.RespondWith(string.Empty, 204);            

            // Assert
            Assert.True(await _offerService.WithdrawOfferAsync(FixtureConfig.OfferId1));            
        }

        [Test]
        public void WithdrawOffer_401Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails401, 401);

            // Assert
            Assert.ThrowsAsync<UnauthorizedApiRequestException>(() => _offerService.WithdrawOfferAsync(FixtureConfig.OfferId1));
        }
        
        [Test]
        public void WithdrawOffer_403Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails403, 403);

            // Assert
            Assert.ThrowsAsync<ForbiddenApiRequestException>(() => _offerService.WithdrawOfferAsync(FixtureConfig.OfferId1));
        }

        [Test]
        public void WithdrawOffer_500Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails500, 500);

            // Assert
            Assert.ThrowsAsync<ApiException>(() => _offerService.WithdrawOfferAsync(FixtureConfig.OfferId1));
        }

        [Test]
        public void WithdrawOffer_ValueNotSpecified()
        {
            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(() => _offerService.WithdrawOfferAsync(string.Empty));

            Assert.True(ex.ExceptionDetails.Errors.ContainsKey("id"));
            var startError = ex.ExceptionDetails.Errors["id"];
            Assert.True(startError.Contains(Constants.ErrorMessages.ValueNotSpecified));
        }
        #endregion

        #region CompleteOffer tests

        [Test]
        public async Task CompleteOffer_OkResult()
        {
            // Arrange            
            _httpTest.RespondWith(string.Empty, 204);

            // Assert
            Assert.True(await _offerService.CompleteOfferAsync(FixtureConfig.OfferId1, FixtureConfig.Fixture.Create<IEnumerable<string>>(), null));
        }

        [Test]
        public void CompleteOffer_401Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails401, 401);

            // Assert
            Assert.ThrowsAsync<UnauthorizedApiRequestException>(() => _offerService.CompleteOfferAsync(FixtureConfig.OfferId1, FixtureConfig.Fixture.Create<IEnumerable<string>>(), null));
        }

        [Test]
        public void CompleteOffer_400Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails400, 400);

            // Assert
            Assert.ThrowsAsync<InvalidApiRequestException>(() => _offerService.CompleteOfferAsync(FixtureConfig.OfferId1, FixtureConfig.Fixture.Create<IEnumerable<string>>(), null));
        }

        [Test]
        public void CompleteOffer_403Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails403, 403);

            // Assert
            Assert.ThrowsAsync<ForbiddenApiRequestException>(() => _offerService.CompleteOfferAsync(FixtureConfig.OfferId1, FixtureConfig.Fixture.Create<IEnumerable<string>>(), null));
        }

        [Test]
        public void CompleteOffer_500Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails500, 500);

            // Assert
            Assert.ThrowsAsync<ApiException>(() => _offerService.CompleteOfferAsync(FixtureConfig.OfferId1, FixtureConfig.Fixture.Create<IEnumerable<string>>(), null));
        }

        [Test]
        public void CompleteOffer_ValueNotSpecified()
        {
            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(() => _offerService.CompleteOfferAsync(string.Empty, FixtureConfig.Fixture.Create<IEnumerable<string>>(), null));

            Assert.True(ex.ExceptionDetails.Errors.ContainsKey("id"));
            var startError = ex.ExceptionDetails.Errors["id"];
            Assert.True(startError.Contains(Constants.ErrorMessages.ValueNotSpecified));
        }

        [Test]
        public void CompleteOffer_NoWorkerIdsSpecified()
        {
            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(async () =>
            {
                var result = await _offerService.CompleteOfferAsync(FixtureConfig.OfferId1, new List<string>(), null);
            });

            Assert.True(ex.ExceptionDetails.Errors.ContainsKey("workerids"));
            var workerIdsErrors = ex.ExceptionDetails.Errors["workerids"];
            Assert.True(workerIdsErrors.Contains(Constants.ErrorMessages.NoWorkerIdsSpecified));
        }
        #endregion

        [TearDown]
        public void DisposeHttpTest()
        {
            _httpTest.Dispose();
        }
    }
}