﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Updatedge.Common.Models.Offer
{
    public class EventDelete
    {
        public string OfferId { get; set; }
        public string UserId { get; set; }
        public string Reason { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
    }

    public class EventDeleteOnDay
    {
        public DateTimeOffset Date { get; set; }
    }
}
