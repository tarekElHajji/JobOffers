using identity.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace JobOffers.Controllers
{
    public class RoleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Role
        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }

        // GET: Role/Details/5
        public ActionResult Details(string id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = db.Roles.Find(id);
            if(role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: Role/Edit/5
        public ActionResult Edit(string id)
        {
            var role = db.Roles.Find(id);
            if(role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Role/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include ="Id, Name")]IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: Role/Delete/5
        public ActionResult Delete(string id)
        {
            var role = db.Roles.Find(id);
            if(role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string Id)
        {
            var role = db.Roles.Find(Id);
            db.Roles.Remove(role);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}