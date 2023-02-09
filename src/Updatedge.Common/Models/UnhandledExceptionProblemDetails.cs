namespace Updatedge.Common.Models
{
    /// <summary>
    /// Global unhandled exception problem details handler
    /// </summary>
    public class UnhandledExceptionProblemDetails : BaseApiProblemDetails
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UnhandledExceptionProblemDetails()
        {
            Title = "Internal platform error";
            Type = "https://apidocs.updatedge.com/docs/errors#internal-platform-error";
            Detail = "The api encountered an internal error when executing your request on the platform.";
            Status = 500;
        }
    }
}
