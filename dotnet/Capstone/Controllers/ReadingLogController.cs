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
    public class ReadingLogController : ControllerBase
    {

        private readonly IReadingLogDAO readingLogDAO;
        private readonly IBookDAO bookDAO;

        public ReadingLogController(IReadingLogDAO readingLog, IBookDAO bookDAO)
        {
            this.readingLogDAO = readingLog;
            this.bookDAO = bookDAO;
        }

        //NEED TO DO SECURITY LATER!!!!!!
        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<List<ReadingLog>> GetUserBooks(int id)
        {
            //int user_id = int.Parse(this.User.FindFirst("sub").Value);

            List<ReadingLog> toReturn = new List<ReadingLog>();
            
            if(id != 0)
            {
                toReturn = readingLogDAO.GetUserBooks(id);
                return Ok(toReturn);
            }
            return NotFound();
        }

        [HttpPost("{id}/AddLog")]
        public ActionResult<ReadingLog> AddNewReadingLog(ReadingLog newLog)
        {
            int rowsChanged = 0;
            int user_id = int.Parse(this.User.FindFirst("sub").Value);
            int bookId = bookDAO.CheckIfBookExistsOnDB(newLog.LoggedBook);

            if(bookId == 0)
            {
                bookId = bookDAO.AddNewBook(newLog.LoggedBook);
                newLog.LogID = readingLogDAO.AddNewReadingLog(newLog, user_id, bookId);
                return Ok(newLog);
            }
            else
            {
                newLog.LogID = readingLogDAO.AddNewReadingLog(newLog, user_id, bookId);
                return Ok(newLog);
            }
        }

    }
}
