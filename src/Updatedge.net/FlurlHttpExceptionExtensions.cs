using Flurl.Http;
using System;
using System.Threading.Tasks;
using Updatedge.net.Exceptions;
using System.Text.Json;
using Udatedge.Common.Models;
using Microsoft.AspNetCore.Http;

namespace Updatedge.net
{
    public static class FlurlHttpExceptionExtensions
    {
        public static async Task<Exception> Handle(this FlurlHttpException exception)
        {
            var bodyContent = await exception.Call.Response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions();

            options.PropertyNameCaseInsensitive = true;
            var apiProblemDetails = JsonSerializer.Deserialize<ApiProblemDetails>(bodyContent, options);

            // Bad Request
            if (exception.Call.HttpStatus == System.Net.HttpStatusCode.BadRequest)
            {                
                return new InvalidApiRequestException(apiProblemDetails.Detail);
            }

            // forbidden
            if (exception.Call.HttpStatus == System.Net.HttpStatusCode.Forbidden)
            {                
                return new ForbiddenApiRequestException(apiProblemDetails.Detail);
            }

            // too many requests
            if (exception.Call.HttpStatus.ToString().ToLower() == "toomanyrequests")
            {
                return new ThrottledApiRequestException(apiProblemDetails.Detail);
            }

            // unauthorized
            if (exception.Call.HttpStatus == System.Net.HttpStatusCode.Unauthorized)
            {
                return new UnauthorizedApiRequestException(apiProblemDetails.Detail);
            }

            return new ApiException(bodyContent);
        }
    }
}
