using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;
using StudentManagement.Models.Repositories;
using StudentManagement.Models.Repositories.services;
using System.Runtime.CompilerServices;

namespace StudentManagement.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;   
        private readonly ISchoolRepository _schoolRepository;
        public StudentController(IStudentRepository studentRepository, ISchoolRepository schoolRepository)
        {
            _studentRepository = studentRepository;
            _schoolRepository = schoolRepository;
        }
        // GET: StudentController
        public ActionResult Index()
        {
            IList<Student> student = _studentRepository.GetAll();
            return View(student);
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            Student student = _studentRepository.GetById(id);
            ViewBag.Schools = new List<School> { _schoolRepository.GetById(student.SchoolID) };
            return View(student);
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            ViewBag.Schools = _schoolRepository.GetAll();

            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            try
            {
                _studentRepository.Add(student);
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
            Student student = _studentRepository.GetById(id);
            ViewBag.Schools = _schoolRepository.GetAll();
            return View(student);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Student student)
        {
            try
            {
                _studentRepository.Edit(id,student);
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
            Student student= _studentRepository.GetById(id);
            return View(student);
        }
        
        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Student student = _studentRepository.GetById(id);
                _studentRepository.Delete(student);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
