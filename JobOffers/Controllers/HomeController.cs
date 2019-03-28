using identity.Models;
using JobOffers.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace identity.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            Session["JobId"] = job.Id;
            return View(job);

        }

        [Authorize]
        public ActionResult Apply()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Apply(string Message)
        {
            var userId = User.Identity.GetUserId();
            var jobId = (int)Session["JobId"];
            var check = db.ApplyForJobs.Where(a => a.JobsId == jobId && a.UserId == userId).ToList();
            if (check.Count() < 1)
            {

                ApplyForJob applyForJob = new ApplyForJob
                {
                    Message = Message,
                    ApplyDate = DateTime.Now,
                    UserId = userId,
                    JobsId = jobId
                };

                db.ApplyForJobs.Add(applyForJob);
                db.SaveChanges();
                return RedirectToAction("index"); 
            }
            else
            {
                ViewBag.Result = "You have already applied for this job !";
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}