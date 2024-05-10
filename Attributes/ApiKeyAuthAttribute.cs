using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace HotelBackend.Attributes {
  public static class ApiKeyAuthExtensions
  {
      public static void AddApiKeyAuth(this IServiceCollection services) 
      {
          services.AddScoped<IAuthorizationFilter>(provider =>
                  new ApiKeyAuthAttribute());
      }
  }

  public class ApiKeyAuthAttribute : Attribute, IAuthorizationFilter
  {
      public void OnAuthorization(AuthorizationFilterContext context)
      {
          var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
          var apiKey = configuration["ApiKey"]; 

          if (!context.HttpContext.Request.Headers.TryGetValue("X-API-KEY", out var apiKeyFromHeader) ||
              !apiKeyFromHeader.Equals(apiKey))
          {
              context.Result = new UnauthorizedResult();
          }
      }
  }
}