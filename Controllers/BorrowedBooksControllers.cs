using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using Book_store_1_.DTOs;
using Book_store_1_.Models;
using Book_store_1_.Repository;

namespace Book_store_1_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BorrowedBooksController :ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly IBaseRepository<BorrowedBooks, BorrowedBookdto> _borrowedBooksRepository;
        
        private readonly IBaseRepository<Member,Memberdto> _memberRepository;

        private readonly IBaseRepository<Book, Bookdto> _bookRepository;
        private readonly IMapper _mapper;

        public BorrowedBooksController(ApplicationDbContext context
        , IBaseRepository< BorrowedBooks, BorrowedBookdto> borrowedBooksRepository
        , IBaseRepository<Member, Memberdto> memberRepository
        , IBaseRepository< Book, Bookdto> bookRepository
        , IMapper mapper)
        {
            _context = context;
            _borrowedBooksRepository = borrowedBooksRepository;
            _memberRepository = memberRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;

        }

        
        // only Admins able to see all borrowed books   pending 
        [HttpGet("AllBooks")]
        public IActionResult GetAllBorrowedBooks()
        {
                
            var borrowedBooks = _borrowedBooksRepository.GetAll();
        
            return Ok(borrowedBooks);
               
        }

        // endpoint: to add new instance in BorrowedBooks model 

      
        [HttpPost("Addingby/{memberId}/Book/{bookId}")]
        public IActionResult AddBorrowedBook([FromBody] BorrowedBookdto dto, int memberId, int bookId)
        {
            var member = _memberRepository.GetById(memberId);

            if (member == null){
                return BadRequest($"{memberId}, You are not member.");

            }
            
  
            int borrowed_books = _memberRepository.GetSpecificValue(m => m.MemberId == memberId, n=> n.NumberOfborrowedBooks);

            if ( borrowed_books <= 5 ){
            
              
                var book = _bookRepository.GetById(bookId);
                var numberOfBooks = _bookRepository.GetSpecificValue(b=>b.BookId == bookId, b=>b.NumberOfCopies);
                

                if (book != null && numberOfBooks > 0 ){

                    _memberRepository.UpdateSpecificField(m => m.MemberId == memberId, n=> n.NumberOfborrowedBooks, (member, borrowed_books) => member.NumberOfborrowedBooks += 1 );
                   
                    _bookRepository.UpdateSpecificField( b => b.BookId == bookId, n => n.NumberOfCopies, (book, numberOfCopies) => book.NumberOfCopies -= 1);

                    _borrowedBooksRepository.Add(dto);
                    return Created();
                    }else {
                        return BadRequest($"Book {bookId} not available.");
                    }
            }else {
                return BadRequest("You are exceeding the required limit of Borrowed books.");
            }

        }



        // endpoint: To allow user to return book
        // retrun book: then delete borrowedbook from database 
        // return increasing number of copies for book and decreasing number of userborrowedbooks 
        [HttpDelete("Return{borrowedBookId}")]
        public IActionResult RetrunBorrowedBook(int borrowedBookId){
           // find instance that needed to delete it 
            var borrowedbook = _borrowedBooksRepository.GetById(borrowedBookId);

            // chech if it exists in database or not 
            if (borrowedbook == null){
                return BadRequest($"This Book {borrowedBookId} not found in borrwed books database.");
            }else{

            // take instances from Book and Memeber entity by using Ids from BorrowedBook data 
            //  
            var book = _bookRepository.GetById(borrowedbook.BookId);
            var member = _memberRepository.GetById(borrowedbook.UserId);
          
            if (book != null && member != null){


                _bookRepository.UpdateSpecificField(b => b.BookId == borrowedbook.BookId ,
                    b => b.NumberOfCopies,
                    (book, NumberOfCopies) => book.NumberOfCopies +=1);
            
                
                _memberRepository.UpdateSpecificField( m => m.MemberId == borrowedbook.UserId,
                    m => m.NumberOfborrowedBooks, 
                     (memeber, NumberOfborrowedBooks) => member.NumberOfborrowedBooks -=1);

            }



            _borrowedBooksRepository.Delete(borrowedbook);

            }            
            return Ok();            
        }


    }
}
