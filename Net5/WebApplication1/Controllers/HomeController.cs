using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using WebApplication1.Filter;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [MyHeader("name", "hung")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [ServiceFilter(typeof(MetadataActionFilterAttribute))]
        public IActionResult Index()
        {
            DataCollector dataCollector = new DataCollector();
            dataCollector.StudentList.Add(new Student() { Id = 2 });
            //     var request = HttpContext.Request;
            var url = UriHelper.GetEncodedUrl(HttpContext.Request);
            var url02 = UriHelper.GetDisplayUrl(HttpContext.Request);
            var url03 = UriHelper.GetEncodedPathAndQuery(HttpContext.Request);


            var cacheControl = new CacheControlHeaderValue()
            {
                Public = true
            };
            cacheControl.Extensions.Add(new NameValueHeaderValue("StaleWhileRevalidate", "2"));

            //HttpContext.Response.GetTypedHeaders().CacheControl.Extensions.Add(new NameValueHeaderValue("stale", "2"));
            //HttpContext.Response.GetTypedHeaders().CacheControl.Extensions.Add(new NameValueHeaderValue("stale", "3"));
            HttpContext.Response.GetTypedHeaders().CacheControl = cacheControl;
            return View();
        }

        [ServiceFilter(typeof(DataTrackingFilter))]
        public IActionResult About()
        {
            var url = UriHelper.GetEncodedUrl(HttpContext.Request);
            var url02 = UriHelper.GetDisplayUrl(HttpContext.Request);
            var url03 = UriHelper.GetEncodedPathAndQuery(HttpContext.Request);
            return View();
        }

        [Authorize(Policy = "RequireAdminRole")]
        [Authorize(Policy = "Founder")]
        public IActionResult Admin()
        {
            var url = UriHelper.GetEncodedUrl(HttpContext.Request);
            var url02 = UriHelper.GetDisplayUrl(HttpContext.Request);
            var url03 = UriHelper.GetEncodedPathAndQuery(HttpContext.Request);
            return View();
        }

      //  [Authorize(Policy = "MinimumRole")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [HttpOptions]
        public IActionResult Get() 
        {
            var data = new List<int>() { 1, 2, 3 };
            return Json(data);
        }
    }

    public class DataCollector 
    {
        public IList<Student> StudentList { get; }
        public DataCollector() 
        {
            StudentList = new List<Student>()
            {
                new Student() { Id = 1 }
            };
        }
    }

    public class Student 
    {
        public int Id { get; set; }
    }
}
