using System;
using System.Collections.Generic;
using System.Text;

namespace Updatedge.Common.Models.TimelineEvents
{
    public class WorkerInferredUnavailabilities
    {
        /// <summary>
        /// Id of worker
        /// </summary>
        public string WorkerId { get; set; }

        public IEnumerable<TimeFrame> Timeframes { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="workerId">Worker id</param>
        /// <param name="timeFrames">Timeframes across which the worker is inferred to be unavailable</param>
        public WorkerInferredUnavailabilities(string workerId, IEnumerable<TimeFrame> timeFrames)
        {
            WorkerId = workerId;
            Timeframes = timeFrames;
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public WorkerInferredUnavailabilities()
        {

        }
    }
}
