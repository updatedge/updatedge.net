using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
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
                // Validate start and end dates
                if (start > end) throw new ApiWrapperException("Start date cannot be after end date");
                if (end > start.AddHours(24)) throw new ApiWrapperException("End date must be within 24 hours of Start date (inclusive).");

                // Validate worker Ids
                if (workerIds.Count() == 0) throw new ApiWrapperException("No worker Ids specficied");

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
