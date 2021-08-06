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
        private readonly string sqlGetPersonalBook = @"SELECT
    b.book_id AS bookID,
	b.title as title,
	b.author_firstName AS author_first,
	b.author_lastName AS author_last,
	SUM(rl.total_time) AS totalTime,
	SUM(CAST (rl.isCompleted AS INT)) AS isCompleted
FROM
	reading_log rl
	INNER JOIN book b on rl.book_id = b.book_id
WHERE
	rl.user_id = 1
GROUP BY
    b.book_id,
	b.title,
	b.author_firstName,
	b.author_lastName
";
        private readonly string sqlGetUserBooks = @"SELECT
    rl.reading_log_id AS log_id,
	rl.user_id AS user_id,
	b.title AS title,
	b.book_id AS book_id,
	b.author_firstName AS author_firstName,
	b.author_lastName AS author_lastName,
	rf.format_type AS format_type,
	b.isbn AS isbn,
	rl.total_time AS total_time,
    rl.notes AS note
FROM reading_log rl
INNER JOIN book b ON rl.book_id = b.book_id
INNER JOIN reading_format rf ON rl.format_id = rf.format_id
WHERE rl.user_id = @user_id;";

        private readonly string sqlCheckIfBookByISBN = @"SELECT book_Id FROM book WHERE isbn = @isbn";
        private readonly string sqlAddBookLog = @"INSERT INTO reading_log(user_id, book_id, format_id, total_time, notes, isCompleted)
	VALUES (@user_id, @book_id, @format_id, @total_time, @note, @isCompleted); SELECT @@IDENTITY";

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
                    readinglog.LogID = Convert.ToInt32(reader["log_id"]);
                    readinglog.ReaderId = Convert.ToInt32(reader["user_id"]);
                    readinglog.LoggedBook.BookId = Convert.ToInt32(reader["book_id"]);
                    readinglog.LoggedBook.Title = Convert.ToString(reader["title"]);
                    readinglog.LoggedBook.AuthorFirstName = Convert.ToString(reader["author_firstName"]);
                    readinglog.LoggedBook.AuthorLastName = Convert.ToString(reader["author_lastName"]);
                    readinglog.LoggedBook.ISBN = Convert.ToInt64(reader["isbn"]);
                    readinglog.TimeRead = Convert.ToInt32(reader["total_time"]);
                    readinglog.FormatType = Convert.ToString(reader["format_type"]);
                    readinglog.Note = Convert.ToString(reader["note"]);
                    readingLogs.Add(readinglog);
                }
            }

            return readingLogs;
        }

        public int AddNewReadingLog(ReadingLog newLog, int userID, int bookID)
        {
            int readingLogID = 0;
            int formatID = GetFormatID(newLog.FormatType);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlAddBookLog, conn);
                cmd.Parameters.AddWithValue("@user_id", userID);
                cmd.Parameters.AddWithValue("@book_id", bookID);
                cmd.Parameters.AddWithValue("@format_id", formatID);
                cmd.Parameters.AddWithValue("@total_time", newLog.TimeRead);
                cmd.Parameters.AddWithValue("@note", newLog.Note);
                cmd.Parameters.AddWithValue("@isCOmpleted", 0);

                readingLogID = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return readingLogID;

        }

        public List<BookHistoryObj> GetUserBookHistory(int userID)
        {
            List<BookHistoryObj> bookHistory = new List<BookHistoryObj>();

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlGetPersonalBook, conn);
                cmd.Parameters.AddWithValue("@user_id", userID);

                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    BookHistoryObj bookLog = new BookHistoryObj();
                    bookLog.BookId = Convert.ToInt32(reader["bookID"]);
                    bookLog.Title = Convert.ToString(reader["title"]);
                    bookLog.AuthorFirstName = Convert.ToString(reader["author_first"]);
                    bookLog.AuthorLastName = Convert.ToString(reader["author_last"]);
                    bookLog.TotalTime = Convert.ToInt32(reader["totalTime"]);
                    bookLog.IsCompleted = Convert.ToInt32(reader["isCompleted"]);

                    bookHistory.Add(bookLog);
                }

            }
            return bookHistory;

        }

        private int GetFormatID(string format)
        {
           
            switch (format.ToLower())
            {
                case "paperback":
                    return 1;
                    break;
                case "ebook":
                    return 2;
                    break;
                case "audiobook":
                    return 3;
                    break;
                case "read-aloud (reader)":
                    return 4;
                    break;
                case "read-aloud (listener)":
                    return 5;
                    break;
                default:
                    return 6;
                    break;
            }
        }

    }
}
