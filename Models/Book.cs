using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Book_store_1_.Models
{
    public class Book
    {
        
        public int BookId{set; get;}
        public string? BookName{set; get;}

        public DateTime ReleaseDate{set; get;}

        public byte CategoryId{set; get;}
        [Required]
        [ForeignKey("CategoryId")]
    
        public Category? Category {set; get;}

        public string? Author{set; get;}

        public int NumberOfCopies{set; get;}


        public ICollection<BorrowedBooks>? BorrowedBooks {set; get;}
        
        public ICollection<Member>? Members {set; get;}


    }
}
