using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Book_store_1_.Models
{
    public class Book
    {
        public int BookId{set; get;}
        public string? Book_Name{set; get;}

        public DateTime Release_date{set; get;}

        public byte Category_Id{set; get;}
        [Required]
        [ForeignKey("Category_Id")]
        // add navigation properity 
        public Category? Category {set; get;}

        public string? Author{set; get;}

        public int Number_Of_Copies{set; get;}

        // to add relation (one-to-many) between Book and BorrowedBook
        public ICollection<Borrowed_books>? Borrowed_Books {set; get;}
        
        public ICollection<Member>? Members {set; get;}

       //  public string? Added_By{set; get;}

    }
}
/*
foriegn key example

    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }
*/