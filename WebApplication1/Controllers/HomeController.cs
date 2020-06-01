using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ResourceService _resourceService;

        public HomeController(ILogger<HomeController> logger, ResourceService resourceService)
        {
            _logger = logger;
            _resourceService = resourceService;
        }

        public IActionResult Index()
        {
            var resources = _resourceService.Get();
            _logger.Log(LogLevel.Information, "Loading resources");

            return View(resources);
        }


        [HttpGet("{id:length(24)}", Name = "GetResource")]
        public ActionResult<Resource> Get(string id)
        {
            var resource = _resourceService.Get(id);

            if (resource == null)
            {
                return NotFound();
            }

            return resource;
        }

        // POST: Resource/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<Resource> Create(Resource resource)
        {
            try
            {
                if (resource == null){
                    resource = new Resource { ResourceTitle = "Test Overflow", ResourceDescription = "This is Create test", Uri = "www.testoverflow.com", UpVote = 11, DownVote = 10 };
                }
                // TODO: Add insert logic here
                _resourceService.Create(resource);

                return CreatedAtRoute("GetResource", new { id = resource.Id.ToString() }, resource);
            }
            catch
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
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
