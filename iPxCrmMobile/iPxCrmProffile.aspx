<%@ Page Language="VB" MasterPageFile="~/iPxCrmMobile/iPxMobileOprBck.master" AutoEventWireup="false" CodeFile="iPxCrmProffile.aspx.vb" Inherits="iPxCrmMobile_iPxCrmProffile" title="E-Ticket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br /><br /><br />
    <div class="row" style="margin-right :15px;">
        <div class="col-md-6">
            <div class="form-group">
                <label for="usr">Hotel Name:</label>
                <asp:TextBox ID="tbCustName" runat="server" CssClass ="form-control" ReadOnly="true"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="usr">Contact Name:</label><font color="red">*</font>
                <asp:TextBox ID="tbNameContact" runat="server" CssClass ="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="usr">Birthday:</label><font color="red">*</font>
                <div class="input-group date datepicker" style="padding:0px;">
                    <asp:TextBox ID="tbBirthday" runat="server" CssClass ="form-control " placeholder="dd/MM/yyyy"></asp:TextBox>
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="form-group">
                <label for="usr">Contact Group:</label>
                <asp:DropDownList ID="dlContactGrp" runat="server" CssClass ="form-control" ReadOnly="true" Enabled="false">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label for="usr">Phone:</label>
                <asp:TextBox ID="tbPhone" runat="server" CssClass ="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="usr">Email:</label>
                <asp:TextBox ID="tbEmail" runat="server" CssClass ="form-control" ReadOnly="true"></asp:TextBox>
            </div>
            <table class="">
                <tr>
                    <td>
                        <asp:linkbutton  data- id="btnchange" runat="server">Change Password</asp:linkbutton>
                        <asp:Panel ID="pnlOldPW" Visible="false" runat="server">
                        <div class="input-group">
                            <asp:textbox class="form-control padding-horizontal-15" id="txtoldpass" placeholder="Old Password" TextMode="Password" runat="server"></asp:textbox>
                            <span class="input-group-btn">
                            <asp:LinkButton ID="btnCheckOldPw" CssClass=" btn btn-default " runat="server">OK</asp:LinkButton>
                            </span>
                        </div>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:LinkButton ID="btnCancel" Visible="false" CssClass=" btn btn-default btn-block" runat="server">Cancel</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:textbox class="form-control padding-horizontal-15" Visible="false" id="txtnewpass" placeholder="New Password" TextMode="Password" runat="server"></asp:textbox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:textbox class="form-control padding-horizontal-15" Visible="false" id="txtnewpassconf" placeholder="Confirm Password" TextMode="Password" runat="server"></asp:textbox>
                    </td>
                </tr>
                <tr>   
                    <td>
                        <asp:LinkButton ID="btnsavepass" CssClass=" btn btn-default btn-block" Visible="false" runat="server">Change Password</asp:LinkButton>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <br />
    <div align="center">
    <asp:LinkButton Width ="150px" ID="lbSave" CssClass ="btn btn-default" runat="server"><i class="fa fa-save"></i> Save Profile</asp:LinkButton>
    <asp:LinkButton Width ="150px" ID="lbAbort" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
    </div>
</asp:Content>
<%--<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>--%>