using Resources.Request;
using Resources.Response;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IAuthService
    {
        Task<LoginOutputResource> LoginAsync(LoginResource resource);
    }
}