<%@ Page Language="VB" MasterPageFile="~/iPxCrmCustomer/iPxCrmCustomerUpload.master" AutoEventWireup="false" CodeFile="iPxCrmFormTicket.aspx.vb" Inherits="iPxCrmCustomer_iPxCrmFormTicket" title="Form E-Ticket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
<script>
    function showWaitingMessage() {
        document.getElementById("waitMessage").style.display = "block";
    }
</script>
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                          <label for="usr">Ticket No:</label>
                            <asp:TextBox ID="tbTicketno" runat="server" CssClass="form-control" 
                                ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="form-group">
                          <label for="usr">Hotel Name:</label>
                            <asp:TextBox ID="tbHotelName" runat="server" CssClass="form-control" 
                                ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="form-group">
                          <label for="usr">Contact:</label>
                            <asp:TextBox ID="tbContact" runat="server" CssClass="form-control" 
                                ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="form-group">
                          <label for="usr">From:</label><font color="red">*</font>
                            <asp:TextBox ID="tbFrom" runat="server" CssClass="form-control" onkeypress="return hanyaHuruf(event)"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="usr">Product Group:</label><font color="red">*</font>
                            <asp:DropDownList ID="dlPrdGrp" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="dlPrdGrp_SelectedIndexChanged">
                            </asp:DropDownList>  
                        </div>
                        <div class="form-group">
                          <label for="usr">Product Name:</label><font color="red">*</font>
                            <asp:DropDownList ID="dlProduct" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="dlProduct_SelectedIndexChanged">
                            </asp:DropDownList>  
                        </div>
                        <div class="form-group">
                          <label for="usr">Menu:</label><font color="red">*</font>
                            <asp:DropDownList ID="dlMenu" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="dlMenu_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                          <label for="usr">Sub Menu:</label><font color="red">*</font>
                            <asp:DropDownList ID="dlSubMenu" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group">
                          <label for="usr">Description:</label><font color="red">*</font>
                            <asp:TextBox ID="tbDescription" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:FileUpload ID="FileUpload1" runat="server" accept="image/jpeg,application/pdf,application/vnd.openxmlformats-officedocument.wordprocessingml.document,application/zip,application/rar,application/msword"/><div id="waitMessage" style="display: none; color:Red; font-size:medium;">Please wait...</div>
                            <asp:Label ID="Label1" runat="server" Text="Max Size 10MB(.docx,.jpeg,.rar,.pdf)"></asp:Label>
                        </div>
                    </div>
                </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
                <p class="text-left">
                    <asp:LinkButton Width="150px" ID="lbSave" CssClass ="btn btn-default" runat="server" OnClientClick="showWaitingMessage()" ><i class="fa fa-send"></i> Send Ticket</asp:LinkButton>
                    <asp:LinkButton Width="150px" ID="lbCancel" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
                </p>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>

