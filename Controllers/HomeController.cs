using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MockDevOps.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MockDevOps.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DevopsContext _context = new DevopsContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //Gets list of all project ids associated with currently logged in user
            List<ProjectUser> UsersProjectIds = new List<ProjectUser>();
            List<Project> UsersProjects = new List<Project>();

            ProjectViewModel pvm = new ProjectViewModel();
            pvm.UserAdminGroups = new List<int?>();
            pvm.UserProjects = new List<Project>();


            if (_context.ProjectUsers.Count() != 0)
            {
                UsersProjectIds = _context.ProjectUsers.Where(x => x.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value).ToList();
                pvm.UserAdminGroups = UsersProjectIds.Where(x => x.GroupAdmin == true).Select(x => x.ProjectId).ToList();
            }
            else
            {
                return View(pvm);
            }
            //Gets list of actual project objects based on the above list
            UsersProjects = _context.Projects.Where(x => UsersProjectIds.Select(x => x.ProjectId).Contains(x.Id)).ToList();
            pvm.UserProjects = UsersProjects;

            return View(pvm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
