using Dima.Api.Response;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using System.Net.Http.Json;

namespace Dima.Web.Handlers
{
    public class CategoryHandler(IHttpClientFactory httpClientFactory) : ICategoryHandler
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);

        public async Task<Resposta<Category?>> CreateAsync(CreateCategoryRequest request)
        {
            var result = await _client.PostAsJsonAsync("v1/categories", request);
            return await result.Content.ReadFromJsonAsync<Resposta<Category?>>()
                ?? new Resposta<Category?>(null, 400, "Falha ao criar categoria");
        }

        public async Task<Resposta<Category?>> DeleteAsync(DeleteCategoryRequest request)
        {
            var result = await _client.DeleteAsync($"v1/categories/{request.Id}");
            return await result.Content.ReadFromJsonAsync<Resposta<Category?>>()
                   ?? new Resposta<Category?>(null, 400, "Falha ao excluir a categoria");
        }

        public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request)
            => await _client.GetFromJsonAsync<PagedResponse<List<Category>>>("v1/categories")
                ?? new PagedResponse<List<Category>>(null, 400, "Não foi possivel encontrar as categorias.");
        

        public async Task<Resposta<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
            => await _client.GetFromJsonAsync<Resposta<Category?>>($"v1/categories/{request.Id}")
                ?? new Resposta<Category?>(null, 400, "Não foi possível encontrar a categoria.");


        public async Task<Resposta<Category?>> UpdateAsync(UpdateCategoryRequest request)
        {
            var result = await _client.PutAsJsonAsync($"v1/categories/{request.Id}", request);
            return await result.Content.ReadFromJsonAsync<Resposta<Category?>>()
                   ?? new Resposta<Category?>(null, 400, "Falha ao atualizar a categoria");
        }
    }
}
