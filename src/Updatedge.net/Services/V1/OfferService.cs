using Flurl;
using Flurl.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Updatedge.Common.Models.Offer;
using Updatedge.Common.Validation;
using Updatedge.net.Configuration;
using Updatedge.net.Exceptions;

namespace Updatedge.net.Services.V1
{
    public class OfferService : BaseService, IOfferService
    {
        public OfferService(IUpdatedgeConfiguration config) : base(config)
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
                    .ReceiveString();

                return response;

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

                return await BaseUrl
                    .AppendPathSegment($"offer/{id}")
                    .SetQueryParam("api-version", ApiVersion)
                    .WithHeader(ApiKeyName, ApiKey)
                    .GetJsonAsync<Offer>();
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }

        public virtual async Task<bool> WithdrawOfferAsync(string id)
        {
            try
            {
                // VALIDATION ------------------------------

                var validator = new RequestValidator(
                     new StringValidation(id, nameof(id)).IsNotNullOrEmpty());

                // ------------------------------------------

                if (validator.HasErrors) throw new ApiWrapperException(validator.ToDetails());

                var response = await BaseUrl
                    .AppendPathSegment($"offer/{id}/withdraw")
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

        public virtual async Task<bool> WithdrawOfferAsync(OfferWithdraw request)
        {
            try
            {
                // VALIDATION ------------------------------

                var validator = new RequestValidator(
                     new StringValidation(request.Id, nameof(request.Id)).IsNotNullOrEmpty(),
                     new StringValidation(request.Reason, nameof(request.Reason)).IsNotNullOrEmpty()
                );

                // ------------------------------------------

                if (validator.HasErrors) throw new ApiWrapperException(validator.ToDetails());

                var response = await BaseUrl
                    .AppendPathSegment($"offer/{request.Id}/withdraw")
                    .SetQueryParam("api-version", ApiVersion)
                    .WithHeader(ApiKeyName, ApiKey)
                    .PostJsonAsync(request);

                return response.IsSuccessStatusCode;
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }

        public virtual async Task<bool> DeleteEventFromOfferAsync(EventDelete request)
        {
            try
            {
                // VALIDATION ------------------------------

                var validator = new RequestValidator(
                    new StringValidation(request.OfferId, nameof(request.OfferId)).IsNotNullOrEmpty(),
                    new StringValidation(request.UserId, nameof(request.UserId)).IsNotNullOrEmpty()
                );

                // ------------------------------------------

                if (validator.HasErrors) throw new ApiWrapperException(validator.ToDetails());

                var response = await BaseUrl
                    .AppendPathSegment($"offer/{request.OfferId}/event")
                    .SetQueryParam("api-version", ApiVersion)
                    .WithHeader(ApiKeyName, ApiKey)
                    .SendJsonAsync(HttpMethod.Delete, request);

                return response.IsSuccessStatusCode;
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }

        public virtual async Task<bool> DeleteEventsFromOfferAsync(List<EventDelete> request)
        {
            try
            {
                var response = await BaseUrl
                    .AppendPathSegment($"offer/{request.First().OfferId}/events")
                    .SetQueryParam("api-version", ApiVersion)
                    .WithHeader(ApiKeyName, ApiKey)
                    .SendJsonAsync(HttpMethod.Delete, request);

                return response.IsSuccessStatusCode;
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }


        public virtual async Task<bool> WithdrawOfferFromWorkerAsync(string id, string workerId)
        {
            try
            {
                // VALIDATION ------------------------------

                var validator = new RequestValidator(
                    new StringValidation(id, nameof(id)).IsNotNullOrEmpty());

                // ------------------------------------------

                if (validator.HasErrors) throw new ApiWrapperException(validator.ToDetails());

                var response = await BaseUrl
                    .AppendPathSegment($"offer/{id}/withdraw/{workerId}")
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

        public virtual async Task<bool> AlterOfferAsync(string id, AlterOffer alterations)
        {
            try
            {
                // VALIDATION ------------------------------

                var validator = new RequestValidator(
                     new WorkerIdValidations(alterations.WorkerIds).ContainsWorkerIds(),
                     new StringValidation(id, nameof(id)).IsNotNullOrEmpty());

                // ------------------------------------------

                if (validator.HasErrors) throw new ApiWrapperException(validator.ToDetails());

                var response = await BaseUrl
                    .AppendPathSegment($"offer/{id}")
                    .SetQueryParam("api-version", ApiVersion)
                    .WithHeader(ApiKeyName, ApiKey)
                    .PutJsonAsync(alterations)
                    .ReceiveString();

                return true;

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
