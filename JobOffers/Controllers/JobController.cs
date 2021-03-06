﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobOffers.Models;
using identity.Models;
using System.IO;
using Microsoft.AspNet.Identity;

namespace JobOffers.Controllers
{
    [Authorize(Roles= "Admin,publishers")]
    public class JobController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Job
        public ActionResult Index()
        {
            var UserId = User.Identity.GetUserId();
            var jobs = db.Jobs.Where(j => j.UserId == UserId).Include(j => j.Categorie);
            return View(jobs.ToList());
        }

        [AllowAnonymous]
        public ActionResult JobsByCategory(string category)
        {
            ViewBag.CategoryName = category; 
            var jobs = db.Jobs.Where(j => j.Categorie.CategoryName == category).Include(j => j.Categorie);
            return View("JobsByCategory", jobs.ToList());
        }
        
        // GET: Job/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jobs jobs = db.Jobs.Find(id);
            if (jobs == null)
            {
                return HttpNotFound();
            }
            return View(jobs);
        }

        // GET: Job/Create
        public ActionResult Create()
        {
            ViewBag.CategoriesId = new SelectList(db.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: Job/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Jobs jobs, HttpPostedFileBase JobImage)
        {           
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads"), JobImage.FileName);
                JobImage.SaveAs(path);

                jobs.JobImage = JobImage.FileName;
                jobs.UserId = User.Identity.GetUserId();
                db.Jobs.Add(jobs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriesId = new SelectList(db.Categories, "Id", "CategoryName", jobs.CategoriesId);
            return View(jobs);
        }

        // GET: Job/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jobs jobs = db.Jobs.Find(id);
            if (jobs == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriesId = new SelectList(db.Categories, "Id", "CategoryName", jobs.CategoriesId);
            return View(jobs);
        }

        // POST: Job/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,JobTitle,JobContent,JobImage,CategoriesId, UserId")] Jobs jobs, HttpPostedFileBase JImage)
        {
            if (ModelState.IsValid)
            {              
                if(JImage != null)
                {
                    //delete old path
                    string oldPath = Path.Combine(Server.MapPath("~/Uploads"), jobs.JobImage);
                    System.IO.File.Delete(oldPath);

                    string path = Path.Combine(Server.MapPath("~/Uploads"), JImage.FileName);
                    JImage.SaveAs(path);

                    jobs.JobImage = JImage.FileName;
                }
                

                db.Entry(jobs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriesId = new SelectList(db.Categories, "Id", "CategoryName", jobs.CategoriesId);
            return View(jobs);
        }

        // GET: Job/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jobs jobs = db.Jobs.Find(id);
            if (jobs == null)
            {
                return HttpNotFound();
            }
            return View(jobs);
        }

        // POST: Job/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jobs jobs = db.Jobs.Find(id);

            string oldPath = Path.Combine(Server.MapPath("~/Uploads"), jobs.JobImage);
            System.IO.File.Delete(oldPath);

            db.Jobs.Remove(jobs);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
