using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class ReadingLog
    {
        public User Reader { get; set; }
        public Book LoggedBook { get; set; }
        public string FormatType { get; set; }
        public int TimeRead { get; set; }
        public List<string> Notes { get; set; }

    }
}
