﻿using Flurl;
using Flurl.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Updatedge.Common.Models.Offer;
using Updatedge.Common.Validation;
using Updatedge.net.Configuration;
using Updatedge.net.Entities.V1;
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
