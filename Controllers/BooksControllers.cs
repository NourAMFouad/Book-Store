using System.ComponentModel;
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
        // add attribute for db context // to add connection between client side and server side to connect with database 
//        private readonly ApplicationDbContext _context;

//    1- adding Repository pattern 
private readonly IBaseRepository<Book> _BooksRepository;

        // adding constructor to initialization 
        // alternative services (repository directory)

// 2- Repository
public BooksController(IBaseRepository<Book> bookRepository){
    _BooksRepository = bookRepository;
}

        // public BooksController(ApplicationDbContext context){
        //     _context = context;
        // }

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
        [HttpGet("GetBookByCategoryId")]
           public IActionResult GetBookByCategoryId(byte Categoryid)
        {
            return Ok(_BooksRepository.Find(b=>b.CategoryId == Categoryid));

        }

        //endpoint: get books by releaseDate
        [HttpGet("GetBookByCategoryReleaseDate")]
        public IActionResult GetBookByReleaseDate(DateTime date)
        {
            return Ok(_BooksRepository.Find(b => b.ReleaseDate == date));
        }


        [HttpPost]
        public  IActionResult AddNewBook([FromBody]Book book){
            _BooksRepository.Add(book);
            return Ok();
        }

        /*

                // endpoint: List all Books from database
                [HttpGet]
                public async Task<IActionResult> GetAllBooks (){
                //   var book = await _context.Book.Include(m => m.Category).ToListAsync();
                 var book = await _context.Book.ToListAsync();
                    return Ok(book);
                }

                // endpoint: get specific book using bookid                                                    DONE 
                [HttpGet("GetBookByBookId")]
                public async Task<IActionResult> GetBookByIdAsync(int Id){
                    // take instance from Book model
                    var book = await _context.Book.FindAsync(Id);

                    // check if book is null or not existing in model then return not found request
                    if (book == null){
                        return NotFound();
                    }

                    var dto =  new Bookdto{
                        //BookId = book.BookId,     
                        Book_Name = book.Book_Name, 
                        Release_date = book.Release_date,
                        Category_Id = book.Category_Id,
                        Author = book.Author,
                        Number_Of_Copies = book.Number_Of_Copies    
                    };

                    return  Ok(dto);

                } 


                // endpoint: to get book by Book_name
            [HttpGet("GetBookByName")]
            public async Task<IActionResult> GetBookByNameAsync(string book_name){

                var books = await _context.Book
                .Where(m => m.Book_Name == book_name)
                .OrderByDescending(x => x.Book_Name)
                .Include(m => m.Category)
                .Select(m => new Bookdto 
                {
                     Book_Name = m.Book_Name,
                      Category_Id = m.Category_Id, 
                      Author = m.Author, 
                      Release_date = m.Release_date, 
                      Number_Of_Copies = m.Number_Of_Copies}
                      ).ToListAsync();



                if (books == null ){

                    return NotFound();
                }

                return Ok(books);
                }


                // endpoint: to get and display All books by category_name
                // asign endpoint to avoid conflict happen when request send 
                [HttpGet("GetByCategoryId")]
                public async Task<IActionResult> GetBooksByCategoryIdAsync(byte category_id){
                    var books = await _context.Book
                    .Where(m => m.Category_Id == category_id)
                    .OrderByDescending(x => x.Book_Name)
                    .Include(m => m.Category)
                    .Select(m => 
                    new Bookdto { Book_Name = m.Book_Name,
                     Category_Id = m.Category_Id, 
                     Author = m.Author, 
                     Release_date = m.Release_date, 
                     Number_Of_Copies = m.Number_Of_Copies})
                     .ToListAsync();

                    return Ok(books);
                }


                // endpoint: to get and display all books by release date
                [HttpGet("GetBooksByRelease_Date")]
                public async Task<IActionResult> GetBooksByReleaseDate(DateTime date){
                    var book = await _context.Book
                    .Where(m => m.Release_date == date)
                    .OrderBy(x => x.Book_Name)
                    .Include(m => m.Category)
                    .Select(m => new Bookdto {
                        Book_Name = m.Book_Name,
                        Category_Id = m.Category_Id,
                        Author = m.Author,
                        Number_Of_Copies = m.Number_Of_Copies
                    })
                    .ToListAsync();

                    return Ok(book);
                }



                // endpoint: To add new Book in database
                // add constrains (Admin only add books, so allow user to add Id and check if id found in database, then allow user to add book otherwise donot accept request)
                [HttpPost]
                public async Task<IActionResult> AddnewBook(Bookdto dto, [FromQuery]int Admin_Id)
                {
                    // find and check if admin id in database or not 
                    var admin = await _context.Admin.FindAsync(Admin_Id);
                    if (admin == null)
                    {
                        return BadRequest($"Admin Id {Admin_Id} not in database, Only Admins able to add books.");
                    }else {

                        // Check if the Category_Id exists in the Category table
                        var category = await _context.Category.FindAsync(dto.Category_Id);
                        if (category == null)
                        {
                            return BadRequest($"Category with ID {dto.Category_Id} does not exist.");
                        }

                        var book = new Book
                        {
                            Book_Name = dto.Book_Name,
                            Release_date = dto.Release_date,
                            Category_Id = dto.Category_Id,
                            Category = category,
                            Author = dto.Author,
                            Number_Of_Copies = dto.Number_Of_Copies
                        };


                        await _context.AddAsync(book);
                        await _context.SaveChangesAsync();

                        return Created();
                    }

                } 


                // endpoint: to Delete book from database
                // only admin able to delete books 
                [HttpDelete("DeleteBookById")]
                public async Task<IActionResult> DeleteBook([FromQuery] string admin_name,[FromQuery] int bookid ,[FromBody]Bookdto dto){

                    // check if admin in database
                    var admin = await _context.Admin.FirstOrDefaultAsync (m => m.Username == admin_name);
                    if (admin != null){
                        var book = await _context.Book.FindAsync(bookid);

                        if (book == null){
                            return BadRequest($"Book with id {bookid} not found.");
                        }else{
                            _context.Remove(book);
                            _context.SaveChanges();
                        }

                        return Ok();
                    }else{
                        return BadRequest($"{admin_name} are not Admin. Only Admin able to delete books.");
                    }

                }
            */
    }
      
}