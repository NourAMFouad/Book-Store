using System.Security.Claims;
using Book_store_1_.DTOs;
using Book_store_1_.Models;
using Book_store_1_.Repository;

namespace Book_store_1_.Controllers
{
    public class AuthController :ControllerBase 
    {
       private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

         [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(ApplicationUserdto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            
            var applicationUser = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Username,
                Email = model.Email,
                PasswordHash = model.Password
            };
            var result = await _authService.RegisterAsync(model);

            if (result == null ){
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(Logindto model){
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            
            var result = await _authService.LoginAsync(model);
            return Ok(result);
        }
        

    }
}