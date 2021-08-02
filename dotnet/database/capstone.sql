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
	user_role varchar(50) NOT NULL
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
	format_id int NOT NULL,
	format_type varchar(50) NOT NULL,

	CONSTRAINT PK_formatID PRIMARY KEY (format_id)
)

CREATE TABLE reading_log(
	user_id int NOT NULL,
	book_id int NOT NULL,
	format_id int NOT NULL,
	total_time int NOT NULL,
	reading_log_id int IDENTITY(1,1) NOT NULL,
	notes varchar(255) ,
	isCompleted bit NOT NULL,

	CONSTRAINT PK_reading_id PRIMARY KEY (reading_log_id),
	CONSTRAINT fk_userID FOREIGN KEY (user_id) REFERENCES users(user_id),
	CONSTRAINT fk_bookID FOREIGN KEY (book_id) REFERENCES book(book_id),
	CONSTRAINT FK_formatID FOREIGN KEY (format_id) REFERENCES reading_format(format_id)
)

-- Populate default data for testing: user and admin with password of 'password'
-- These values should not be kept when going to Production
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');

INSERT INTO book (title, author_firstName, author_lastName, isbn) VALUES ('HitchHikers Guide To the Galxy', 'Douglass', 'Adams', 9781529046137)
--INSERT INTO reading_log(user_id, book_id, format_id, total_time,reading_log_id,notes,isCompleted)

GO
SELECT * FROM book