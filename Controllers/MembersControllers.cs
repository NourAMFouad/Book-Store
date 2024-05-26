
using AutoMapper;
using Book_store_1_.DTOs;
using Book_store_1_.Models;
using Book_store_1_.Repository;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Book_store_1_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MembersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly IBaseRepository<Member, Memberdto> _memberRepository;

        private readonly IMapper _mapper;
        
        // constructor 
        public MembersController (ApplicationDbContext context, IBaseRepository<Member, Memberdto> memberRepository, IMapper mapper){
            _context = context;
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        // Get all Members 
        [HttpGet]
        public IActionResult GetAll(){
            var Members = _memberRepository.GetAll();
            return Ok(Members);
        }


        // Post all Memebers
        [HttpPost]
        public IActionResult CreateNewMember(Memberdto dto)
        {
            var Member = _memberRepository.Add(dto);

            return Ok(Member);
            
        }
    }
}