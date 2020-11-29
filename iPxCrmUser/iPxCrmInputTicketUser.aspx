<%@ Page Language="VB" MasterPageFile="~/iPxCrmUser/iPxMasterUserUpload.master" AutoEventWireup="false" CodeFile="iPxCrmInputTicketUser.aspx.vb" Inherits="iPxCrmUser_iPxCrmInputTicketUser" title="Form E-Ticket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script>
    function showWaitingMessage() {
        document.getElementById("waitMessage").style.display = "block";
    }
</script>
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <!-- Login Modal-->
      <div id="login-modal" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-md">
          <div class="modal-content">
            <div class="modal-header">
              <%--<button type="button" data-dismiss="modal" aria-label="Close" class="close"><span aria-hidden="true">×</span></button>--%>
              <h4 id="login-modalLabel" class="modal-title">Autentifikasi</h4>
            </div>
            <div class="modal-body">
                <h4>sure you want to delete data</h4>
                <p class="text-center">
                    <asp:LinkButton ID="LinkButton1" Width ="150px" CssClass ="btn btn-default" runat="server"><i class="fa fa-trash"></i> Delete E-Ticket</asp:LinkButton>
                    <asp:LinkButton ID="LinkButton2" Width ="150px" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
                </p>
            </div>
          </div>
        </div>
      </div>
      <!-- Login modal end-->
      <!-- Form Menu Modal-->
      <div id="formMenu-modal" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-md">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" data-dismiss="modal" aria-label="Close" class="close"><span aria-hidden="true">×</span></button>
              <h4 id="H1" class="modal-title">Form Menu</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="usr">Product:</label>
                            <asp:TextBox ID="tbDetailProduct" runat="server" CssClass ="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="usr">Menu ID:</label><font color=red>*</font>
                            <asp:TextBox ID="tbMenuID" runat="server" CssClass ="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="usr">Menu Name:</label><font color=red>*</font>
                            <asp:TextBox ID="tbMenuName" runat="server" CssClass ="form-control"></asp:TextBox>
                        </div>
                    </div>  
                </div>
                <div class="form-group center">
                  <div class="form-group">
                    <%--<asp:LinkButton width=125px CssClass="btn btn-default dropdown-toggle" data- ID="LinkButton2" runat="server"><span class="fa fa-filter "></span> Query</asp:LinkButton>--%>
                    <asp:LinkButton width="150px" ID="lbSaveMenu" CssClass ="btn btn-default" runat="server"><i class="fa fa-save"></i> Save</asp:LinkButton>
                    <asp:LinkButton width="150px" ID="lbAbortMenu" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
                  </div>
                </div>            
            </div>
          </div>
        </div>
      </div>
      <!-- Form Menu modal end-->
      <!-- Form SubMenu Modal-->
      <div id="formsub-modal" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-md">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" data-dismiss="modal" aria-label="Close" class="close"><span aria-hidden="true">×</span></button>
              <h4 id="H3" class="modal-title">Form Sub Menu</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="usr">Product:</label>
                            <asp:TextBox ID="tbDetailProductSub" runat="server" CssClass ="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="usr">Sub Menu ID:</label>
                            <asp:TextBox ID="tbSubID" runat="server" CssClass ="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="usr">Menu:</label>
                            <asp:TextBox ID="tbDetailMenuSub" runat="server" CssClass ="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="usr">Name:</label><font color=red>*</font>
                            <asp:TextBox ID="tbSubmenu" runat="server" CssClass ="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="usr">Link SOP</label>
                            <div class="input-group">
                                <asp:TextBox ID="tbLink" runat="server" CssClass ="form-control"></asp:TextBox>
                                <span class="input-group-addon"><i class="glyphicon glyphicon-paperclip"></i></span>
                            </div>
                        </div>
                    </div>   
                </div>
                <div class="form-group center">
                  <div class="form-group">
                    <%--<asp:LinkButton width=125px CssClass="btn btn-default dropdown-toggle" data- ID="LinkButton2" runat="server"><span class="fa fa-filter "></span> Query</asp:LinkButton>--%>
                    <asp:LinkButton width="150px" ID="lbSaveSub" CssClass ="btn btn-default" runat="server"><i class="fa fa-save"></i> Save</asp:LinkButton>
                    <asp:LinkButton width="150px" ID="lbAbortSub" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
                  </div>
                </div>            
            </div>
          </div>
        </div>
      </div>
      <!-- Form SubMenu modal end-->
    <div class="row">
         <div class="col-md-6">
            <div class="form-group">
                <label for="usr">Ticket No:</label>
                <asp:TextBox ID="tbTicketno" runat="server" CssClass="form-control" 
                 ReadOnly="True"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="usr">Hotel Name:</label>
                <%--<asp:DropDownList ID="dlHotel" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="dlHotel_SelectedIndexChanged">
                </asp:DropDownList>--%>
                
                <div class="input-group">
                <asp:TextBox ID="tbHotelName" runat="server" CssClass="form-control" ReadOnly="True" placeholder="Hotel Name"></asp:TextBox>
                    <div class="input-group-btn">
                        <asp:LinkButton Width ="50px" ID="lbFindHotel" class="btn btn-default" runat="server" Font-Size="Small"><span class="glyphicon glyphicon-search" style="height:20px;"></span></asp:LinkButton>
                    </div>
                </div> 
            </div>
            <div class="form-group">
                <label for="usr">Contact:</label>
                <asp:TextBox ID="tbContact" runat="server" CssClass="form-control" ReadOnly="True" placeholder="Hotel Contact"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="usr">Submit Via:</label>
                <asp:DropDownList ID="dlSubmitVia" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label for="usr">From:</label><font color=red>*</font>
                <asp:TextBox ID="tbFrom" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
         </div>
         <div class="col-md-6">
            <div class="form-group">
                <label for="usr">Date:</label><font color=red>*</font>
                <div class="input-group date datepicker" style="padding:0;">
                     <asp:TextBox ID="tbDate" runat="server" CssClass ="form-control" placeholder="dd-MM-yyyy"></asp:TextBox>
                     <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                </div>
            </div>
            <div class="form-group">
                <label for="usr">Product Group:</label><font color=red>*</font>
                <asp:DropDownList ID="dlPrdGrp" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="dlPrdGrp_SelectedIndexChanged">
                </asp:DropDownList>  
            </div>
            <div class="form-group">
                <label for="usr">Product Name:</label><font color=red>*</font>
                <asp:DropDownList ID="dlProduct" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="dlProduct_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label for="usr">Menu:</label><font color=red>*</font>
                <div class="input-group">
                <asp:DropDownList ID="dlMenu" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="dlMenu_SelectedIndexChanged">
                </asp:DropDownList> 
                    <div class="input-group-btn">
                        <asp:LinkButton Width ="130px" ID="lbAddMenu" class="btn btn-default" runat="server" Font-Size="Small"><span class="glyphicon glyphicon-plus" style="height:20px;"></span> Add Menu</asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <label for="usr">Sub Menu:</label><font color=red>*</font>
                <div class="input-group">
                <asp:DropDownList ID="dlSubMenu" runat="server" CssClass="form-control">
                </asp:DropDownList>
                    <div class="input-group-btn">
                        <asp:LinkButton Width ="130px" ID="lbAddSubmenu" class="btn btn-default" runat="server" Font-Size="Small"><span class="glyphicon glyphicon-plus" style="height:20px;"></span> Add Submenu</asp:LinkButton>
                    </div>
                </div>
            </div>
         </div>
         <div class="col-md-12">
            <div class="form-group">
                <label for="usr">Email From:</label>
                <asp:TextBox ID="tbEmailFrom" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="usr">Subject:</label>
                <asp:TextBox ID="tbSubject" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="usr">Description:</label><font color=red>*</font>
                <asp:TextBox ID="tbDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Height="125px"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:FileUpload ID="FileUpload1" runat="server" accept="image/jpeg,application/pdf,application/vnd.openxmlformats-officedocument.wordprocessingml.document,application/zip,application/rar,application/msword"/><div id="waitMessage" style="display: none; color:Red; font-size:medium;">Please wait...</div>
                <asp:Label ID="lbFile" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="Label1" runat="server" Text="Max Size 10MB(.docx,.jpeg,.rar,.pdf)"></asp:Label>
            </div>
         </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <p class="text-left">
         <asp:LinkButton ID="lbSave" CssClass ="btn btn-default" runat="server" Width="150px" OnClientClick="showWaitingMessage()" ><i class="fa fa-send"></i> Send E-Ticket</asp:LinkButton>
         <asp:LinkButton ID="lbDelete" CssClass ="btn btn-default" runat="server" Width="150px"><i class="fa fa-trash-o"></i> Delete E-Ticket</asp:LinkButton>
         <asp:LinkButton ID="lbCancel" CssClass ="btn btn-default" runat="server" Width="150px"><i class="fa fa-close"></i> Abort</asp:LinkButton>        
    </p>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>

