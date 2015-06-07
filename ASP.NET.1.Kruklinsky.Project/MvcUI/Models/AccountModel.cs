using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcUI.Models
{
    public class LogInModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email адрес")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Пожалуйста введите полный адрес.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }

    public class SignUpModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Адрес")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Пожалуйста введите полный адрес.")]
        [Remote("IsFreeEmail", "Account", HttpMethod = "POST", ErrorMessage = "Адрес уже зарегестрирован. Пожалуйста введите новый адрес.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [StringLength(100, ErrorMessage = "{0} должен быть минимум {2} символов.", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }
    }

    public class ActivateModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Display(Name = "Very secret code")]
        [HiddenInput(DisplayValue = false)]
        public string SecretCode { get; set; }
    }
}