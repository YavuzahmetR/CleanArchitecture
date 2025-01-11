
using CleanArchitecture.Domain.Entitites;
using CleanArchitecture.Persistance.Context;
using FluentValidation;
using System.Net;

namespace CleanArchitecture.WebApi.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly AppDbContext _dbContext;
        public ExceptionMiddleware(AppDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
				await next(context);
            }
			catch (Exception ex)
            {
                await LogExceptionToDatabase(ex, context.Request);
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            if (ex.GetType() == typeof(ValidationException))
            {
                return context.Response.WriteAsync(new ValidationErrorResult
                {
                    StatusCode = 403,
                    Errors = ((ValidationException)ex).Errors.Select(x => x.ErrorMessage)
                }.ToString());
            }
            return context.Response.WriteAsync(new ErrorResult
            {
                StatusCode = context.Response.StatusCode,
                Message = ex.Message
            }.ToString());
        }
        private async Task LogExceptionToDatabase(Exception ex, HttpRequest request)
        {
            // Log exception to database
            ErrorLog errorLog = new ErrorLog
            {
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace,
                RequestPath = request.Path,
                RequestMethod = request.Method,
                TimeStamp = DateTime.UtcNow
            };
            await _dbContext.Set<ErrorLog>().AddAsync(errorLog,default);
            await _dbContext.SaveChangesAsync(default);
        }
    }
}
