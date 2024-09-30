using System;
using System.Collections.Generic;
using System.Text;

namespace Updatedge.Common.Models.Workers
{
    public class EndScreeningPeriodModel
    {
        /// <summary>
        /// Id of the verification record (if deleteing by id)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Type of the verification record (if deleteing by type+workerContactId)
        /// </summary>
        public VerificationType VerificationType { get; set; }

        /// <summary>
        /// Id of the verified contact (if deleteing by type+workerContactId)
        /// </summary>
        public string WorkerContactId { get; set; }

        /// <summary>
        /// The end date of the verification
        /// </summary>
        public DateTimeOffset End { get; set; }
    }
}
