-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UsersAll]
	@Offset INT,
	@Limit INT,
	@UserType INT
AS
BEGIN
	
	SET NOCOUNT ON;

    DECLARE @TotalRecords BIGINT;

	SET @TotalRecords = (
	   SELECT 
			count(*) 
		FROM 
			[dbo].Users
		WHERE 
		(ISNULL(@UserType,'') = 0 or UserType = @UserType) 
	)

	IF @Limit = 0 
	BEGIN 
	     SET @Limit = @TotalRecords
	END

	SELECT 
	     *
	FROM
	    [dbo].Users U
	WHERE 
	(ISNULL(@UserType,'') = 0 or U.UserType = @UserType) 
	ORDER BY U.Id DESC
	OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY

	SELECT @TotalRecords AS TotalRecords

END