namespace Updatedge.Common.Models.Availability
{
    public class TimesheetTerminology
    {
        /// <summary>
        /// Minimum hourly range to apply terminology to
        /// </summary>
        public double MinHours { get; set; }

        /// <summary>
        /// Maximum hourly range to apply terminology to
        /// </summary>
        public double MaxHours { get; set; }

        /// <summary>
        /// Name of terminology
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Whether to merge all events for a given day together when rendering
        /// </summary>
        public bool Merge { get; set; }
    }
}
