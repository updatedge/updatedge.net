using System;
using System.Collections.Generic;
using System.Text;

namespace Updatedge.Common.Models.Workers
{
    public class Qualification
    {
        public int Id { get; set; }
        public string Field { get; set; }
        public string School { get; set; }
        public string Degree { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Grade { get; set; }
        public string Description { get; set; }
        public long StartDateTimestamp { get; set; }
        public long EndDateTimestamp { get; set; }

    }
}
