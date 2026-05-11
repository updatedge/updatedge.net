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
        public WorkerIdValidations(IEnumerable<string> ids, IEnumerable<CreateOffer.WorkerDetails> workers)
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
            var hasIds = Ids != null && Ids.Any();
            var hasWorkers = Workers != null && Workers.Any();

            if (!hasWorkers && !hasIds)
            {
                Add("workerIds", Constants.ErrorMessages.NoWorkerIdsSpecified);
                return this;
            }

            // If Workers is populated, validate each worker has Id or Name
            if (hasWorkers)
            {
                foreach (var worker in Workers)
                {
                    // each worker must have either an Id or Name or Email specified
                    if (string.IsNullOrWhiteSpace(worker.Id) &&
                        string.IsNullOrWhiteSpace(worker.Name) && 
                        string.IsNullOrWhiteSpace(worker.Email))                    
                    {
                        Add("workers", "Each worker must have either an Id or Name or Email specified");
                        break;
                    }
                }
            }

            return this;
        }

    }
}
