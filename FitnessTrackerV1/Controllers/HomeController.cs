using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FitnessTrackerV1.Models;

namespace FitnessTrackerV1.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult LogOn()
        {
            return View();
        }

        public ViewResult Register()
        {
            return View();
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult LogOn(Login model)
        {
            if (ModelState.IsValid)
            {
                if (model.DoesUserExist(model.EmailAddress, model.Password))
                {
                    ViewBag.UserName = model.EmailAddress;
                    FormsAuthentication.RedirectFromLoginPage(model.EmailAddress, false);
                }
                else
                {
                    ModelState.AddModelError("", "Email Address or Password incorrect.");
                }
            }

            User userModel = new User();
            userModel.EmailAddress = model.EmailAddress;
            userModel.SetNames();

            return RedirectToAction("Index", "UserPage", userModel);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                string realCaptcha = Session["captcha"].ToString();
                if (model.Captcha == realCaptcha)
                {
                    if (model.Insert())
                    {
                        return RedirectToAction("LogOn", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "User already exists.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Verification code incorrect.");
                }
            }
            return View(model);
        }

        public CaptchaImageAction Image()
        {
            string randomText = SelectRandomWord(6);
            Session["captcha"] = randomText;
            HttpContext.Session["RandomText"] = randomText;
            return new CaptchaImageAction() {BackgroundColor = Color.LightGray, RandomTextColor = Color.Black, RandomText = randomText};
        }

        private string SelectRandomWord(int numberOfChars)
        {
            if (numberOfChars > 36)
            {
                throw new InvalidOperationException("Random Word Characters cannot be greater than 36");
            }

            char[] columns = new char[36];

            for (int charPos = 65; charPos < 65 + 26; charPos++)
            {
                columns[charPos - 65] = (char) charPos;
            }

            for (int intPos = 48; intPos <= 57; intPos++)
            {
                columns[26 + (intPos - 48)] = (char) intPos;
            }

            StringBuilder builder = new StringBuilder();
            Random randomSeed = new Random();

            for (int incr = 0; incr < numberOfChars; incr++)
            {
                builder.Append(columns[randomSeed.Next(36)].ToString());
            }
            return builder.ToString();
        }

    }
}