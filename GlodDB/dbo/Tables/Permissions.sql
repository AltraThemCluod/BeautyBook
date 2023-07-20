CREATE TABLE [dbo].[Permissions] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [ModuleName]     VARCHAR (100)  NULL,
    [SubModuleName]  VARCHAR (100)  NULL,
    [PageName]       VARCHAR (100)  NULL,
    [PermissionDesc] VARCHAR (2000) NULL,
    [CreatedBy]      INT            NULL,
    [CreatedDate]    DATETIME       NULL,
    CONSTRAINT [PK_PermissionId] PRIMARY KEY CLUSTERED ([Id] ASC)
);

