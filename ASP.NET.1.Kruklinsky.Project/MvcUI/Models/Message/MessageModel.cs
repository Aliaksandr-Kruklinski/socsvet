using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcUI.Models
{
    public class MessageModel
    {
        public IEnumerable<BLL.Interface.Entities.Dialog> Dilogs { get; set; }
        public String UserId { get; set; }
    }
}