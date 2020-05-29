using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            MongoCRUD db = new MongoCRUD("Resources");
//            db.InsertRecord("Resources", new Resource { ResourceTitle = "Stack Overflow", ResourceDescription = "This is stack overflow test", Uri = "www.stackoverflow.com", UpVote = 10, DownVote = 10 });
            var resources = db.LoadResources<Resource>("Resources");
            _logger.Log(LogLevel.Information, "Loading resources");
            foreach (var item in resources)
            {
                _logger.Log(LogLevel.Information, item.ResourceTitle);
            }
            
/*            ViewData["Resources"] = resources;
            ViewData["NumTimes"] = 1;*/

            return View(resources);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
