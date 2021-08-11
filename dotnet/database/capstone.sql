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
	family_id int NOT NULL,
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

--CREATE TABLE family (
--	ID int IDENTITY(1,1) NOT NULL,
--	user_id int NOT NULL,
--	family_id int NOT NULL,

--	CONSTRAINT fk_user_Id FOREIGN KEY (user_id) REFERENCES users(user_id),
--	CONSTRAINT pk_family_id PRIMARY KEY (family_id)
--)

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

--CREATE TABLE family_library (
--	ID int IDENTITY(1, 1) NOT NULL,
--	family_id int NOT NULL,
--	book_id int NOT NULL,

--	CONSTRAINT PK_fl_id PRIMARY KEY (ID),
--	CONSTRAINT fk_family_id FOREIGN KEY (family_id) REFERENCES family(family_id),
--	CONSTRAINT fk_book_id FOREIGN KEY (book_id) REFERENCES book(book_id)

--)


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
INSERT INTO users (username, password_hash, salt, user_role, family_id) 
VALUES 
	('parent','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','parent', 1),
	('child','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','child', 1);




INSERT INTO reading_format (format_type) VALUES ('Paperback'), ('ebook'), ('Audiobook'), ('Read-Aloud (Reader)'), ('Read-Aloud (Listener)'), ('Other');

INSERT INTO book (title, author_firstName, author_lastName, isbn) VALUES ('HitchHikers Guide To the Galaxy', 'Douglas', 'Adams', 9781529046137);
INSERT INTO book (title, author_firstName, author_lastName, isbn) VALUES ('The Hobbit', 'J.R.R', 'Tolken', 9780345253422);
INSERT INTO book (title, author_firstName, author_lastName, isbn) VALUES ('Dune', 'Frank', 'Herbert', 9780425027066)

INSERT INTO personal_library(user_id, book_id, isCompleted) 
VALUES 
	(1, 1, 0), 
	(1, 2, 0),
	(1, 3, 0),
	(2, 2, 0);

--INSERT INTO family_library(library_id, book_id) VALUES (1,1), (1, 2), (1, 3);
--INSERT INTO reading_log(personal_library_id, format_id, total_time, notes) 
--VALUES 
--	(1, 1, 30, 'foo'), 
--	(1, 2, 1, 'buzz!');

GO


--displays family library
SELECT 
	b.title,
	b.author_firstname,
	b.author_lastname
FROM
	users u
	INNER JOIN personal_library pl ON u.user_id = pl.user_id
	INNER JOIN book b ON b.book_id = pl.book_id
WHERE
	u.family_id = 1
GROUP BY
	b.title,
	b.author_firstname,
	b.author_lastname;

-- Non user registers (just needs to add family library)
--INSERT INTO users
--	(username,
--	password_hash,
--	salt,
--	user_role,
--	family_id)
--VALUES
--	('a',
--	'asdf',
--	'asdf',
--	'parent',
--	(1 + (SELECT MAX(family_id)
--	FROM users)))

--user registers new family member
--INSERT INTO users
--	(username,
--	password_hash,
--	salt,
--	user_role,
--	family_id)
--VALUES
--	('a',
--	'asdf',
--	'asdf',
--	'child',
--	(SELECT family_id
--	FROM users
--	WHERE user_id = 4))


SELECT * FROM users

SELECT 
	MAX(family_id)
FROM
	users