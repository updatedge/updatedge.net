using NUnit.Framework;
using System.Threading.Tasks;
using Updatedge.net.Services.V1;
using Flurl.Http.Testing;
using Updatedge.net.Exceptions;
using Udatedge.Common;
using Udatedge.Common.Models;

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

            // instantiate the Availability service
            _inviteService = new InviteService(TestValues.BaseUrl, TestValues.ApiKey);
            
        }


        #region InviteWorker tests
        [Test]
        public async Task InviteWorker_OkResult()
        {            
            // Arrange            
            _httpTest.RespondWith(TestValues.InviteId1);
            
            var result = await _inviteService.InviteWorkerAsync(TestValues.UserId1, TestValues.Worker1Email);

            // Assert
            Assert.True(result == TestValues.InviteId1);            
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
            Assert.ThrowsAsync<UnauthorizedApiRequestException>(() => _inviteService.InviteWorkerAsync(TestValues.UserId1, TestValues.Worker1Email));            
        }

        [Test]
        public void InviteWorker_400Result()
        {
            // Arrange
            var apiResponse = new ApiProblemDetails
            {
                Status = 400
            };
            _httpTest.RespondWithJson(apiResponse, 400);
            
            // Assert
            Assert.ThrowsAsync<InvalidApiRequestException>(() => _inviteService.InviteWorkerAsync(TestValues.UserId1, TestValues.Worker1Email));            
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
            Assert.ThrowsAsync<ForbiddenApiRequestException>(() => _inviteService.InviteWorkerAsync(TestValues.UserId1, TestValues.Worker1Email));
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
            Assert.ThrowsAsync<ApiException>(() => _inviteService.InviteWorkerAsync(TestValues.UserId1, TestValues.Worker1Email));
        }

        [Test]
        public void InviteWorker_ValueNotSpecified()
        {              
            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(() => _inviteService.InviteWorkerAsync(string.Empty, TestValues.Worker1Email));

            Assert.True(ex.ExceptionDetails.Errors.ContainsKey("fromuserid"));
            var startError = ex.ExceptionDetails.Errors["fromuserid"];            
            Assert.True(startError.Contains(Constants.ErrorMessages.ValueNotSpecified));
        }

        [Test]
        public void InviteWorker_EmailInvalid()
        {
            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(() => _inviteService.InviteWorkerAsync(TestValues.UserId1, "#thisisnotanemail"));

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
            _httpTest.RespondWith(TestValues.InviteId1);

            var result = await _inviteService.InviteHirerAsync(TestValues.UserId1, TestValues.Hirer1Email);

            // Assert
            Assert.True(result == TestValues.InviteId1);
        }

        [Test]
        public void InviteHirer_401Result()
        {
            // Arrange
            var apiResponse = new ApiProblemDetails
            {
                Status = 401
            };
            _httpTest.RespondWithJson(apiResponse, 401);

            // Assert
            Assert.ThrowsAsync<UnauthorizedApiRequestException>(() => _inviteService.InviteHirerAsync(TestValues.UserId1, TestValues.Hirer1Email));
        }

        [Test]
        public void InviteHirer_400Result()
        {
            // Arrange
            var apiResponse = new ApiProblemDetails
            {
                Status = 400
            };
            _httpTest.RespondWithJson(apiResponse, 400);

            // Assert
            Assert.ThrowsAsync<InvalidApiRequestException>(() => _inviteService.InviteHirerAsync(TestValues.UserId1, TestValues.Hirer1Email));
        }

        [Test]
        public void InviteHirer_403Result()
        {
            // Arrange
            var apiResponse = new ApiProblemDetails
            {
                Status = 403
            };
            _httpTest.RespondWithJson(apiResponse, 403);

            // Assert
            Assert.ThrowsAsync<ForbiddenApiRequestException>(() => _inviteService.InviteHirerAsync(TestValues.UserId1, TestValues.Hirer1Email));
        }

        [Test]
        public void InviteHirer_500Result()
        {
            // Arrange
            var apiResponse = new ApiProblemDetails
            {
                Status = 500
            };
            _httpTest.RespondWithJson(apiResponse, 500);

            // Assert
            Assert.ThrowsAsync<ApiException>(() => _inviteService.InviteHirerAsync(TestValues.UserId1, TestValues.Hirer1Email));
        }

        [Test]
        public void InviteHirer_ValueNotSpecified()
        {
            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(() => _inviteService.InviteHirerAsync(string.Empty, TestValues.Hirer1Email));

            Assert.True(ex.ExceptionDetails.Errors.ContainsKey("fromuserid"));
            var startError = ex.ExceptionDetails.Errors["fromuserid"];
            Assert.True(startError.Contains(Constants.ErrorMessages.ValueNotSpecified));
        }

        [Test]
        public void InviteHirer_EmailInvalid()
        {
            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(() => _inviteService.InviteHirerAsync(TestValues.UserId1, "#thisisnotanemail"));

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