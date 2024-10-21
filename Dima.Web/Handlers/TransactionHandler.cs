﻿using Dima.Api.Response;
using Dima.Core.Common.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using System.Net.Http.Json;

namespace Dima.Web.Handlers
{
    public class TransactionHandler (IHttpClientFactory httpClientFactory) : ITransactionHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<Resposta<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/transactions", request);
            return await result.Content.ReadFromJsonAsync<Resposta<Transaction?>>()
                   ?? new Resposta<Transaction?>(null, 400, "Não foi possível criar sua transação");
        }

        public async Task<Resposta<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            var result = await _client.DeleteAsync($"v1/transactions/{request.Id}");
            return await result.Content.ReadFromJsonAsync<Resposta<Transaction?>>()
                ?? new Resposta<Transaction?>(null, 400, "Falha ao deletar transação.");
        }

        public async Task<Resposta<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request) 
            => await _client.GetFromJsonAsync<Resposta<Transaction?>>($"v1/transactions/{request.Id}")
                ?? new Resposta<Transaction?>(null, 400, "Não foi possivel encontrar as transações.");

        public async Task<PagedResponse<List<Transaction>?>> GetByPeriod(GetTransactionByPeriodRequest request)
        {
            const string format = "yyyy-MM-dd";
            var startDate = request.StartDate is not null
                ? request.StartDate.Value.ToString(format)
                : DateTime.Now.GetFirstDay().ToString(format);

            var endDate = request.EndDate is not null
                ? request.EndDate.Value.ToString(format)
                : DateTime.Now.GetLastDay().ToString(format);

            var url = $"v1/transactions?startDate={startDate}&endDate={endDate}";

            return await _client.GetFromJsonAsync<PagedResponse<List<Transaction>?>>(url)
                ?? new PagedResponse<List<Transaction>?>(null, 400, "Não foi possivel obter as transações.");
        }

        public async Task<Resposta<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            var result = await _client.PutAsJsonAsync($"v1/transactions/{request.Id}", request);
            return await result.Content.ReadFromJsonAsync<Resposta<Transaction?>>()
                ?? new Resposta<Transaction?>(null, 400, "Falha ao atualizar transação.");
        }
    }
}
