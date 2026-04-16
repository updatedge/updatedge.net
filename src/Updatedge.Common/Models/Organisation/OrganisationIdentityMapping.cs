using System;
using System.Collections.Generic;
using System.Text;

namespace Updatedge.Common.Models.Organisation
{
    public class OrganisationIdentityMapping
    {
        public string OrganisationId { get; set; }
        public string IdentitySource { get; set; }
        public string IdentityIdName { get; set; }
        public string IdentityIdValue { get; set; }
    }
}
