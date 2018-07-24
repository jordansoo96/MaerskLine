using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MaerskLine.Account
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            name.Text = Session["userFirstName"].ToString();
        }
        
        protected void changePassword(object sender, EventArgs e)
        {
            var oldPass = "";
            int currentID = int.Parse(Session["userID"].ToString());

            SqlConnection dbConnect = new SqlConnection();
            dbConnect.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string validatePassword = "SELECT password FROM Users WHERE user_id = @id;";
            SqlCommand cmdValidatePassword = new SqlCommand(validatePassword, dbConnect);
            cmdValidatePassword.Parameters.Add("@id", SqlDbType.Int);
            cmdValidatePassword.Parameters["@id"].Value = currentID;
            dbConnect.Open();

            SqlDataReader reader = cmdValidatePassword.ExecuteReader();

            if (reader.Read())
            {
                oldPass = reader["password"].ToString();
                reader.Close();
            }
            dbConnect.Close();

            if (oldPass.Equals(NewPassword.Text))
            {
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                {
                    String cstext = "alert('Change denied! New password should not be the same as old password.');";
                    cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                }
            }
            else
            {
                string updatePassword = "UPDATE Users SET password = @password WHERE user_id = @id;";
                SqlCommand cmdUpdatePassword = new SqlCommand(updatePassword, dbConnect);
                cmdUpdatePassword.Parameters.Add("@id", SqlDbType.Int);
                cmdUpdatePassword.Parameters["@id"].Value = currentID;
                cmdUpdatePassword.Parameters.Add("@password", SqlDbType.NVarChar);
                cmdUpdatePassword.Parameters["@password"].Value = ConfirmPassword.Text;

                dbConnect.Open();
                int pass = cmdUpdatePassword.ExecuteNonQuery();
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
}