using System;

namespace Updatedge.net.Entities.V1.Availability
{
    public class WorkerTotalAvailability
    {
        public string WorkerId { get; set; }

        public float PercentageAvailable { get; set; }

        /// <summary>
        /// Inference breakdown.
        /// </summary>
        public Inference Inference { get; set; }

        /// <summary>
        /// Point in time at which user last shared
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
