<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="pgAccountDetails.aspx.cs" Inherits="pgAccountDetails" %>
<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="ContentArea1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="body1">
        <div class="alignRight">
            <asp:LinkButton ID="lbtnLogout" runat="server" PostBackUrl="~/pgLogin.aspx">Logout</asp:LinkButton>
        </div>
        <table>
            <tr>
                <td> 
                    <asp:Label ID="aclblUserName" AssociatedControlID="txtUserName" runat="server" Text="Username:"></asp:Label>
                    <asp:TextBox ID="txtUserName" runat="server" ReadOnly="True"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="aclblCity" AssociatedControlID="txtCity" runat="server" Text="City:"></asp:Label>
                    <asp:TextBox ID="txtCity" runat="server" TabIndex="1" ></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="aclblState" AssociatedControlID="ddlState" runat="server" Text="State:"></asp:Label>
                    <asp:DropDownList ID="ddlState" runat="server" DataSourceID="SqlDataSourceStates" DataTextField="StateName" DataValueField="StateAbbr" TabIndex="2"></asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSourceStates" runat="server" ConnectionString="Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\ProgramaholicsAnonymous.mdb" ProviderName="System.Data.OleDb" SelectCommand="SELECT * FROM [tblStates] ORDER BY [StateAbbr]"></asp:SqlDataSource>
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
                    <td><asp:Label ID="aclblFavorite" AssociatedControlID="txtFavorite" runat="server" Text="Favorite:"></asp:Label></td>
                    <td><asp:TextBox ID="txtFavorite" runat="server" Width="300px" TabIndex="3"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Label ID="aclblLeastFavorite" AssociatedControlID="txtLeastFavorite" runat="server" Text="Least Favorite:"></asp:Label></td>
                    <td><asp:TextBox ID="txtLeastFavorite" runat="server" Width="300px" TabIndex="4"></asp:TextBox></td>
                </tr>
            </table>
        </fieldset>
    </div>
</asp:Content>
<asp:Content ID="ContentArea3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div class="body3">
        <fieldset>
            <legend>Applications Completed</legend><br />
            <asp:Label ID="aclblLastCompletionDate" AssociatedControlID="txtLastCompletionDate" runat="server" Text="Last Completion Date:"></asp:Label>
            <asp:TextBox ID="txtLastCompletionDate" runat="server" ReadOnly="True"></asp:TextBox><br /><br />
            <asp:GridView ID="gvApplicationsCompleted" runat="server" AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black">
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                <RowStyle BackColor="White" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#808080" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
            </asp:GridView>
            <br />
        </fieldset>
    </div>
</asp:Content>
<asp:Content ID="ContentArea4" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <div class="body4">
        <table>
            <tr>
                <td><asp:Button ID="btnUpdate" runat="server" Text="Update Account" PostBackUrl="~/pgAccountConfirmation.aspx" TabIndex="4"  /></td>
                <td><asp:Button ID="btnDelete" runat="server" Text="Delete Account" OnClick="btnDelete_Click" TabIndex="5" /></td>
                <td><asp:Button ID="btnExportStats" runat="server" Text="Export Statistics" OnClick="btnExportStats_Click" TabIndex="6" /></td>
            </tr>
        </table>
    </div>
</asp:Content>
