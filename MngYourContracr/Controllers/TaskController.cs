﻿using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MngYourContracr.MngYourContractDatabase;
using MngYourContracr.Service;

namespace MngYourContracr.Controllers
{
    public class TaskController : Controller
    {
        private CompanyContext db = new CompanyContext();
        private TaskService taskService;

        public TaskController() {
            taskService = new TaskService(db);
        }
        // GET: /Task/
        public ActionResult Index()
        {
            return View(db.Tasks.ToList());
        }

        // GET: /Task/Details/5
        public ActionResult Details(int id)
        {

            Task task = taskService.GetByID(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: /Task/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Task/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TaskId,Name,Description,Deadline,StartDate,EndDate,EmployeeId")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(task);
        }

        // GET: /Task/Edit/5
        public ActionResult Edit(int id)
        {

            Task task = taskService.GetByID(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: /Task/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TaskId,Name,Description,Deadline,StartDate,EndDate,EmployeeId")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(task);
        }

        // GET: /Task/Delete/5
        public ActionResult Delete(int id)
        {

            Task task = taskService.GetByID(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: /Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = taskService.GetByID(id);
            db.Tasks.Remove(task);
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