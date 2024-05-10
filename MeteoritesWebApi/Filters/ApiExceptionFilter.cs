using Domain.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace MeteoritesWebApi.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        public ApiExceptionFilter(IWebHostEnvironment env)
        {
            _env = env;
        }
        public void OnException(ExceptionContext context)
        {
            if (_env.IsDevelopment())
                return;

            switch (context.Exception)
            {
                case NotFoundException exp:
                    context.Exception = null;
                    context.HttpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;
                    context.Result = new NotFoundResult();
                    break;
                /*...*/
                default:
                    break;
            }
        }
    }
}
