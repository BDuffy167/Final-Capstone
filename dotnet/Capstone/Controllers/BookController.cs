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
        [AllowAnonymous]
        public ActionResult<List<Book>> GetAllBooks()
        {
            List<Book> books = bookDAO.GetAllBooks();


            return books;
        }
    }
}
