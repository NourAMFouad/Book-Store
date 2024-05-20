namespace Book_store_1_.DTOs
{
    public class BorrowedBookdto
    {
        public int BookId{set; get;}
        
        public int User_Id {set; get;}
      
        public DateTime Borrow_date {set; get;}

        public DateTime Return_date {set; get;}
    }
}