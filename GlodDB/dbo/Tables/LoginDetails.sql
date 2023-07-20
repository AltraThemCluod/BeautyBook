CREATE TABLE [dbo].[LoginDetails] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [UserId]       INT            NULL,
    [AppVersion]   VARCHAR (20)   NULL,
    [BuildVersion] VARCHAR (20)   NULL,
    [DeviceType]   VARCHAR (20)   NULL,
    [Model]        NVARCHAR (100) NULL,
    [Make]         NVARCHAR (50)  NULL,
    [OS]           VARCHAR (20)   NULL,
    [CreatedBy]    INT            NULL,
    [CreatedDate]  VARCHAR (50)   NULL,
    [ModifiedBy]   INT            NULL,
    [ModifiedDate] VARCHAR (50)   NULL,
    [DeletedBy]    INT            NULL,
    [DeletedDate]  VARCHAR (50)   NULL,
    CONSTRAINT [PK_dbo.LoginDetails] PRIMARY KEY CLUSTERED ([Id] ASC)
);

