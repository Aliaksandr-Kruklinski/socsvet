using MvcUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcUI.Controllers
{
    public class MenuController : Controller
    {
        public PartialViewResult Menu(int selectedLink = 0)
        {
            var viewModel = new Menu();
            List<LinkInfo> linksInfo = new List<LinkInfo>();
            linksInfo.Add(new LinkInfo { LinkText = "Стена", ControllerName = "Home", ActionName = "Index" });
            linksInfo.Add(new LinkInfo { LinkText = "Дневник", ControllerName = "Home", ActionName = "Private" });
            linksInfo.Add(new LinkInfo { LinkText = "Друзья", ControllerName = "Friend", ActionName = "Index" });
            linksInfo.Add(new LinkInfo { LinkText = "Психологи", ControllerName = "Helper", ActionName = "Index" });
            linksInfo.Add(new LinkInfo { LinkText = "Сообщения", ControllerName = "Message", ActionName = "Index" });
            linksInfo.Add(new LinkInfo { LinkText = "Настройки", ControllerName = "Options", ActionName = "Index" });
            viewModel.SelectedLink = selectedLink;
            viewModel.LinksInfo = linksInfo;
            return PartialView(viewModel);
        }

    }
}
