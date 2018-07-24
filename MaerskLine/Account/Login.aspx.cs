using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using MaerskLine.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace MaerskLine.Account
{
    public partial class Login : Page
    {

        private bool isPersistent = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["userID"] = "";
            Session["userEmail"] = "";
            Session["userCredentials"] = "";
            Session["userFirstName"] = "";
            Session["Login"] = "false";

            RegisterHyperLink.NavigateUrl = "Register";

            //// Enable this once you have account confirmation enabled for password reset functionality
            ////ForgotPasswordHyperLink.NavigateUrl = "Forgot";
            ////OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            //var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            //if (!String.IsNullOrEmpty(returnUrl))
            //{
            //    RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            //}
        }

        protected void LogIn(object sender, EventArgs e)
        {
            //if (IsValid)
            //{
            //    // Validate the user password
            //    var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //    var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

            //    // This doen't count login failures towards account lockout
            //    // To enable password failures to trigger lockout, change to shouldLockout: true
            //    var result = signinManager.PasswordSignIn(Email.Text, Password.Text, RememberMe.Checked, shouldLockout: false);

            //    switch (result)
            //    {
            //        case SignInStatus.Success:
            //            IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            //            break;
            //        case SignInStatus.LockedOut:
            //            Response.Redirect("/Account/Lockout");
            //            break;
            //        case SignInStatus.RequiresVerification:
            //            Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}", 
            //                                            Request.QueryString["ReturnUrl"],
            //                                            RememberMe.Checked),
            //                              true);
            //            break;
            //        case SignInStatus.Failure:
            //        default:
            //            FailureText.Text = "Invalid login attempt";
            //            ErrorMessage.Visible = true;
            //            break;
            //    }
            //}

            SqlConnection dbConnect = new SqlConnection();
            try
            {
                dbConnect.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                dbConnect.Open();

                SqlCommand checkLogin = new SqlCommand("SELECT user_id,email_address,credentials,first_name FROM Users WHERE email_address=@email and password=@password", dbConnect);
                checkLogin.Parameters.Add(new SqlParameter("@email", Email.Text));
                checkLogin.Parameters.Add(new SqlParameter("@password", Password.Text));
                SqlDataReader reader = checkLogin.ExecuteReader();

                if (reader.Read())
                {
                    var userID = reader["user_id"].ToString();
                    int uID = int.Parse(userID);
                    var firstName = reader["first_name"].ToString();
                    var credentials = reader["credentials"].ToString();
                    dbConnect.Close();
                    reader.Close();

                    Session["userID"] = uID;
                    Session["userEmail"] = Email.Text;
                    Session["userFirstName"] = firstName;
                    Session["userCredentials"] = credentials;
                    Session["Login"] = "true";

                    //Customer login
                    if (credentials.Equals("Staff"))
                    {
                        Response.Redirect("/Account/ResetPassword",false);
                    }
                    else if (credentials.Equals("Customer"))
                    {
                        Response.Redirect("/ShippingList",false);
                    }
                }
                else
                {
                    Type type = this.GetType();
                    ClientScriptManager manager = Page.ClientScript;
                    if (!manager.IsStartupScriptRegistered(type,"PopupScript"))
                    {
                        String errorText = "alert('Username and password entered is invalid! Please try again');";
                        manager.RegisterStartupScript(type, "PopupScript", errorText, true);
                    }
                }

                if (!reader.IsClosed)
                {
                    reader.Close();
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                if(dbConnect.State == System.Data.ConnectionState.Open)
                {
                    dbConnect.Close();
                    dbConnect.Dispose();
                }
            }
        }
    }
}