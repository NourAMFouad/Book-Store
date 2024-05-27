
using System.ComponentModel.DataAnnotations.Schema;

namespace Book_store_1_.Models
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]

        public byte CategoryId{set; get;}

        // adding constrain 
        [MaxLength(100)]
        public string? CategoryName{set; get;}
         public ICollection<Book>? Books { get; set; }
         
    }
}





/*
Additional notes
--> why adding namespace?

*/