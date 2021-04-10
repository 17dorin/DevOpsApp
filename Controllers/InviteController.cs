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
            i.ProjectName = _context.Projects.Where(x => x.Id == projectId).Select(x => x.ProjectName).ToList()[0];
            i.Receiver = _context.AspNetUsers.Where(x => x.Id == ium).Select(x => x.Id).ToList()[0];

            _context.Invites.Add(i);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult YourInvites()
        {
            List<Invite> userInvites = _context.Invites.Where(x => x.Receiver == User.FindFirst(ClaimTypes.NameIdentifier).Value).ToList();

            return View(userInvites);
        }

        public async Task<IActionResult> AcceptInvite(int projectId)
        {
            ProjectUser invitedUser = new ProjectUser();
            invitedUser.ProjectId = projectId;
            invitedUser.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            invitedUser.GroupAdmin = false;

            _context.ProjectUsers.Add(invitedUser);
            await _context.SaveChangesAsync();

            Invite inviteToRemove = _context.Invites.Where(x => x.ProjectId == projectId && x.Receiver == User.FindFirst(ClaimTypes.NameIdentifier).Value).ToList()[0];
            _context.Invites.Remove(inviteToRemove);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }

}
