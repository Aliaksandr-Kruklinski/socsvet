using BLL.Interface.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcUI.Models;
using System.Web.Security;
using MvcUI.Binders;
using MvcUI.Infrastructure.Abstract;

namespace MvcUI.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        private IMessageService messageService;
        private ITestQueryService testQueryService;
        private ISubjectQueryService subjectQueryService;
        private ITestingService testingService;
        private ITestSessionFactory testSessionFactory;
        private IUserQueryService userQueryService;


        //public TestController(ISubjectQueryService subjectQueryService, ITestingService testingService, ITestQueryService testQueryService, ITestSessionFactory testSessionFactory)
        //{
        //    if (subjectQueryService == null)
        //    {
        //        throw new System.ArgumentNullException("subjectQueryService", "Subject auery service is null.");
        //    }
        //    if (testingService == null)
        //    {
        //        throw new System.ArgumentNullException("testService", "Test service is null.");
        //    }
        //    if (testQueryService == null)
        //    {
        //        throw new System.ArgumentNullException("testQueryService", "Test query service is null.");
        //    }
        //    if (testSessionFactory == null)
        //    {
        //        throw new System.ArgumentNullException("testSessionFactory", "Test session factory is null.");
        //    }
        //    this.testQueryService = testQueryService;
        //    this.subjectQueryService = subjectQueryService;
        //    this.testingService = testingService;
        //    this.testSessionFactory = testSessionFactory;
        //}

        public TestController(ISubjectQueryService subjectQueryService, ITestingService testingService, ITestQueryService testQueryService, ITestSessionFactory testSessionFactory, IMessageService messageService, IUserQueryService userQueryService)
        {
            if (subjectQueryService == null)
            {
                throw new System.ArgumentNullException("subjectQueryService", "Subject auery service is null.");
            }
            if (testingService == null)
            {
                throw new System.ArgumentNullException("testService", "Test service is null.");
            }
            if (testQueryService == null)
            {
                throw new System.ArgumentNullException("testQueryService", "Test query service is null.");
            }
            if(testSessionFactory == null)
            {
                throw new System.ArgumentNullException("testSessionFactory", "Test session factory is null.");
            }
            this.testQueryService = testQueryService;
            this.subjectQueryService = subjectQueryService;
            this.testingService = testingService;
            this.testSessionFactory = testSessionFactory;
            this.messageService = messageService;
            this.userQueryService = userQueryService;
        }

        public ActionResult Index()
        {
            var subjects = this.subjectQueryService.GetAllSubjects();
            var model = new List<SubjectEditor>(
                subjects.Select(s =>
                    new SubjectEditor
                    {
                        Subject = s.ToWeb(),
                        Tests = s.Tests.Select(t => t.ToWeb())
                    }));
            return View(model);
        }

        public ActionResult Test(int testId)
        {
            var test = this.testQueryService.GetTest(testId);
            if (test != null)
            {
                return View(test.ToWeb());
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult StartTest(int testId)
        {
            if (!this.testSessionFactory.GetTestSession().IsStarted)
            {
                var test = this.testQueryService.GetTest(testId);
                if (test != null)
                {
                    Testing onTest = new Testing
                    {
                        Test = test.ToWeb(),
                        Questions = new List<QuestionEditor>(test.Questions.Select(q =>
                            new QuestionEditor
                            {
                                Question = q.ToWeb(),
                                Answers = q.Answers.Select(a => a.ToWeb()).ToList(),
                                Fakes = q.Fakes.Select(f => f.ToWeb()).ToList()
                            }))
                    };
                    string userId = Membership.GetUser(this.User.Identity.Name).ProviderUserKey.ToString();
                    int resultId = this.testingService.StartTest(userId, testId, 1200);
                    this.testSessionFactory.GetTestSession().Start(onTest, resultId);
                }
            }
            return RedirectToAction("Testing", "Test");
        }

        public ActionResult Testing()
        {
            if (this.testSessionFactory.GetTestSession().IsStarted)
            {
                return View(this.testSessionFactory.GetTestSession().Test);
            }
            return RedirectToAction("Index", "Test");
        }

        [HttpPost]
        public ActionResult Testing([ModelBinder(typeof(TestingModelBinder))] IEnumerable<Answers> answers)
        {
            if (answers == null)
            {
                throw new System.ArgumentNullException("answers", "Answers is null.");
            }
            var testSession = this.testSessionFactory.GetTestSession();
            if (testSession.IsStarted)
            {
                #region Authom
                var users = userQueryService.GetAllUsers();
                var validUsers = users.Select(u => this.userQueryService.GetUser(u.Id)).ToList()
                    .Where(u => u.IsApproved)
                    .Where(u => u.Roles != null && u.Roles.Count() != 0);

                List<string> dUsers = new List<string>();
                var userId = Membership.GetUser(this.User.Identity.Name).ProviderUserKey.ToString();
                dUsers.Add(userId);
                dUsers.Add(users.First().Id);
                var dialogId = this.messageService.AddDialog(dUsers);

                var result = new BLL.Interface.Entities.Message
                {
                    Text = "Panda Quest: " + testSession.ResultId.ToString(),
                    Time = DateTime.Now,
                    User = new BLL.Interface.Entities.User { Id = userId },
                    Dialog = new BLL.Interface.Entities.Dialog { Id = dialogId }
                };
                this.messageService.AddMessage(result);
                #endregion
                this.testingService.FinishTest(testSession.ResultId, testSession.Finish(answers.ToList()));
                return RedirectToAction("Index", "Result");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
