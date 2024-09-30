using Light.GuardClauses;
using Updatedge.Common.Models.Users;
using Updatedge.Common;
using Updatedge.net.Configuration;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace Updatedge.net.Services.V1
{
    public class TokenService : BaseService, ITokenService
    {
        public TokenService(IUpdatedgeConfiguration config) : base(config)
        {
        }

        public async virtual Task<string> GetAuthTokenAsync(string userId)
        {
            userId.MustNotBeNullOrEmpty(nameof(userId));

            try
            {
                return await BaseUrl
                    .AppendPathSegment($"organisations/token/{userId}")
                    .SetQueryParam("api-version", ApiVersion)
                    .WithHeader(ApiKeyName, ApiKey)
                    .GetJsonAsync<string>();
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }
    }
}
