namespace Book_store_1_.DTOs
{
    public class Bookdto()
    {

       public int BookId {set; get;}
       public string? BookName{set; get;}

        public DateTime ReleaseDate{set; get;}

        public byte CategoryId{set; get;}

        public string? Author{set; get;}

        public int NumberOfCopies{set; get;}
    }
}