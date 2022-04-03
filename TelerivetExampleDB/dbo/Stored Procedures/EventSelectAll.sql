
CREATE PROCEDURE [dbo].[EventSelectAll]

AS

SET NOCOUNT ON

SELECT [EventID],
	[Content],
	[FromNumber],
	[PhoneID],
	[CreatedDate]
FROM [Event]
ORDER by CreatedDate DESC