using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface IBookDAO
    {
        public int CheckIfBookExistsOnDB(Book inputBook);
        public int AddNewBook(Book newBook);
        public List<Book> GetAllBooks();
        public Book GetSpecificBook(long isbn);
        public bool AddToFamilyLibrary(int bookID, int libraryID);
    }
}
