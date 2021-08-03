using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class ReadingLogDAO : IReadingLogDAO
    {
        private readonly string connectionString;
        private readonly string sqlGetUserBooks = @"SELECT 
	u.username AS username,
	b.title AS title,
	b.book_id AS book_id,
	b.author_firstName AS author_firstName,
	b.author_lastName AS author_lastName,
	rf.format_type AS format_type,
	b.isbn AS isbn,
	rl.total_time AS total_time
FROM reading_log rl
INNER JOIN users u ON rl.user_id = u.user_id
INNER JOIN book b ON rl.book_id = b.book_id
INNER JOIN reading_format rf ON rl.format_id = rf.format_id
WHERE rl.user_id = @user_id;";

        private readonly string sqlCheckIfBookByISBN = @"SELECT book_Id FROM book WHERE isbn = @isbn";
        private readonly string sqlAddBookLog = @"";

        public ReadingLogDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<ReadingLog> GetUserBooks(int id)
        {
            List<ReadingLog> readingLogs = new List<ReadingLog>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlGetUserBooks, conn);
                cmd.Parameters.AddWithValue("@user_id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    
                    ReadingLog readinglog = new ReadingLog();

                    readinglog.LoggedBook.BookId = Convert.ToInt32(reader["book_id"]);
                    readinglog.LoggedBook.Title = Convert.ToString(reader["title"]);
                    readinglog.LoggedBook.AuthorFirstName = Convert.ToString(reader["author_firstName"]);
                    readinglog.LoggedBook.AuthorLastName = Convert.ToString(reader["author_lastName"]);
                    readinglog.LoggedBook.ISBN = Convert.ToInt64(reader["isbn"]);
                    readinglog.TimeRead = Convert.ToInt32(reader["total_time"]);
                    readinglog.FormatType = Convert.ToString(reader["format_type"]);
                    readingLogs.Add(readinglog);
                }
            }

            return readingLogs;
        }

     
        public void AddNewBookLog(ReadingLog book, int userid)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand addBook = new SqlCommand(sqlAddBookLog, conn);
                addBook.Parameters.AddWithValue("@title", book.LoggedBook.Title);
            }
        }

    }
}
