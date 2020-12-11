using System;

namespace Updatedge.Common.Models.Availability
{
    /// <summary>
    /// Holds a calculated value for a worker's overall availability.
    /// </summary>
    public class WorkerOverallAvailability
    {
        /// <summary>
        /// Id of worker
        /// </summary>
        public string WorkerId { get; set; }

        /// <summary>
        /// Percentage availability of the worker.
        /// </summary>
        public double PercentageAvailable { get; set; }

        /// <summary>
        /// Inference breakdown.
        /// </summary>
        public Inference Inference { get; set; }

        /// <summary>
        /// When the worker last shared their availability
        /// </summary>
        public DateTimeOffset? LastShared { get; set; }
    }

    /// <summary>
    /// Inference breakdown
    /// </summary>
    public class Inference
    {
        /// <summary>
        /// Inferred availability detials
        /// </summary>
        public InferenceDetails Availability { get; set; }

        /// <summary>
        /// Inferred unavailability detials
        /// </summary>
        public InferenceDetails Unavailability { get; set; }
    }

    /// <summary>
    /// Inferred availability details.
    /// </summary>
    public class InferenceDetails
    {
        /// <summary>
        /// Whether the calculation has been inferred
        /// </summary>
        public bool Inferred { get; set; }

        /// <summary>
        /// Percentage of availabilty that was inferred across the time period.
        /// </summary>
        public double Percentage { get; set; }
    }
}
