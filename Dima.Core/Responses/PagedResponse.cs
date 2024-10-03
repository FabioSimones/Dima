using Dima.Core;
using System.Text.Json.Serialization;

namespace Dima.Api.Response
{
    public class PagedResponse<TData> : Resposta<TData>
    {
        public int CurrentPage { get; set; }        
        public int PageSize { get; set; } = Configuration.DefaultPageSize;
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);


        public PagedResponse(TData? data, int code = Configuration.DefaultStatusCode, string message = null) 
            :base(data, code, message)
        {
            
        }

        [JsonConstructor]
        public PagedResponse(TData? data, int totalCount, int currentPage = 1, int pageSize = Configuration.DefaultPageSize)            
        {
            Data = data;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalCount = totalCount;            
        }
    }
}
