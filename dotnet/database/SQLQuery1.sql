CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL, 
	family_id int,
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

CREATE TABLE family_library (
	ID int IDENTITY(1, 1) NOT NULL,
	family_id int NOT NULL,
	book_id int NOT NULL,

	CONSTRAINT PK_fl_id PRIMARY KEY (ID),
	CONSTRAINT fk_family_id FOREIGN KEY (family_id) REFERENCES family(family_id),
	CONSTRAINT fk_book_id FOREIGN KEY (book_id) REFERENCES book(book_id)

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