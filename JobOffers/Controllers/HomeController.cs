using identity.Models;
using JobOffers.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

        [Authorize]
        public ActionResult GetJobsByUser()
        {
            var UserId = User.Identity.GetUserId();
            var jobs = db.ApplyForJobs.Where(a => a.UserId == UserId).ToList();
            return View(jobs);
        }

        [Authorize]
        public ActionResult ApplyDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplyForJob applyForJob)
        {
            if (ModelState.IsValid)
            {
                applyForJob.ApplyDate = DateTime.Now;
                db.Entry(applyForJob).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetJobsByUser");
            }
            return View(applyForJob);
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            db.ApplyForJobs.Remove(job);
            db.SaveChanges();
            return RedirectToAction("GetJobsByUser");
        }

        [Authorize]
        public ActionResult GetJobsByPublisher()
        {
            var userId = User.Identity.GetUserId();

            var jobs = from app in db.ApplyForJobs
                       join job in db.Jobs
                       on app.JobsId equals job.Id
                       where job.UserId == userId
                       select app;

            var grouped = from j in jobs
                          group j by j.Job.JobTitle
                          into gr
                          select new JobsByPublisher
                          {
                              JobTitle = gr.Key,
                              Requests = gr
                          };

            return View(grouped.ToList());
        }

        [HttpPost]
        public ActionResult Search(string search)
        {
            var result = db.Jobs.Where(a => a.JobTitle.Contains(search)
            || a.JobContent.Contains(search)
            || a.Categorie.CategoryName.Contains(search)
            || a.Categorie.CategoryDescription.Contains(search)).ToList();

            ViewBag.search = search;
            return View(result);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Y0our application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactModel contact)
        {
            var mail = new MailMessage();

            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("tarek.hajji.93@gmail.com"));
            mail.Subject = contact.Subject;
            mail.Body = contact.Message;

            var loginInfo = new NetworkCredential("tarek.hajji.93@gmail.com", "xxxxxx");

            var smtpclient = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = loginInfo
            };
            smtpclient.Send(mail);

            return RedirectToAction("index");
        }
    }
}