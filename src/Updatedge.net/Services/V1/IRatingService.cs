using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Updatedge.Common.Models.Availability;
using Updatedge.Common.Models.Rating;

namespace Updatedge.net.Services.V1
{
    public interface IRatingService
    {
        /// <summary>
        /// Get the url to show public webpage of worker passed the the request
        /// </summary>
        /// <param name="request">The workers and school urn from the school metadata service</param>
        /// <returns>string with public accessable URL</returns>
        /// <exception cref="FlurlHttpException">Thrown if the https response is not in the 2xx range.</exception>
        Task<string> PostExternalRatings(SyncRatingsRequest request);
    }
}
