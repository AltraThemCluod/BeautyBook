CREATE TABLE [dbo].[Roles] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (50) NULL,
    [IsActive]     BIT           NULL,
    [CreatedBy]    INT           NULL,
    [CreatedDate]  VARCHAR (50)  NULL,
    [ModifiedBy]   INT           NULL,
    [ModifiedDate] VARCHAR (50)  NULL,
    [DeletedBy]    INT           NULL,
    [DeletedDate]  VARCHAR (50)  NULL,
    CONSTRAINT [PK_dbo.Roles] PRIMARY KEY CLUSTERED ([Id] ASC)
);

