using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PokemonGameAPI.Infrastructure.Exceptions;

namespace PokemonGameAPI.API.ExceptionHandlers;

public class NotFoundExceptionHandler:IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not NotFoundException)
        {
            return false;
        }

        var problemDetails = new ProblemDetails()
        {
            Status = StatusCodes.Status404NotFound,
            Title = "Not found",
            Detail = exception.Message,
        };
        httpContext.Response.StatusCode = problemDetails.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}