using NUnit.Framework;
using System.Threading.Tasks;
using Updatedge.net.Services.V1;
using Flurl.Http.Testing;
using Updatedge.net.Exceptions;
using Udatedge.Common;
using Udatedge.Common.Models.Workers;
using AutoFixture;

namespace Updatedge.net.Tests
{
    public class WorkerServiceTests
    {
        private HttpTest _httpTest;        
        private WorkerService _workerService;        
                
        [SetUp]
        public void Setup()
        {
            // Put Flurl into test mode
            _httpTest = new HttpTest();
            
            // register and create Timeline service
            FixtureConfig.Fixture.Register(() => new WorkerService(FixtureConfig.BaseUrl, FixtureConfig.ApiKey));
            _workerService = FixtureConfig.Fixture.Create<WorkerService>();     
        }


        #region InviteWorker tests
        [Test]
        public async Task GetWorkerById_OkResult()
        {
            // Arrange
            var okResult = FixtureConfig.Fixture.Create<Worker>();

            _httpTest.RespondWithJson(okResult);


            // Arrange            
            _httpTest.RespondWith(FixtureConfig.InviteId1);
            
            var result = await _workerService.GetWorkerByIdAsync(FixtureConfig.UserId1);

            // Assert
            Assert.True(result.Id == okResult.Id);
            Assert.True(result.FirstName == okResult.FirstName);
            Assert.True(result.Verified == okResult.Verified);
        }

        [Test]
        public void InviteWorker_401Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails401, 401);

            // Assert
            Assert.ThrowsAsync<UnauthorizedApiRequestException>(() => _workerService.GetWorkerByIdAsync(FixtureConfig.UserId1));            
        }

    
        [Test]
        public void InviteWorker_403Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails403, 403);
                                    
            // Assert
            Assert.ThrowsAsync<ForbiddenApiRequestException>(() => _workerService.GetWorkerByIdAsync(FixtureConfig.UserId1));
        }

        [Test]
        public void InviteWorker_500Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails500, 500);
                                    
            // Assert
            Assert.ThrowsAsync<ApiException>(() => _workerService.GetWorkerByIdAsync(FixtureConfig.UserId1));
        }
                
        #endregion

        #region GetWorkerByEmail tests

        [Test]
        public async Task GetWorkerByEmail_OkResult()
        {
            // Arrange
            var okResult = FixtureConfig.Fixture.Create<BaseWorker>();
            
            _httpTest.RespondWithJson(okResult);


            // Arrange            
            _httpTest.RespondWith(FixtureConfig.InviteId1);

            var result = await _workerService.GetWorkerByEmailAsync(FixtureConfig.Worker1Email);

            // Assert
            Assert.True(result.Id == okResult.Id);
            Assert.True(result.FirstName == okResult.FirstName);           
        }

        [Test]
        public void GetWorkerByEmail_401Result()
        {
            // Arrange           
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails401, 401);

            // Assert
            Assert.ThrowsAsync<UnauthorizedApiRequestException>(() => _workerService.GetWorkerByEmailAsync(FixtureConfig.Worker1Email));
        }

        [Test]
        public void GetWorkerByEmail_400Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails400, 400);

            // Assert
            Assert.ThrowsAsync<InvalidApiRequestException>(() => _workerService.GetWorkerByEmailAsync(FixtureConfig.Worker1Email));
        }

        [Test]
        public void GetWorkerByEmail_403Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails403, 403);

            // Assert
            Assert.ThrowsAsync<ForbiddenApiRequestException>(() => _workerService.GetWorkerByEmailAsync(FixtureConfig.Worker1Email));
        }

        [Test]
        public void GetWorkerByEmail_500Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails500, 500);

            // Assert
            Assert.ThrowsAsync<ApiException>(() => _workerService.GetWorkerByEmailAsync(FixtureConfig.Worker1Email));
        }
                
        [Test]
        public void GetWorkerByEmail_EmailInvalid()
        {
            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(() => _workerService.GetWorkerByEmailAsync(FixtureConfig.NotAnEmail));

            Assert.True(ex.ExceptionDetails.Errors.ContainsKey("email"));
            var startError = ex.ExceptionDetails.Errors["email"];
            Assert.True(startError.Contains(Constants.ErrorMessages.EmailInvalid));
        }
        #endregion

        #region Nudge Worker tests
        
        [Test]
        public async Task NudgeWorker_OkResult()
        {
            var result = await _workerService.NudgeWorkerAsync(FixtureConfig.UserId1, FixtureConfig.UserId2);

            // Assert
            Assert.True(result);            
        }       

        [Test]
        public void NudgeWorker_401Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails401, 401);

            // Assert
            Assert.ThrowsAsync<UnauthorizedApiRequestException>(() => _workerService.NudgeWorkerAsync(FixtureConfig.UserId1, FixtureConfig.UserId2));
        }

        [Test]
        public void NudgeWorker_400Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails400, 400);

            // Assert
            Assert.ThrowsAsync<InvalidApiRequestException>(() => _workerService.NudgeWorkerAsync(FixtureConfig.UserId1, FixtureConfig.UserId2));
        }

        [Test]
        public void NudgeWorker_403Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails403, 403);

            // Assert
            Assert.ThrowsAsync<ForbiddenApiRequestException>(() => _workerService.NudgeWorkerAsync(FixtureConfig.UserId1, FixtureConfig.UserId2));
        }

        [Test]
        public void NudgeWorker_429Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails429, 429);

            // Assert
            Assert.ThrowsAsync<ThrottledApiRequestException>(() => _workerService.NudgeWorkerAsync(FixtureConfig.UserId1, FixtureConfig.UserId2));
        }

        [Test]
        public void NudgeWorker_500Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails500, 500);

            // Assert
            Assert.ThrowsAsync<ApiException>(() => _workerService.NudgeWorkerAsync(FixtureConfig.UserId1, FixtureConfig.UserId2));
        }

        [Test]
        public void NudgeWorker_ValueNotSpecified()
        {
            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(() => _workerService.NudgeWorkerAsync(string.Empty, FixtureConfig.UserId2));

            Assert.True(ex.ExceptionDetails.Errors.ContainsKey("fromuserid"));
            var startError = ex.ExceptionDetails.Errors["fromuserid"];
            Assert.True(startError.Contains(Constants.ErrorMessages.ValueNotSpecified));
        }
        #endregion

        [TearDown]
        public void DisposeHttpTest()
        {
            _httpTest.Dispose();
        }
    }
}