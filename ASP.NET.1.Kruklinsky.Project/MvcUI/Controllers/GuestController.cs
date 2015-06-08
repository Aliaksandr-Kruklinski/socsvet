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
    public class GuestController : Controller
    {
        private IUserQueryService userQueryService;
        private IProfileService profileService;
        private IWallService wallService;

        public GuestController(IProfileService profileService, IWallService wallService, IUserQueryService userQueryService)
        {
            if (profileService == null)
            {
                throw new System.ArgumentNullException("profileService", "Profile service is null.");
            }
            this.profileService = profileService;
            this.wallService = wallService;
            this.userQueryService = userQueryService;
        }
        //
        // GET: /Guest/

        public ActionResult Index(string userId, int page = 1)
        {
            var user = userQueryService.GetUser(userId);
            if (user != null)
            {
                var model = new Guest { Profile = user.Profile.ToWeb() };
                model.UserId = userId;
                ViewBag.SelectedPage = page;
                if ( !model.Profile.Birthday.HasValue || model.Profile.Birthday.Value.Year.CompareTo(DateTime.Today.Year - 100) <= 0) model.Profile.Birthday = null;
                return View(model);
            }
            return RedirectToAction("Index", "Home", new { page = 1 });
        }

        [HttpPost]
        public ActionResult Index(Guest model)
        {
            try
            {
                var userId = Membership.GetUser(this.User.Identity.Name).ProviderUserKey.ToString();
                var sender = userQueryService.GetUser(userId);
                var user = userQueryService.GetUser(model.UserId);

                wallService.AddMessage(user.Wall, new BLL.Interface.Entities.WallMessage
                {
                    Text = model.Profile.FirstName,
                    Time = DateTime.Now,
                    UserId = sender.Id
                });

                return RedirectToAction("Index", "Guest", new { userId = model.UserId, page = 1});
            }
            catch
            {
                return RedirectToAction("Index", "Guest", new { userId = model.UserId, page = 1 });
            }
        }
    }
}
