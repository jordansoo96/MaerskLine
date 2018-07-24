using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaerskLine
{
    public partial class ReceiveShipping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            name.Text = Session["userFirstName"].ToString();
            emptyShippingTable();
        }

        private void emptyShippingTable()
        {
            int row = gvShippingReceival.Rows.Count;

            if (row.Equals(0))
            {
                lblemptyShippingTable.Visible = true;
            }
            else
            {
                lblemptyShippingTable.Visible = false;
            }
        }

        protected void gvShippingReceival_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var cmd = e.CommandName;

            if (cmd.Equals("btnReceive"))
            {
                int row = int.Parse(e.CommandArgument.ToString());
                int shippingID = int.Parse(gvShippingReceival.Rows[row].Cells[0].Text);
                var shippingStatus = WebConfigurationManager.AppSettings["statusReceived"];

                SqlConnection dbConnect = new SqlConnection();
                dbConnect.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                string shippingReceival = "UPDATE Shippings SET status = @status WHERE shipping_id = @id;";
                SqlCommand cmdShippingReceival = new SqlCommand(shippingReceival, dbConnect);
                cmdShippingReceival.Parameters.Add("@id", SqlDbType.Int);
                cmdShippingReceival.Parameters["@id"].Value = shippingID;
                cmdShippingReceival.Parameters.Add("@status", SqlDbType.NVarChar);
                cmdShippingReceival.Parameters["@status"].Value = shippingStatus;

                dbConnect.Open();
                int pass = cmdShippingReceival.ExecuteNonQuery();
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