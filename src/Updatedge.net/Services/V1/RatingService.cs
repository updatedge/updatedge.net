using Flurl.Http;
using Flurl;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Updatedge.Common.Models.Rating;
using Updatedge.net.Configuration;

namespace Updatedge.net.Services.V1
{
    public class RatingService : BaseService, IRatingService
    {
        public RatingService(IUpdatedgeConfiguration config) : base(config)
        {
        }

        public async virtual Task<string> PostExternalRatings(SyncRatingsRequest request)
        {
            try
            {
                var result = await BaseUrl
                    .AppendPathSegment("/token/rating/update")
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
