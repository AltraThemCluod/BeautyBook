CREATE TYPE [dbo].[DesignationPermissionType] AS TABLE (
    [PermissionMappingId] INT NULL,
    [PermissionId]        INT NOT NULL,
    [AllowAccess]         BIT NOT NULL);

