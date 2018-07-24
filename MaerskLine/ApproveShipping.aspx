<%@ Page Title="Shippings Approval" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApproveShipping.aspx.cs" Inherits="MaerskLine.ApproveShipping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Hello! <asp:Label runat="server" Font-Bold="true" ForeColor="Yellow" ID="name"></asp:Label></h3>
    <h2><%: Title %></h2>
    <p>
        <asp:GridView ID="gvShippingApproval" runat="server" AutoGenerateColumns="False" DataKeyNames="shipping_id" DataSourceID="ShippingApprovalDataSource" Width="1279px" CssClass="table table-striped table-bordered table-condensed">
            <Columns>
                <asp:BoundField DataField="shipping_id" HeaderText="Shipping ID" InsertVisible="False" ReadOnly="True" SortExpression="shipping_id" />
                <asp:BoundField DataField="ship_type" HeaderText="Ship Types" SortExpression="ship_type" />
                <asp:BoundField DataField="additional_info" HeaderText="Additional Information" SortExpression="additional_info" />
                <asp:BoundField DataField="departure_port" HeaderText="Departure Port" SortExpression="departure_port" />
                <asp:BoundField DataField="arrival_port" HeaderText="Arrival Port" SortExpression="arrival_port" />
                <asp:BoundField DataField="request_date" HeaderText="Requested Date" SortExpression="request_date" />
                <asp:BoundField DataField="status" HeaderText="Shipping Status" SortExpression="status" />
                <asp:BoundField DataField="total_price" HeaderText="Total Price(RM)" SortExpression="total_price" />
                <asp:ButtonField CommandName="btnApprove" Text="Approve" />
                <asp:ButtonField CommandName="btnDecline" Text="Decline" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="ShippingApprovalDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:MaerskLine2018DDAC_dbConnectionString %>" SelectCommand="SELECT * FROM [Shippings]"></asp:SqlDataSource>
    </p>

    <div class="form-horizontal">
            <div class="form-group">
                <asp:Label runat="server" ID="lblemptyShippingTable" style="text-align:left;" CssClass="col-md-12 control-label" Font-Size="X-Large" Visible="false">No shippings can be found or created.</asp:Label>
            </div>
    </div>

</asp:Content>
