using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnitionTicketingApp.DTO.BugDTO;
using UnitionTicketingApp.Entities;
using UnitionTicketingApp.Interfaces.IServices;

namespace UnitionTicketingApp.Controllers
{
    public class BugController : Controller
    {
        private readonly IBugService _bugService;

        public BugController(IBugService bugService)
        {
            _bugService = bugService;
        }

        public IActionResult CreateBug()
        {
            return View();
        }

        
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBug(CreateBugRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var createBug = _bugService.CreateBug(model);
            if(!createBug.Status)
            {
                ViewBag.Message = createBug.Message;
                return View();
            }
            return RedirectToAction("AllBugs", "Bug");
        }
        [Authorize]
        [HttpGet]
        public ActionResult AllBugs() 
        {
            var bugs = _bugService.AllBugs();
            return View(bugs.Data);
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetBug(Guid bugId)
        {
            var bug = _bugService.GetBug(bugId);
            if (!bug.Status) 
            {
                return NotFound(bug.Message);
            }
            return View(bug.Data);
        }
        [Authorize(Roles ="RD")]
        public IActionResult UpdateBugStatus(Guid Id)
        {
            var bug = _bugService.GetBug(Id);
            if (!bug.Status)
            {
                return NotFound(bug.Message);
            }
            return View(bug.Data);
        }
        [Authorize(Roles = "RD")]
        [HttpPost]
        public IActionResult UpdateBugStatus(Guid Id, string status)
        {
            var bug = _bugService.UpdateBugStatus(Id, status);
            if (!bug.Status)
            {
                return NotFound(bug.Message);
            }
            return RedirectToAction("AllBugs");
        }

    }
}
