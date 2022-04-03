-- =============================================
CREATE PROCEDURE [dbo].[ApproveUser] 
	@Phone nvarchar(20)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @UserID INT = (SELECT UserID from [User] where Phone = @Phone);
	IF(ISNULL(@UserID, 0) = 0)
	BEGIN
		SET @UserID = -1;
	END
	ELSE
	BEGIN
		UPDATE [User] SET Status = 1 WHERE UserID = @UserID
	END
	SELECT @UserID;
END