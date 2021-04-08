using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MockDevOps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MockDevOps.Controllers
{
    public class InviteController : Controller
    {
        private readonly DevopsContext _context;

        public InviteController (DevopsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult SendInvite()
        {
            List<InviteUserModel> users = new List<InviteUserModel>();
            List<Project> projects = _context.Projects.ToList();
            InviteViewModel ivm = new InviteViewModel();

            foreach(AspNetUser u in _context.AspNetUsers)
            {
                if(u.Id != User.FindFirst(ClaimTypes.NameIdentifier).Value)
                {
                    InviteUserModel i = new InviteUserModel();
                    i.Id = u.Id;
                    i.Username = u.NormalizedUserName;
                    users.Add(i);
                }
            }

            ivm.Projects = projects;
            ivm.Users = users;

            return View(ivm);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SendInvite(string ium, int projectId)
        {
            Invite i = new Invite();
            i.ProjectId = projectId;
            i.Receiver = _context.AspNetUsers.Where(x => x.Id == ium).Select(x => x.Id).ToList()[0];

            _context.Invites.Add(i);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }

}
