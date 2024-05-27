using System.ComponentModel;
using AutoMapper;
using Book_store_1_.DTOs;
using Book_store_1_.Models;
using Book_store_1_.Repository;
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
        [HttpGet("GetAllBooksWiththeSameName")]
        public IActionResult GetAllBooksWithName(string name){
            // display instances
            var books = _BooksRepository.Find(b=>b.BookName == name);   //, 
            return Ok(books);
        }

        // endpoint: get specific book using bookid 
        [HttpGet("GetBookByBookId")]
        public IActionResult GetBookById(int Id)
        {
            var data = _BooksRepository.GetById(Id);
            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest($"Id {Id} not found in database");
            }
        }



        // endpoint: get books by categoryId
        [HttpGet("GetBooksByCategoryId")]
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
        [HttpGet("GetBookByReleaseDate")]
        public IActionResult GetBookByReleaseDate(DateTime date)
        {
            return Ok(_BooksRepository.Find(b => b.ReleaseDate == date));
        }

        [HttpPost("AddNewBook")]
        public IActionResult AddNewBook([FromBody] Bookdto dto){
           
             _BooksRepository.Add(dto);
             return Ok();
        }
      

        // endpoint: to delete book by using BookId
        [HttpDelete("deleteBook")]
        public IActionResult DeleteBook(int Id){
            var book = _BooksRepository.GetById(Id);
            // check if book in database or not 
            if (book != null){
                     // delete book
                    _BooksRepository.Delete(book);
                    return Ok();
               
            }else{
                return BadRequest($"Book With Id {Id} is not in database.");
            }
        }


[HttpPut("EditBookById/{id}")]
public IActionResult UpdateBook([FromBody] Bookdto dto, int id)
{
    // Check if book exists in database
    var book = _BooksRepository.GetById(id);

    if (book != null)
    {
   
        book.BookName = dto.BookName ;
        book.ReleaseDate = dto.ReleaseDate;
        book.CategoryId = dto.CategoryId;
        book.Author = dto.Author;
        book.NumberOfCopies = dto.NumberOfCopies;
        

        _BooksRepository.Update(dto);
        return Ok(book);
    }
    else
    {
        return BadRequest($"Book with ID {id} not found.");
    }
}
    }
      
}