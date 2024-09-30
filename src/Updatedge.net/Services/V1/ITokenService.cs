using System.Threading.Tasks;

namespace Updatedge.net.Services.V1
{
    public interface ITokenService
    {
        Task<string> GetAuthTokenAsync(string userId);
    }
}