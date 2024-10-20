using Dima.Api.Data;
using Dima.Api.Response;
using Dima.Core.Common.Extensions;
using Dima.Core.Enums;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers
{
    public class TransactionHandler (AppDbContext context) : ITransactionHandler
    {
        public async Task<Resposta<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            if (request is { Type: ETransactionType.Withdraw, Amount: >= 0 })
                request.Amount *= -1;

            try
            {
                var transaction = new Transaction
                {
                    UserId = request.UserId,
                    CategoryId = request.CategoryId,
                    CreatedAt = DateTime.Now,
                    Amount = request.Amount,
                    PaidOrReceivedAt = request.PaidOrReceivedAt,
                    Title = request.Title,
                    Type = request.Type
                };
                await context.Transactions.AddAsync(transaction);
                await context.SaveChangesAsync();

                return new Resposta<Transaction?>(transaction, 201, "Transação criada com sucesso");
            }
            catch 
            {
                return new Resposta<Transaction?>(null, 501, "Não foi possivel criar a transação.");
            }
        }

        public async Task<Resposta<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            if (request is { Type: ETransactionType.Withdraw, Amount: >= 0 })
                request.Amount *= -1;

            try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transaction is null)
                    return new Resposta<Transaction?>(null, 404, "Transação não encontrada");

                transaction.CategoryId = request.CategoryId;
                transaction.Amount = request.Amount;
                transaction.Title = request.Title;
                transaction.Type = request.Type;
                transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;
                
                context.Transactions.Update(transaction);
                await context.SaveChangesAsync();

                return new Resposta<Transaction?>(transaction);
            }
            catch 
            {
                return new Resposta<Transaction?>(null, 500, "Não foi possivel recuperar sua transação.");
            }
        }

        public async Task<Resposta<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transaction is null)
                    return new Resposta<Transaction?>(null, 404, "Transação não encontrada");
                context.Transactions.Remove(transaction);
                await context.SaveChangesAsync();

                return new Resposta<Transaction?>(transaction);
            }
            catch
            {
                return new Resposta<Transaction?>(null, 500, "Não foi possivel recuperar sua transação.");
            }
        }

        public async Task<Resposta<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
        {
            try
            {
                var transaction = await context.Transactions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                return transaction is null
                    ? new Resposta<Transaction?>(null, 404, "Transação não encontrada")
                    : new Resposta<Transaction?>(transaction);
            }
            catch
            {
                return new Resposta<Transaction?>(null, 500, "Não foi possivel encontrar a transação.");
            }
        }

        public async Task<PagedResponse<List<Transaction>?>> GetByPeriod(GetTransactionByPeriodRequest request)
        {
            try
            {
                request.StartDate ??= DateTime.Now.GetFirstDay();
                request.EndDate ??= DateTime.Now.GetLastDay();
            }
            catch
            {
                return new PagedResponse<List<Transaction>?>(null, 500,
                    "Não foi possível determinar a data de início ou término");
            }

            try
            {
                var query = context
                    .Transactions
                    .AsNoTracking()
                    .Where(x =>
                        x.PaidOrReceivedAt >= request.StartDate &&
                        x.PaidOrReceivedAt <= request.EndDate &&
                        x.UserId == request.UserId)
                    .OrderBy(x => x.PaidOrReceivedAt);

                var transactions = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Transaction>?>(
                    transactions,
                    count,
                    request.PageNumber,
                    request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Não foi possível obter as transações");
            }
        }

    }
}
