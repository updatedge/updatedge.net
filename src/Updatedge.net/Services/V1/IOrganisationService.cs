using System.Threading.Tasks;

namespace Updatedge.net.Services.V1
{
    public interface IOrganisationService
    {
        Task<byte[]> GetExternalLogo(string domain);
        Task UploadExternalLogo(string domain, string base64Image);
    }
}