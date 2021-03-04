using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication1.Filter
{
    public class MyHeaderAttribute: ResultFilterAttribute
    {
        private readonly string _name;
        private readonly string _value;
        public MyHeaderAttribute(string name, string value) 
        {
            _name = name;
            _value = value;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add(_name, new string[] { _value });
            base.OnResultExecuting(context);
        }
    }
}
