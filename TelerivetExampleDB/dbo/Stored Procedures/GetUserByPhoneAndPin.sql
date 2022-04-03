CREATE PROCEDURE GetUserByPhoneAndPin
(
	@Name nvarchar(50),
	@Phone nvarchar(20),
	@Pin nvarchar(50)
)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT ISNULL(UserID, 0) FROM [User] WHERE Name = @Name and Phone = @Phone and Pin = @Pin
END