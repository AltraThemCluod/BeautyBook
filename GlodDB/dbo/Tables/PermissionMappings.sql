CREATE TABLE [dbo].[PermissionMappings] (
    [Id]           INT      IDENTITY (1, 1) NOT NULL,
    [RoleId]       INT      NULL,
    [PermissionId] INT      NULL,
    [AllowAccess]  BIT      NULL,
    [CreatedBy]    INT      NULL,
    [CreatedDate]  DATETIME NULL,
    [ModifiedBy]   INT      NULL,
    [ModifiedDate] DATETIME NULL,
    CONSTRAINT [PK_PermissionMappings] PRIMARY KEY CLUSTERED ([Id] ASC)
);

