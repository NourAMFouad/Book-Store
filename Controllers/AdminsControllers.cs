using AutoMapper;
using Book_store_1_.DTOs;
using Book_store_1_.Models;
using Book_store_1_.Repository;
using Microsoft.AspNetCore.Http;

namespace Book_store_1_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly IBaseRepository<Admin, Admindto> _adminRepository;
   
        private readonly IMapper _mapper;

 
        public AdminController (ApplicationDbContext context, IBaseRepository<Admin, Admindto> adminRepository, IMapper mapper)
        {
            _context = context;
            _adminRepository = adminRepository;
            _mapper = mapper;
        }

        // add getAll function 
        // endpoint: to allow user to display all Admins info from database
        [HttpGet]
        public  IActionResult GetAllAdmins()
        {
            var Admin = _adminRepository.GetAll();
            
            return Ok(Admin);
        }


        
        // Post all Memebers
        [HttpPost]
        public IActionResult CreateNewAdmin(Admindto dto)
        {
            var Admin =  _adminRepository.Add(dto);
            return Ok(Admin);
            
        }

    }

}
