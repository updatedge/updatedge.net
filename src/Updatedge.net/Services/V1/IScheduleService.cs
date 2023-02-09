using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Updatedge.Common.Models.Availability;

namespace Updatedge.net.Services.V1
{
    public interface IScheduleService
    {
        Task<string> GetTeachersAvailabilityPublicUrl(HirerScheduleUrlRequest request);
    }
}
