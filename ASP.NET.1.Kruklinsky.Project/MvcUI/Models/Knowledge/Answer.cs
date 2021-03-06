﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MvcUI.Models
{
    public class Answer
    {
        [Display(Name = "Answer id")]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Question id")]
        [HiddenInput(DisplayValue = false)]
        public int QuestionId { get; set; }

        [Required]
        [Display(Name = "Текст")]
        public string Text { get; set; }
    }
}
