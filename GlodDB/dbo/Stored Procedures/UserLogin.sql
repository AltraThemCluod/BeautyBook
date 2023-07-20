CREATE PROCEDURE [dbo].[UserLogin]
	@Email varchar(500),
	@Password varchar(200),
	@AppVersion varchar(20) = NULL,
	@BuildVersion varchar(20) = NULL,
	@DeviceType varchar(20) = NULL,
	@Model varchar(200) = NULL,
	@Make varchar(100) = NULL,
	@OS varchar(20) = NULL
AS
BEGIN
	DECLARE @Code INT = 401,
	@Message VARCHAR(255) = ''
	IF EXISTS (SELECT 1 FROM [dbo].[Users] where Email = @Email And [Password] = CONVERT(VARCHAR(300), HASHBYTES('SHA1', @Password  + Salt), 1))
	BEGIN
		SET @Code = 200
		SET @Message = 'User retrived sucessfully'

		SELECT @Code AS Code, @Message AS Message
		SELECT 
		*
		FROM 
		[dbo].[Users] U 
		WHERE 
		U.Email = @Email AND 
		U.[Password] = CONVERT(VARCHAR(300), HASHBYTES('SHA1', @Password  + Salt), 1)
	END
	ELSE
	BEGIN
	    SET @Code = 401
		SET @Message = 'Incorrect email and password'
		SELECT @Code AS Code, @Message AS Message
		SELECT * FROM [dbo].[Users] where Id=0			
	END
END