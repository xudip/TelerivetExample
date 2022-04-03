
CREATE PROCEDURE [dbo].[EventInsertOrUpdate]
(
	@EventID int,
	@Content nvarchar(1000),
	@FromNumber nvarchar(20),
	@PhoneID nvarchar(50)
)

AS

IF @EventID > 0
BEGIN
	UPDATE [Event]SET [Content] = @Content,
	[FromNumber] = @FromNumber,
	[PhoneID] = @PhoneID
WHERE [EventID] = @EventID

SELECT @EventID
END
ELSE
BEGIN
	INSERT INTO [Event](	[Content],	[FromNumber],	[PhoneID])VALUES(	@Content,	@FromNumber,	@PhoneID)
SELECT SCOPE_IDENTITY()
END