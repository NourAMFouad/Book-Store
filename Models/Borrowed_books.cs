using System.ComponentModel.DataAnnotations.Schema;

namespace Book_store_1_.Models
{
    public class Borrowed_books
    {
        [Key]
        public int BorrowedBooksId {set; get;}

        
        public int BookId{set; get;}
        [Required]
        [ForeignKey("BookId")]
        public Book? Book {set; get;}

        public int User_Id {set; get;}
        [ForeignKey("User_Id")]
        public Member? Member{set; get;} 

        public DateTime Borrow_date {set; get;}

        public DateTime Return_date {set; get;}

    }
}