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
	b.isbn AS isbn,
	rl.total_time AS total_time
FROM reading_log rl
INNER JOIN users u ON rl.user_id = u.user_id
INNER JOIN book b ON rl.book_id = b.book_id
WHERE rl.user_id = @user_id;";

        public ReadingLogDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Book> GetUserBooks(int id)
        {
            List<Book> userBooks = new List<Book>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlGetUserBooks, conn);
                cmd.Parameters.AddWithValue("@user_id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Book book = new Book();

                    book.BookId = Convert.ToInt32(reader["book_id"]);
                    book.Title = Convert.ToString(reader["title"]);
                    book.AuthorFirstName = Convert.ToString(reader["author_firstName"]);
                    book.AuthorLastName = Convert.ToString(reader["author_lastName"]);
                    book.ISBN = Convert.ToInt64(reader["isbn"]);
                    book.TimeRead = Convert.ToInt32(reader["total_time"]);
                    userBooks.Add(book);
                }
            }

            return userBooks;
        }

    }
}
