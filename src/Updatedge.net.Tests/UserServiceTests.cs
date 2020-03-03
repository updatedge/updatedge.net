using NUnit.Framework;
using System.Threading.Tasks;
using Updatedge.net.Services.V1;
using Flurl.Http.Testing;
using Updatedge.net.Exceptions;
using AutoFixture;
using System.Collections.Generic;
using System.Linq;
using Udatedge.Common.Models.Users;
using System.Text.Json;
using Udatedge.Common;

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

            FixtureConfig.Fixture.Customize<User>(u => u.With(u => u.Email, FixtureConfig.User1Email));
            FixtureConfig.Fixture.Customize<CreateUser>(u => u.With(u => u.Email, FixtureConfig.User1Email));
        }


        #region GetAll tests
        [Test]
        public async Task GetAll_OkResult()
        {
            // Arrange     
            var okResult = FixtureConfig.Fixture.Create<IEnumerable<User>>();
            _httpTest.RespondWithJson(okResult, 200);
            
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
        public void GetAll_500Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails500, 500);
                                    
            // Assert
            Assert.ThrowsAsync<ApiException>(() => _userService.GetAllAsync());
        }

        [Test]
        public async Task GetAll_NoUsersReturned()
        {
            // Arrange            
            _httpTest.RespondWith(string.Empty, 204);

            // Act            
            var result = await _userService.GetAllAsync();

            // Assert
            Assert.True(result.Count() == 0);
        }
        #endregion


        #region GetById tests
        [Test]
        public async Task GetById_OkResult()
        {
            // Arrange     
            var okResult = FixtureConfig.Fixture.Create<User>();
            _httpTest.RespondWithJson(okResult, 200);

            var result = await _userService.GetByIdAsync(FixtureConfig.Fixture.Create<string>());

            // Assert
            Assert.True(result.Id == okResult.Id);
            Assert.True(result.FirstName == okResult.FirstName);
            Assert.True(result.LastName == okResult.LastName);
            Assert.True(result.Email == okResult.Email);            
        }

        [Test]
        public void GetById_401Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails401, 401);

            // Assert
            Assert.ThrowsAsync<UnauthorizedApiRequestException>(() => _userService.GetByIdAsync(FixtureConfig.Fixture.Create<string>()));
        }
                
        [Test]
        public void GetById_403Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails403, 403);

            // Assert
            Assert.ThrowsAsync<ForbiddenApiRequestException>(() => _userService.GetByIdAsync(FixtureConfig.Fixture.Create<string>()));
        }

        [Test]
        public void GetById_500Result()
        {
            // Arrange            
            _httpTest.RespondWithJson(FixtureConfig.ApiProblemDetails500, 500);

            // Assert
            Assert.ThrowsAsync<ApiException>(() => _userService.GetByIdAsync(FixtureConfig.Fixture.Create<string>()));
        }

        [Test]
        public async Task GetById_NoUserReturned()
        {
            // Arrange            
            _httpTest.RespondWith(string.Empty, 200);

            // Act            
            var result = await _userService.GetByIdAsync(FixtureConfig.Fixture.Create<string>());

            // Assert
            Assert.True(result == null);
        }
        #endregion

        #region GetByEmail tests
        [Test]
        public async Task GetByEmail_OkResult()
        {
            // Arrange     
            var okResult = FixtureConfig.Fixture.Create<User>();
            _httpTest.RespondWithJson(okResult, 200);

            var result = await _userService.GetByEmailAsync(FixtureConfig.User1Email);

            // Assert
            Assert.True(result.Id == okResult.Id);
            Assert.True(result.FirstName == okResult.FirstName);
            Assert.True(result.LastName == okResult.LastName);
            Assert.True(result.Email == okResult.Email);
        }

        [Test]
        public void GetByEmail_401Result()
        {
            // Arrange            
            _httpTest.RespondWith(JsonSerializer.Serialize(FixtureConfig.ApiProblemDetails401), 401);

            // Assert
            Assert.ThrowsAsync<UnauthorizedApiRequestException>(() => _userService.GetByEmailAsync(FixtureConfig.User1Email));
        }

        [Test]
        public void GetByEmail_400Result()
        {
            // Arrange            
            _httpTest.RespondWith(JsonSerializer.Serialize(FixtureConfig.ApiProblemDetails400), 400);

            // Assert
            Assert.ThrowsAsync<InvalidApiRequestException>(() => _userService.GetByEmailAsync(FixtureConfig.User1Email));
        }
               
        [Test]
        public void GetByEmail_500Result()
        {
            // Arrange            
            _httpTest.RespondWith(JsonSerializer.Serialize(FixtureConfig.ApiProblemDetails500), 500);

            // Assert
            Assert.ThrowsAsync<ApiException>(() => _userService.GetByEmailAsync(FixtureConfig.User1Email));
        }

        [Test]
        public async Task GetByEmail_NoUserReturned()
        {
            // Arrange            
            _httpTest.RespondWith(string.Empty, 204);

            // Act            
            var result = await _userService.GetByEmailAsync(FixtureConfig.User1Email);

            // Assert
            Assert.True(result == null);
        }
        #endregion

        #region CreateUser tests
        [Test]
        public async Task CreateUser_OkResult()
        {
            // Arrange                             
            var okResult = FixtureConfig.Fixture.Create<string>();
            _httpTest.RespondWith(okResult, 200);

            var result = await _userService.CreateUserAsync(FixtureConfig.Fixture.Create<CreateUser>());

            // Assert
            Assert.True(result == okResult);            
        }

        [Test]
        public void CreateUser_401Result()
        {
            // Arrange            
            _httpTest.RespondWith(JsonSerializer.Serialize(FixtureConfig.ApiProblemDetails401), 401);

            // Assert
            Assert.ThrowsAsync<UnauthorizedApiRequestException>(() => _userService.CreateUserAsync(FixtureConfig.Fixture.Create<CreateUser>()));
        }

        [Test]
        public void CreateUser_400Result()
        {
            // Arrange            
            _httpTest.RespondWith(JsonSerializer.Serialize(FixtureConfig.ApiProblemDetails400), 400);

            // Assert
            Assert.ThrowsAsync<InvalidApiRequestException>(() => _userService.CreateUserAsync(FixtureConfig.Fixture.Create<CreateUser>()));
        }

        [Test]
        public void CreateUser_500Result()
        {
            // Arrange            
            _httpTest.RespondWith(JsonSerializer.Serialize(FixtureConfig.ApiProblemDetails500), 500);

            // Assert
            Assert.ThrowsAsync<ApiException>(() => _userService.CreateUserAsync(FixtureConfig.Fixture.Create<CreateUser>()));
        }

        [Test]
        public void CreateUser_EmailInvalid()
        {
            // Arrange
            var user = FixtureConfig.Fixture.Build<CreateUser>()                    
                    .With(o => o.Email, FixtureConfig.NotAnEmail)
                    .Create();

            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(async () =>
            {
                var result = await _userService.CreateUserAsync(user);
            });

            Assert.True(ex.ExceptionDetails.Errors.ContainsKey("email"));
            var workerIdsErrors = ex.ExceptionDetails.Errors["email"];
            Assert.True(workerIdsErrors.Contains(Constants.ErrorMessages.EmailInvalid));
        }

        [Test]
        public void CreateUser_ValueNotSpecified()
        {
            // Arrange
            var user = FixtureConfig.Fixture.Create<CreateUser>();
            user.FirstName = null;
            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(async () =>
            {
                var result = await _userService.CreateUserAsync(user);
            });

            Assert.True(ex.ExceptionDetails.Errors.ContainsKey("firstname"));
            var workerIdsErrors = ex.ExceptionDetails.Errors["firstname"];
            Assert.True(workerIdsErrors.Contains(Constants.ErrorMessages.ValueNotSpecified));
        }

        #endregion

        #region UpdateUser tests
        [Test]
        public async Task UpdateUser_OkResult()
        {
            // Arrange                             
            var okResult = FixtureConfig.Fixture.Create<string>();
            _httpTest.RespondWith(okResult, 204);

            var result = await _userService.UpdateUserAsync(FixtureConfig.Fixture.Create<User>());

            // Assert
            Assert.True(result);
        }

        [Test]
        public void UpdateUser_401Result()
        {
            // Arrange            
            _httpTest.RespondWith(JsonSerializer.Serialize(FixtureConfig.ApiProblemDetails401), 401);

            // Assert
            Assert.ThrowsAsync<UnauthorizedApiRequestException>(() => _userService.UpdateUserAsync(FixtureConfig.Fixture.Create<User>()));
        }

        [Test]
        public void UpdateUser_403Result()
        {
            // Arrange            
            _httpTest.RespondWith(JsonSerializer.Serialize(FixtureConfig.ApiProblemDetails403), 403);

            // Assert
            Assert.ThrowsAsync<ForbiddenApiRequestException>(() => _userService.UpdateUserAsync(FixtureConfig.Fixture.Create<User>()));
        }

        [Test]
        public void UpdateUser_400Result()
        {
            // Arrange            
            _httpTest.RespondWith(JsonSerializer.Serialize(FixtureConfig.ApiProblemDetails400), 400);

            // Assert
            Assert.ThrowsAsync<InvalidApiRequestException>(() => _userService.UpdateUserAsync(FixtureConfig.Fixture.Create<User>()));
        }

        [Test]
        public void UpdateUser_500Result()
        {
            // Arrange            
            _httpTest.RespondWith(JsonSerializer.Serialize(FixtureConfig.ApiProblemDetails500), 500);

            // Assert
            Assert.ThrowsAsync<ApiException>(() => _userService.UpdateUserAsync(FixtureConfig.Fixture.Create<User>()));
        }

        [Test]
        public void UpdateUser_EmailInvalid()
        {
            // Arrange
            var user = FixtureConfig.Fixture.Build<User>()
                    .With(o => o.Email, FixtureConfig.NotAnEmail)
                    .Create();

            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(async () =>
            {
                var result = await _userService.UpdateUserAsync(user);
            });

            Assert.True(ex.ExceptionDetails.Errors.ContainsKey("email"));
            var workerIdsErrors = ex.ExceptionDetails.Errors["email"];
            Assert.True(workerIdsErrors.Contains(Constants.ErrorMessages.EmailInvalid));
        }

        [Test]
        public void UpdateUser_ValueNotSpecified()
        {
            // Arrange
            var user = FixtureConfig.Fixture.Create<User>();
            user.FirstName = null;
            // Assert            
            var ex = Assert.ThrowsAsync<ApiWrapperException>(async () =>
            {
                var result = await _userService.UpdateUserAsync(user);
            });

            Assert.True(ex.ExceptionDetails.Errors.ContainsKey("firstname"));
            var workerIdsErrors = ex.ExceptionDetails.Errors["firstname"];
            Assert.True(workerIdsErrors.Contains(Constants.ErrorMessages.ValueNotSpecified));
        }
        #endregion

        #region DeleteUser tests
        [Test]
        public async Task DeleteUser_OkResult()
        {
            // Arrange                             
            var okResult = FixtureConfig.Fixture.Create<string>();
            _httpTest.RespondWith(okResult, 204);

            var result = await _userService.UpdateUserAsync(FixtureConfig.Fixture.Create<User>());

            // Assert
            Assert.True(result);
        }

        [Test]
        public void DeleteUser_401Result()
        {
            // Arrange            
            _httpTest.RespondWith(JsonSerializer.Serialize(FixtureConfig.ApiProblemDetails401), 401);

            // Assert
            Assert.ThrowsAsync<UnauthorizedApiRequestException>(() => _userService.UpdateUserAsync(FixtureConfig.Fixture.Create<User>()));
        }

        [Test]
        public void DeleteUser_403Result()
        {
            // Arrange            
            _httpTest.RespondWith(JsonSerializer.Serialize(FixtureConfig.ApiProblemDetails403), 403);

            // Assert
            Assert.ThrowsAsync<ForbiddenApiRequestException>(() => _userService.UpdateUserAsync(FixtureConfig.Fixture.Create<User>()));
        }

        [Test]
        public void DeleteUser_500Result()
        {
            // Arrange            
            _httpTest.RespondWith(JsonSerializer.Serialize(FixtureConfig.ApiProblemDetails500), 500);

            // Assert
            Assert.ThrowsAsync<ApiException>(() => _userService.UpdateUserAsync(FixtureConfig.Fixture.Create<User>()));
        }
        #endregion

        [TearDown]
        public void DisposeHttpTest()
        {
            _httpTest.Dispose();
        }
    }
}