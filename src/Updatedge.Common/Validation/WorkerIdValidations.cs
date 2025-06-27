using System.Collections.Generic;
using System.Linq;
using Updatedge.Common.Models.Offer;

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
        /// Gets the collection of worker details associated with the offer creation process.
        /// </summary>
        public IEnumerable<CreateOffer.WorkerDetails> Workers { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ids">List of worker ids</param>
        public WorkerIdValidations(IEnumerable<string> ids, IEnumerable<CreateOffer.WorkerDetails> workers = null)
        {
            Ids = ids;
            Workers = workers;
        }

        /// <summary>
        /// Ensures an enumerable string collection contains at least one entry
        /// </summary>
        /// <returns></returns>
        public WorkerIdValidations ContainsWorkers()
        {
            // Check if either Workers or Ids contains at least one entry
            if (!(Workers?.Any() ?? false) && (Ids == null || !Ids.Any()))
            {
                Add("workerIds", Constants.ErrorMessages.NoWorkerIdsSpecified);
            }

            return this;
        }

    }
}
