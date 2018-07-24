using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MaerskLine
{
    public partial class ShippingList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            name.Text = Session["userFirstName"].ToString();
            emptyShippingTable();
        }

        protected void requestShipping(object sender, EventArgs e)
        {
            Response.Redirect("/AddShipping.aspx", false);
        }

        private void emptyShippingTable()
        {
            int row = gvShippingList.Rows.Count;

            if (row.Equals(0))
            {
                lblemptyShippingTable.Visible = true;
            }
            else
            {
                lblemptyShippingTable.Visible = false;
            }
        }

        protected void gvShippingList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var cmd = e.CommandName;

            if (cmd.Equals("RemoveShipping"))
            {
                int row = int.Parse(e.CommandArgument.ToString());
                int selectedShippingID = int.Parse(gvShippingList.Rows[row].Cells[0].Text);
                var shippingStatus = gvShippingList.Rows[row].Cells[6].Text;

                if (!shippingStatus.Equals("Pending for Approval") && !shippingStatus.Equals("Declined"))
                {
                    Type type = this.GetType();
                    ClientScriptManager manager = Page.ClientScript;
                    if (!manager.IsStartupScriptRegistered(type, "PopupScript"))
                    {
                        String errorText = "alert('Permission denied! Shipping status with Pending for Approval and Declined are allowed to be remove.');";
                        manager.RegisterStartupScript(type, "PopupScript", errorText, true);
                    }
                }
                else
                {
                    SqlConnection dbConnect = new SqlConnection();
                    dbConnect.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    string removeShipping = "DELETE FROM Shippings WHERE shipping_id = @id;";
                    SqlCommand cmdRemoveShipping = new SqlCommand(removeShipping, dbConnect);

                    cmdRemoveShipping.Parameters.Add("@id", SqlDbType.Int);
                    cmdRemoveShipping.Parameters["@id"].Value = selectedShippingID;

                    dbConnect.Open();
                    int pass = cmdRemoveShipping.ExecuteNonQuery();
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
}