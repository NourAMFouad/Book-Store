namespace Book_store_1_.Models
{
    public class BlacklistToken 
    {
        public int Id {get; set;}

        public string? Token {get; set;}

        public DateTime ExpiryDate {get; set;}
    }
}