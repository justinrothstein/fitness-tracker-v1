using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FitnessTrackerV1.Models;

namespace FitnessTrackerV1.Controllers
{
    public class FitnessController : Controller
    {
        // GET: Fitness
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateRoutine(string emailAddress, string firstName)
        {
            ViewBag.Username = emailAddress;
            ViewBag.Firstname = firstName;
            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CreateRoutine(Routine model)
        {
            return RedirectToAction("Index", "UserPage", new
            {
                emailAddress = ViewBag.Username,
                firstName = ViewBag.Firstname
            });
        }
    }
}