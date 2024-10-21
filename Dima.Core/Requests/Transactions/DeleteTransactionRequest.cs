using Dima.Core.Request;

namespace Dima.Core.Requests.Transactions
{
    public class DeleteTransactionRequest : Requisicao
    {
        public long Id { get; set; }
    }
}
