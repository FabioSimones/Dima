CREATE or ALTER VIEW [vwGetExpensesByCategory] AS
SELECT [Transaction].[UserId], [Category].[Title] as [Category], Year([Transaction].[PaidOrReceivedAt]) as [Year], SUM([Transaction].[Amount]) as [Expenses]
from [Transaction] INNER JOIN [Category] ON [Transaction].[CategoryId] = [Category].[Id]
WHERE[Transaction].[PaidOrReceivedAt] >= DATEADD(Month, -11, Cast(GetDate() as Date))
AND [Transaction].[PaidOrReceivedAt] < DATEADD(Month, 1, Cast(GetDate() as Date))
AND [Transaction].[Type] = 2
GROUP BY
[Transaction].[UserId],
[Category].[Title],
Year([Transaction].[PaidOrReceivedAt])
