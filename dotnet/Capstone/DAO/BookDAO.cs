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
        private readonly string sqlAddNewBook = @"INSERT INTO book (title, author_firstName, author_lastName, isbn) VALUES (@title, @firstName, @lastName, @isbn); SELECT @@IDENTITY";
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

                SqlDataReader reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                    bookID = Convert.ToInt32(reader["book_id"]);
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

                bookID = Convert.ToInt32(cmd.ExecuteScalar());

            }

                return bookID;
        }

        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"SELECT * FROM book", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Book book = new Book();
                    book.BookId = Convert.ToInt32(reader["book_id"]);
                    book.Title = Convert.ToString(reader["title"]);
                    book.AuthorFirstName = Convert.ToString(reader["author_firstName"]);
                    book.AuthorLastName = Convert.ToString(reader["author_lastName"]);
                    book.ISBN = Convert.ToInt64(reader["isbn"]);

                    books.Add(book);
                }

                return books;

            }

        }
    }
}
