using BLL.Interface.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcUI.Models;
using System.Web.Security;

namespace MvcUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IUserQueryService userQueryService;
        private IProfileService profileService;
        private IWallService wallService;

        public HomeController(IProfileService profileService, IWallService wallService, IUserQueryService userQueryService)
        {
            if (profileService == null)
            {
                throw new System.ArgumentNullException("profileService", "Profile service is null.");
            }
            this.profileService = profileService;
            this.wallService = wallService;
            this.userQueryService = userQueryService;
        }

        public ActionResult Index(int page = 1)
        {
            var model = HttpContext.Profile.ToWeb();
            ViewBag.SelectedPage = page;
            if (model.Birthday.Value.Year.CompareTo(DateTime.Today.Year - 100) <= 0) model.Birthday = null;
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string FirstName)
        {
            try
            {
                var userId = Membership.GetUser(this.User.Identity.Name).ProviderUserKey.ToString();
                var user = userQueryService.GetUser(userId);
                wallService.AddMessage(user.Wall, new BLL.Interface.Entities.WallMessage {
                    Text = FirstName,
                    Time = DateTime.Now,
                    UserId = user.Id
                });

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Private(int page = 1)
        {
            var model = HttpContext.Profile.ToWeb();
            ViewBag.SelectedPage = page;
            if (model.Birthday.Value.Year.CompareTo(DateTime.Today.Year - 100) <= 0) model.Birthday = null;
            return View(model);
        }

        [HttpPost]
        public ActionResult Private(string FirstName)
        {
            try
            {
                var userId = Membership.GetUser(this.User.Identity.Name).ProviderUserKey.ToString();
                var user = userQueryService.GetUser(userId);
                wallService.AddMessage(user.PrivateWall, new BLL.Interface.Entities.WallMessage
                {
                    Text = FirstName,
                    Time = DateTime.Now,
                    UserId = user.Id
                });

                return RedirectToAction("Private");
            }
            catch
            {
                return RedirectToAction("Private");
            }
        }
    }
}
