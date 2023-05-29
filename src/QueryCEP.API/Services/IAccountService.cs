using System.Threading.Tasks;
using QueryCEP.API.Dtos;

namespace QueryCEP.API.Services
{
    public interface IAccountService
    {
        Task<RoleResponse> CreateRoleAsync(RoleRequest request);
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
    }
}