using Flurl;
using Flurl.Http;
using System.Threading.Tasks;
using Updatedge.Common.Validation;
using Updatedge.net.Configuration;
using Updatedge.net.Exceptions;

namespace Updatedge.net.Services.V1
{
    public class InviteService : BaseService, IInviteService
    {
        public InviteService(IUpdatedgeConfiguration config) : base(config)
        {
        }
                
        public async virtual Task<string> InviteWorkerAsync(string fromUserId, string toWorkerEmail)
        {
            try
            {
                // VALIDATION ------------------------------

                var validator = new RequestValidator(
                     new StringValidation(fromUserId, nameof(fromUserId)).IsNotNullOrEmpty(),
                     new StringValidation(toWorkerEmail, nameof(toWorkerEmail)).IsNotNullOrEmpty().IsEmail());
                                
                // ------------------------------------------

                if (validator.HasErrors) throw new ApiWrapperException(validator.ToDetails());

                var response = await BaseUrl
                    .AppendPathSegment("invite/worker")
                    .SetQueryParam("api-version", ApiVersion)
                    .SetQueryParam("fromuserid", fromUserId)
                    .SetQueryParam("toworkeremail", toWorkerEmail)                    
                    .WithHeader(ApiKeyName, ApiKey)
                    .GetAsync();
                                
                var responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }
                
        public async virtual Task<string> InviteHirerAsync(string fromUserId, string toHirerEmail)
        {
            try
            {
                // VALIDATION ------------------------------

                var validator = new RequestValidator(
                     new StringValidation(fromUserId, nameof(fromUserId)).IsNotNullOrEmpty(),
                     new StringValidation(toHirerEmail, nameof(toHirerEmail)).IsNotNullOrEmpty().IsEmail());

                // ------------------------------------------

                if (validator.HasErrors) throw new ApiWrapperException(validator.ToDetails());

                var response = await BaseUrl
                    .AppendPathSegment("invite/worker")
                    .SetQueryParam("api-version", ApiVersion)
                    .SetQueryParam("fromuserid", fromUserId)
                    .SetQueryParam("tohireremail", toHirerEmail)
                    .WithHeader(ApiKeyName, ApiKey)
                    .GetAsync();

                var responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }
    }
}
