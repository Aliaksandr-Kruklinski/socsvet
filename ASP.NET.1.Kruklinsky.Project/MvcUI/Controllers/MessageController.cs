using BLL.Interface.Abstract;
using MvcUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcUI.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private IMessageService messageService;

        private IUserQueryService userQueryService;

        public MessageController(IMessageService messageService, IUserQueryService userQueryService)
        {
            if(messageService == null)
            {
                throw new System.NullReferenceException();
            }
            this.messageService = messageService;
            this.userQueryService = userQueryService;
        }

        public ActionResult Index()
        {
            var userId = Membership.GetUser(this.User.Identity.Name).ProviderUserKey.ToString();
            var dilogs = userQueryService.GetUserDilogs(userId);
            MessageModel view = new MessageModel { Dilogs = dilogs, UserId = userId };
            return View(view);
        }

        public ActionResult Dialog (int id)
        {
            var userId = Membership.GetUser(this.User.Identity.Name).ProviderUserKey.ToString();
            var dilog = userQueryService.GetUserDilog(userId, id);
            if(dilog != null)
            {
                MessagePagingModel result = new MessagePagingModel { Messages = dilog.Messages.OrderBy(m => m.Time).Skip(dilog.Messages.Count() - 20), DilogId = id };
                return View(result);
            }
            return Redirect("Index");
        }

        public ActionResult AddDialog (string userId)
        {
            List<string> users = new List<string>();
            var cUserId = Membership.GetUser(this.User.Identity.Name).ProviderUserKey.ToString();
            users.Add(userId);
            users.Add(cUserId);
            var dialogId = this.messageService.AddDialog(users);
            return RedirectToAction("Dialog", new { id = dialogId });
        }

        [HttpPost]
        public JsonResult AddMessage (int dialogId,string userId, string text)
        {
            if(!string.IsNullOrWhiteSpace(text))
            {
                var result = new BLL.Interface.Entities.Message {
                 Text = text,
                 Time = DateTime.Now,
                 User = new BLL.Interface.Entities.User { Id = userId},
                  Dialog = new BLL.Interface.Entities.Dialog {Id = dialogId}
                };
                this.messageService.AddMessage(result);
                return Json(result);
            }
            return Json("The string is empty");
        }
    }
}
