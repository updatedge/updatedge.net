using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Updatedge.Common.Models.Availability;
using Updatedge.Common.Validation;
using Updatedge.net.Configuration;
using Updatedge.net.Entities.V1;
using Updatedge.net.Exceptions;

namespace Updatedge.net.Services.V1
{
    public class AvailabilityService : BaseService, IAvailabilityService
    {
        public AvailabilityService(IUpdatedgeConfiguration config) : base(config)
        {
        }

        public async virtual Task<List<WorkerAvailabilityIntervals>> GetAvailabilityDailyAsync
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
                
                var result =  await BaseUrl
                    .AppendPathSegment("availability/getperdailyinterval")
                    .SetQueryParam("api-version", ApiVersion)
                    .SetQueryParam("start", start.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"))
                    .SetQueryParam("end", end.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"))
                    .SetQueryParam("daysToRepeat", daysToRepeat)
                    .WithHeader(ApiKeyName, ApiKey)
                    .PostJsonAsync(workerIds)
                    .ReceiveJson<OkApiResult<List<WorkerAvailabilityIntervals>>>();

                return result.Data;
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }

        public async virtual Task<List<WorkerOverallAvailability>> GetTotalAvailability(WorkersIntervalsRequest request)
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

                var result =  await BaseUrl
                    .AppendPathSegment("availability/getoverallacrossintervals")
                    .SetQueryParam("api-version", ApiVersion)
                    .WithHeader(ApiKeyName, ApiKey)
                    .PostJsonAsync(request)
                    .ReceiveJson<OkApiResult<List<WorkerOverallAvailability>>>();

                return result.Data;
            }
            catch (FlurlHttpException flEx)
            {                
                throw await flEx.Handle();
            }
        }

        public async virtual Task<string> GetTeachersAvailabilityPublicUrl(WorkersAvailabilityUrlRequest request)
        {
            // deprecated - superceded by GetHirerTimesheetPublicUrl
            try
            {
                var result = await BaseUrl
                    .AppendPathSegment("/token/availability/preview")
                    .SetQueryParam("api-version", ApiVersion)
                    .WithHeader(ApiKeyName, ApiKey)
                    .PostJsonAsync(request)
                    .ReceiveString();

                return result;
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }

        public async virtual Task<string> GetHirerTimesheetPublicUrl(HirerTimesheetUrlRequest request)
        {
            try
            {
                var result = await BaseUrl
                    .AppendPathSegment("/token/availability/preview")
                    .SetQueryParam("api-version", ApiVersion)
                    .WithHeader(ApiKeyName, ApiKey)
                    .PostJsonAsync(request)
                    .ReceiveString();

                return result;
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }

        public async virtual Task<string> GetWorkerTimesheetPublicUrl(WorkerTimesheetUrlRequest request)
        {
            try
            {
                var result = await BaseUrl
                    .AppendPathSegment("/token/availability/preview/worker")
                    .SetQueryParam("api-version", ApiVersion)
                    .WithHeader(ApiKeyName, ApiKey)
                    .PostJsonAsync(request)
                    .ReceiveString();

                return result;
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }

        public async virtual Task<AvailabilityForRangeResponse> GetAvailabilityForRange(AvailabilityForRangeRequest request)
        {
            try
            {
                // VALIDATION ------------------------------

                var validator = new RequestValidator(
                   new IntervalValidations(request.StartDate, request.EndDate).StartEndSpecified().LessThanXHours(24),
                   new WorkerIdValidations(request.WorkerIds).ContainsWorkerIds());

                if (validator.HasErrors) throw new ApiWrapperException(validator.ToDetails());

                // ------------------------------------------

                var result = await BaseUrl
                    .AppendPathSegment("/availability/getAvailabilityForRange")
                    .SetQueryParam("api-version", ApiVersion)
                    .WithHeader(ApiKeyName, ApiKey)
                    .PostJsonAsync(request)
                    .ReceiveJson<OkApiResult<AvailabilityForRangeResponse>>();

                return result.Data;
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }
    }
}
