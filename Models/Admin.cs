

namespace Book_store_1_.Models
{
    public class Admin : ApplicationUser
    {   
 
        [Required]
    
        
        public int UserId { get; set; }

        public ApplicationUser  User { get; set; }
        

        // public int AdminId{set; get;}
        
        // [Required]
        // public string? FirstName{set; get;}

        // [Required]
        // public string? LastName{set; get;}
        
        // [Required]
        // public Guid UserId { get; set; }

        // public virtual ApplicationUser  User { get; set; }
    }
}