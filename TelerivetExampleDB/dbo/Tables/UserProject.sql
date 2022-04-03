CREATE TABLE [dbo].[UserProject] (
    [UserProjectID] INT            IDENTITY (1, 1) NOT NULL,
    [UserID]        INT            NULL,
    [ProjectCode]   NVARCHAR (50)  NOT NULL,
    [Amount]        DECIMAL (9, 2) NOT NULL,
    [Pin]           NVARCHAR (50)  NOT NULL,
    [Status]        BIT            NOT NULL,
    [CreatedDate]   DATETIME       CONSTRAINT [DF_UserProject_CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_UserProject] PRIMARY KEY CLUSTERED ([UserProjectID] ASC)
);



