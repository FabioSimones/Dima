using Dima.Core.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Requests.Categories
{
    public class GetCategoryByIdRequest : Requisicao
    {
        public long Id { get; set; }
    }
}
