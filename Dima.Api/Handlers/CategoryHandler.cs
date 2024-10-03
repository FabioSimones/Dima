﻿using Dima.Api.Data;
using Dima.Api.Response;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers
{
    public class CategoryHandler(AppDbContext context) : ICategoryHandler
    {
        public async Task<Resposta<Category?>> CreateAsync(CreateCategoryRequest request)
        {
            try
            {
                var category = new Category
                {
                    UserId = request.UserId,
                    Title = request.Title,
                    Description = request.Description
                };

                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return new Resposta<Category?>(category, 201, "Categoria criada com sucesso.");
            }
            catch
            {
                return new Resposta<Category?>(null, 500, "Não foi possivel criar a categoria.");
            }
        }

        public async Task<Resposta<Category?>> UpdateAsync(UpdateCategoryRequest request)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (category is null)
                    return new Resposta<Category?>(null, 404, "Categoria não encontrada");

                category.Title = request.Title;
                category.Description = request.Description;

                context.Categories.Update(category);
                await context.SaveChangesAsync();
                return new Resposta<Category?>(category, message: "Categoria atualizada com sucesso");
            }
            catch
            {
                return new Resposta<Category?>(null, 500, "Não foi possivel atualizar a categoria.");
            }

        }

        public async Task<Resposta<Category?>> DeleteAsync(DeleteCategoryRequest request)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (category is null)
                    return new Resposta<Category?>(null, 404, "Categoria não encontrada");

                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                return new Resposta<Category?>(category, message: "Categoria excluída com sucesso");
            }
            catch
            {
                return new Resposta<Category?>(null, 500, "Não foi possivel excluir a categoria.");
            }
        }

        public async Task<Resposta<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
        {
            try
            {
                var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                return category is null
                    ? new Resposta<Category?>(null, 404, "Categoria não encontrada")
                    : new Resposta<Category?>(category);
            }
            catch
            {
                return new Resposta<Category?>(null, 500, "Não foi possivel encontrar a categoria.");
            }
        }

        public async Task<PagedResponse<List<Category>>> GetAllAsync(GetAllCategoriesRequest request)
        {
            try
            {
                var query = context.Categories
                    .AsNoTracking()
                    .Where(x => x.UserId == request.UserId)
                    .OrderBy(x => x.Title);

                var categories = await query
                    .Skip((request.PageNumber -1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Category>>(categories, count, request.PageNumber, request.PageSize);
            }
            catch
            {

                return new PagedResponse<List<Category>>(null, 500, "Não foi possivel listar as categoria.");
            }
        }   
    }
}