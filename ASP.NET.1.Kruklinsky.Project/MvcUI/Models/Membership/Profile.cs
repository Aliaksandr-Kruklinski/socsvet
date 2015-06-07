using MvcUI.Providers.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcUI.Models.Membership
{
    public class Profile
    {
        [DataType(DataType.Text)]
        [Display(Name = "First name: ")]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Second name: ")]
        public string SecondName { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Birthday { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int AvatarUrl { get; set; }
    }
}