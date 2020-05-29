using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    /// <summary>
    /// Start here
    /// https://docs.microsoft.com/en-us/graph/sdks/create-requests?tabs=CS
    /// https://docs.microsoft.com/en-us/graph/api/resources/onenote-api-overview?view=graph-rest-1.0
    /// https://docs.microsoft.com/en-us/graph/integrate-with-onenote
    /// </summary>
    [Authorize]
    public class QueryController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public QueryController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// query against all related resources to the user and then give a view that displays search results.
        /// </summary>
        /// <returns></returns>
        public IActionResult Search(string args)
        {
            var data = QueryData(args);
            return View();
        }

        /// <summary>
        /// Make all async queries against available resources.
        /// </summary>
        /// <param name="agrs"></param>
        /// <returns></returns>
        [HttpGet]
        private async Task<IActionResult> QueryData(string agrs)
        {
            // Build a client application.
            IPublicClientApplication publicClientApplication = PublicClientApplicationBuilder
                        .Create("INSERT-CLIENT-APP-ID")
                        .Build();
            // Create an authentication provider by passing in a client application and graph scopes.
            DeviceCodeProvider authProvider = new DeviceCodeProvider(publicClientApplication);
            // Create a new instance of GraphServiceClient with the authentication provider.
            GraphServiceClient graphClient = new GraphServiceClient(authProvider);
            IOnenoteSectionsCollectionPage myOneNoteResults = await graphClient.Me.Onenote.Sections.Request()
               .Select(u => new
               {
                   u.DisplayName,
                   u.CreatedBy,
                   u.PagesUrl

               }).Filter("<filter condition>")
               .OrderBy("receivedDateTime")
               .GetAsync();


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
/*
 * https://docs.microsoft.com/en-us/graph/api/resources/onenote-api-overview?view=graph-rest-1.0
 */
