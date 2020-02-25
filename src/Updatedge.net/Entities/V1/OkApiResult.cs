using System.Collections.Generic;

namespace Updatedge.net.Entities.V1
{
    /// <summary>
    /// An Ok Api result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OkApiResult<T>
    {
        /// <summary>
        /// The response data
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Errors with the request
        /// </summary>
        public List<ApiResultError> Errors { get; set; }
    }
}