using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public int FormatType { get; set; }
        public long ISBN { get; set; }
        public int TimeRead { get; set; }
        public List<string> Notes { get; set; }
        
    }
}
