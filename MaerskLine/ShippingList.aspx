<%@ Page Title="Shipping Lists" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShippingList.aspx.cs" Inherits="MaerskLine.ShippingList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>

    <div style="margin-top:20px;">
    <asp:GridView ID="gvShippingList" runat="server" AutoGenerateColumns="False" DataKeyNames="shipping_id" DataSourceID="ShippingListDataSource" Height="224px" Width="1011px" CssClass="table table-striped table-bordered table-condensed">
        <Columns>
            <asp:BoundField DataField="shipping_id" HeaderText="shipping_id" InsertVisible="False" ReadOnly="True" SortExpression="shipping_id" />
            <asp:BoundField DataField="ship_type" HeaderText="ship_type" SortExpression="ship_type" />
            <asp:BoundField DataField="additional_info" HeaderText="additional_info" SortExpression="additional_info" />
            <asp:BoundField DataField="departure_port" HeaderText="departure_port" SortExpression="departure_port" />
            <asp:BoundField DataField="arrival_port" HeaderText="arrival_port" SortExpression="arrival_port" />
            <asp:BoundField DataField="request_date" HeaderText="request_date" SortExpression="request_date" />
            <asp:BoundField DataField="status" HeaderText="status" SortExpression="status" />
            <asp:BoundField DataField="total_price" HeaderText="total_price" SortExpression="total_price" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="ShippingListDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:MaerskLine2018DDAC_dbConnectionString %>" SelectCommand="SELECT * FROM [Shippings]"></asp:SqlDataSource>

        <div class="form-group">
            <div class="col-md-offset-0 col-md-10">
                <asp:Button ID="btnRequest" runat="server" OnClick="requestShipping" Text="Request New Shipping" CssClass="btn btn-default" />
            </div>
        </div>

        <div class="form-horizontal">
            <div class="form-group">
                <asp:Label runat="server" ID="lblemptyShippingTable" style="text-align:left;" CssClass="col-md-12 control-label" Font-Size="Large" Visible="false">Currently there are none shippings coming in</asp:Label>
            </div>
        </div>
</div>
</asp:Content>
