CREATE PROCEDURE [dbo].[ChangeUserPin] 
(
	@Name nvarchar(50),
	@Phone nvarchar(20),
	@Pin nvarchar(50)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @UserID INT = (SELECT UserID FROM [User] WHERE [Name] = @Name AND  [Phone] = @Phone);
	IF(ISNULL(@UserID,0) =0)
	BEGIN
		SELECT -1;
	END
	ELSE
	BEGIN
		UPDATE [User] SET Pin = @Pin WHERE UserID = @UserID
		SELECT @UserID;
	END
END