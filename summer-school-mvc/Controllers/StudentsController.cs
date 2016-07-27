using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using summer_school_mvc.Models;

namespace summer_school_mvc.Controllers
{
    public class StudentsController : Controller
    {
        private SummerSchoolMVCEntities db = new SummerSchoolMVCEntities();

        // GET: Students
        public ActionResult Index()
        {
            ViewBag.Sum = db.Students.Sum(item => item.EnrollmentFee);
            int checkCount = db.Students.Count();
            ViewBag.closedEnrollment = "false";
            if (checkCount >= 15)
            {
                ViewBag.closedEnrollment = "true";
            }
            return View(db.Students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
            
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            int checkCount = db.Students.Count();
            ViewBag.closedEnrollment = "false";
            if (checkCount >= 15)
            {
                ViewBag.closedEnrollment = "true";
            }

            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        //public ActionResult Create([Bind(Include = "StudentID,FirstName,LastName,EnrollmentFee")] Student student)
        public ActionResult Create([Bind(Include = "StudentID,FirstName,LastName")] Student student)
        {
            student.EnrollmentFee = 200;
            int checkCount = db.Students.Count();

            if ((student.FirstName.ToLower()).First() == (student.LastName.ToLower()).First())
            {
                student.EnrollmentFee = Convert.ToInt32(student.EnrollmentFee * .9);
            }

            if ((student.LastName.ToLower()).Contains("potter"))
            {
                student.EnrollmentFee = Convert.ToInt32(student.EnrollmentFee * .5);
            }

            if ((student.LastName.ToLower()).Contains("malfoy"))
            {
                ViewBag.malfoy = "true";

                return View(student);

                //student.EnrollmentFee = Convert.ToInt32(student.EnrollmentFee * .5);
            }
            if ((student.LastName.ToLower()).Contains("longbottom"))
            {
                if (checkCount < 4)
                {
                    student.EnrollmentFee = 0;
                }
                else
                {
                    student.EnrollmentFee = Convert.ToInt32(student.EnrollmentFee);
                }
            }
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,FirstName,LastName,EnrollmentFee")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
