﻿CREATE TABLE [dbo].[Quote] (
    [Id]                    INT           IDENTITY (1, 1) NOT NULL,
    [ServiceType]           INT           NULL,
    [PostalCode]            VARCHAR (10)  NULL,
    [PropertyLayout]        VARCHAR (50)  NULL,
    [Floor]                 INT           NULL,
    [Bedroom]               INT           NULL,
    [Bathroom]              INT           NULL,
    [Extras]                VARCHAR (MAX) NULL,
    [PropertyCondition]     VARCHAR (50)  NULL,
    [CleaningType]          INT           NULL,
    [CleaningPrefDay]       INT           NULL,
    [PropertyAddress]       VARCHAR (MAX) NULL,
    [ApplyCodes]            VARCHAR (50)  NULL,
    [ServiceDate]           VARCHAR (50)  NULL,
    [ServiceTime]           VARCHAR (50)  NULL,
    [FlexibleDateTime]      VARCHAR (200) NULL,
    [GainAccess]            VARCHAR (200) NULL,
    [GainAccessDescription] VARCHAR (500) NULL,
    [Park]                  VARCHAR (200) NULL,
    [AdditionalInformation] VARCHAR (500) NULL,
    [UploadDoc]             VARCHAR (100) NULL,
    [Tip]                   VARCHAR (50)  NULL,
    CONSTRAINT [PK_Quote] PRIMARY KEY CLUSTERED ([Id] ASC)
);

