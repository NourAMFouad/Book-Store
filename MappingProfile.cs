using AutoMapper;
using Book_store_1_.DTOs;
using Book_store_1_.Models;


public class MappingProfile : Profile
{
    // create map to define bidirectional mapping between model and DTO  (Book and BooksDto)
     // addding constructor 
     public MappingProfile(){
        // initialize and create map
        CreateMap<Book, Bookdto>().ReverseMap();
        CreateMap<Category, Categorydto>().ReverseMap();
        CreateMap<Admin, Admindto>().ReverseMap();
        CreateMap<Member, Memberdto>().ReverseMap();
        CreateMap<Borrowed_books, BorrowedBookdto>().ReverseMap();

     }    
}
