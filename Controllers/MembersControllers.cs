
using Book_store_1_.DTOs;
using Book_store_1_.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Book_store_1_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MembersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        // constructor 
        public MembersController (ApplicationDbContext context){
            _context = context;
        }

        // Get all Members 
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(){
            var Members = await _context.Member.ToListAsync();
            return Ok(Members);
        }


        // Post all Memebers
        [HttpPost]
        public async Task<IActionResult> CreateNewMember(Memberdto dto)
        {
            var Member = new Member {
                Username = dto.Username,
                Password = dto.Password,
                NumberOfborrowedBooks = dto.NumberOfborrowedBooks
            };

            await _context.AddAsync(Member);
            _context.SaveChanges();

            return Ok(Member);
            
        }
    }
}