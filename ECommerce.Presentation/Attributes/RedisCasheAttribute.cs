using ECommerce.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Presentation.Attributes
{
    public class RedisCasheAttribute : ActionFilterAttribute
    {
        private readonly int _durationInMins;

        public RedisCasheAttribute(int durationInMins = 5)
        {
            _durationInMins = durationInMins;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Get CasheService from DI container
            var cacheservice = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            // Create cacheKey based on(request path-Query param)
            var cacheKey = CreateCacheKey(context.HttpContext.Request);
            // Check if data exists in cash
            var cacheValue = await cacheservice.GetAsync(cacheKey);
            // If data exists, skip executing next(endpoint)
            if(cacheValue != null)
            {
                context.Result = new ContentResult()
                {
                    Content = cacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }
            // If not exists, execute next and store data in cash if endpoint response is 200 ok
            var executedContext = await next.Invoke();
            if(executedContext.Result is OkObjectResult result)
            {
                await cacheservice.SetAsync(cacheKey, result.Value!, TimeSpan.FromMinutes(_durationInMins));
            }
        }
        private string CreateCacheKey(HttpRequest request)
        {
            StringBuilder key = new StringBuilder();
            key.Append(request.Path);

            foreach (var item in request.Query.OrderBy(x => x.Key))
            {
                key.Append($"|{item.Key}-{item.Value}");
            }
            return key.ToString();
        }
    }
}
