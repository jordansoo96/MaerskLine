using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MaerskLine
{
    public partial class ApproveShipping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            name.Text = Session["userFirstName"].ToString();
            emptyShippingTable();
        }

        private void emptyShippingTable()
        {
            int row = gvShippingApproval.Rows.Count;

            if (row.Equals(0))
            {
                lblemptyShippingTable.Visible = true;
            }
            else
            {
                lblemptyShippingTable.Visible = false;
            }
        }

        protected void gvShippingApproval_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var cmd = e.CommandName;

            if (cmd.Equals("btnApprove"))
            {
                int row = int.Parse(e.CommandArgument.ToString());
                int shippingID = int.Parse(gvShippingApproval.Rows[row].Cells[0].Text);
                var shippingStatus = WebConfigurationManager.AppSettings["statusApproved"];

                SqlConnection dbConnect = new SqlConnection();
                dbConnect.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                string shippingApproval = "UPDATE Shippings SET status = @status WHERE shipping_id = @id;";
                SqlCommand cmdShippingApproval = new SqlCommand(shippingApproval, dbConnect);
                cmdShippingApproval.Parameters.Add("@id", SqlDbType.Int);
                cmdShippingApproval.Parameters["@id"].Value = shippingID;
                cmdShippingApproval.Parameters.Add("@status", SqlDbType.NVarChar);
                cmdShippingApproval.Parameters["@status"].Value = shippingStatus;

                dbConnect.Open();
                int pass = cmdShippingApproval.ExecuteNonQuery();
                dbConnect.Close();

                if (pass == 0)
                {

                }
                else
                {
                    Response.Redirect(Request.RawUrl);
                }
            }
            else if (cmd.Equals("btnDecline"))
            {
                int row = int.Parse(e.CommandArgument.ToString());
                int shippingID = int.Parse(gvShippingApproval.Rows[row].Cells[0].Text);
                var shippingStatus = WebConfigurationManager.AppSettings["statusDeclined"];

                SqlConnection dbConnect = new SqlConnection();
                dbConnect.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                string shippingRejected = "UPDATE Shippings SET status = @status WHERE shipping_id = @id;";
                SqlCommand cmdShippingRejected = new SqlCommand(shippingRejected, dbConnect);
                cmdShippingRejected.Parameters.Add("@id", SqlDbType.Int);
                cmdShippingRejected.Parameters["@id"].Value = shippingID;
                cmdShippingRejected.Parameters.Add("@status", SqlDbType.NVarChar);
                cmdShippingRejected.Parameters["@status"].Value = shippingStatus;

                dbConnect.Open();
                int pass = cmdShippingRejected.ExecuteNonQuery();
                dbConnect.Close();

                if (pass == 0)
                {

                }
                else
                {
                    Response.Redirect(Request.RawUrl);
                }
            }
        }
    }
}