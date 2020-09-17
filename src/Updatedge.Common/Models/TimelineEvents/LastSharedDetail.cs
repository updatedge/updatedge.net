using System;
using System.Collections.Generic;
using System.Text;

namespace Updatedge.Common.Models.TimelineEvents
{
    public class LastSharedDetail
    {
        public string WorkerId { get; set; }

        public DateTimeOffset LastShared { get; set; }
    }
}
