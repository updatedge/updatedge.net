using System.Collections.Generic;
using System.Linq;

namespace Updatedge.Common.Validation
{
    /// <summary>
    /// Validates worker ids
    /// </summary>
    public class WorkerIdValidations : BaseValidations
    {
        /// <summary>
        /// Internal storage for ids
        /// </summary>
        private IEnumerable<string> Ids { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ids">List of worker ids</param>
        public WorkerIdValidations(IEnumerable<string> ids)
        {
            Ids = ids;
        }

        /// <summary>
        /// Ensures an enumerable string collection contains at least one entry
        /// </summary>
        /// <returns></returns>
        public WorkerIdValidations ContainsWorkerIds()
        {
            if (Ids != null && Ids.Any()) return this;

            Add("workerIds", Constants.ErrorMessages.NoWorkerIdsSpecified);
            return this;
        }

    }
}
