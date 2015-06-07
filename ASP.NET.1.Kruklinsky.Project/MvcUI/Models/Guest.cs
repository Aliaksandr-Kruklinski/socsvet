using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcUI.Models
{
    public class Guest
    {
        public Membership.Profile Profile { get; set; }
        public string UserId { get; set; }
    }
}