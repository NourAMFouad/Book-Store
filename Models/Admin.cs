namespace Book_store_1_.Models
{
    public class Admin
    {
        [Key]   // to set primary key 
        public int Admin_Id{set; get;}

        public string? Username{set; get;}

        public string? Password{set; get;}
        
    }
}