using Microsoft.AspNetCore.Identity;


namespace Book_store_1_.Models
{
    public class Admin : IdentityUser
    {   
        [Required]
        public string? FirstName{set; get;}

        [Required]
        public string? LastName{set; get;}
        
    }
}