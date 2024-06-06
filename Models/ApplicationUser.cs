using Microsoft.AspNetCore.Identity;

namespace Book_store_1_.Models
{
    public class ApplicationUser : IdentityUser
    {
         [Required, MaxLength(50)]
        public string? FirstName { get; set; }

        [Required, MaxLength(50)]
        public string? LastName { get; set; }

        public ICollection<Admin>? Admin {set; get;}
        public ICollection<Member>? Member{set; get; }
        
    }
}