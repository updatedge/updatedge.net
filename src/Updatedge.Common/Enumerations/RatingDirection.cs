using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Updatedge.Common.Enumerations
{
    public enum RatingDirection
    {
        Worker_HirersOrganisation = 1,
        Worker_OrganisationsHirer = 2,
        Worker_Agency = 3,
        Worker_AgencysConsultant = 4,
        HirersOrganisation_Worker = 5,
        HirersOrganisation_Agency = 6,
        HirersOrganisation_AgencysConsultant = 7,
        OrganisationsHirer_Worker = 8,
        OrganisationsHirer_Agency = 9,
        OrganisationsHirer_AgencysConsultant = 10,
        Agency_HirersOrganisation = 11,
        Agency_Worker = 12,
        Agency_OrganisationsHirer = 13,
        AgencysConsultant_HirersOrganisation = 14,
        AgencysConsultant_Worker = 15,
        AgencysConsultant_OrganisationsHirer = 16
    }
}