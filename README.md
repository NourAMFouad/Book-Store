Endpoints:
# Auth
Include:
- POST/ register/  
- POST/ login/   

# Admin
Include 2 endpoints 
- GET/ api/Admin  :  To list and display all Adimns from database.
- POST/ api/Admin :  To add new admin in database


# Books
Include 7 endpoints
- GET/ api/Books   :   To display all books from database.
- POST/ api/Books  :   Only Admins able to Add new book.
- GET/  api/Books/GetBookByBookId  :  display book details by adding Book ID
- GET/  api/Books/GetBookByName    :  display book details by adding Book Name
- GET/  api/Books/GetByCategoryId  :  dispaly All books with details by Category Id
- GET/  api/Books/GetBooksByRelease_Date  :  dispaly All books with details by Adding Release_date
- DELETE/ api/Books/DeleteBookById :  Only Admins able to delete books.


# BorrowedBooks
Include 3 endpoints
- GET/   api/BorrowedBooks/ListAllBorrowedBooks   :  Only Admin can see all borrowedbooks with details from database
- POST/  api/BorrowedBooksForMember/   :  Allow user to borrow books by adding Member_Id and book_Id
- DELETE/  api/BorrowedBooks/ReturnBorrowBook  : Allow user to return book by adding borrowedBook_Id and adding more details about borrowed request to delete it from database if it found.

# Category
Include 4 endpoints 
- GET/  api/Category  :  To list and display all categories from database
- POST/  api/Category :  To allow user to add new Category in database
- PUT/  api/Category  :  To edit specific category by adding Category_id
- DElETE  /  api/Category/{id}   : To delete Category by adding category_id


# Members
Include 2 Endpoints:
- GET/ api/Members  :  to list and display all Members details.
- POST/  api/Members  : able to add new member in database. 
