
CREATE PROCEDURE [dbo].[UserSelectAll]

AS

SET NOCOUNT ON

SELECT [UserID],
	[Name],
	[Phone],
	[Pin],
	[Status],
	[CreatedDate]
FROM [User]
ORDER BY [CreatedDate] DESC