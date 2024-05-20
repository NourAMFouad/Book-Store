using System.Reflection.Metadata.Ecma335;
using Book_store_1_.DTOs;
using Book_store_1_.Models;

namespace Book_store_1_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BorrowedBooksController :ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BorrowedBooksController(ApplicationDbContext context)
        {
            _context = context;

        }

        // starting to add endpoints
        // endpoint: to get all data from BorrowedBooks model
        // only Admins able to see all borrowed books 
        [HttpGet("ListAllBorrowedBooks")]
        public async Task<IActionResult> GetAllBorrowedBooks([FromQuery]string admin_name)
        {
            
            var admins = await _context.Admin.FirstOrDefaultAsync(m => m.Username == admin_name);                  //.FindAsync(admin_name);    this for integer only 
            // check it it object null or not
            if (admins != null){
                if (admins.Username == admin_name){
                    var borrowedBooks = await _context.Borrowed_Books.ToListAsync();
                    
                    return Ok(borrowedBooks);
                }
            }
            return BadRequest($"Only admins able to see Borrowed books and {admin_name} not Admin.");
        }
        
//         // function to find number of borrowed books for specific member 

//         public int CheckBorrowedNumber(int Memeber_Id){
//             var member =  _context.Member.Find(Memeber_Id);
            
//             if (member != null){
//                 int borrowed_books = member.Number_Of_borrowedBooks ;

//                 return borrowed_books;
//             }else {
//                 return 0;
//             }
//         }
 
//  // increasing number of borrowedBooks by using memeber id 
//        public void IncreasingNumberOfBorrowedBooks(int Member_Id){
//              var member = _context.Member.Find(Member_Id);

//              if (member != null){
//                 member.Number_Of_borrowedBooks +=1;
//              }
//        }


//  // decreasing number of borrowedBooks by using memeber id 
//        public void DecreasingNumberOfBorrowedBooks(int Member_Id){
//              var member = _context.Member.Find(Member_Id);

//              if (member != null){
//                 member.Number_Of_borrowedBooks -=1;
//              }
//        }


        // endpoint: to add new instance in BorrowedBooks model 

        /*
        Borrowed Book:
            Check if memberId in database or not,
            True 
                Check if book_name in database and number of copies not equal 0
                True
                        Add book in borrowedBook table and increasing number of borrowed books of user  and decrease number of copies for book
            Send successful message
        */
        [HttpPost("BorrowedBookForMemeber")]
        public async Task<IActionResult> AddBorrowedBook([FromBody] BorrowedBookdto dto, [FromQuery] int Member_Id, [FromQuery] int book_id)
        {
            var member = await _context.Member.FindAsync(Member_Id);

            if (member == null){
                return BadRequest($"{Member_Id}, You are not member.");

            }
            
            // check if member not exceeding the required limit of Borrowed books
            // assume that member able to borrow 5 books maximum if exceeding this limit refuse request to borrow the book 
            
            int borrowed_books = member.Number_Of_borrowedBooks ;

            //CheckBorrowedNumber(Member_Id)
            if ( borrowed_books <= 5 ){
            
                // check if book available or not
                var book = await _context.Book.FindAsync(book_id);

                if (book != null && book.Number_Of_Copies > 0 ){

                    var Book = new Borrowed_books{
                        BookId = dto.BookId,
                        User_Id = dto.User_Id,
                        Borrow_date = dto.Borrow_date,
                        Return_date = dto.Return_date
                        };

                    // increasing number of borrowed book for user 
                    member.Number_Of_borrowedBooks +=1;
                    // IncreasingNumberOfBorrowedBooks(Member_Id);
                    // decreasing number of copies for this book
                    book.Number_Of_Copies -=1;

                    await _context.AddAsync(Book);
                    _context.SaveChanges();
                    return Created();
                    }else {
                        return BadRequest($"Book {book_id} not available.");
                    }
            }else {
                return BadRequest("You are exceeding the required limit of Borrowed books.");
            }

        }



        // endpoint: To allow user to return book
        // retrun book: then delete borrowedbook from database 
        // return increasing number of copies for book  and decreasing number of userborrowedbooks 
        [HttpDelete("ReturnBorrowedBook")]
        public async Task<IActionResult> RetrunBorrowedBook(int borrowedBook_id, BorrowedBookdto dto){
            var borrowedbook = await _context.Borrowed_Books.FindAsync(borrowedBook_id);

            if (borrowedbook == null){
                return BadRequest($"This Book {borrowedBook_id} not found in borrwed books database.");
            }else{

            // increasing number of copies for specific book
            //  
            var book = await _context.Book.FindAsync(borrowedbook.BookId);
            var member = await _context.Member.FindAsync(borrowedbook.User_Id);
            if (book != null && member != null){
                book.Number_Of_Copies +=1;
                member.Number_Of_borrowedBooks -=1;
            }

                _context.Remove(borrowedbook);
                _context.SaveChanges();


            }            
            return Ok();            
        }


    }
}