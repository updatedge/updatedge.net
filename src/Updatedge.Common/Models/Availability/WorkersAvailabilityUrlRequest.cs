namespace Updatedge.Common.Models.Availability
{
    public class WorkersAvailabilityUrlRequest
    {
        public string metadatasource { get; set; }

        public string urn { get; set; }

        public string[] workerIds { get; set; }
    }
}
