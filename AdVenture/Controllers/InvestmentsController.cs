﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdVenture.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace AdVenture.Controllers
{
    public class InvestmentsController : Controller
    {
        private VentureCapitalDbContext db = new VentureCapitalDbContext();
        
        // GET: Investments
        public ActionResult Index()
        {
            ApplicationUser currentUser = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(User.Identity.GetUserId());
            var investments = from b in db.Bids where b.status == "complete" && b.investorID == currentUser.Id select b;
            return View(investments.ToList());
        }

        // GET: Investments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bids bids = db.Bids.Find(id);
            if (bids == null)
            {
                return HttpNotFound();
            }
            return View(bids);
        }

        // GET: Investments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Investments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,investorID,ventureID,bid,bidStake,createdOn,status")] Bids bids)
        {
            if (ModelState.IsValid)
            {
                db.Bids.Add(bids);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bids);
        }

        // GET: Investments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bids bids = db.Bids.Find(id);
            if (bids == null)
            {
                return HttpNotFound();
            }
            return View(bids);
        }

        // POST: Investments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,investorID,ventureID,bid,bidStake,createdOn,status")] Bids bids)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bids).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bids);
        }

        // GET: Investments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bids bids = db.Bids.Find(id);
            if (bids == null)
            {
                return HttpNotFound();
            }
            return View(bids);
        }

        // POST: Investments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bids bids = db.Bids.Find(id);
            db.Bids.Remove(bids);
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
