using Microsoft.AspNetCore.Identity;


namespace Book_store_1_.Models
{
    public class Admin 
    {   

        public int AdminId{set; get;}

        [Required]
        public string? FirstName{set; get;}

        [Required]
        public string? LastName{set; get;}
        

        // public Guid UserId { get; set; }

        // public virtual ApplicationUser  User { get; set; }
    }
}