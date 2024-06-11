using Book_store_1_.Models;
using Book_store_1_.DTOs;
using Book_store_1_.Repository;
using AutoMapper;



namespace Book_store_1_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        private readonly IBaseRepository<Category, Categorydto> _categoryRepository;

        private readonly IMapper _mapper;
    

        public CategoryController(ApplicationDbContext context, IBaseRepository<Category, Categorydto> categoryRepository, IMapper mapper){
            _context = context;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        // endpoint: to allow user to display all categories from database
        [HttpGet]
        public IActionResult GetAll(){
            var Category = _categoryRepository.GetAll();
            return Ok(Category);
        }




        // endpoint: display categories with pagination concept
        [HttpGet("Page")]
        public IActionResult GetAllInPaging([FromQuery] PagesParameter parameter)
        {    
            var categories =  _categoryRepository.GetAllInPages(parameter);
            return Ok(categories);
        }



        


        // endpoint: to take category name and add new category
        [HttpPost]
        public IActionResult CreateNewCategory(Categorydto categoryDto,string categName){
            // create new instance from dto 

            categoryDto.CategoryName = categName;
            var category = _categoryRepository.Add(categoryDto);
            return Ok(category);
        } 



        // endpoint: to update in specific Id 
        [HttpPut("{id}")]
        public IActionResult UpdateCategoryById (byte id,[FromBody] Categorydto dto){
            // var category = _categoryRepository.GetById(id) ;
            // if(category == null)
            //      return NotFound($"No Category was found with Id: {id}"); 
            
            // update dto in record that match with value from url 
            dto.CategoryId = id;
            _categoryRepository.Update(dto); 

            return Ok();
        }

        // endpoint: to delete specific category by id 
        [HttpDelete("{id}")]
        public IActionResult DeleteCategoryById(byte id)
        {
            var category = _categoryRepository.GetById(id);

            if (category == null){
                return NotFound($"No Category found with ID {id}");
            }
            
            _categoryRepository.Delete(category);

            return Ok();
            
        }
    }
}