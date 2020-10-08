CREATE PROC UserAdd
@Id int,
@FirstName varchar(50),
@LastName varchar(50),
@Email varchar(50),
@Age int,
@Username varchar(50),
@Password varchar(50) 
AS
	INSERT INTO [Table](FirstName, LastName, Email, Age, Username, Password)
	VALUES (@FirstName, @LastName, @Email, @Age, @Username, @Password)