﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FitnessTrackerV1.DataAccess;
using FitnessTrackerV1.Models;

namespace FitnessTrackerV1.Controllers
{
    public class UserPageController : Controller
    {
        [Authorize]
        public ActionResult Index(string emailAddress, string firstName)
        {
            ViewBag.Username = emailAddress;
            ViewBag.Firstname = firstName;
            return View();
        }

        [Authorize]
        public ActionResult ActiveRoutines(string emailAddress)
        {
            FitnessTrackerDa da = new FitnessTrackerDa();
            List<Routine> routines = new List<Routine>();
            routines = da.FindActiveRoutines(emailAddress);

            return View("_ActiveRoutines", routines);
        }
    }
}