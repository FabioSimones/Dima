﻿using Dima.Core.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Requests.Transactions
{
    public class GetTransactionByIdRequest : Requisicao
    {
        public long Id { get; set; }
    }
}
