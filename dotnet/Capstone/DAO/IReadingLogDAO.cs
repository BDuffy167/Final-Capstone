using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface IReadingLogDAO
    {
        public List<ReadingLog> GetUserBooks(int id);
        public int AddNewReadingLog(ReadingLog newLog, int userID, int bookID);
        public List<BookHistoryObj> GetUserBookHistory(int userID);
    }
}
