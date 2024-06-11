namespace Book_store_1_.DTOs
{
    public class PagesParameter
    {
        const int maxPageSize = 4;

        public int PageNumber {get; set;} =1;

        private int _pageSize  = 10; 

        public int PageSize {
            get
            {
                return _pageSize;
            }
            
            set
            {
                 _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
            }

         
    }
}


/*

maxPageSize: A constant that defines the maximum number of items that can be returned in a single page.
PageNumber: The current page number, defaulting to 1.
_pageSize: A private field to store the page size.
PageSize:  A public property to get or set the page size, ensuring it does not exceed maxPageSize.

*/