using NUnit.Framework;
using System.Threading.Tasks;
using Updatedge.net.Services.V1;
using Flurl.Http.Testing;
using Updatedge.net.Exceptions;
using AutoFixture;
using System.Collections.Generic;
using System.Linq;
using Udatedge.Common.Models.Users;

namespace Updatedge.net.Tests
{
    public class UserServiceTests
    {
        private HttpTest _httpTest;
        private IUserService _userService;        
                        
        [SetUp]
        public void Setup()
        {
            // Put Flurl into test mode
            _httpTest = new HttpTest();
            
            // register and create offer service
            FixtureConfig.Fixture.Register<IUserService>(() => new UserService(FixtureConfig.BaseUrl, FixtureConfig.ApiKey));
            _userService = FixtureConfig.Fixture.Create<IUserService>();           
        }


        #region GetAll tests
        [Test]
        public async Task GetAll_OkResult()
        {
            // Arrange     
            var okResult = FixtureConfig.Fixture.Create<IEnumerable<User>>();
            _httpTest.RespondWithJson(okResult);
            
            var result = await _userService.GetAllAsync();

            // Assert
            Assert.True(result.Count() == okResult.Count());            
        }

        [Test]
        public void GetAll_401Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails401, 401);

            // Assert
            Assert.ThrowsAsync<UnauthorizedApiRequestException>(() => _userService.GetAllAsync());            
        }

        [Test]
        public void GetAll_400Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails400, 400);
            
            // Assert
            Assert.ThrowsAsync<InvalidApiRequestException>(() => _userService.GetAllAsync());            
        }

        [Test]
        public void GetAll_403Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails403, 403);
                                    
            // Assert
            Assert.ThrowsAsync<ForbiddenApiRequestException>(() => _userService.GetAllAsync());
        }

        [Test]
        public void GetAll_500Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails500, 500);
                                    
            // Assert
            Assert.ThrowsAsync<ApiException>(() => _userService.GetAllAsync());
        }

        [Test]
        public async Task CreateOffer_NoUsersReturned()
        {
            // Arrange            
            _httpTest.RespondWith(string.Empty, 204);

            // Act            
            var result = await _userService.GetAllAsync();

            // Assert
            Assert.True(result.Count() == 0);
        }        

        #endregion

        [TearDown]
        public void DisposeHttpTest()
        {
            _httpTest.Dispose();
        }
    }
}