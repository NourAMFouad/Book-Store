using Book_store_1_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Book_store_1_.DTOs;



namespace Book_store_1_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        // alternative services (repository directory)
        // adding constructor 
        public CategoryController(ApplicationDbContext context){
            _context = context;
        }

        // endpoint: to allow user to display all categories from database
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(){
            var Category = await _context.Category.ToListAsync();
            return Ok(Category);
        }

        // endpoint: to take category name and add new category
        [HttpPost]
        public async Task<IActionResult> CreateNewCategory(Categorydto categoryDto){
            // create new instance from dto 
            var category = new Category { CategoryName = categoryDto.Name} ;

            await _context.AddAsync(category);
            _context.SaveChanges();

            return Ok(category);

        } 


        // endpoint: to update in specific Id 
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryById (byte id,[FromBody] Categorydto dto){
            var category = await _context.Category.FirstOrDefaultAsync(c => c.CategoryId == id);

            if(category ==null)
                 return NotFound($"No Category was found with Id: {id}"); 
            
            category.CategoryName = dto.Name;

            _context.SaveChanges();

            return Ok(category);
        }




        // endpoint: to delete specific category by id 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryById(byte id , [FromBody] Categorydto dto)
        {
            var category = await _context.Category.FindAsync(id);

            if (category == null){
                return NotFound($"No Category found with ID {id}");
            }
            _context.Remove(category);
            _context.SaveChanges();
            return Ok(category);
            
        }
    }
}