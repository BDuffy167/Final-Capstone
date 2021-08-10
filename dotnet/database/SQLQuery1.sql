--Non User creates a new account
BEGIN TRAN

INSERT INTO users (
	username,
	password_hash,
	salt,
	user_role,

	
	
	