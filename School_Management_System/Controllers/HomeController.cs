using School_Management_System.Models;
using School_Management_System.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace School_Management_System.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetStudents()
        {
            var db = new SchoolDBEntities2();
            var listStudent = db.Students.ToList();
            return Json(listStudent, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetStudentById(int id)
        {
            var db = new SchoolDBEntities2();
            var oStudent = db.Students.Where(w => w.StudentID == id).FirstOrDefault();
            return Json(oStudent, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertStudent(VmStudent model)//HttpPostedFileBase file)
        {
            object resdata = null;
            //HttpPostedFileBase file = Request.Files[0];
            string picture = "";
            if (model.file != null && model.file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(model.file.FileName);
                string fileLocation = Path.Combine(
                    Server.MapPath("~/uploads"), fileName);
                model.file.SaveAs(fileLocation);

                picture = "/uploads/" + model.file.FileName;

                resdata = new { message = "image inserted successfully." };
            }
            var db = new SchoolDBEntities2();
            var oStudent = new Student();
            oStudent.Section = model.Section;
            oStudent.DoB = model.DoB;
            oStudent.Picture = picture;
            oStudent.StudentName = model.StudentName;
            db.Students.Add(oStudent);
            db.SaveChanges();
            return Json(resdata);
        }

        [HttpPost]
        public ActionResult UpdateStudent(VmStudent model)//HttpPostedFileBase file)
        {
            object resdata = null;
            //HttpPostedFileBase file = Request.Files[0];
            string picture = "";
            if (model.file != null && model.file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(model.file.FileName);
                string fileLocation = Path.Combine(
                    Server.MapPath("~/uploads"), fileName);
                model.file.SaveAs(fileLocation);

                picture = "/uploads/" + model.file.FileName;

                resdata = new { message = "image updated successfully." };
            }
            var db = new SchoolDBEntities2();
            var oStudent = db.Students.Where(w => w.StudentID == model.StudentID).FirstOrDefault();
            if (oStudent != null)
            {
                oStudent.Section = model.Section;
                oStudent.DoB = model.DoB;
                oStudent.Picture = picture;
                oStudent.StudentName = model.StudentName;
                db.SaveChanges();
            }

            return Json(resdata);
        }

        [HttpPost]
        public ActionResult DeleteStudent(int id)
        {
            object resdata = null;
            var db = new SchoolDBEntities2();
            var oStudent = db.Students.Where(w => w.StudentID == id).FirstOrDefault();
            if (oStudent != null)
            {
                db.Students.Remove(oStudent);
                db.SaveChanges();

                var fileName = Path.GetFileName(oStudent.Picture);
                string fileLocation = Path.Combine(
                    Server.MapPath("~/uploads"), fileName);

                // Check if file exists with its full path    
                if (System.IO.File.Exists(fileLocation))
                {
                    // If file found, delete it    
                    System.IO.File.Delete(fileLocation);
                }

                resdata = new { message = "image deleted successfully." };
            }

            return Json(resdata);
        }

    }
}