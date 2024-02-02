using System;
using System.Collections.Generic;
using System.Text;

namespace Updatedge.Common.Models.Availability
{
    public class HirerTimesheetUrlRequest : WorkersAvailabilityUrlRequest
    {
        // additional payment and summary fields necessary for hirer timesheet 

        /// <summary>
        /// total amount charged for all workers combined
        /// </summary>
        public double TotalGrossCharge { get; set; }

        /// <summary>
        /// Currency of gross pay
        /// </summary>
        public string Currency { get; set; }

        /// <summary> 
        /// Total Hours by all Workers
        /// </summary>
        public double TotalHours { get; set; }
    }
}
