using Dima.Api.Response;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;

namespace Dima.Core.Handlers
{
    public interface ICategoryHandler
    {
        Task<Resposta<Category?>> CreateAsync(CreateCategoryRequest request);
        Task<Resposta<Category?>> UpdateAsync(UpdateCategoryRequest request);
        Task<Resposta<Category?>> DeleteAsync(DeleteCategoryRequest request);
        Task<Resposta<Category?>> GetByIdAsync(GetCategoryByIdRequest request);
        Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request);
    }
}
