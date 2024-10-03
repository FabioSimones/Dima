using Dima.Api.Response;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Handlers
{
    public interface ITransactionHandler
    {
        Task<Resposta<Transaction?>> CreateAsync(CreateTransactionRequest request);
        Task<Resposta<Transaction?>> UpdateAsync(UpdateTransactionRequest request);
        Task<Resposta<Transaction?>> DeleteAsync(DeleteTransactionRequest request);
        Task<Resposta<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request);
        Task<Resposta<List<Transaction>?>> GetByPeriod(GetTransactionByPeriodRequest request);

    }
}
