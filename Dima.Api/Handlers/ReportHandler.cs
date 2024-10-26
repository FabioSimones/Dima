using Dima.Api.Data;
using Dima.Api.Response;
using Dima.Core.Enums;
using Dima.Core.Handlers;
using Dima.Core.Models.Reports;
using Dima.Core.Requests.Reports;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers
{
    public class ReportHandler(AppDbContext context) : IReportHandler
    {
        public async Task<Resposta<List<ExpensesByCategory>?>> GetExpensesByCategoryReportAsync(GetExpensesByCategoryRequest request)
        {
            try
            {
                var data = await context.ExpensesByCategories.AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderByDescending(x => x.Year)
                .ThenBy(x => x.Category)
                .ToListAsync();

                return new Resposta<List<ExpensesByCategory>?>(data);
            }
            catch
            {
                return new Resposta<List<ExpensesByCategory>?>(null, 500, "Não foi possível obter as entradas por categoria.");
            }
        }

        public async Task<Resposta<FinancialSummary?>> GetFinancialSummaryReportAsync(GetFinancialSummaryRequest request)
        {
            //Resumo financeiro do mês atual.

            try
            {
                var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var data = await context.Transactions.
                    AsNoTracking().
                    Where(x => x.UserId == request.UserId
                    && x.PaidOrReceivedAt >= startDate
                    && x.PaidOrReceivedAt <= DateTime.Now).
                    GroupBy(x => true).
                    Select(x => new FinancialSummary(
                        request.UserId,
                        x.Where(ty => ty.Type == ETransactionType.Deposit).Sum(t => t.Amount),
                        x.Where(ty => ty.Type == ETransactionType.Withdraw).Sum(t => t.Amount)
                        )).
                    FirstOrDefaultAsync();

                return new Resposta<FinancialSummary?>(data);
            }
            catch
            {
                return new Resposta<FinancialSummary?>(null, 500, "Não foi possível obter o resultado financeiro.");
            }
        }

        public async Task<Resposta<List<IncomesAndExpenses>?>> GetIncomesAndExpensesReportAsync(GetIncomesAndExpensesRequest request)
        {
            try
            {
                var data = await context.IncomesAndExpenses
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderByDescending(x => x.Year)
                .ThenBy(x => x.Month)
                .ToListAsync();

                return new Resposta<List<IncomesAndExpenses>?>(data);
            }
            catch
            {
                return new Resposta<List<IncomesAndExpenses>?>(null, 500, "Não foi possível obter as entradas e saídas.");                
            }
        }

        public async Task<Resposta<List<IncomesByCategory>?>> GetIncomesByCategoryReportAsync(GetIncomesByCategoryRequest request)
        {
            try
            {
                var data = await context.IncomesByCategories.AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderByDescending(x => x.Year)
                .ThenBy(x => x.Category)
                .ToListAsync();

                return new Resposta<List<IncomesByCategory>?>(data);
            }
            catch
            {
                return new Resposta<List<IncomesByCategory>?>(null, 500, "Não foi possível obter as entradas por categoria.");
            }
        }
    }
}
