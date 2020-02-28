using Flurl;
using Flurl.Http;
using Light.GuardClauses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Udatedge.Common.Models.TimelineEvents;
using Udatedge.Common.Validation;
using Updatedge.net.Exceptions;

namespace Updatedge.net.Services.V1
{
    public class TimelineService : BaseService, ITimelineService
    {
        public TimelineService(string baseUrl, string apiKey) : base(baseUrl, apiKey)
        {
        }
                
        public async virtual Task<List<TimelineEvent>> GetEventsAsync(string workerId, DateTimeOffset start, DateTimeOffset end)
        {
            try
            {
                // VALIDATION ------------------------------

                var validator = new RequestValidator(
                    new IntervalValidations(start, end)
                        .StartEndSpecified()
                        .LessThanXDays(32)
                        .EndsAfterStart()
                    );

                if (validator.HasErrors) throw new ApiWrapperException(validator.ToDetails());

                var response = await BaseUrl
                    .AppendPathSegment($"timeline/{workerId}")
                    .SetQueryParam("api-version", ApiVersion)
                    .SetQueryParam("start", start.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"))
                    .SetQueryParam("end", end.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"))                    
                    .WithHeader(ApiKeyName, ApiKey)
                    .GetAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return new List<TimelineEvent>();
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<TimelineEvent>>(responseContent);
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }

        public async virtual Task<List<TimelineEvent>> GetEventAsync(string id)           
        {
            try 
            {
                // check that we have an Event Id.
                id.MustNotBeNullOrEmpty("Please provide an Event Id.");
                var response = await BaseUrl
                    .AppendPathSegment($"timeline/event/{id}")
                    .SetQueryParam("api-version", ApiVersion)
                    .WithHeader(ApiKeyName, ApiKey)
                    .GetAsync();

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return new List<TimelineEvent>();
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<TimelineEvent>>(responseContent);
                                
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }
    }
}
