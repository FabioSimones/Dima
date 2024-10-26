using Dima.Api.Common.Api;
using Dima.Api.Response;
using Dima.Core.Handlers;
using Dima.Core.Models.Reports;
using Dima.Core.Requests.Reports;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Reports
{
    public class GetIncomesByCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapGet("/incomes", HandlerAsync)
            .Produces<Resposta<List<IncomesByCategory>?>>();

        private static async Task<IResult> HandlerAsync(ClaimsPrincipal user, GetIncomesByCategoryRequest request, IReportHandler handler)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
            var result = await handler.GetIncomesByCategoryReportAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
