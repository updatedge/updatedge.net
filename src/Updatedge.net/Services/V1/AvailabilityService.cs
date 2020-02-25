using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Updatedge.net.Entities.V1;
using Updatedge.net.Entities.V1.Availability;
using Updatedge.net.Exceptions;

namespace Updatedge.net.Services.V1
{
    public class AvailabilityService : BaseService
    {
        public AvailabilityService(string baseUrl, string apiKey) : base(baseUrl, apiKey)
        {
        }

        public async virtual Task<OkApiResult<List<WorkerAvailabilityIntervals>>> GetAvailabilityDailyAsync
            (DateTimeOffset start, DateTimeOffset end, int daysToRepeat, IEnumerable<string> workerIds)
        {
            try
            {               
                return await BaseUrl
                    .AppendPathSegment("availability/getperdailyinterval")
                    .SetQueryParam("api-version", ApiVersion)
                    .SetQueryParam("start", start.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"))
                    .SetQueryParam("end", end.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"))
                    .SetQueryParam("daysToRepeat", daysToRepeat)
                    .WithHeader(ApiKeyName, ApiKey)
                    .PostJsonAsync(workerIds)
                    .ReceiveJson<OkApiResult<List<WorkerAvailabilityIntervals>>>();
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }

        public async virtual Task<OkApiResult<List<WorkerTotalAvailability>>> GetTotalAvailability(TotalAvailabilityRequest request)
        {
            try
            {
               return await BaseUrl
                    .AppendPathSegment("availability/getoverallacrossintervals")
                    .SetQueryParam("api-version", ApiVersion)
                    .WithHeader(ApiKeyName, ApiKey)
                    .PostJsonAsync(request)
                    .ReceiveJson<OkApiResult<List<WorkerTotalAvailability>>>();
            }
            catch (FlurlHttpException flEx)
            {
                throw flEx;
                throw await flEx.Handle();
            }
        }
    }
}
