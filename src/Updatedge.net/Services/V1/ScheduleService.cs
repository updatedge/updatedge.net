using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Updatedge.Common.Models.Availability;
using Updatedge.net.Configuration;

namespace Updatedge.net.Services.V1
{
    public class ScheduleService : BaseService, IScheduleService
    {
        public ScheduleService(IUpdatedgeConfiguration config) : base(config)
        {
        }

        /// <summary>
        /// Submits a request for a schedule url to display bookings and offers for an agency's hirers
        /// </summary>
        /// <param name="request">Request data</param>
        /// <returns>An async task that returns the url with token when complete</returns>
        public virtual async Task<string> GetTeachersAvailabilityPublicUrl(HirerScheduleUrlRequest request)
        {
            try
            {
                var result = await BaseUrl
                    .AppendPathSegment("/token/rota/preview")
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
    }
}
