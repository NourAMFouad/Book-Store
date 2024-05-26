using Book_store_1_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
        // alternative services (repository directory)
        // adding constructor 
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

        // endpoint: to take category name and add new category
        [HttpPost]
        public IActionResult CreateNewCategory(Categorydto categoryDto){
            // create new instance from dto 
            var category = _categoryRepository.Add(categoryDto);
            return Ok(category);
        } 



        // endpoint: to update in specific Id 
        [HttpPut("{id}")]
        public IActionResult UpdateCategoryById (byte id,[FromBody] Categorydto dto){
            var category = _categoryRepository.GetById(id) ;
            if(category == null)
                 return NotFound($"No Category was found with Id: {id}"); 
            
            // update dto in record that match with value from url 
            category.CategoryName = dto.Name;
            _categoryRepository.Update(dto); 

            return Ok(category);
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