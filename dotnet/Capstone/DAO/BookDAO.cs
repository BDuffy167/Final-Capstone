using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class BookDAO : IBookDAO
    {
        private readonly string connectionString;

        public BookDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
    }
}
