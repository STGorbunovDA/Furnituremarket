using Furnituremarket.Domain.Response;
using Furnituremarket.Domain.ViewModels.Account;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Furnituremarket.Service.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);

        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);
    }
}
