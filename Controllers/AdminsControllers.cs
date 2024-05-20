using Book_store_1_.DTOs;
using Book_store_1_.Models;

using Microsoft.AspNetCore.Http;

namespace Book_store_1_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // add constructor 
        public AdminController (ApplicationDbContext context)
        {
            _context = context;
        }

        // add getAll function 
        // endpoint: to allow user to display all Admins info from database
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var Admin = await _context.Admin.ToListAsync();
            return Ok(Admin);
        }


        
        // Post all Memebers
        [HttpPost]
        public async Task<IActionResult> CreateNewMember(Admindto dto)
        {
            var Admin = new Admin {
                Username = dto.Username,
                Password = dto.Password,
            };

            await _context.AddAsync(Admin);
            _context.SaveChanges();

            return Ok(Admin);
            
        }

    }

}



/*
Additional note why you need async and wait ?


*/