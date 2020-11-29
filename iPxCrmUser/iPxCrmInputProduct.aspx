<%@ Page Language="VB" MasterPageFile="~/iPxCrmUser/iPxMasterUser.master" AutoEventWireup="false" CodeFile="iPxCrmInputProduct.aspx.vb" Inherits="iPxCrmUser_iPxCrmInputProduct" title="Form Product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                          <label for="usr">Product Group:</label>
                            <asp:DropDownList ID="dlProductGrp" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="dlProductGrp_SelectedIndexChanged" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                          <label for="usr">Product ID:</label><font color=red>*</font>
                            <asp:TextBox ID="tbPrdID" runat="server" OnTextChanged="cari" Enabled="false" AutoPostBack="true" CssClass ="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group">
                          <label for="usr">Product Name:</label><font color=red>*</font>
                            <asp:TextBox ID="tbPrdName" runat="server" CssClass ="form-control"></asp:TextBox>
                        </div>
                    </div>
                    
                    <div class="col-md-4">
                        <div class="form-group">
                          <label for="usr">Normal Price:</label>
                            <asp:TextBox ID="tbNormal" runat="server" CssClass ="form-control" style="text-align: right" onkeypress="return hanyaAngka(event)"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                          <label for="usr">Low Price:</label>
                            <asp:TextBox ID="tbLow" runat="server" CssClass ="form-control" style="text-align: right" onkeypress="return hanyaAngka(event)"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                          <label for="usr">Hight Price:</label>
                            <asp:TextBox ID="tbHight" runat="server" CssClass ="form-control" style="text-align: right" onkeypress="return hanyaAngka(event)"></asp:TextBox>
                        </div>
                    </div>
                </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <p class="text-left">
        <asp:LinkButton Width="150px" ID="lbSave" CssClass ="btn btn-default" runat="server"><i class="fa fa-save"></i> Save Product</asp:LinkButton>
        <asp:LinkButton Width="150px" ID="lbAbort" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
    </p>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>

