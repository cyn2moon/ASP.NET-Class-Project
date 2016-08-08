<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="pgAccountConfirmation.aspx.cs" Inherits="pgAccountConfirmation" %>
<%@ MasterType VirtualPath="~/Site.master" %>
<%@ PreviousPageType VirtualPath="~/pgAccountDetails.aspx" %>

<asp:Content ID="ContentArea1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="body1">
        <table>
            <tr>
                <td> 
                    <asp:Label ID="aclblUserName" AssociatedControlID="lblUserName" runat="server" Text="Username:" Font-Bold="True"></asp:Label>
                    <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:Label ID="aclblCity" AssociatedControlID="lblCity" runat="server" Text="City:" Font-Bold="True"></asp:Label>
                    <asp:Label ID="lblCity" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:Label ID="aclblState" AssociatedControlID="lblState" runat="server" Text="State:" Font-Bold="True"></asp:Label>
                    <asp:Label ID="lblState" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="ContentArea2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="body2">
        <fieldset>
            <legend>Programming Language </legend>
            <table>
                <tr>
                    <td><asp:Label ID="aclblFavorite" AssociatedControlID="lblFavorite" runat="server" Text="Favorite:" Font-Bold="True"></asp:Label></td>
                    <td><asp:Label ID="lblFavorite" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td><asp:Label ID="aclblLeastFavorite" AssociatedControlID="lblLeastFavorite" runat="server" Text="Least Favorite:" Font-Bold="True"></asp:Label></td>
                    <td><asp:Label ID="lblLeastFavorite" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
        </fieldset>
    </div>
</asp:Content>
<asp:Content ID="ContentArea3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div class="body3 row">
        <fieldset>
            <legend>Applications Completed</legend><br />
            <asp:Label ID="aclblLastCompletionDate" AssociatedControlID="lblLastCompletionDate" runat="server" Text="Last Completion Date:" Font-Bold="True"></asp:Label>
                <asp:Label ID="lblLastCompletionDate" runat="server" Text=""></asp:Label><br /><br />
            <asp:Table ID="userApplicationTable" runat="server" CellPadding="3" CellSpacing="0" GridLines="both">
            </asp:Table> 
        </fieldset>
    </div>
</asp:Content>
<asp:Content ID="ContentArea4" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <div class="body4">
        <table>
            <tr>
                <td><asp:Button ID="btnConfirm" runat="server" Text="Confirm" OnClick="btnConfirm_Click" TabIndex="1" /></td>
                <td><asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" TabIndex="2" /></td>
                <td><asp:Button ID="btnGoBack" runat="server" Text="Go Back" OnClick="btnGoBack_Click" Visible="False" /></td>
            </tr>
        </table>
    </div>
</asp:Content>