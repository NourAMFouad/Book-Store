
using System.ComponentModel.DataAnnotations.Schema;

namespace Book_store_1_.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte CategoryId{set; get;}

        // adding constrain 
         [MaxLength(100)]
         public string? CategoryName{set; get;}

         public ICollection<Book>? Books { get; set; }
         
    }
}
