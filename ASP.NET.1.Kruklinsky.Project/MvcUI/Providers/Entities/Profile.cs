using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcUI.Providers.Entities
{
    public class Profile
    {
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public DateTime? Birthday { get; set; }

        public int Avatar { get; set; }
    }
}