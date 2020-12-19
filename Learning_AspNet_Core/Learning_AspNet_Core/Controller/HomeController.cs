using Learning_AspNet_Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning_AspNet_Core
{
    //[Route("[controller]")]
    // [Route("[controller]/[action]")]
    //[Route("")]
    public class HomeController : Controller
    {
        private readonly IStudentRepository _repository = null;

        public HomeController(IStudentRepository repository)
        {
            _repository = repository;
        }

        // [Route("/Index")]
        //[Route("")]
        //[Route("[controller]")]
        //[Route("[controller]/[action]")]
        //[Route("[controller]/[action]/{id?}")]
        //[Route("")]
        //[Route("Home")]
        //[Route("Home/Index")]
        //[Route("kirtesh")]
        //[Route("Home")]
        //[Route("Home/Index")]
       // [Route("Home/Index/{id:int}")]
        public ViewResult Index()
        {
            return View();
        }

        public string PostsByID(int id)
        {
            if (id != 0)
            {
                return "[PostsByID] Received " + id.ToString();
            }
            else
            {
                return "[PostsByID] Received nothing";
            }
        }

        public string PostsByPostName(string id)
        {
            if (id != null)
            {
                return "[PostsByPostName] Received " + id.ToString();
            }
            else
            {
                return "[PostsByPostName] Received nothing";
            }
        }

        //[Route("")]        
        public ViewResult Details()
        {
            Student studentDetails = _repository.GetStudentById(101);

            ViewData["Student"] = studentDetails;
            ViewData["Title"] = "Students";

            //return View();
            return View("Details");
            // return View("/Views/Home/Details.cshtml");
            //return View("../Home/Details");
        }

        public ViewResult Details1()
        {
            Student studentDetails = _repository.GetStudentById(101);

            // To store the page title and empoyee model object in the 
            // ViewBag we are using dynamic properties PageTitle and Employee
            ViewBag.PageTitle = "Employee Details";
            ViewBag.Employee = studentDetails;

            return View();
        }

        public JsonResult GetStudentDetails(int Id)
        {
            Student studentDetails = _repository.GetStudentById(Id);
            return Json(studentDetails);
        }

        public JsonResult GetStudentDetails1(int Id, [FromServices] IStudentRepository repository)
        {
            Student studentDetails = repository.GetStudentById(Id);
            return Json(studentDetails);
        }

        public JsonResult GetStudentDetails2(int Id)
        {
            var services = this.HttpContext.RequestServices;
            var _repository = (IStudentRepository)services.GetService(typeof(IStudentRepository));
            Student studentDetails = _repository.GetStudentById(Id);
            return Json(studentDetails);
        }
    }
}
