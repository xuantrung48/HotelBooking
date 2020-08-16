/****** Object:  StoredProcedure [dbo].[ServiceImage_Save]    Script Date: 15/08/2020 6:02:29 CH ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Trung
-- Create date: 15/8/2020
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ServiceImage_Save]
	@ServiceId INT,
	@ImageData NVARCHAR(max)
AS
BEGIN 
DECLARE @Message NVARCHAR(200) = 'Something went wrong, please try again!'
BEGIN TRY
		INSERT INTO [dbo].[SERVICEIMAGES]
        (ServiceId,
		ImageData)
		VALUES
        (@ServiceId,
		@ImageData)
		SET @Message = 'Service Image has been save successfully!'
		SELECT SCOPE_IDENTITY() AS Id, @Message AS [Message]
END TRY
BEGIN CATCH
		SELECT 0 AS Id, @Message AS [Message]
END CATCH
END
GO

