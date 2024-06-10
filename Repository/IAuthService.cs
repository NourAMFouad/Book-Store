using System.IdentityModel.Tokens.Jwt;
using Book_store_1_.DTOs;
using Book_store_1_.Models;
using Microsoft.AspNetCore.Http;

namespace Book_store_1_.Repository
{
    public interface IAuthService
    {
        Task<RegistrationResult> RegisterAdminAsync(ApplicationUserdto model);

        Task<RegistrationResult> RegisterMemberAsync(ApplicationUserdto model);

        Task<AuthModel> LoginAsync(Logindto model);

        // Task<AuthModel> LogoutAsync(Logindto mode);

        Task<List<ApplicationUser>> GetAdminUserAsync();

        Task<List<ApplicationUser>> GetMemberUserAsync();

        Task AddTokenInBlacklistAsync(BlacklistTokendto dto);
        
        Task<bool> IsTokeninBlacklist(string token);



    }
}

/*public interface ITokenBlacklistService
{
    Task AddTokenToBlacklistAsync(string token, DateTime expiryDate);
    Task<bool> IsTokenBlacklistedAsync(string token);
}

public class TokenBlacklistService : ITokenBlacklistService
{
    private readonly ApplicationDbContext _context;

    public TokenBlacklistService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddTokenToBlacklistAsync(string token, DateTime expiryDate)
    {
        var blacklistedToken = new TokenBlacklist
        {
            Token = token,
            ExpiryDate = expiryDate
        };
        _context.TokenBlacklists.Add(blacklistedToken);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsTokenBlacklistedAsync(string token)
    {
        return await _context.TokenBlacklists.AnyAsync(t => t.Token == token);
    }
}
*/