<%@ Page Title="Shipping Lists" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShippingList.aspx.cs" Inherits="MaerskLine.ShippingList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Hello! <asp:Label runat="server" Font-Bold="true" ForeColor="Yellow" ID="name"></asp:Label></h3>
    <h2><%: Title %></h2>

    <div style="margin-top:20px;">
    <asp:GridView ID="gvShippingList" runat="server" AutoGenerateColumns="False" DataKeyNames="shipping_id" DataSourceID="ShippingListDataSource" Height="16px" Width="1204px" CssClass="table table-striped table-bordered table-condensed" OnRowCommand="gvShippingList_RowCommand">
        <Columns>
            <asp:BoundField DataField="shipping_id" HeaderText="Shipping ID" InsertVisible="False" ReadOnly="True" SortExpression="shipping_id" />
            <asp:BoundField DataField="ship_type" HeaderText="Ship Types" SortExpression="ship_type" />
            <asp:BoundField DataField="additional_info" HeaderText="Additional Information" SortExpression="additional_info" />
            <asp:BoundField DataField="departure_port" HeaderText="Departure Port" SortExpression="departure_port" />
            <asp:BoundField DataField="arrival_port" HeaderText="Arrival Port" SortExpression="arrival_port" />
            <asp:BoundField DataField="request_date" HeaderText="Requested Date" SortExpression="request_date" />
            <asp:BoundField DataField="status" HeaderText="Shipping Status" SortExpression="status" />
            <asp:BoundField DataField="total_price" HeaderText="Total Price(RM)" SortExpression="total_price" />
            <asp:ButtonField CommandName="RemoveShipping" Text="Remove" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="ShippingListDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:MaerskLine2018DDAC_dbConnectionString %>" SelectCommand="SELECT * FROM [Shippings] WHERE status = 'Pending for Approval' OR status = 'Declined'"></asp:SqlDataSource>

        <div class="form-group">
            <div class="col-md-offset-0 col-md-10">
                <asp:Button ID="btnRequest" runat="server" OnClick="requestShipping" Text="Request New Shipping" CssClass="btn btn-default" />
            </div>
        </div>

        <div class="form-horizontal">
            <div class="form-group">
                <asp:Label runat="server" ID="lblemptyShippingTable" style="text-align:left;" CssClass="col-md-12 control-label" Font-Size="X-Large" Visible="false">No shippings can be found. Please click the button above to request for new shipping.</asp:Label>
            </div>
        </div>
</div>
</asp:Content>
