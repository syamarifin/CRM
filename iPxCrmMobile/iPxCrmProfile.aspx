<%@ Page Language="VB" MasterPageFile="~/iPxCrmMobile/iPxMobileOprBck.master" AutoEventWireup="false" CodeFile="iPxCrmProfile.aspx.vb" Inherits="iPxCrmMobile_iPxCrmProfile" title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br /><br /><br />
<div align="center">
    <table class="table" style="width: 100%;">
        <tr>
            <td style="width:100px">
                Customer ID
            </td>
            <td style="width:10px">
                :
            </td>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Customer Name
            </td>
            <td>
                :
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Position
            </td>
            <td>
                :
            </td>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Phone
            </td>
            <td>
                :
            </td>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                e-Mail
            </td>
            <td>
                :
            </td>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Birthday
            </td>
            <td>
                :
            </td>
            <td>
                <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Hotel Name
            </td>
            <td>
                :
            </td>
            <td>
                <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Adress
            </td>
            <td>
                :
            </td>
            <td>
                <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Country
            </td>
            <td>
                :
            </td>
            <td>
                <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                province
            </td>
            <td>
                :
            </td>
            <td>
                <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                City
            </td>
            <td>
                :
            </td>
            <td>
                <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Star Class
            </td>
            <td>
                :
            </td>
            <td>
                <asp:Label ID="Label12" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:LinkButton ID="lbSunting" CssClass ="btn btn-default" runat="server"><i class="fa fa-edit"></i> Edit Profile</asp:LinkButton>
</div>
</asp:Content>

