using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Models;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        DBEntities db = new DBEntities();
        // GET: Home
        public ActionResult Index()
        {
            var model = db.Departments;
            return View(model);
        }
        public ActionResult YoungestAndEldest()
        {
            var model = db.Departments;

            var youngestEmployee = db.EmployeeProfiles.OrderBy(e => e.Age).FirstOrDefault();

            var eldestEmployee = db.EmployeeProfiles.OrderByDescending(e => e.Age).FirstOrDefault();

            var youngestEmployeeDesignation = db.Departments
                .Where(d => d.EmpId == youngestEmployee.Id)
                .Select(d => d.Designation)
                .FirstOrDefault();

            var eldestEmployeeDesignation = db.Departments
                .Where(d => d.EmpId == eldestEmployee.Id)
                .Select(d => d.Designation)
                .FirstOrDefault();

            var viewModelYoungest = new EmployeeViewModel
            {
                Name = youngestEmployee.Name,
                Age = youngestEmployee.Age,
                Designation = youngestEmployeeDesignation
            };

            var viewModelEldest = new EmployeeViewModel
            {
                Name = eldestEmployee.Name,
                Age = eldestEmployee.Age,
                Designation = eldestEmployeeDesignation
            };

            var viewModelList = new List<EmployeeViewModel> { viewModelEldest , viewModelYoungest };

            return View(viewModelList);
        }

    }


}