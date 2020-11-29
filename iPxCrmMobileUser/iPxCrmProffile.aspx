<%@ Page Language="VB" MasterPageFile="~/iPxCrmMobileUser/iPxMobileOprBck.master" AutoEventWireup="false" CodeFile="iPxCrmProffile.aspx.vb" Inherits="iPxCrmMobileUser_iPxCrmProffile" title="Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br /><br /><br />
    <div class="row" style="margin-left:0;margin-right:0;">
        <div class="col-md-6">
            <div class="form-group">
                <label for="usr">Name:</label>
                <asp:TextBox ID="tbName" runat="server" CssClass="form-control" onkeypress="return hanyaHuruf(event)"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="usr">Email:</label>
                <asp:TextBox ID="tbEmail" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-6">
            <div class="form-group">
                <label for="usr">Dept:</label>
                <asp:DropDownList ID="dlDept" runat="server" CssClass="form-control" >
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label for="usr">Position:</label>
                <asp:DropDownList ID="dlPosition" runat="server" CssClass="form-control" >
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-md-12">
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
    <div class="row" style="margin-left:0;margin-right:0;text-align:center;">
    <asp:LinkButton Width ="150px" ID="lbSave" CssClass ="btn btn-default" runat="server"><i class="fa fa-save"></i> Save Profile</asp:LinkButton>
    <asp:LinkButton Width ="150px" ID="lbAbort" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
    </div>
</asp:Content>

