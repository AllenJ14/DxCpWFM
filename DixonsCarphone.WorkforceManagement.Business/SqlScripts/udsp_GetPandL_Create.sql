CREATE PROCEDURE udsp_GetPandL
@StoreNumber int,
@PeriodYear nvarchar(50),
@PeriodMonth smallint,
@QtdStartMonth smallint,
@YtdStartMonth smallint
AS
--head
SELECT a.AccountEntryHeaderId, a.AccountEntryHeaderText, a.PeriodYear, a.PeriodMonth, a.StoreNumber, a.ManagerName,

--detail
b.AccountEntryDetailId, g.AccountEntryTypeId, b.AccountEntrySubTypeId, f.AccountEntySubTypeName AS AccountEntrySubTypeText,
b.DetailTitle, b.DetailText, b.ActualAmount, b.BudgetAmount, 

d.ActualAmountTotal As QtdActualAmount, 
d.BudgetAmountTotal AS QtdBudgetAmount, e.ActualAmountTotal As YtdActualAmount, e.BudgetAmountTotal AS YtdBudgetAmount,

--breakdown
 c.AccountEntryDetailBreakDownId, c.BreakdownTitle, c.BreakdownText, c.ActualAmount as BreakdownActualAmount,
c.BudgetAmount AS BreakdownBudgetAmount

FROM AccountEntryHeader a
INNER JOIN AccountEntryDetail b ON a.AccountEntryHeaderId = b.AccountEntryHeaderId
INNER JOIN AccountEntySubType f on b.AccountEntrySubTypeId = f.AccountEntySubTypeId
INNER JOIN AccountEntryType g on f.AccountEntryTypeId = g.AccountEntryTypeId
LEFT JOIN AccountEntryDetailBreakdown c on b.AccountEntryDetailId = c.AccountEntryDetailId
LEFT JOIN dbo.fnCalculatePeriodTotals(@StoreNumber, @PeriodYear, @QtdStartMonth, @PeriodMonth) d ON b.AccountEntryDetailId = d.AccountEntryDetailId
LEFT JOIN dbo.fnCalculatePeriodTotals(@StoreNumber, @PeriodYear, @YtdStartMonth, @PeriodMonth) e ON b.AccountEntryDetailId = e.AccountEntryDetailId
WHERE a.StoreNumber = @StoreNumber AND a.PeriodMonth = @PeriodMonth AND a.PeriodYear = @PeriodYear