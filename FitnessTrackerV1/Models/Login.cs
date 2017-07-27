using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FitnessTrackerV1.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Email is required.")]
        [DisplayName("Email Address")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$",
                                    ErrorMessage = "Email Format is wrong")]
        [StringLength(50, ErrorMessage = "Must be less than 50 characters.")]
        public string EmailAddress { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password Required")]
        [DisplayName("Password")]
        [StringLength(30, ErrorMessage = "Must be less than 30 characters.")]
        public string Password { get; set; }

        public bool DoesUserExist(string emailAddress, string password)
        {
            bool flag = false;
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["FitnessTracker"].ConnectionString);
            con.Open();
            SqlCommand command = new SqlCommand("SELECT count(*) FROM SystemUser WHERE UserID= '" + emailAddress + "' AND PasswordConfirm='" + password + "'", con);
            flag = Convert.ToBoolean(command.ExecuteScalar());
            con.Close();
            return flag;
        }

    }

    public class User
    {
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public void SetNames()
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["FitnessTracker"].ConnectionString);
            using (con)
            {
                con.Open();
                SqlCommand command = new SqlCommand("SELECT FirstName, LastName FROM SystemUser WHERE UserID= '" + EmailAddress + "'", con);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FirstName = (string)reader.GetValue(reader.GetOrdinal("FirstName"));
                        LastName = (string)reader.GetValue(reader.GetOrdinal("LastName"));
                    }
                }
            }
            

        }
    }

    public class Register
    {
        [Required(ErrorMessage = "FirstName Required.")]
        [DisplayName("First Name")]
        [RegularExpression(@"^[a-zA-Z'.\s]{1,40}$", ErrorMessage = "Special Characters not allowed")]
        [StringLength(50, ErrorMessage = "Name must be less than 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName Required.")]
        [RegularExpression(@"^[a-zA-Z'.\s]{1,40}$", ErrorMessage = "Special Characters not allowed")]
        [DisplayName("Last Name")]
        [StringLength(50, ErrorMessage = "Name must be less than 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email Address Required.")]
        [DisplayName("Email Address")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email Format is wrong")]
        [StringLength(50, ErrorMessage = "Email Address must be less than 50 characters.")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password Required.")]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        [StringLength(30, ErrorMessage = "Password must be less than 30 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password Required")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Passwords don't match.")]
        [DisplayName("Confirm Password")]
        [StringLength(30, ErrorMessage = "Password must be less than 30 characters.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Street Address Required.")]
        [DisplayName("Street Address")]
        [StringLength(100, ErrorMessage = "Street Address must be less than 100 characters.")]
        public string StreetAddress { get; set; }

        [Required(ErrorMessage = "City Required.")]
        [DisplayName("City")]
        [RegularExpression(@"^[a-zA-Z'.\s]{1,40}$", ErrorMessage = "Special Characters not allowed")]
        [StringLength(50, ErrorMessage = "City must be less than 50 characters.")]
        public string City { get; set; }

        [Required(ErrorMessage = "State Required")]
        [DisplayName("State")]
        [RegularExpression(@"^[a-zA-Z'.\s]{1,40}$", ErrorMessage = "Special Characters not allowed")]
        [StringLength(50, ErrorMessage = "State must be less than 50 characters.")]
        public string State { get; set; }

        [Required(ErrorMessage = "ZipCode Required")]
        [DisplayName("Zip Code")]
        [StringLength(20, ErrorMessage = "Zip Code must be less than 20 characters.")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Enter Verification Code")]
        [DisplayName("Verification Code")]
        public string Captcha { get; set; }

        public bool DoesUserExist(string emailAddress)
        {
            bool flag = false;
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["FitnessTracker"].ConnectionString);
            con.Open();
            SqlCommand command = new SqlCommand("SELECT count(*) FROM SystemUser WHERE UserID= '" + emailAddress + "'", con);
            flag = Convert.ToBoolean(command.ExecuteScalar());
            con.Close();
            return flag;
        }

        public bool Insert()
        {
            bool flag = false;
            if (!DoesUserExist(EmailAddress))
            {
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["FitnessTracker"].ConnectionString);
                con.Open();
                SqlCommand command = new SqlCommand("INSERT INTO SystemUser values('" + EmailAddress + "','" + FirstName + "','"
                    + LastName + "','" + Password + "','" + StreetAddress + "','" + City + "','" + State + "','" + ZipCode + "')", con);
                flag = Convert.ToBoolean(command.ExecuteNonQuery());
                con.Close();
                return flag;
            }
            return flag;
        }

    }

    public class CaptchaImageAction : ActionResult
    {
        public Color BackgroundColor { get; set; }
        public Color RandomTextColor { get; set; }
        public string RandomText { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            RenderCaptchaImage(context);
        }

        private void RenderCaptchaImage(ControllerContext context)
        {
            Bitmap objBmp = new Bitmap(150, 60);
            Graphics objGraphic = Graphics.FromImage(objBmp);
            objGraphic.Clear(BackgroundColor);
            SolidBrush objBrush = new SolidBrush(RandomTextColor);
            Font objFont = null;
            int a;
            string myFont, str;

            string[] crypticsFont = new string[11];
            crypticsFont[0] = "Times New roman";
            crypticsFont[1] = "Verdana";
            crypticsFont[2] = "Sylfaen";
            crypticsFont[3] = "Microsoft Sans Serif";
            crypticsFont[4] = "Algerian";
            crypticsFont[5] = "Agency FB";
            crypticsFont[6] = "Andalus";
            crypticsFont[7] = "Cambria";
            crypticsFont[8] = "Calibri";
            crypticsFont[9] = "Courier";
            crypticsFont[10] = "Tahoma";

            for (a = 0; a < RandomText.Length; a++)
            {
                myFont = crypticsFont[a];
                objFont = new Font(myFont, 18, FontStyle.Bold | FontStyle.Italic | FontStyle.Strikeout);
                str = RandomText.Substring(a, 1);
                objGraphic.DrawString(str, objFont, objBrush, a*20, 20);
                objGraphic.Flush();
            }

            context.HttpContext.Response.ContentType = "image/GF";
            objBmp.Save(context.HttpContext.Response.OutputStream, ImageFormat.Gif);
            objFont.Dispose();
            objGraphic.Dispose();
            objBmp.Dispose();
        }

    }

}