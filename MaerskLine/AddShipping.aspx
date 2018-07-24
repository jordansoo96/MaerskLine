<%@ Page Title="Request for New Shipping" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddShipping.aspx.cs" Inherits="MaerskLine.AddShipping" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %></h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <h4>Add a new shipping</h4>
        <hr />

        <asp:ValidationSummary runat="server" CssClass="text-danger" />

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="dropDownBeginning" CssClass="col-md-2 control-label">Departure Port:</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="dropDownBeginning" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="dropDownBeginning_SelectedIndexChanged" runat="server"
                    CssClass="form-control" DataSourceID="PortDataSource" DataTextField="port_name" DataValueField="price">
                    <asp:ListItem Value="" Selected="True" Text="Select Departure Port"></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource ID="PortDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT port_name,price FROM Ports "></asp:SqlDataSource>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="dropDownBeginning" CssClass="text-danger" Display="Dynamic" ErrorMessage="Please select one!" InitialValue=""></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="dropDownArrival" CssClass="col-md-2 control-label">Arrival Port:</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="dropDownArrival" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="dropDownArrival_SelectedIndexChanged" runat="server"
                    CssClass="form-control" DataSourceID="PortDataSource" DataTextField="port_name" DataValueField="price">
                    <asp:ListItem Value="" Selected="True" Text="Select Arrival Port"></asp:ListItem>
                </asp:DropDownList>
                <asp:CompareValidator runat="server" ControlToCompare="dropDownBeginning" ControlToValidate="dropDownArrival" CssClass="text-danger" Display="Dynamic" Operator="NotEqual" ErrorMessage="Both of the ports should not be matched" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="dropDownArrival" CssClass="text-danger" Display="Dynamic" ErrorMessage="Please select one!" InitialValue=""></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="dropDownShipType" CssClass="col-md-2 control-label">Ship Types:</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="dropDownShipType" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="dropDownShipType_SelectedIndexChanged" runat="server"
                    CssClass="form-control" DataSourceID="ShipTypeDataSource" DataTextField="ship_type" DataValueField="ship_price">
                    <asp:ListItem Value="" Selected="True" Text="Select Ship Type"></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource ID="ShipTypeDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT ship_type,ship_price FROM Ships "></asp:SqlDataSource>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="dropDownShipType" CssClass="text-danger" Display="Dynamic" ErrorMessage="Please select one!" InitialValue=""></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="additionalInfo" CssClass="col-md-2 control-label">Additional Information:</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="additionalInfo" TextMode="MultiLine" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="additionalInfo" CssClass="text-danger" Display="Dynamic" ErrorMessage="The additional information field is required" InitialValue=""></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="TotalPrice" CssClass="col-md-2 control-label">Total Price(RM):</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="TotalPrice" Text="0.00" CssClass="form-control" Enabled="false" />
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button ID="btnAdd" runat="server" OnClick="addShipping" Text="Add Shipping" CssClass="btn btn-default" />
            </div>
        </div>

    </div>
</asp:Content>

