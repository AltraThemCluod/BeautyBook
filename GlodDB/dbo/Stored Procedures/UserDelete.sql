-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UserDelete] 
    @Id int,
	@DeletedBy int 
AS
BEGIN
	
	     SET NOCOUNT ON;

		 UPDATE [dbo].Users
		 set 
			 [Status] = 0,
	     	 DeletedBy=@DeletedBy,
	     	 DeletedDate=convert(varchar,getdate(),20)
		 where 
		     Id=@Id		 
		 
		 select 1

END