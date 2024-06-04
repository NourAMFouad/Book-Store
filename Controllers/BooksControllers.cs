using System.ComponentModel;
using System.Security.Claims;
using AutoMapper;
using Book_store_1_.DTOs;
using Book_store_1_.Models;
using Book_store_1_.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Book_store_1_.Controllers
{
    // adding general route of api 
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

    //  1- adding Repository pattern 
    private readonly IBaseRepository<Book, Bookdto> _BooksRepository;
    // adding CategoryRepository
    private readonly IBaseRepository<Category, Categorydto> _CategoryRepository; 
    private readonly IMapper _mapper;



    // 2- Repository
    public BooksController(IBaseRepository<Book, Bookdto> bookRepository, IBaseRepository<Category, Categorydto> categoryRepository , IMapper mapper){
        _BooksRepository = bookRepository;
        _mapper = mapper;
        _CategoryRepository = categoryRepository;
    }


        //endpoint: get all books from database
        [HttpGet("GetAllBooks")]
        public IActionResult GetAllBooks(){
        //    var books = _BooksRepository.FindAll(new [] {"Category"});
          var books = _BooksRepository.GetAll();
            return Ok(books);
        }

        // endpoint: get all books 
        // to list all books with the same name
        
        [HttpGet("books", Name = "GetAllBooksWithName")]
        public IActionResult GetAllBooksWithName(string name){
            // display instances
            var books = _BooksRepository.Find(b=>b.BookName == name);  
            return Ok(books);
        }

        // endpoint: get specific book using bookid 
        // [HttpGet("BookId{id}")]
        [HttpGet("books/{id}", Name = "GetBookById")]
        public IActionResult GetBookById(int id)
        {
            var data = _BooksRepository.GetById(id);
            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest($"Id {id} not found in database");
            }
        }



        // endpoint: get books by categoryId
        [HttpGet("categoryID:{categoryId}")]
        public IActionResult GetBookByCategoryId(byte categoryId)
        {
            var book =  _BooksRepository.Find(b => b.CategoryId == categoryId);

            // check if book is null or not
            if (book == null ){
                return BadRequest($"No book include category Id : {categoryId}");
            }
            return Ok(book);

        }

        //endpoint: get books by releaseDate
        [HttpGet("Date{date}")]
        public IActionResult GetBookByReleaseDate(DateTime date)
        {
            return Ok(_BooksRepository.Find(b => b.ReleaseDate == date));
        }

        [HttpPost("AddNewBook")]
        // [Authorize(Roles = "Admin")]
        public IActionResult AddNewBook([FromBody] Bookdto dto){
            
            // var identity = HttpContext.User.Identity as ClaimsIdentity;
            // var userId = identity.FindFirst("uid");
            // if (userId == null){
                // return BadRequest("Unothorized");
            // }
            
             _BooksRepository.Add(dto);
             return Ok();
        }
      

        // endpoint: to delete book by using BookId
        [HttpDelete("deleteBook{id}")]
        public IActionResult DeleteBook(int id){
            var book = _BooksRepository.GetById(id);
            // check if book in database or not 
            if (book != null){
                     // delete book
                    _BooksRepository.Delete(book);
                    return Ok();
               
            }else{
                return BadRequest($"Book With Id {id} is not in database.");
            }
        }


[HttpPut("BookById/{id}")]
public IActionResult UpdateBook([FromBody] Bookdto dto, int id)
{
    // Check if book exists in database
    // var book = _BooksRepository.GetById(id);

        dto.BookId = id;
        _BooksRepository.Update(dto);
        return Ok();
   
   
}
    }
      
}