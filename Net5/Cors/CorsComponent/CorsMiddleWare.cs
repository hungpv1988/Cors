using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;

namespace Cors.CorsComponent
{
    public class CorsMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ICorsService _corsService;
        private readonly ICorsPolicyProvider _corsPolicyProvider;
        private readonly CorsPolicy _policy;
        private readonly string _corsPolicyName;

        public CorsMiddleWare(RequestDelegate next, ICorsService service, ICorsPolicyProvider corsPolicyProvider, CorsPolicy policy, string corsPolicyName) 
        {
            _next = next;
            _corsService = service;
            _corsPolicyProvider = corsPolicyProvider;
            _policy = policy;
            _corsPolicyName = corsPolicyName;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey(CorsConstants.Origin) && context.Request.Path.ToString().Contains("Home"))
            {
                var corsPolicy = _policy ?? await _corsPolicyProvider?.GetPolicyAsync(context, _corsPolicyName);
                if (corsPolicy != null)
                {
                    var corsResult = _corsService.EvaluatePolicy(context, corsPolicy);
                    _corsService.ApplyResult(corsResult, context.Response);

                    var accessControlRequestMethod =
                        context.Request.Headers[CorsConstants.AccessControlRequestMethod];
                    if (string.Equals(
                            context.Request.Method,
                            CorsConstants.PreflightHttpMethod,
                            StringComparison.Ordinal) &&
                            !StringValues.IsNullOrEmpty(accessControlRequestMethod))
                    {
                        // Since there is a policy which was identified,
                        // always respond to preflight requests.
                        context.Response.StatusCode = StatusCodes.Status204NoContent;
                        return;
                    }
                }
            }

            await _next(context);
        }
    }
}
