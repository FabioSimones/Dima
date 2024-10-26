using Dima.Api.Common.Api;
using Dima.Api.Response;
using Dima.Core.Handlers;
using Dima.Core.Models.Reports;
using Dima.Core.Requests.Reports;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Reports
{
    public class GetExpensesByCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapGet("/incomes-expenses", HandlerAsync)
            .Produces<Resposta<List<IncomesAndExpenses>?>>();

        private static async Task<IResult> HandlerAsync(ClaimsPrincipal user, GetIncomesAndExpensesRequest request, IReportHandler handler)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
            var result = await handler.GetIncomesAndExpensesReportAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
