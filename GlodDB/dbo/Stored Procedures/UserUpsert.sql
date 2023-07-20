-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UserUpsert] 
	@Id int = 0,
	@FirstName varchar(50) = null,
	@LastName varchar(50) = null,
	@Email varchar(250) = null,
	@Password varchar(250) = null,
	@Mobile varchar(50) = null,
	@Gender int = null,
	@Dob varchar(50) = null,
	@Latitude varchar(50) = null,
	@Longitude varchar(50) = null,
	@Status bit = null,	
	@LastLogin varchar(50) = null,
	@CreatedBy int  = null,
	@ModifiedBy int= null
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @Code INT = 401,
	        @Message VARCHAR(255) = ''

   IF @Id = 0
   BEGIN     
         IF EXISTS (SELECT * FROM [dbo].Users WHERE Email=@Email)
		 BEGIN
		      SET @Id = 0
		      SET @Code = 403
			  SET @Message = 'Email already exists.'
		 END	
		 ELSE
		 BEGIN	
				
			 IF @FirstName IS NOT NULL AND @FirstName = ''
			 BEGIN
			       SET @Code = 400
				   SET @Message = 'Please enter firstname'
			 END
			 ELSE IF @LastName IS NOT NULL AND @LastName = ''
			 BEGIN
			        SET @Code = 400
					SET @Message = 'Please enter lastname'
			 END
			 ELSE IF @Email IS NOT NULL AND @Email = ''
			 BEGIN
			        SET @Code = 400
					SET @Message = 'Please enter email'
			 END
			 ELSE
			 BEGIN	     
					INSERT INTO [dbo].Users
			                (			  			   
			                  FirstName
			                 ,LastName
							 ,Email
							 ,[Password]
							 ,Mobile
							 ,Status
							 ,CreatedBy
			                 ,CreatedDate							
			                ) 
			                values 
			                (			   			    
							  @FirstName
							 ,@LastName
							 ,@Email
							 ,CONVERT(VARCHAR(300),HASHBYTES('SHA1',@Password + CONVERT(NVARCHAR(100),NEWID())),1)
							 ,@Mobile
							 ,@Status
			                 ,@CreatedBy
			                 ,convert(varchar, getdate(), 20)							 
			                )
			        SET @Id = SCOPE_IDENTITY()
			        SET @Code = 200
					SET @Message = 'Users inserted successfully'	    
				END  
		    END
	END
	ELSE
	BEGIN	 
		     UPDATE
				[dbo].Users 
			 SET 			 
				 FirstName=ISNULL(@FirstName,FirstName)
				,LastName=ISNULL(@LastName,LastName)
				,Email=ISNULL(@Email,Email)
				,Mobile=ISNULL(@Mobile,Mobile)
				,Gender=ISNULL(@Gender,Gender)
				,Dob=ISNULL(@Dob,Dob)
				,Latitude=ISNULL(@Latitude,Latitude)
				,Longitude=ISNULL(@Longitude,Longitude)
				,[Status]=ISNULL(@Status,[Status])
				,ModifiedBy=ISNULL(@Id,ModifiedBy)
				,ModifiedDate=convert(varchar, getdate(), 20)					
			    WHERE 
			    Id = @Id			  
		          
				  SET @Code = 200
				  SET @Message = 'User update successfully'
	END
    
	SELECT @Code AS Code, @Message AS [Message]

	 SELECT 
		   *
	  FROM 
	       [dbo].[Users] 
	  where 
	       Id=@Id
	
END