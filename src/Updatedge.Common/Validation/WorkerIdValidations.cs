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
            var hasIds = Ids != null && Ids.Any();
            var hasWorkers = Workers != null && Workers.Any();

            if (!hasWorkers && !hasIds)
            {
                Add("workerIds", Constants.ErrorMessages.NoWorkerIdsSpecified);
                return this;
            }

            // If Workers is populated, validate each worker has Id and Name
            if (hasWorkers)
            {
                foreach (var worker in Workers)
                {
                    if (string.IsNullOrWhiteSpace(worker.Id))
                    {
                        Add("workers", "All workers must have an Id specified");
                        break;
                    }
                    if (string.IsNullOrWhiteSpace(worker.Name))
                    {
                        Add("workers", "All workers must have a Name specified");
                        break;
                    }
                }
            }

            return this;
        }

    }
}
