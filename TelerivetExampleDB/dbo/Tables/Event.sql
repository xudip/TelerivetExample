CREATE TABLE [dbo].[Event] (
    [EventID]     INT             IDENTITY (1, 1) NOT NULL,
    [Content]     NVARCHAR (1000) NOT NULL,
    [FromNumber]  NVARCHAR (20)   NOT NULL,
    [PhoneID]     NVARCHAR (50)   NOT NULL,
    [CreatedDate] DATETIME        CONSTRAINT [DF_Event_CreatedDate] DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED ([EventID] ASC)
);

