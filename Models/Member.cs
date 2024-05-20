namespace Book_store_1_.Models
{
    public class Member
    {
        [Key]
        public int Member_Id{set; get;}

        public string? Username{set; get;}

        public string? Password{set; get;}

        public ushort Number_Of_borrowedBooks{set; get;}
    }
}