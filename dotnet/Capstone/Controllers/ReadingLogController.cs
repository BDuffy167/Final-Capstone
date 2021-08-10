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

        
        [HttpGet("{id}")]
        public ActionResult<List<ReadingLog>> GetUserReadingLogs(int id)
        {
            int user_id = int.Parse(this.User.FindFirst("sub").Value);

            List<ReadingLog> toReturn = new List<ReadingLog>();
            
            if(id == user_id)
            {
                toReturn = readingLogDAO.GetUserBooks(id);
                return Ok(toReturn);
            }
            return Forbid();
        }

        [HttpPost("{id}/AddLog")]
        public ActionResult<List<ReadingLog>> AddNewReadingLog(int id, NewLog newLog)
        {
            List<ReadingLog> toReturn = new List<ReadingLog>();
         

            int user_id = int.Parse(this.User.FindFirst("sub").Value);
            if(id == user_id)
            {
                int createdID = readingLogDAO.AddNewReadingLog(newLog);
                toReturn = readingLogDAO.GetUserBooks(id);

                return Ok(toReturn);
            }
            return Forbid();
        }

        [HttpGet("{id}/UserHistory")]
        public ActionResult<List<BookHistoryObj>> GetUserBookHistory(int id)
        {

            int user_id = int.Parse(this.User.FindFirst("sub").Value);
            if (id == user_id)
            {
                List<BookHistoryObj> toReturn = readingLogDAO.GetUserBookHistory(id);

                return Ok(toReturn);
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
