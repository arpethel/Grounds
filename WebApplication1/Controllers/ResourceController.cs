using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Localization;

namespace WebApplication1.Controllers
{

    [Route("/resource")]
    [ApiController]
    public class ResourceController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ResourceService _resourceService;

        public ResourceController(ILogger<HomeController> logger, ResourceService resourceService)
        {
            _logger = logger;
            _resourceService = resourceService;
        }

      //  [HttpGet("get{id}")]
        public ActionResult<Resource> Get(string id)
        {
            _logger.Log(LogLevel.Information, "Getting resource");
            var resource = _resourceService.Get(id);

            if (resource == null)
            {
                return NotFound();
            }

            return View(resource);
        }

        // POST: Resource/Create
        [HttpPost("create")]
       // [ValidateAntiForgeryToken]
        public ActionResult<Resource> Create()
        {
            _logger.Log(LogLevel.Information, "Creating resource");
            Resource resource = null;
            try
            {
                if (resource == null)
                {
                    resource = new Resource { ResourceTitle = "Test Overflow", ResourceDescription = "This is Create test", Uri = "www.testoverflow.com", UpVote = 11, DownVote = 10 };
                }
                // TODO: Add insert logic here
                _resourceService.Create(resource);

                return Redirect("/");
                //return CreatedAtRoute("GetResource", new { id = resource.Id.ToString() }, resource);
            }
            catch
            {
                return Content("Insert Record Error");
            }
        }


        /*        // GET: Resource/Details/5
                public ActionResult Details(int id)
                {
                    return View();
                }

                // GET: Resource/Create
                public ActionResult Create()
                {
                    return View();
                }

                // POST: Resource/Create
                [HttpPost]
                [ValidateAntiForgeryToken]
                public ActionResult Create(IFormCollection collection)
                {
                    try
                    {
                        // TODO: Add insert logic here

                        return RedirectToAction(nameof(Index));
                    }
                    catch
                    {
                        return View();
                    }
                }

                // GET: Resource/Edit/5
                public ActionResult Edit(int id)
                {
                    return View();
                }

                // POST: Resource/Edit/5
                [HttpPost]
                [ValidateAntiForgeryToken]
                public ActionResult Edit(int id, IFormCollection collection)
                {
                    try
                    {
                        // TODO: Add update logic here

                        return RedirectToAction(nameof(Index));
                    }
                    catch
                    {
                        return View();
                    }
                }

                // GET: Resource/Delete/5
                public ActionResult Delete(int id)
                {
                    return View();
                }*/

 /*       // POST: Resource/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
    }
}