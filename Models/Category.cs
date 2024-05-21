// adding domain model(Category) with annotation

using System.ComponentModel.DataAnnotations.Schema;

namespace Book_store_1_.Models
{
    public class Category
    {
        // to generate database in after you added server connection 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        // adding attributes
        [Key]
        public byte CategoryId{set; get;}

        // adding constrain 
        [MaxLength(100)]
        public string? CategoryName{set; get;}
        // public int Id { get; internal set; }

        // write this to allow us to access all books for specific category 
         public ICollection<Book>? Books { get; set; }
         
    }
}





/*
Additional notes
--> why adding namespace?

*/