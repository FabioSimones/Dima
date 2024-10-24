using Dima.Api.Response;
using Dima.Core.Models.Reports;
using Dima.Core.Requests.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Handlers
{
    public interface IReportHandler
    {
        Task<Resposta<List<IncomesAndExpenses>?>> GetIncomesAndExpensesReportAsync(GetIncomesAndExpensesRequest request);
        Task<Resposta<List<IncomesByCategory>?>> GetIncomesByCategoryReportAsync(GetIncomesByCategoryRequest request);
        Task<Resposta<List<ExpensesByCategory>?>> GetExpensesByCategoryReportAsync(GetExpensesByCategoryRequest request);
        Task<Resposta<FinancialSummary?>> GetFinancialSummaryReportAsync(GetFinancialSummaryRequest request);
    }
}
