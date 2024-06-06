

namespace Book_store_1_.Models
{
    public class Member : ApplicationUser
    {
        [Required]
     
        public int UserId { get; set; }

        public ApplicationUser  User { get; set; }
        
        public ushort NumberOfborrowedBooks{set; get;}


        // [Key]
        // public int MemberId{set; get;}

        // public string? Username{set; get;}
       
        // public string? Password{set; get;}
    }
}