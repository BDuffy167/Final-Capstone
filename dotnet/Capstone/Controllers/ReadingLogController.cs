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

        public ReadingLogController(IReadingLogDAO readingLog)
        {
            this.readingLogDAO = readingLog;
        }

        //NEED TO DO SECURITY LATER!!!!!!
        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<List<Book>> GetUserBooks(int id)
        {
            List<Book> toReturn = new List<Book>();
            
            if(id != null)
            {
                toReturn = readingLogDAO.GetUserBooks(id);
                return Ok(toReturn);
            }
            return NotFound();
            

        }
    }
}
