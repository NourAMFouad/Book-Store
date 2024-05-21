using System.ComponentModel.DataAnnotations.Schema;

namespace Book_store_1_.Models
{
    public class Borrowed_books
    {
        [Key]
        public int BorrowedbooksId {set; get;}

        
        public int BookId{set; get;}
        [Required]
        [ForeignKey("BookId")]
        public Book? Book {set; get;}

        public int UserId {set; get;}
        [ForeignKey("UserId")]
        public Member? Member{set; get;} 

        public DateTime BorrowDate {set; get;}

        public DateTime ReturnDate {set; get;}

    }
}