using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Cors.CorsComponent
{
    public class OurCorsPolicyProvider : ICorsPolicyProvider
    {
        private readonly CorsOptions _options;

        public OurCorsPolicyProvider(IOptions<CorsOptions> options)
        {
            _options = options.Value;
        }

        public Task<CorsPolicy> GetPolicyAsync(HttpContext context, string policyName)
        {
            // if no EnableCors, it still comes to this function with empty policyName
            return Task.FromResult(_options.GetPolicy(policyName ?? _options.DefaultPolicyName));
        }
    }
}
