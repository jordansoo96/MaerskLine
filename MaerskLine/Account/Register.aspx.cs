using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using MaerskLine.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.Configuration;

namespace MaerskLine.Account
{
    public partial class Register : Page
    {
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            //var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            //var user = new ApplicationUser() { UserName = Email.Text, Email = Email.Text };
            //IdentityResult result = manager.Create(user, Password.Text);
            //if (result.Succeeded)
            //{
            //    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
            //    //string code = manager.GenerateEmailConfirmationToken(user.Id);
            //    //string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
            //    //manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");

            //    signInManager.SignIn( user, isPersistent: false, rememberBrowser: false);
            //    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            //}
            //else 
            //{
            //    ErrorMessage.Text = result.Errors.FirstOrDefault();
            //}

            SqlConnection dbConnect = new SqlConnection();
            dbConnect.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string registerUsers = "INSERT INTO Users (first_name,last_name,email_address,password,credentials) VALUES (@fname,@lname,@email,@password,@credentials);";
            SqlCommand cmdRegister = new SqlCommand(registerUsers, dbConnect);
            cmdRegister.Parameters.Add("@fname", SqlDbType.NVarChar);
            cmdRegister.Parameters["@fname"].Value = FirstName.Text;
            cmdRegister.Parameters.Add("@lname", SqlDbType.NVarChar);
            cmdRegister.Parameters["@lname"].Value = LastName.Text;
            cmdRegister.Parameters.Add("@email", SqlDbType.NVarChar);
            cmdRegister.Parameters["@email"].Value = Email.Text;
            cmdRegister.Parameters.Add("@password", SqlDbType.NVarChar);
            cmdRegister.Parameters["@password"].Value = Password.Text;
            cmdRegister.Parameters.Add("@credentials", SqlDbType.NVarChar);
            cmdRegister.Parameters["@credentials"].Value = WebConfigurationManager.AppSettings["Customer"];

            dbConnect.Open();
            int pass = cmdRegister.ExecuteNonQuery();
            dbConnect.Close();

            if (pass == 0)
            {

            }
            else
            {
                Response.Redirect("/Account/Login", false);
            }
        }
    }
}