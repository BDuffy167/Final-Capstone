using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IBookDAO bookDAO;

        public BookController(IBookDAO bookDAO)
        {
            this.bookDAO = bookDAO;
        }

        [HttpGet]
        public ActionResult<List<Book>> GetAllBooks()
        {
            List<Book> books = bookDAO.GetAllBooks();

            return books;
        }

        [HttpGet("isbn")]
        public ActionResult<Book> GetSpecificBook(long isbn)
        {
            Book specificBook = bookDAO.GetSpecificBook(isbn);

            if(specificBook == null)
            {
                return NotFound();
            }
            else
            {
                return specificBook;
            }
        }


        [HttpPost("{familyID}/AddBook/")]
        public ActionResult<Book> AddBookToFamilyLibrary(int familyID, Book book)
        {
            int bookID = bookDAO.CheckIfBookExistsOnDB(book);
            bool completed = false;

            if(bookID == 0)
            {
                book.BookId = bookDAO.AddNewBook(book);
                completed = bookDAO.AddToFamilyLibrary(book.BookId, familyID);
            }
            else
            {
                completed = bookDAO.AddToFamilyLibrary(bookID, familyID);
            }

            if(completed)
            {
                return Ok(book);
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
