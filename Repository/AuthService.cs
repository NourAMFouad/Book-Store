using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Book_store_1_.DTOs;
using Book_store_1_.Helpers;
using Book_store_1_.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Book_store_1_.Repository
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWT _jwt;

         public AuthService (UserManager<ApplicationUser> userManager, IOptions<JWT> jwt){
            _userManager = userManager;
            _jwt = jwt.Value;
         } 

          public async Task<RegistrationResult> RegisterAsync(ApplicationUserdto model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                 return new RegistrationResult("Email is already registered!");


            if (await _userManager.FindByNameAsync(model.Username) is not null)
                 return new RegistrationResult("Username is already registered!");

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new RegistrationResult(errors);
            }

           var addRole = await _userManager.AddToRoleAsync(user, "Admin");

            // Check if adding user to role was successful
        if (!addRole.Succeeded)
        {
            var errors = string.Empty;
            foreach (var error in addRole.Errors)
                errors += $"{error.Description},";
            return new RegistrationResult(errors);
        }

        // Return success message and the user object
        return new RegistrationResult("User registered successfully!");
        }


        // login 
        public async Task<AuthModel> LoginAsync(Logindto model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return new AuthModel {Message = "Username or password valid "};
            }

            var jwtSecurityToken = await CreateJwtToken(user);
             return new AuthModel 
            {
                Message = "Login Successfully",
                IsAuthenticated = true,
                Username = model.Username,
                Email = model.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                ExpiresOn = jwtSecurityToken.ValidTo,
                Roles = new List<string> {"Admin"},
            };

          
        }
        


        // create token 
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

    }
}
