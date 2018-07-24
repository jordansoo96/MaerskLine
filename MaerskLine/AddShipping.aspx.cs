using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Configuration;

namespace MaerskLine
{
    public partial class AddShipping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            dropDownBeginning.Items[0].Attributes.Add("disabled", "disabled");
            dropDownArrival.Items[0].Attributes.Add("disabled", "disabled");
            name.Text = Session["userFirstName"].ToString();
        }

        protected void addShipping(object sender, EventArgs e)
        {
            var shiptype = dropDownShipType.SelectedItem.ToString();
            var addInfo = additionalInfo.Text;
            var departurePortID = dropDownBeginning.SelectedItem.ToString();
            var arrivalPortID = dropDownArrival.SelectedItem.ToString();
            DateTime current = DateTime.Now;
            var status = WebConfigurationManager.AppSettings["pendingStatus"];
            double price = double.Parse(TotalPrice.Text);

            SqlConnection dbConnect = new SqlConnection();
            dbConnect.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string addShipping = "INSERT INTO Shippings (ship_type,additional_info,departure_port,arrival_port,request_date,status,total_price)" + "VALUES (@shipType,@addInfo,@departPort,@arrivalPort,@date,@status,@totalPrice);";
            SqlCommand cmdAddShipping = new SqlCommand(addShipping, dbConnect);

            cmdAddShipping.Parameters.Add("@shipType", SqlDbType.NVarChar);
            cmdAddShipping.Parameters["@shipType"].Value = shiptype;
            cmdAddShipping.Parameters.Add("@addInfo", SqlDbType.NVarChar);
            cmdAddShipping.Parameters["@addInfo"].Value = addInfo;
            cmdAddShipping.Parameters.Add("@departPort", SqlDbType.NVarChar);
            cmdAddShipping.Parameters["@departPort"].Value = departurePortID;
            cmdAddShipping.Parameters.Add("@arrivalPort", SqlDbType.NVarChar);
            cmdAddShipping.Parameters["@arrivalPort"].Value = arrivalPortID;
            cmdAddShipping.Parameters.Add("@date", SqlDbType.DateTime);
            cmdAddShipping.Parameters["@date"].Value = current;
            cmdAddShipping.Parameters.Add("@status", SqlDbType.NVarChar);
            cmdAddShipping.Parameters["@status"].Value = status;
            cmdAddShipping.Parameters.Add("@totalPrice", SqlDbType.Float);
            cmdAddShipping.Parameters["@totalPrice"].Value = price;

            dbConnect.Open();
            int pass = cmdAddShipping.ExecuteNonQuery();
            dbConnect.Close();

            if (pass == 0)
            {
                
            }
            else
            {
                Response.Redirect("/ShippingList.aspx", false);
            }
        }

        private void priceUpdate()
        {
            double totalPrice = 0.00;
            double portPrice;

            if(!dropDownBeginning.SelectedIndex.Equals(0) && !dropDownArrival.SelectedIndex.Equals(0) && !dropDownArrival.SelectedIndex.Equals(!dropDownBeginning.SelectedIndex.Equals(0)))
            {
                var depPort = dropDownBeginning.SelectedValue;
                var arrPort = dropDownArrival.SelectedValue;

                double depPortPrice = double.Parse(depPort.Split(',')[0]);
                double arrPortPrice = double.Parse(arrPort.Split(',')[0]);

                portPrice = depPortPrice + arrPortPrice;
                totalPrice = totalPrice + portPrice;
            }

            if (!dropDownShipType.SelectedIndex.Equals(0))
            {
                double shipPrice = double.Parse(dropDownShipType.SelectedValue.Split(',')[0]);
                totalPrice = totalPrice + shipPrice;
            }

            TotalPrice.Text = totalPrice.ToString();
        }

        protected void dropDownBeginning_SelectedIndexChanged(object sender, EventArgs e)
        {
            priceUpdate();
        }

        protected void dropDownArrival_SelectedIndexChanged(object sender, EventArgs e)
        {
            priceUpdate();
        }

        protected void dropDownShipType_SelectedIndexChanged(object sender, EventArgs e)
        {
            priceUpdate();
        }
    }
}