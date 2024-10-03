﻿using Dima.Api.Data;
using Dima.Api.Response;
using Dima.Core.Common.Extensions;
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
            try
            {
                var transaction = new Transaction
                {
                    UserId = request.UserId,
                    CategoryId = request.CategoryId,
                    CreatedAt = DateTime.UtcNow,
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

        public async Task<Resposta<List<Transaction>?>> GetByPeriod(GetTransactionByPeriodRequest request)
        {
            try
            {
                request.StartDate = DateTime.Now.GetFirstDay();
                request.EndDate = DateTime.Now.GetLastDay();
            }
            catch
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Não foi possível determinar a data de início ou termino.");
            }

            try
            {
                var query = context.Transactions
                .AsNoTracking()
                .Where(x => x.CreatedAt >= request.StartDate && x.CreatedAt <= request.EndDate && x.UserId == request.UserId)
                .OrderBy(x => x.CreatedAt);

                var transactions = await query.Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();
                var count = await query.CountAsync();

                return new PagedResponse<List<Transaction>?>(transactions, count, request.PageNumber, request.PageSize);
            }
            catch
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Não foi possivel obter as transações.");
            }


        }

    }
}