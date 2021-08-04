﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class ReadingLog
    {
        public int LogID { get; set; }
        public User Reader { get; set; }
        public Book LoggedBook { get; set; } = new Book();
        public string FormatType { get; set; }
        public int TimeRead { get; set; }
        public List<string> Notes { get; set; }

    }
}
