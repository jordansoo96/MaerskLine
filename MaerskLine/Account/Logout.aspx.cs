using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaerskLine.Account
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["userID"] = "";
            Session["userEmail"] = "";
            Session["userCredentials"] = "";
            Session["Login"] = "false";
            Response.Redirect("/Account/Login",false);
        }
    }
}