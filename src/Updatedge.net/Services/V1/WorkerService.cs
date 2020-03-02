using Flurl;
using Flurl.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Udatedge.Common.Models.Workers;
using Udatedge.Common.Validation;
using Updatedge.net.Exceptions;

namespace Updatedge.net.Services.V1
{
    public class WorkerService : BaseService, IWorkerService
    {
        public WorkerService(string baseUrl, string apiKey) : base(baseUrl, apiKey)
        {
        }
                
        public async virtual Task<Worker> GetWorkerByIdAsync(string id)
        {
            try
            {
                // VALIDATION ------------------------------

                var validator = new RequestValidator(
                     new StringValidation(id, nameof(id)).IsNotNullOrEmpty());

                // ------------------------------------------

                if (validator.HasErrors) throw new ApiWrapperException(validator.ToDetails());

                return await BaseUrl
                    .AppendPathSegment($"workers/{id}")
                    .SetQueryParam("api-version", ApiVersion)
                    .SetQueryParam("id", id)
                    .WithHeader(ApiKeyName, ApiKey)
                    .GetJsonAsync<Worker>();               
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }

        public async virtual Task<BaseWorker> GetWorkerByEmailAsync(string email)
        {
            try
            {
                // VALIDATION ------------------------------

                var validator = new RequestValidator(                     
                     new StringValidation(email, nameof(email)).IsNotNullOrEmpty().IsEmail());

                // ------------------------------------------

                if (validator.HasErrors) throw new ApiWrapperException(validator.ToDetails());

                var response = await BaseUrl
                    .AppendPathSegment($"workers/getbyemail")
                    .SetQueryParam("api-version", ApiVersion)
                    .SetQueryParam("email", email)
                    .WithHeader(ApiKeyName, ApiKey)
                    .GetAsync();

                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return new BaseWorker();
                }

                return JsonSerializer.Deserialize<Worker>(responseContent, JsonOptions);
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }

        public async virtual Task<bool> NudgeWorkerAsync(string fromUserId, string toWorkerId)
        {
            try
            {
                // VALIDATION ------------------------------

                var validator = new RequestValidator(
                    new StringValidation(fromUserId, nameof(fromUserId)).IsNotNullOrEmpty(),
                    new StringValidation(toWorkerId, nameof(toWorkerId)).IsNotNullOrEmpty()
                );

                // ------------------------------------------

                if (validator.HasErrors) throw new ApiWrapperException(validator.ToDetails());

                var response = await BaseUrl
                    .AppendPathSegment($"workers/{fromUserId}/nudge")
                    .SetQueryParam("api-version", ApiVersion)
                    .SetQueryParam("fromuserid", fromUserId)
                    .SetQueryParam("toworkerid", toWorkerId)
                    .WithHeader(ApiKeyName, ApiKey)
                    .GetAsync();

                return true;
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }

    }
}
