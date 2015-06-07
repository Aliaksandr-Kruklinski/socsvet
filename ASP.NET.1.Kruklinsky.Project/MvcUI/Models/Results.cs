using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcUI.Models
{
    public class Results
    {
        public List<Result> Result { get; set; }
        public List<Test> Tests { get; set; }
        public List<Subject> Subjects { get; set; }
    }

    public class UserResults
    {
        public BLL.Interface.Entities.Result Result { get; set; }
        public BLL.Interface.Entities.Test Test { get; set; }

        public List<QuestionEditor> Questions { get; set; }

        public List<Answers> Answers { get; set; }
    }
}