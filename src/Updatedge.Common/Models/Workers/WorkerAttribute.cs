using System;
using System.Collections.Generic;
using System.Text;

namespace Updatedge.Common.Models.Workers
{
    public class WorkerAttribute
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int SectorId { get; set; }
        public int SkillId { get; set; }
        public int SubSectorId { get; set; }
        public string SectorName { get; set; }
        public string SkillName { get; set; }
        public string SubSectorName { get; set; }
    }
}
