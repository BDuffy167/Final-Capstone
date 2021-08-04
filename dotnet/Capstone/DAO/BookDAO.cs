using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class BookDAO : IBookDAO
    {
        private readonly string sqlGetBookID = @"SELECT book_id FROM book where isbn = @isbn";
        private readonly string sqlAddNewBook = @"INSERT INTO book (title, author_firstName, author_lastName, isbn) VALUES (@title, @firstName, @lastName, @isbn);";
        private readonly string connectionString;

        public BookDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public int CheckIfBookExistsOnDB(Book inputBook)
        {
            int bookID = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlGetBookID, conn);
                cmd.Parameters.AddWithValue("@isbn", inputBook.ISBN);

                if(!DBNull.Value.Equals(cmd.ExecuteScalar()));
                {
                    bookID = (Int32)cmd.ExecuteScalar();
                }
                
            }
             return bookID;
        }

        public int AddNewBook(Book newBook)
        {
            int bookID = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sqlAddNewBook, conn);
                cmd.Parameters.AddWithValue("@title", newBook.Title);
                cmd.Parameters.AddWithValue("@firstName", newBook.AuthorFirstName);
                cmd.Parameters.AddWithValue("@lastName", newBook.AuthorLastName);
                cmd.Parameters.AddWithValue("@isbn", newBook.ISBN);

                bookID = (Int32)cmd.ExecuteScalar();

            }

                return bookID;
        }
    }
}
