using Candidate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Candidate.Web
{
    public class LayoutDataAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var repo = new CandidateRepository(Properties.Settings.Default.ConStr);
            filterContext.Controller.ViewBag.Pending = repo.GetPending().Count();
            filterContext.Controller.ViewBag.Confirmed = repo.GetConfirmed().Count();
            filterContext.Controller.ViewBag.Rejected = repo.GetRejected().Count();
            base.OnActionExecuting(filterContext);
        }
    }
}