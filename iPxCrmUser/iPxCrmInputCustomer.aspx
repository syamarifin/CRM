<%@ Page Language="VB" MasterPageFile="~/iPxCrmUser/iPxMasterUser.master" AutoEventWireup="false" CodeFile="iPxCrmInputCustomer.aspx.vb" Inherits="iPxCrmUser_iPxCrmInputCustomer" title="Form Customer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="lblTitleTicket" runat="server" Text="Input Data Customer" Font-Bold="true" Font-Size="Large"></asp:Label><hr />
    <div class="row">
        <div class="col-sm-6">
            <div class="form-group">
                <label for="">Customer ID</label>
                <asp:TextBox ID="tbCustId" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="">Customer Group</label>
                <div class="input-group">
                <asp:DropDownList ID="dlCustGrub" runat="server" CssClass="form-control">
                </asp:DropDownList>
                    <div class="input-group-btn">
                        <asp:LinkButton ID="lbAddGroup" class="btn btn-default" runat="server" Font-Size="Small"><span class="glyphicon glyphicon-plus" style="height:20px;"></span> Add Group</asp:LinkButton>
                     </div>
                 </div>
            </div>
            <div class="form-group">
                <label for="">Hotel Name</label><font color=red>*</font>
                <asp:TextBox ID="tbName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="">Phone</label><font color=red>*</font>
                <asp:TextBox ID="tbPhone" runat="server" CssClass="form-control" onkeypress="return hanyaAngka(event)"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="">Fax</label><font color=red>*</font>
                <asp:TextBox ID="tbFax" runat="server" CssClass="form-control" onkeypress="return hanyaAngka(event)"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="">Total Room</label><font color=red>*</font>
                <asp:TextBox ID="tbTroom" runat="server" CssClass="form-control" onkeypress="return hanyaAngka(event)"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="">Address</label><font color=red>*</font>
                <asp:TextBox ID="tbAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-6">
            <div class="form-group">
                <label for="">Country</label>
                <asp:DropDownList ID="dlCountry" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="dlCountry_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label for="">province</label>
                <asp:DropDownList ID="dlProvinsi" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="dlProvinsi_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label for="">City</label>
                <asp:DropDownList ID="dlCity" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label for="">Star Class</label>
                <asp:TextBox ID="tbStar" runat="server" CssClass="form-control" onkeypress="return hanyaAngka(event)"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="">Anniversary</label>
                <div class="input-group date datepicker">
                     <asp:TextBox ID="tbAnniversery" runat="server" CssClass ="form-control" placeholder="dd-MM-yyyy"></asp:TextBox>
                     <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                </div>
            </div>
            <div class="form-group">
                <label for="">Status</label>
                <asp:DropDownList ID="dlStatus" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label for="">Note</label>
                <asp:TextBox ID="tbNote" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
        <div class="col-sm-6">
        <p class="text-left">
            <asp:LinkButton Width="150px" ID="lbSave" CssClass ="btn btn-default" runat="server"><i class="fa fa-save"></i> Save Member</asp:LinkButton>
            <asp:LinkButton Width="150px" ID="lbAbort" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
        </p>
        </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>

