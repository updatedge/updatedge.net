using NUnit.Framework;
using System.Threading.Tasks;
using Updatedge.net.Services.V1;
using Flurl.Http.Testing;
using Updatedge.net.Exceptions;
using Udatedge.Common;
using AutoFixture;

namespace Updatedge.net.Tests
{
    public class InviteServiceTests
    {
        private HttpTest _httpTest;        
        private InviteService _inviteService;                
        
        [SetUp]
        public void Setup()
        {
            // Put Flurl into test mode
            _httpTest = new HttpTest();                       

            // register and create Invite service
            FixtureConfig.Fixture.Register(() => new InviteService(FixtureConfig.BaseUrl, FixtureConfig.ApiKey));
            _inviteService = FixtureConfig.Fixture.Create<InviteService>();

        }


        #region InviteWorker tests
        [Test]
        public async Task InviteWorker_OkResult()
        {            
            // Arrange            
            _httpTest.RespondWith(FixtureConfig.InviteId1);
            
            var result = await _inviteService.InviteWorkerAsync(FixtureConfig.UserId1, FixtureConfig.Worker1Email);

            // Assert
            Assert.True(result == FixtureConfig.InviteId1);            
        }

        [Test]
        public void InviteWorker_401Result()
        {
            // Arrange           
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails401, 401);

            // Assert
            Assert.ThrowsAsync<UnauthorizedApiRequestException>(() => _inviteService.InviteWorkerAsync(FixtureConfig.UserId1, FixtureConfig.Worker1Email));            
        }

        [Test]
        public void InviteWorker_400Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails400, 400);
            
            // Assert
            Assert.ThrowsAsync<InvalidApiRequestException>(() => _inviteService.InviteWorkerAsync(FixtureConfig.UserId1, FixtureConfig.Worker1Email));            
        }

        [Test]
        public void InviteWorker_403Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails403, 403);
                                    
            // Assert
            Assert.ThrowsAsync<ForbiddenApiRequestException>(() => _inviteService.InviteWorkerAsync(FixtureConfig.UserId1, FixtureConfig.Worker1Email));
        }

        [Test]
        public void InviteWorker_500Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails500, 500);
                                    
            // Assert
            Assert.ThrowsAsync<ApiException>(() => _inviteService.InviteWorkerAsync(FixtureConfig.UserId1, FixtureConfig.Worker1Email));
        }

        [Test]
        public void InviteWorker_ValueNotSpecified()
        {              
            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(() => _inviteService.InviteWorkerAsync(string.Empty, FixtureConfig.Worker1Email));

            Assert.True(ex.ExceptionDetails.Errors.ContainsKey("fromuserid"));
            var startError = ex.ExceptionDetails.Errors["fromuserid"];            
            Assert.True(startError.Contains(Constants.ErrorMessages.ValueNotSpecified));
        }

        [Test]
        public void InviteWorker_EmailInvalid()
        {
            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(() => _inviteService.InviteWorkerAsync(FixtureConfig.UserId1, FixtureConfig.NotAnEmail));

            Assert.True(ex.ExceptionDetails.Errors.ContainsKey("toworkeremail"));
            var startError = ex.ExceptionDetails.Errors["toworkeremail"];
            Assert.True(startError.Contains(Constants.ErrorMessages.EmailInvalid));
        }

        #endregion

        #region InviteHirer tests

        [Test]
        public async Task InviteHirer_OkResult()
        {
            // Arrange            
            _httpTest.RespondWith(FixtureConfig.InviteId1);

            var result = await _inviteService.InviteHirerAsync(FixtureConfig.UserId1, FixtureConfig.Hirer1Email);

            // Assert
            Assert.True(result == FixtureConfig.InviteId1);
        }

        [Test]
        public void InviteHirer_401Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails401, 401);

            // Assert
            Assert.ThrowsAsync<UnauthorizedApiRequestException>(() => _inviteService.InviteHirerAsync(FixtureConfig.UserId1, FixtureConfig.Hirer1Email));
        }

        [Test]
        public void InviteHirer_400Result()
        {
            // Arrange           
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails400, 400);

            // Assert
            Assert.ThrowsAsync<InvalidApiRequestException>(() => _inviteService.InviteHirerAsync(FixtureConfig.UserId1, FixtureConfig.Hirer1Email));
        }

        [Test]
        public void InviteHirer_403Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails403, 403);

            // Assert
            Assert.ThrowsAsync<ForbiddenApiRequestException>(() => _inviteService.InviteHirerAsync(FixtureConfig.UserId1, FixtureConfig.Hirer1Email));
        }

        [Test]
        public void InviteHirer_500Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails500, 500);

            // Assert
            Assert.ThrowsAsync<ApiException>(() => _inviteService.InviteHirerAsync(FixtureConfig.UserId1, FixtureConfig.Hirer1Email));
        }

        [Test]
        public void InviteHirer_ValueNotSpecified()
        {
            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(() => _inviteService.InviteHirerAsync(string.Empty, FixtureConfig.Hirer1Email));

            Assert.True(ex.ExceptionDetails.Errors.ContainsKey("fromuserid"));
            var startError = ex.ExceptionDetails.Errors["fromuserid"];
            Assert.True(startError.Contains(Constants.ErrorMessages.ValueNotSpecified));
        }

        [Test]
        public void InviteHirer_EmailInvalid()
        {
            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(() => _inviteService.InviteHirerAsync(FixtureConfig.UserId1, FixtureConfig.NotAnEmail));

            Assert.True(ex.ExceptionDetails.Errors.ContainsKey("tohireremail"));
            var startError = ex.ExceptionDetails.Errors["tohireremail"];
            Assert.True(startError.Contains(Constants.ErrorMessages.EmailInvalid));
        }


        #endregion

        [TearDown]
        public void DisposeHttpTest()
        {
            _httpTest.Dispose();
        }
    }
}