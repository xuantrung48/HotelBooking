USE [CoCoHomeStayDb]
GO

/****** Object:  StoredProcedure [dbo].[Promotion_GetAvailable]    Script Date: 14/08/2020 10:01:56 CH ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Trung
-- Create date: 14/8/2020
-- Description:	Get Promotion's Max DiscountRates available
-- =============================================
CREATE PROCEDURE [dbo].[Promotion_GetAvailable]
AS
BEGIN
	DECLARE @CurrentDate DATE
	SET @CurrentDate = GETDATE()
	SELECT RoomTypeId, MAX(DiscountRates) AS DiscountRates FROM PROMOTION
	INNER JOIN PROMOTIONAPPLY ON PROMOTION.PromotionId = PROMOTIONAPPLY.PromotionId
	WHERE @CurrentDate >= StartDate AND @CurrentDate <= EndDate AND IsDeleted = 0
	GROUP BY RoomTypeId
END
GO

