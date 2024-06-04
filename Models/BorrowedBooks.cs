using System.ComponentModel.DataAnnotations.Schema;

namespace Book_store_1_.Models
{
    public class BorrowedBooks
    {
        [Key]
        public int BorrowedbooksId {set; get;}
         
        public int BookId{set; get;}
        [Required]
        [ForeignKey("BookId")]
        public Book? Book {set; get;}

        [ForeignKey("UserId")]
        public int UserId {set; get;}
        
        public Member? Member{set; get;} 

        public DateTime BorrowDate {set; get;}

        public DateTime ReturnDate {set; get;}

    }
}