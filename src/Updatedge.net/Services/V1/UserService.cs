using Flurl;
using Flurl.Http;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Udatedge.Common.Models.Users;
using Udatedge.Common.Validation;
using Updatedge.net.Exceptions;

namespace Updatedge.net.Services.V1
{
    public class UserService : BaseService, IUserService
    {
        public UserService(string baseUrl, string apiKey) : base(baseUrl, apiKey)
        {
        }
                
        public async virtual Task<IEnumerable<User>> GetAllAsync()
        {
            try
            {
                var response = await BaseUrl
                    .AppendPathSegment("users")
                    .SetQueryParam("api-version", ApiVersion)
                    .WithHeader(ApiKeyName, ApiKey)
                    .GetAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return new List<User>();
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<User>>(responseContent, JsonOptions);
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }
                
        public async virtual Task<User> GetByIdAsync(string id)
        {
            try
            {
                // VALIDATION ------------------------------

                var validator = new RequestValidator(
                     new StringValidation(id, nameof(id)).IsNotNullOrEmpty());

                // ------------------------------------------

                if (validator.HasErrors) throw new ApiWrapperException(validator.ToDetails());

                return await BaseUrl
                    .AppendPathSegment($"users/byId/{id}")
                    .SetQueryParam("api-version", ApiVersion)                    
                    .WithHeader(ApiKeyName, ApiKey)
                    .GetJsonAsync<User>();                
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }

        public async virtual Task<User> GetByEmailAsync(string email)
        {
            try
            {
                // VALIDATION ------------------------------

                var validator = new RequestValidator(
                     new StringValidation(email, nameof(email)).IsNotNullOrEmpty().IsEmail());

                // ------------------------------------------

                if (validator.HasErrors) throw new ApiWrapperException(validator.ToDetails());

                var response = await BaseUrl
                    .AppendPathSegment($"users/byEmail/{email}")
                    .SetQueryParam("api-version", ApiVersion)
                    .WithHeader(ApiKeyName, ApiKey)
                    .GetAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return null;
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<User>(responseContent, JsonOptions);
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }

        public async virtual Task<string> CreateUserAsync(CreateUser user)
        {
            try
            {
                // VALIDATION ------------------------------

                var validator = new RequestValidator(
                    new StringValidation(user.Email, nameof(user.Email)).IsNotNullOrEmpty().IsEmail(),
                    new StringValidation(user.FirstName, nameof(user.FirstName)).IsNotNullOrEmpty(),
                    new StringValidation(user.LastName, nameof(user.LastName)).IsNotNullOrEmpty()
                    );
                

                // ------------------------------------------

                if (validator.HasErrors) throw new ApiWrapperException(validator.ToDetails());

                return await BaseUrl
                    .AppendPathSegment("users")
                    .SetQueryParam("api-version", ApiVersion)
                    .WithHeader(ApiKeyName, ApiKey)
                    .PostJsonAsync(user)
                    .ReceiveJson<string>();                                
                           
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }

        public async virtual Task<bool> UpdateUserAsync(User user)
        {
            try
            {
                // VALIDATION ------------------------------

                var validator = new RequestValidator(
                new UpdateUserValidation(user).IdNotNullOrEmptyAndCorrectFormat().EmailNotNullOrEmptyAndCorrectFormat()
                                                .FirstNameNotNullOrEmpty().LastNameNotNullOrEmpty()
                );

                // ------------------------------------------

                if (validator.HasErrors) throw new ApiWrapperException(validator.ToDetails());

                var response = await BaseUrl
                    .AppendPathSegment("users")
                    .SetQueryParam("api-version", ApiVersion)
                    .WithHeader(ApiKeyName, ApiKey)
                    .PutJsonAsync(user)
                    .ReceiveString();

                return true;
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }

        public async virtual Task<bool> DeleteUserAsync(string id)
        {
            try
            {
                // VALIDATION ------------------------------

                var validator = new RequestValidator(
                     new StringValidation(id, nameof(id)).IsNotNullOrEmpty());

                // ------------------------------------------

                if (validator.HasErrors) throw new ApiWrapperException(validator.ToDetails());

                var response = await BaseUrl
                    .AppendPathSegment($"users/{id}")
                    .SetQueryParam("api-version", ApiVersion)
                    .WithHeader(ApiKeyName, ApiKey)
                    .DeleteAsync()
                    .ReceiveString();

                return true;
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }
    }
}
