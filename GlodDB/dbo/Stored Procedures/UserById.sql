-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
-- exec [dbo].[UserById] 110
CREATE PROCEDURE [dbo].[UserById]
    @Id int
AS
BEGIN
	
	SET NOCOUNT ON;

    DECLARE @Code INT = 200,
			@Message VARCHAR(255) = ''

    IF @Id <= 0
	BEGIN
	     SET @Code = 400
		 set @Message = ''
	END

    SELECT @Code AS Code, @Message AS [Message]

	SELECT *
	FROM 
       [dbo].[Users] U
	WHERE 
       U.Id=@Id
END