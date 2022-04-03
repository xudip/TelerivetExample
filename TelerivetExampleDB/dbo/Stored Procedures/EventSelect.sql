
CREATE PROCEDURE [dbo].[EventSelect]
(
	@EventID int
)

AS

SET NOCOUNT ON

SELECT [EventID],
	[Content],
	[FromNumber],
	[PhoneID]
FROM [Event]
WHERE [EventID] = @EventID