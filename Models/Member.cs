

namespace Book_store_1_.Models
{
    public class Member
    {
        [Key]
        public int MemberId{set; get;}

    
        public string? Username{set; get;}
       
        public string? Password{set; get;}

        public ushort NumberOfborrowedBooks{set; get;}
    }
}