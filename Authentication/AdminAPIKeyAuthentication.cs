using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Movies.Authentication
{
	public class AdminAPIKeyAuthentication : IAuthorizationFilter
	{
		private readonly IConfiguration _configuration;

		public AdminAPIKeyAuthentication(IConfiguration configuration)
		{
			_configuration = configuration;
		}
	
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			if (!context.HttpContext.Request.Headers.TryGetValue(AuthKeyConstant.ApiKeyHeaderName,
				out var extractedApiKey))
			{
				context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
				return;
			}
			var apiAdminKey = _configuration.GetValue<string>(AuthKeyConstant.ApiKeySectionName);
			if (!apiAdminKey.Equals(extractedApiKey))
			{
				context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
				return;
			}

		}

	}
}
