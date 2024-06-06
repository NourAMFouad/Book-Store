using System.IdentityModel.Tokens.Jwt;
using Book_store_1_.DTOs;
using Book_store_1_.Models;

namespace Book_store_1_.Repository
{
    public interface IAuthService
    {
        Task<RegistrationResult> RegisterAdminAsync(ApplicationUserdto model);

        Task<RegistrationResult> RegisterMemberAsync(ApplicationUserdto model);

        Task<AuthModel> LoginAsync(Logindto model);

    }
}