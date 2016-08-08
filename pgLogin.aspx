<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.master" CodeFile="pgLogin.aspx.cs" Inherits="pgLogin" %>
<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="ContentArea1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="body1">
        <div class="alignLeft">
            <asp:Label ID="lblUsername" runat="server" Width="5em">Username:</asp:Label>
            <asp:TextBox ID="txtUsername" runat="server" TabIndex="1"></asp:TextBox><br />
            <asp:Label ID="lblPassword" runat="server" Width="5em">Password:</asp:Label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" TabIndex="2"></asp:TextBox><br /><br />
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" TabIndex="3" />
        </div>
    </div>
</asp:Content>
