using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Updatedge.Common.Models.Organisation;
using Updatedge.Common.Models.TimelineEvents;
using Updatedge.Common.Models.Users;
using Updatedge.Common.Validation;
using Updatedge.net.Configuration;
using Updatedge.net.Exceptions;

namespace Updatedge.net.Services.V1
{
    public class OrganisationService : BaseService, IOrganisationService
    {
        public OrganisationService(IUpdatedgeConfiguration config) : base(config)
        {
        }

        public async virtual Task<byte[]> GetExternalLogo(string domain)
        {
            try
            {
                // VALIDATION ------------------------------

                var validator = new RequestValidator(
                     new StringValidation(domain, nameof(domain)).IsNotNullOrEmpty());

                // ------------------------------------------

                if (validator.HasErrors) throw new ApiWrapperException(validator.ToDetails());

                domain = domain.Trim('/').Replace("www.", string.Empty).Replace("/", "_");

                return await BaseUrl
                    .AppendPathSegment($"organisations/external/logo/{domain}")
                    .SetQueryParam("api-version", ApiVersion)
                    .WithHeader(ApiKeyName, ApiKey)
                    .GetBytesAsync();
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }

        public async virtual Task UploadExternalLogo(string domain, string base64Image)
        {
            try
            {
                // VALIDATION ------------------------------

                var validator = new RequestValidator(
                     new StringValidation(domain, nameof(domain)).IsNotNullOrEmpty(),
                     new StringValidation(base64Image, nameof(base64Image)).IsNotNullOrEmpty());

                // ------------------------------------------

                if (validator.HasErrors) throw new ApiWrapperException(validator.ToDetails());

                var model = new UploadExternalImage { ImageBase64 = base64Image };

                domain = domain.Trim('/').Replace("www.", string.Empty).Replace("/", "_");

                var result = await BaseUrl
                    .AppendPathSegment($"organisations/external/logo/{domain}/upload")
                    .SetQueryParam("api-version", ApiVersion)
                    .WithHeader(ApiKeyName, ApiKey)
                    .PutJsonAsync(model);
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }

        /// <summary>
        /// Updates the representation statuses of a list of workers
        /// </summary>
        /// <param name="statuses">The workerIds and representation status of each</param>
        /// <returns>An async task that returns when complete</returns>
        public virtual async Task UpdateRepresentedByAsync(List<RepresentationStatus> statuses)
        {
            try
            {
                var response = await BaseUrl
                    .AppendPathSegment($"organisations/representation")
                    .SetQueryParam("api-version", ApiVersion)
                    .WithHeader(ApiKeyName, ApiKey)
                    .PutJsonAsync(statuses);
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }

        public async virtual Task<List<OrganisationIdentityMapping>> GetAllOrganisationIdentityMappingsAsync()
        {
            try
            {
                //var response = await BaseUrl
                //    .AppendPathSegment($"organisations/identityMappings2")
                //    .SetQueryParam("api-version", ApiVersion)
                //    .WithHeader(ApiKeyName, ApiKey)
                //    .GetAsync(); // get all records at once to avoid System.ObjectDisposedException
                //                 //.ReceiveJson<List<OrganisationIdentityMapping>>();
                //if (response.StatusCode != HttpStatusCode.OK)
                //    return new List<OrganisationIdentityMapping>();

                //var responseContent = await response.Content.ReadAsStringAsync();
                //return JsonSerializer.Deserialize<List<OrganisationIdentityMapping>>(responseContent, JsonOptions);

                // In BaseService constructor, add:
                // protected readonly HttpClient HttpClient = new HttpClient();

                var url = $"{BaseUrl.TrimEnd('/')}/organisations/identityMappings?api-version={ApiVersion}";

                var request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Get, url);
                request.Headers.Add(ApiKeyName, ApiKey);

                HttpClient HttpClient = new HttpClient();
                var response = await HttpClient.SendAsync(request);

                if (response.StatusCode != HttpStatusCode.OK)
                    return new List<OrganisationIdentityMapping>();

                var responseContent = await response.Content.ReadAsStringAsync();

                // Deserialize the Flurl wrapper object first
                var wrapper = JsonSerializer.Deserialize<OrganisationIdentityMappingWrapper>(responseContent, JsonOptions);
                var mappings = wrapper?.Data ?? new List<OrganisationIdentityMapping>();

                return mappings;
            }
            catch (FlurlHttpException flEx)
            {
                throw await flEx.Handle();
            }
        }
        private class OrganisationIdentityMappingWrapper
        {
            public List<Updatedge.Common.Models.Organisation.OrganisationIdentityMapping> Data { get; set; }
        }
    }
}
