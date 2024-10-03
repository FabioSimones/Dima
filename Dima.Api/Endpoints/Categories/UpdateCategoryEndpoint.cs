﻿using Dima.Api.Common.Api;
using Dima.Api.Response;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Categories
{
    public class UpdateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
            => app.MapPut(pattern: "/{id}", HandleAsync)
            .WithName("Categories: Update")
            .WithSummary("Atualiza uma categoria")
            .WithDescription("Atualiza uma categoria")
            .WithOrder(2)
            .Produces<Resposta<Category?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            ICategoryHandler handler, UpdateCategoryRequest request, long id)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;            
            var result = await handler.UpdateAsync(request);
            return result.IsSuccess
                ? Results.Ok(result)
                : Results.BadRequest(result);
        }
    }
}