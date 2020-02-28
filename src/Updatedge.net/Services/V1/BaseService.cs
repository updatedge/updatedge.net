using Light.GuardClauses;

namespace Updatedge.net.Services.V1
{
    public class BaseService
    {
        protected readonly string BaseUrl;

        protected readonly string ApiKey;

        protected readonly string ApiVersion = "1.0";

        protected readonly string ApiKeyName = "X-UE-Api-Subscription-Key";

        public BaseService(string baseUrl, string apiKey)
        {
            BaseUrl = baseUrl.MustNotBeNullOrEmpty(nameof(baseUrl));
            ApiKey = apiKey.MustNotBeNullOrEmpty(nameof(apiKey));
        }
    }
}
