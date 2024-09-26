namespace Updatedge.Common.Models.Workers
{
    /// <summary>
    /// Base worker model
    /// </summary>
    public class BaseWorker
    {
        /// <summary>
        /// The worker's id
        /// </summary>
        public string Id { get; set; }

        public string ContactId { get; set; }

        /// <summary>
        /// The worker's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The worker's last name
        /// </summary>BaseWorker
        public string LastName { get; set; }
    }
}
