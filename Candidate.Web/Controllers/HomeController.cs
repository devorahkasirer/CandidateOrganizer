using Candidate.Data;
using Candidate.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Candidate.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Candidate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Candidate(Applicant applicant)
        {
            var repo = new CandidateRepository(Properties.Settings.Default.ConStr);
            repo.AddCandidate(applicant);
            return Redirect("/home/index");
        }
        public ActionResult Pending()
        {
            return View();
        }
        public ActionResult Confirmed()
        {
            var repo = new CandidateRepository(Properties.Settings.Default.ConStr);
            var vm = new CandidatesViewModel();
            vm.Candidates = repo.GetConfirmed();
            vm.Decision = Status.Confirmed;
            return View("Decided", vm);
        }
        public ActionResult Rejected()
        {
            var repo = new CandidateRepository(Properties.Settings.Default.ConStr);
            var vm = new CandidatesViewModel();
            vm.Candidates = repo.GetRejected();
            vm.Decision = Status.Rejected;
            return View("Decided", vm);
        }
        public ActionResult GetPending()
        {
            var repo = new CandidateRepository(Properties.Settings.Default.ConStr);
            var pending = repo.GetPending();
            var result = pending.Select(a => new
            {
                id = a.Id,
                firstName = a.FirstName,
                lastName = a.LastName,
                email = a.Email,
                number = a.Number,
                note = a.Note ?? ""
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult addConfirm(int id)
        {
            var repo = new CandidateRepository(Properties.Settings.Default.ConStr);
            repo.Update(Status.Confirmed, id);
            return Json(new
            {
                pendingCount = repo.GetPending().Count(),
                confirmedCount = repo.GetConfirmed().Count()
            });
        }
        [HttpPost]
        public ActionResult addReject(int id)
        {
            var repo = new CandidateRepository(Properties.Settings.Default.ConStr);
            repo.Update(Status.Rejected, id);
            return Json(new
            {
                pendingCount = repo.GetPending().Count(),
                rejectedCount = repo.GetRejected().Count()
            });
        }
    }
}