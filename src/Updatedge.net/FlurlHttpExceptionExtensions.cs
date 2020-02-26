using Flurl.Http;
using System;
using System.Threading.Tasks;
using Updatedge.net.Exceptions;

namespace Updatedge.net
{
    public static class FlurlHttpExceptionExtensions
    {
        public static async Task<Exception> Handle(this FlurlHttpException exception)
        {
            var bodyContent = await exception.Call.Response.Content.ReadAsStringAsync();

            // unauthorized
            if (exception.Call.HttpStatus == System.Net.HttpStatusCode.Unauthorized)
            {
                return new UnauthorizedApiRequestException(bodyContent);
            }

            // nothing found
            if (exception.Call.HttpStatus == System.Net.HttpStatusCode.BadRequest)
            {
                return new InvalidApiRequestException(bodyContent);
            }

            return new ApiException(bodyContent);
        }
    }
}
