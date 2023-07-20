-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[VerifyEmail]
   @Email varchar(20)
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @Code INT = 401,
	        @Message VARCHAR(255) = '',
			@Id INT = 0

    IF EXISTS (SELECT * FROM [dbo].Users WHERE Email=@Email AND DeletedBy is null)
	BEGIN
	     SET @Id = 0
	     SET @Code = 200
		 SET @Message = 'Email already Exists'  	   		  
	END
	ELSE
	BEGIN
	    SET @Id = 0
	    SET @Code = 404
	    SET @Message = 'Email not exists'
	END

	SELECT @Code AS Code, @Message AS Message

	SELECT @Id AS Id                
END