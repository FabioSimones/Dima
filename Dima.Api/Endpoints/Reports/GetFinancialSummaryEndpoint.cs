﻿using Azure.Core;
using Dima.Api.Common.Api;
using Dima.Api.Response;
using Dima.Core.Handlers;
using Dima.Core.Models.Reports;
using Dima.Core.Requests.Reports;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Reports
{
    public class GetFinancialSummaryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app) =>
            app.MapGet("/summary", HandlerAsync)
            .Produces<Resposta<FinancialSummary>>();

        private static async Task<IResult> HandlerAsync(
            ClaimsPrincipal user,
            GetFinancialSummaryRequest request,
            IReportHandler handler
            )
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
            var result = await handler.GetFinancialSummaryReportAsync(request);

            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
