using Light.GuardClauses;
using System.Text.Json;
using Updatedge.net.Configuration;

namespace Updatedge.net.Services.V1
{
    public class BaseService
    {
        protected readonly string BaseUrl;

        protected readonly string ApiKey;

        protected readonly string ApiVersion = "1.0";

        protected readonly string ApiKeyName = "X-UE-Api-Subscription-Key";
        protected readonly JsonSerializerOptions JsonOptions;

        public BaseService(IUpdatedgeConfiguration config)
        {
            BaseUrl = config.BaseUrl.MustNotBeNullOrEmpty(nameof(config.BaseUrl));
            ApiKey = config.ApiKey.MustNotBeNullOrEmpty(nameof(config.ApiKey));

            JsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }
    }
}
