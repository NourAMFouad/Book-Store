using System.IdentityModel.Tokens.Jwt;
using Book_store_1_.DTOs;
using Book_store_1_.Models;

namespace Book_store_1_.Repository
{
    public interface IAuthService
    {
        Task<RegistrationResult> RegisterAsync(ApplicationUserdto model);

        Task<AuthModel> LoginAsync(Logindto model);

    }
}