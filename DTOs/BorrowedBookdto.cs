namespace Book_store_1_.DTOs
{
    public class BorrowedBookdto
    {
        public int BookId{set; get;}
        
        public int UserId {set; get;}
      
        public DateTime BorrowDate {set; get;}

        public DateTime ReturnDate {set; get;}
    }
}