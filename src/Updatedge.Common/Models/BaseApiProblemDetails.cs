namespace Updatedge.Common.Models
{
    /// <summary>
    /// Custom problem details object that can also be an action result
    /// </summary>
    public class BaseApiProblemDetails
    {
        /// <summary>
        /// Short summary of the problem type.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// URI reference to a description of the problem type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// More descriptive explanation of the problem type.
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// HTTP status code
        /// </summary>
        public int? Status { get; set; }

    }
}
