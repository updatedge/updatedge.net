using Flurl;
using Flurl.Http;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Udatedge.Common.Models.Offer;
using Udatedge.Common.Validation;
using Updatedge.net.Entities.V1;
using Updatedge.net.Exceptions;

namespace Updatedge.net.Services.V1
{
    public class OfferService : BaseService, IOfferService
    {
        public OfferService(string baseUrl, string apiKey) : base(baseUrl, apiKey)
        {
        }
                
        public async virtual Task<string> CreateOfferAsync(CreateOffer offer)
        {
            try
            {
                // VALIDATION ------------------------------

                var validator = new RequestValidator(
                 new CreateOfferValidation(offer).TitleNotNullOrEmpty().CreatedByUserIdNotNullOrEmpty(),
                 new WorkerIdValidations(offer.WorkerIds).ContainsWorkerIds(),
                 new IntervalValidations(offer.Events, nameof(offer.Events)).StartInFuture().StartEndSpecified().EndsAfterStart()
                 );

                // ------------------------------------------

                if (validator.HasErrors) throw new ApiWrapperException(validator.ToDetails());

                var response = await BaseUrl
                    .AppendPathSegment("offer")
                    .SetQueryParam("api-version", ApiVersion)                                       
                    .WithHeader(ApiKeyName, ApiKey)
                    .PostJsonAsync(offer)
                    .ReceiveJson<OkApiResult<string>>();

                return response.Data;
                
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }
                
        public async virtual Task<Offer> GetOfferAsync(string id)
        {
            try
            {
                // VALIDATION ------------------------------

                var validator = new RequestValidator(
                     new StringValidation(id, nameof(id)).IsNotNullOrEmpty());

                // ------------------------------------------

                if (validator.HasErrors) throw new ApiWrapperException(validator.ToDetails());

                var response = await BaseUrl
                    .AppendPathSegment($"offer/{id}")
                    .SetQueryParam("api-version", ApiVersion)                    
                    .WithHeader(ApiKeyName, ApiKey)
                    .GetAsync();

                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<Offer>(responseContent);
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }

        public async virtual Task<bool> WithdrawOfferAsync(string id)
        {
            try
            {
                // VALIDATION ------------------------------

                var validator = new RequestValidator(
                     new StringValidation(id, nameof(id)).IsNotNullOrEmpty());

                // ------------------------------------------

                if (validator.HasErrors) throw new ApiWrapperException(validator.ToDetails());

                var response = await BaseUrl
                    .AppendPathSegment($"offer/{id}")
                    .SetQueryParam("api-version", ApiVersion)
                    .WithHeader(ApiKeyName, ApiKey)
                    .GetAsync();
                
                return response.IsSuccessStatusCode;
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }

        public async virtual Task<bool> CompleteOfferAsync(string id, IEnumerable<string> workerIds)
        {
            try
            {
                // VALIDATION ------------------------------

                var validator = new RequestValidator(
                    new WorkerIdValidations(workerIds).ContainsWorkerIds(),
                    new StringValidation(id, nameof(id)).IsNotNullOrEmpty());                

                // ------------------------------------------

                if (validator.HasErrors) throw new ApiWrapperException(validator.ToDetails());

                var response = await BaseUrl
                    .AppendPathSegment($"offer/{id}/complete")
                    .SetQueryParam("api-version", ApiVersion)
                    .WithHeader(ApiKeyName, ApiKey)
                    .PostJsonAsync(workerIds)
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
