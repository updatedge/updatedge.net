using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Updatedge.net.Exceptions;

namespace Updatedge.net
{
    public static class FlurlHttpExceptionExtensions
    {
        public static async Task<Exception> Handle(this FlurlHttpException exception)
        {
            var bodyContent = await exception.Call.Response.Content.ReadAsStringAsync();

            // nothing found
            if (exception.Call.HttpStatus == System.Net.HttpStatusCode.BadRequest)
            {
                return new InvalidApiRequestException(bodyContent);
            }

            return new ApiException(bodyContent);
        }
    }
}
