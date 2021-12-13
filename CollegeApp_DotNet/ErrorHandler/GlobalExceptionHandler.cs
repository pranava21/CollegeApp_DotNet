using CollegeApp_DotNet.WebServices.ErrorHandler;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace CollegeApp_DotNet.WebServices.ErrorHandler
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, Serilog.ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.Error($"Something went wrong: {contextFeature.Error}");

                        ErrorDetails details = new ErrorDetails();
                        details.StatusCode = context.Response.StatusCode;
                        details.Message = "Internal Server Error";

                        logger.Error(JsonConvert.SerializeObject(details));
                    }
                });
            });
        }
    }
}