using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Udatedge.Common.Models.Availability;
using Udatedge.Common.Validation;
using Updatedge.net.Entities.V1;
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
                // VALIDATION ------------------------------

                var validator = new RequestValidator(
                    new IntervalValidations(start, end)
                        .StartEndSpecified()
                        .LessThanXHours(24)
                        .EndsAfterStart(),
                    new WorkerIdValidations(workerIds).ContainsWorkerIds(),
                    new NumericValidations(daysToRepeat).NumberIsBetweenInclusive(0, 31, nameof(daysToRepeat))
                    );

                if (validator.HasErrors) throw new ApiWrapperException(validator.ToDetails());

                // ------------------------------------------
                
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

        public async virtual Task<OkApiResult<List<WorkerOverallAvailability>>> GetTotalAvailability(WorkersIntervalsRequest request)
        {
            try
            {
                // VALIDATION ------------------------------

                var validator = new RequestValidator(
                    new IntervalValidations(request.Intervals, nameof(request.WorkerIds))
                        .StartEndSpecified()
                        .ContainsIntervals()
                        .ContainsNoOverlappingIntervals()
                        .EndsAfterStart()
                        .LessThanXHours(24),
                    new WorkerIdValidations(request.WorkerIds).ContainsWorkerIds()
                    );

                if (validator.HasErrors) throw new ApiWrapperException(validator.ToDetails());

                // ------------------------------------------

                return await BaseUrl
                    .AppendPathSegment("availability/getoverallacrossintervals")
                    .SetQueryParam("api-version", ApiVersion)
                    .WithHeader(ApiKeyName, ApiKey)
                    .PostJsonAsync(request)
                    .ReceiveJson<OkApiResult<List<WorkerOverallAvailability>>>();
            }
            catch (FlurlHttpException flEx)
            {
                throw flEx;
                throw await flEx.Handle();
            }
        }
    }
}
