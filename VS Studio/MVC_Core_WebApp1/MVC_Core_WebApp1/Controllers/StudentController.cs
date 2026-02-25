using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using MVC_Core_WebApp1.Models;
namespace MVC_Core_WebApp1.Controllers
{
    public class StudentController : Controller
    {
        StudentRepo sRepo = null;
        public StudentController()
        {
            sRepo = new StudentRepo();
        }
        //Http get sttribute is used when we are creating a custom method t=and to show that httpget used when get the thing 
        [HttpGet]
        public string[] GetAllCities()
        {
            return new string[] { "Pune", "Mumbai", "Chennai", "Bengaluru", "Hyderabad" };
        }
        // GET: StudentController
        public ActionResult Index()
        {
            List<Student> sList = sRepo.ShowAllData();
            return View(sList);
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            Student s = sRepo.ShowDetailsByID(id);
            return View(s);
        }

        // GET: StudentController/Details/5
        public ActionResult Details1(int rollNo)
        {
            Student s = sRepo.ShowDetailsByID(rollNo);
            return View(s);
        }


        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student s1)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    sRepo.AddData(s1);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
