using Dima.Core.Enums;
using Dima.Core.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Requests.Transactions
{
    public class CreateTransactionRequest : Requisicao
    {
        [Required(ErrorMessage = "Título inválido")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Tipo inválido")]
        public ETransactionType Type { get; set; }

        [Required(ErrorMessage = "Valor inválido")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Categoria inválido")]
        public long CategoryId { get; set; }

        [Required(ErrorMessage = "Data inválido")]
        public DateTime? PaidOrReceivedAt { get; set; }
    }
}
