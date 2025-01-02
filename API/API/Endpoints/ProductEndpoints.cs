using API.Contracts;
using API.Interfaces;
using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints
{
    public static class ProductEndpoints
    {
        public static IEndpointRouteBuilder MapProductEndPoint(this IEndpointRouteBuilder app)
        {

            app.MapPost("/product", async ([FromBody] CreateProductDto request, IProductService _service) =>
            {
                var result = await _service.CreateAsync(request);
                if (result.IsSuccess)
                    return Results.Ok(result); // Returning 200

                return Results.BadRequest(result); // Returning 404
            });

            app.MapGet("/product/getAll", async (IProductService _service) =>
            {
                var result = await _service.GetAllAsync();
                if (result.IsSuccess)
                    return Results.Ok(result); // Returning 200

                return Results.BadRequest(result); // Returning 404
            });
            app.MapGet("/product/{id:int}", async (int id, IProductService _service) =>
            {
                var result = await _service.GetById(id);
                if (result.IsSuccess)
                    return Results.Ok(result); // Returning 200

                return Results.BadRequest(result); // Returning 404
            });

            app.MapPut("/product/{id:int}", async (int id, [FromBody] CreateProductDto request, IProductService _service) =>
            {
                if (id != request.Id)
                    return Results.BadRequest("Id Mismatch.");

                var result = await _service.UpdateAsync(id, request);
                if (result.IsSuccess)
                    return Results.Ok(result); // Returning 200

                return Results.BadRequest(result); // Returning 404
            });


            app.MapDelete("/product/{id:int}", async (int id, IProductService _service) =>
            {
                var result = await _service.DeleteAsync(id);
                if (result.IsSuccess)
                    return Results.Ok(result); // Returning 200

                return Results.BadRequest(result); // Returning 404

            });
            return app;
        }
    }
}
