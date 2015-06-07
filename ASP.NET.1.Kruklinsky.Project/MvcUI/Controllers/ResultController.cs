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
    public class ResultController : Controller
    {
        private ITestQueryService testQueryService;
        private IResultQueryService resultQueryService;
        private ITestingService testingService;
        private ISubjectQueryService subjectQueryService;

        public ResultController(ITestingService testingService, IResultQueryService resultQueryService, ITestQueryService testQueryService,ISubjectQueryService subjectQueryService)
        {
            if(resultQueryService == null)
            {
                throw new System.ArgumentNullException("resultQueryService", "Result query service service is null.");
            }
            if (testingService == null)
            {
                throw new System.ArgumentNullException("testService", "Test service service is null.");
            }
            this.testQueryService = testQueryService;
            this.resultQueryService = resultQueryService;
            this.testingService = testingService;
            this.subjectQueryService = subjectQueryService;
        }

        public ActionResult Index()
        {
            string userId = Membership.GetUser(this.User.Identity.Name).ProviderUserKey.ToString();
            var results = this.resultQueryService.GetUserResults(userId);
            var tests = new List<BLL.Interface.Entities.Test>();
            foreach(var result in results)
            {
                tests.Add(this.testQueryService.GetTest(result.TestId));
            }
            Results model = new Results { Result = results.Select(r => r.ToWeb()).ToList(), Tests = tests.Select(t => t.ToWeb()).ToList() };
            return View(model);
        }

        public ActionResult IndexAdmin(int id)
        {
            string userId = Membership.GetUser(this.User.Identity.Name).ProviderUserKey.ToString();
            var results = this.resultQueryService.GetResult(id);
            var tests = this.testQueryService.GetTest(results.TestId);
            var result = new UserResults
            {
                Result = results,
                Test = tests,
                Questions = tests.Questions.Select(q => new QuestionEditor
                {
                    Question = q.ToWeb()
                }
                ).ToList()
            };
            #region a
            var answers = new List<Answers>();
            foreach(var question in tests.Questions)
            {
                var newAnswers = new Answers();
                newAnswers.UserAnswers = new List<AnswerPair>();
                foreach(var answer in question.Answers)
                {
                    var newAnswe = new AnswerPair
                    {
                        IsRight = true,
                        Text = answer.Text,
                        UserAnswer = results.Answers.Where(a => a.QuestionId == question.Id).First().IsRight
                    };
                    newAnswers.UserAnswers.Add(newAnswe);
                }
                foreach(var fake in question.Fakes)
                {
                    var newAnswe = new AnswerPair
                    {
                        IsRight = true,
                        Text = fake.Text,
                        UserAnswer = !results.Answers.Where(a => a.QuestionId == question.Id).First().IsRight
                    };
                    newAnswers.UserAnswers.Add(newAnswe);
                }
                answers.Add(newAnswers);

            }
            #endregion
            result.Answers = answers;
            return View(result);
        }

        [HttpPost]
        public ActionResult SetResultDescription (int id,string description)
        {
            this.testingService.SetResultDescription(id,description);
            return RedirectToAction("Index", "Admin", null);
        }


        public PartialViewResult List(string userId)
        {
            var results = this.resultQueryService.GetUserResults(userId).OrderByDescending(r => r.Start).ToList().DistinctBy(r => r.TestId);

            var tests = new List<BLL.Interface.Entities.Test>();

            var subjects = new List<BLL.Interface.Entities.Subject>();
            foreach(var result in results)
            {
                tests.Add(this.testQueryService.GetTest(result.TestId));
            }

            foreach(var test in tests)
            {
                subjects.Add(this.subjectQueryService.GetSubject(test.SubjectId));
            }
            subjects = subjects.DistinctBy(s => s.Id).ToList();
            Results model = new Results { 
                Result = results.Select(r => r.ToWeb()).ToList(), 
                Tests = tests.Select(t => t.ToWeb()).ToList(),
                Subjects = subjects.Select(s => s.ToWeb()).ToList()
            };
            return PartialView(model);
        }
    }
}
