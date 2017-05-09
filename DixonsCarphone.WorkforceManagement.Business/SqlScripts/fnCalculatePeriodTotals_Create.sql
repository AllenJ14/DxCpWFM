SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION fnCalculatePeriodTotals
(	
 @StoreNumber int,
 @PeriodYear nvarchar(50),
 @StartMonth smallint, 
 @EndMonth smallint
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT AccountEntryDetailId, SUM(ActualAmount) as ActualAmountTotal, SUM(BudgetAmount) As BudgetAmountTotal
	FROM AccountEntryDetail
	WHERE AccountEntryHeaderId IN 
	(
		SELECT DISTINCT(AccountEntryHeaderId) FROM AccountEntryHeader 
		WHERE StoreNumber = @StoreNumber AND PeriodYear = @PeriodYear AND (PeriodMonth BETWEEN @StartMonth AND @EndMonth)
	)
    GROUP BY AccountEntryDetailId
)
GO
