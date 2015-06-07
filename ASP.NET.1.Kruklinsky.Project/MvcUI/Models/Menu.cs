using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcUI.Models
{
    public class Menu
    {
        public List<LinkInfo> LinksInfo { get; set; }
        public int SelectedLink { get; set; }
    }

    public class LinkInfo
    {
        public string LinkText { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
    }
}