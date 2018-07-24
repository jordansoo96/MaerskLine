using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MaerskLine
{
    public partial class ShippingList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void requestShipping(object sender, EventArgs e)
        {
            Response.Redirect("/AddShipping.aspx", false);
        }

        protected void emptyShippingTable(object sender, EventArgs e)
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
    }
}