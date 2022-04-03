CREATE TABLE [dbo].[User] (
    [UserID]      INT           IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50) NOT NULL,
    [Phone]       NVARCHAR (20) NOT NULL,
    [Pin]         NVARCHAR (50) NOT NULL,
    [Status]      BIT           NOT NULL,
    [CreatedDate] DATETIME      CONSTRAINT [DF_User_CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    [Role]        NVARCHAR (50) CONSTRAINT [DF_User_Role] DEFAULT (N'Admin') NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserID] ASC)
);

