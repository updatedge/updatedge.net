using NUnit.Framework;
using System.Threading.Tasks;
using Updatedge.net.Services.V1;
using Flurl.Http.Testing;
using Updatedge.net.Exceptions;
using Udatedge.Common;
using Udatedge.Common.Models;
using Udatedge.Common.Models.Workers;
using System.Collections.Generic;
using System;

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

            // instantiate the Availability service
            _workerService = new WorkerService(TestValues.BaseUrl, TestValues.ApiKey);
            
        }


        #region InviteWorker tests
        [Test]
        public async Task GetWorkerById_OkResult()
        {
            // Arrange
            var okResult = new Worker
            {                
                Id = "1",
                Emails = new List<string>(),
                FirstName = "First",
                LastName = "Last",
                LastShared = DateTimeOffset.Now.AddDays(2),
                Verified = true                
            };

            _httpTest.RespondWithJson(okResult);


            // Arrange            
            _httpTest.RespondWith(TestValues.InviteId1);
            
            var result = await _workerService.GetWorkerByIdAsync(TestValues.UserId1);

            // Assert
            Assert.True(result.Id == okResult.Id);
            Assert.True(result.FirstName == okResult.FirstName);
            Assert.True(result.Verified == okResult.Verified);
        }

        [Test]
        public void InviteWorker_401Result()
        {
            // Arrange
            var apiResponse = new ApiProblemDetails
            {
                Status = 401
            };
            _httpTest.RespondWithJson(apiResponse, 401);

            // Assert
            Assert.ThrowsAsync<UnauthorizedApiRequestException>(() => _workerService.GetWorkerByIdAsync(TestValues.UserId1));            
        }

    
        [Test]
        public void InviteWorker_403Result()
        {
            // Arrange
            var apiResponse = new ApiProblemDetails
            {
                Status = 403
            };
            _httpTest.RespondWithJson(apiResponse, 403);
                                    
            // Assert
            Assert.ThrowsAsync<ForbiddenApiRequestException>(() => _workerService.GetWorkerByIdAsync(TestValues.UserId1));
        }

        [Test]
        public void InviteWorker_500Result()
        {
            // Arrange
            var apiResponse = new ApiProblemDetails
            {
                Status = 500
            };
            _httpTest.RespondWithJson(apiResponse, 500);
                                    
            // Assert
            Assert.ThrowsAsync<ApiException>(() => _workerService.GetWorkerByIdAsync(TestValues.UserId1));
        }
                
        #endregion

        #region GetWorkerByEmail tests

        [Test]
        public async Task GetWorkerByEmail_OkResult()
        {
            // Arrange
            var okResult = new BaseWorker
            {
                Id = "1",                
                FirstName = "First",
                LastName = "Last"                
            };

            _httpTest.RespondWithJson(okResult);


            // Arrange            
            _httpTest.RespondWith(TestValues.InviteId1);

            var result = await _workerService.GetWorkerByEmailAsync(TestValues.Worker1Email);

            // Assert
            Assert.True(result.Id == okResult.Id);
            Assert.True(result.FirstName == okResult.FirstName);           
        }

        [Test]
        public void GetWorkerByEmail_401Result()
        {
            // Arrange
            var apiResponse = new ApiProblemDetails
            {
                Status = 401
            };
            _httpTest.RespondWithJson(apiResponse, 401);

            // Assert
            Assert.ThrowsAsync<UnauthorizedApiRequestException>(() => _workerService.GetWorkerByEmailAsync(TestValues.Worker1Email));
        }

        [Test]
        public void GetWorkerByEmail_400Result()
        {
            // Arrange
            var apiResponse = new ApiProblemDetails
            {
                Status = 400
            };
            _httpTest.RespondWithJson(apiResponse, 400);

            // Assert
            Assert.ThrowsAsync<InvalidApiRequestException>(() => _workerService.GetWorkerByEmailAsync(TestValues.Worker1Email));
        }

        [Test]
        public void GetWorkerByEmail_403Result()
        {
            // Arrange
            var apiResponse = new ApiProblemDetails
            {
                Status = 403
            };
            _httpTest.RespondWithJson(apiResponse, 403);

            // Assert
            Assert.ThrowsAsync<ForbiddenApiRequestException>(() => _workerService.GetWorkerByEmailAsync(TestValues.Worker1Email));
        }

        [Test]
        public void GetWorkerByEmail_500Result()
        {
            // Arrange
            var apiResponse = new ApiProblemDetails
            {
                Status = 500
            };
            _httpTest.RespondWithJson(apiResponse, 500);

            // Assert
            Assert.ThrowsAsync<ApiException>(() => _workerService.GetWorkerByEmailAsync(TestValues.Worker1Email));
        }
                
        [Test]
        public void GetWorkerByEmail_EmailInvalid()
        {
            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(() => _workerService.GetWorkerByEmailAsync(TestValues.NotAnEmail));

            Assert.True(ex.ExceptionDetails.Errors.ContainsKey("email"));
            var startError = ex.ExceptionDetails.Errors["email"];
            Assert.True(startError.Contains(Constants.ErrorMessages.EmailInvalid));
        }
        #endregion

        #region Nudge Worker tests
        
        [Test]
        public async Task NudgeWorker_OkResult()
        {
            var result = await _workerService.NudgeWorkerAsync(TestValues.UserId1, TestValues.UserId2);

            // Assert
            Assert.True(result);            
        }       

        [Test]
        public void NudgeWorker_401Result()
        {
            // Arrange
            var apiResponse = new ApiProblemDetails
            {
                Status = 401
            };
            _httpTest.RespondWithJson(apiResponse, 401);

            // Assert
            Assert.ThrowsAsync<UnauthorizedApiRequestException>(() => _workerService.NudgeWorkerAsync(TestValues.UserId1, TestValues.UserId2));
        }

        [Test]
        public void NudgeWorker_400Result()
        {
            // Arrange
            var apiResponse = new ApiProblemDetails
            {
                Status = 400
            };
            _httpTest.RespondWithJson(apiResponse, 400);

            // Assert
            Assert.ThrowsAsync<InvalidApiRequestException>(() => _workerService.NudgeWorkerAsync(TestValues.UserId1, TestValues.UserId2));
        }

        [Test]
        public void NudgeWorker_403Result()
        {
            // Arrange
            var apiResponse = new ApiProblemDetails
            {
                Status = 403
            };
            _httpTest.RespondWithJson(apiResponse, 403);

            // Assert
            Assert.ThrowsAsync<ForbiddenApiRequestException>(() => _workerService.NudgeWorkerAsync(TestValues.UserId1, TestValues.UserId2));
        }

        [Test]
        public void NudgeWorker_429Result()
        {
            // Arrange
            var apiResponse = new ApiProblemDetails
            {
                Status = 429
            };
            _httpTest.RespondWithJson(apiResponse, 403);

            // Assert
            Assert.ThrowsAsync<ForbiddenApiRequestException>(() => _workerService.NudgeWorkerAsync(TestValues.UserId1, TestValues.UserId2));
        }

        [Test]
        public void NudgeWorker_500Result()
        {
            // Arrange
            var apiResponse = new ApiProblemDetails
            {
                Status = 500
            };
            _httpTest.RespondWithJson(apiResponse, 500);

            // Assert
            Assert.ThrowsAsync<ApiException>(() => _workerService.NudgeWorkerAsync(TestValues.UserId1, TestValues.UserId2));
        }

        [Test]
        public void NudgeWorker_ValueNotSpecified()
        {
            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(() => _workerService.NudgeWorkerAsync(string.Empty, TestValues.UserId2));

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