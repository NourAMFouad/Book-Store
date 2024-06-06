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

         [HttpPost("Admin Registeration")]
        public async Task<IActionResult> RegisterAdminAsync(ApplicationUserdto model)
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
            var result = await _authService.RegisterAdminAsync(model);

            if (result == null ){
                return BadRequest();
            }

            return Ok(result);
        }


        [HttpPost("Member Registeration")]
        public async Task<IActionResult> RegisterMemberAsync(ApplicationUserdto model)
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
            var result = await _authService.RegisterMemberAsync(model);

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
        

        [HttpGet("Admins")]
        public async Task<IActionResult> GetAdmin(){
            var admins = await _authService.GetAdminUserAsync();
            return Ok(admins);
        }

        [HttpGet("Members")]
         public async Task<IActionResult> GetMember(){
            var admins = await _authService.GetMemberUserAsync();
            return Ok(admins);
        }

    }
}