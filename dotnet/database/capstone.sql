USE master
GO

-- Drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

-- Create Tables
CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL, 
	family_library int NOT NULL
	CONSTRAINT PK_user PRIMARY KEY (user_id)
)

CREATE TABLE book(
	book_id int IDENTITY(1,1) NOT NULL,
	title varchar(255) NOT NULL,
	author_firstName varchar(50) NOT NULL,
	author_lastName varchar(50) NOT NULL,
	isbn bigint NOT NULL,
	--maybe link isbn to format?
	CONSTRAINT PK_id PRIMARY KEY (book_id)
)
--add a book url
CREATE TABLE reading_format(
	format_id int IDENTITY(1, 1) NOT NULL,
	format_type varchar(50) NOT NULL,

	CONSTRAINT PK_formatID PRIMARY KEY (format_id)
)

CREATE TABLE family_library (
	ID int IDENTITY(1, 1) NOT NULL,
	library_id int NOT NULL,
	book_id int NOT NULL,

	CONSTRAINT PK_fl_id PRIMARY KEY (ID),
	CONSTRAINT fk_book_Id FOREIGN KEY (book_id) REFERENCES book(book_id)

)

CREATE TABLE personal_library(
	ID INT IDENTITY(1, 1) NOT NULL,
	user_id int NOT NULL,
	book_id int NOT NULL,
	isCompleted int NOT NULL

	CHECK(isCompleted <= 1 AND isCompleted >= 0),
	CONSTRAINT pk_personLib_id PRIMARY KEY(ID), 
	CONSTRAINT fk_pl_usr_id FOREIGN KEY (user_id) REFERENCES users(user_id),
	CONSTRAINT fk_pl_bookID FOREIGN KEY (book_id) REFERENCES book(book_id),
)

CREATE TABLE reading_log(
	reading_log_id int IDENTITY(1,1) NOT NULL,
	personal_library_id int NOT NULL,
	format_id int NOT NULL,
	total_time int NOT NULL,
	notes varchar(255)

	CONSTRAINT PK_reading_id PRIMARY KEY (reading_log_id),
	CONSTRAINT fk_pl_id FOREIGN KEY (personal_library_id) REFERENCES personal_library(id),
	CONSTRAINT FK_formatID FOREIGN KEY (format_id) REFERENCES reading_format(format_id)
)

-- Populate default data for testing: user and admin with password of 'password'
-- These values should not be kept when going to Production
INSERT INTO users (username, password_hash, salt, user_role, family_library) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user', 1);
INSERT INTO users (username, password_hash, salt, user_role, family_library) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin', 1);

INSERT INTO reading_format (format_type) VALUES ('Paperback'), ('ebook'), ('Audiobook'), ('Read-Aloud (Reader)'), ('Read-Aloud (Listener)'), ('Other');

INSERT INTO book (title, author_firstName, author_lastName, isbn) VALUES ('HitchHikers Guide To the Galxy', 'Douglass', 'Adams', 9781529046137);
INSERT INTO book (title, author_firstName, author_lastName, isbn) VALUES ('The Hobbit', 'J.R.R', 'Tolken', 9780044403371);

INSERT INTO personal_library(user_id, book_id, isCompleted) VALUES (1, 1, 1), (1, 2, 0)

INSERT INTO family_library(library_id, book_id) VALUES (1, 1);
INSERT INTO reading_log(personal_library_id, format_id, total_time, notes) VALUES (1, 1, 30, 'foo'), (1, 2, 1, 'buzz!');
GO

--User views personal library
SELECT
	pl.id AS pl_id,
	b.title AS title,
	b.author_firstName AS a_first,
	b.author_lastName AS a_last,
	b.isbn AS isbn,
	pl.isCompleted AS is_completed
FROM
	personal_library pl
	INNER JOIN users u ON pl.user_id = u.user_id
	INNER JOIN book b ON pl.book_id = b.book_id
WHERE
	u.user_id = 2; --make dynamic

--User views personal reading log
SELECT
	rl.reading_log_id AS logID,
	pl.user_id AS user_id,
	b.book_id AS book_id,
	b.title AS title,
	b.author_firstName AS author_first,
	b.author_lastName AS author_last,
	b.isbn AS isbn,
	rl.total_time AS total_time,
	rf.format_type AS format_type,
	rl.notes AS note,
	pl.isCompleted AS isCompleted
FROM
	reading_log rl
	INNER JOIN personal_library pl ON rl.personal_library_id = pl.ID
	INNER JOIN book b ON pl.book_id = b.book_id
	INNER JOIN reading_format rf ON rl.format_id = rf.format_id
WHERE
	pl.user_id = 1;

--Get summed reading logs for each book
SELECT
    b.book_id AS bookID,
	b.title as title,
	b.author_firstName AS author_first,
	b.author_lastName AS author_last,
    b.isbn AS isbn,
	SUM(rl.total_time) AS totalTime,
	SUM(CAST (pl.isCompleted AS INT)) AS isCompleted
FROM
	reading_log rl
	INNER JOIN personal_library pl ON rl.personal_library_id = pl.ID
	INNER JOIN book b ON pl.book_id = b.book_id
WHERE
	pl.user_id = 1
GROUP BY
    b.book_id,
	b.title,
	b.author_firstName,
	b.author_lastName,
    b.isbn




SELECT * FROM personal_library
SELECT * FROM book
select * from reading_log
SELECT * FROM users
SELECT * FROM family_library


SELECT
	b.book_id AS book_id,
	b.title AS title,
	b.author_firstName AS author_first,
	b.author_lastName AS author_last,
	b.isbn AS isbn
FROM
	family_library fl
	INNER JOIN book b ON fl.book_id = b.book_id
WHERE
	library_id = 1

SELECT book_id FROM book where isbn = 9780044403371 

--Various tests
SELECT * FROM reading_format;
