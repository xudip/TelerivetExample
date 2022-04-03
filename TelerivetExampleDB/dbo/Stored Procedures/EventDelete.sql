
CREATE PROCEDURE [dbo].[EventDelete]
(
	@EventID int
)

AS

SET NOCOUNT OFF

DELETE FROM [Event]
WHERE [EventID] = @EventID