using Microsoft.AspNetCore.Mvc.Cors;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Cors.CorsComponent
{
    public class CorsAuthorizationFilterFactory : IFilterFactory, IOrderedFilter
    {
        private readonly string _policyName;

        public CorsAuthorizationFilterFactory(string policyName)
        {
            _policyName = policyName;
        }

        public int Order { get { return int.MinValue + 100; } }

        public bool IsReusable => true;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var filter = serviceProvider.GetRequiredService<CorsAuthorizationFilter>();
            filter.PolicyName = _policyName;
            return filter;
        }

    }
}
