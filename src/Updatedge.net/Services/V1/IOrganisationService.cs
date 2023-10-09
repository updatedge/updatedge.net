using System.Collections.Generic;
using System.Threading.Tasks;
using Updatedge.Common.Models.Organisation;

namespace Updatedge.net.Services.V1
{
    public interface IOrganisationService
    {
        Task<byte[]> GetExternalLogo(string domain);
        Task UpdateRepresentedByAsync(List<RepresentationStatus> statuses);
        Task UploadExternalLogo(string domain, string base64Image);
    }
}