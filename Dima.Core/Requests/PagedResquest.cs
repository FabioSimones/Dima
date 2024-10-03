using Dima.Core.Request;

namespace Dima.Core.Request
{
    public abstract class PagedResquest : Requisicao
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = Configuration.DefaultPageSize;
    }
}
