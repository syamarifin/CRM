<%@ Page Language="VB" MasterPageFile="~/iPxCrmMobileUser/iPxMobileOprBck.master" AutoEventWireup="false" CodeFile="iPxCrmDetailFollowDone.aspx.vb" Inherits="iPxCrmMobileUser_iPxCrmDetailFollowDone" title="Follow Up" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="../assets/js/jquery.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function() {
//        $('#messages').animate({
//            scrollTop: $('#messages')[0].scrollHeight
//        }, 1000);
        $("html, messages").animate({ scrollTop: $(document).height() }, 1000);
    });
</script>
    <style>
        .footer {
           position: fixed;
           left: 0;
           bottom: 0;
           width: 100%;
           background-color: #00000017;
           color: white;
           text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br /><br />
      <!-- Menu Modal-->
      <div id="formMenu-modal" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-lg">
          <div class="modal-content">
            <div class="modal-header">
              <h4 id="H2" class="modal-title">Detail</h4>
            </div>
            <div class="modal-body">
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
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                          <label for="usr">Product Name:</label>
                            <asp:TextBox ID="tbProduct" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="form-group">
                          <label for="usr">Menu:</label>
                            <asp:TextBox ID="tbMenu" runat="server" CssClass ="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="form-group">
                          <label for="usr">Sub Menu:</label>
                            <asp:TextBox ID="tbSubMenu" runat="server" CssClass = "form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group">
                          <label for="usr">From:</label>
                            <asp:TextBox ID="tbFrom" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="form-group">
                          <label for="usr">Description:</label>
                            <asp:TextBox ID="tbDescription" runat="server" CssClass="form-control" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                          <label for="usr">Case:</label>
                            <asp:DropDownList ID="dlCase" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                          <label for="usr">Assign To:</label>
                            <asp:DropDownList ID="dlAssign" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <p class="text-center">
                    <asp:LinkButton Width ="150px" ID="lbStart" CssClass ="btn btn-default" runat="server"><i class="fa fa-send"></i> Start Follow Up</asp:LinkButton>
                    <asp:LinkButton Width ="150px" ID="lbAbortStart" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
                </p>
            </div>
          </div>
        </div>
      </div>
      <!-- Menu modal end-->
        <!-- Login Modal-->
      <div id="login-modal" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-lg">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" data-dismiss="modal" aria-label="Close" class="close"><span aria-hidden="true">×</span></button>
              <h4 id="login-modalLabel" class="modal-title">Follow Up</h4>
            </div>
            <div class="modal-body">
                
            </div>
          </div>
        </div>
      </div>
      <!-- Login modal end-->
      <!-- Form Edit Status Modal-->
      <div id="formMenu-modal" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-sm">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" data-dismiss="modal" aria-label="Close" class="close"><span aria-hidden="true">×</span></button>
              <h4 id="H1" class="modal-title">Update Status E-Ticket</h4>
            </div>
            <div class="modal-body">
                        
                <div class="form-group center">
                  <div class="form-group">
                    <%--<asp:LinkButton width=125px CssClass="btn btn-default dropdown-toggle" data- ID="LinkButton2" runat="server"><span class="fa fa-filter "></span> Query</asp:LinkButton>--%>
                    <asp:LinkButton width="100px" ID="lbSaveupStatus" CssClass ="btn btn-default" runat="server"><i class="fa fa-save"></i> Save</asp:LinkButton>
                    <asp:LinkButton width="100px" ID="lbAbortupStatus" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
                  </div>
                </div>            
            </div>
          </div>
        </div>
      </div>
      <!-- Form Edit Status modal end-->
        <%--<div class="row">
          <div class="col-sm-4">--%>
        <div id="messages">
            <asp:GridView HeaderStyle-BackColor="#0a818e" HeaderStyle-ForeColor="White" ShowHeader="false" HeaderStyle-Font-Bold="true" Font-Size="X-Small" EmptyDataText="there is no follow up from support" ID="gvFollow" runat="server" AutoGenerateColumns="false" CssClass="table" GridLines="None">
                <Columns>
                    <asp:TemplateField HeaderText="Follow Up">
                        <ItemTemplate>
                            <div class='<%# If(Eval("FollowUpCode").ToString() = "H", "box3 sb14 darker", "box3 sb13") %>'>
                              <img src='<%# If(Eval("FollowUpCode").ToString() = "H", "../assets/images/icon/user.png", "../assets/images/icon/userwhite.png") %>' alt="Avatar" class='<%# If(Eval("FollowUpCode").ToString() = "H", "", "right") %>' style="width:70%;">
                              <%#Eval("FollowUpBy").ToString()%><br />
                              <p style='<%#"margin-bottom:0; color:" + If(Eval("FollowUpCode").ToString() = "H", "", "")+";" %>'><%#Eval("FollowUpNote").ToString().Replace(vbLf, "<br />")%></p>
                              <asp:Panel visible='<%# If(Eval("FollowUpCode").ToString() = "P", "true", "false") %>' ID="Panel1" runat="server">
                              <a target="_blank" href='<%#If(Eval("FollowUpSopLink").ToString() <> "", Eval("FollowUpSopLink"), "")%>' class="text-primary">
                                <asp:Label ID="Label2" runat="server" Text='<%#If(Eval("FollowUpSopLink").ToString() <> "", "Open Link <br />", "<br/>")%>'></asp:Label>
                              </a>
                              </asp:Panel>
                              <asp:LinkButton ID="LinkButton4" CssClass="btn-link" runat="server" CommandName="getUnduh" CommandArgument='<%# Eval("FollowUpSopLink") %>' visible='<%# If(Eval("FollowUpCode").ToString() = "H", "true", "false") %>'><%#If(Eval("FollowUpSopLink").ToString() <> "", "<i class='fa fa-paperclip'></i> Unduh File <br/>", "<br/>")%></asp:LinkButton>
                              <span class='<%# If(Eval("FollowUpCode").ToString() = "H", "time-right", "time-left") %>'> 
                                  <asp:Label ID="Label1" runat="server" Text='<%#Eval("FollowUpDate", "{0:d MMM yyyy HH:mm}")%>'></asp:Label>
                              </span>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                 </Columns>
            </asp:GridView>
        </div>
        <div class="footer">
            <div class="form-group" style="margin:3px;text-align: left;">
                <asp:Panel ID="pnHeader" Visible="false" runat="server" BackColor="White">
                    <div class="row" style="margin-left :0px;">
                      <div class="col-sm-12" style="overflow:auto; padding-left:0;">
                        <asp:GridView HeaderStyle-BackColor="#0a818e" HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="true" Font-Size="Smaller" ShowHeader="false" ID="gvTicket" runat="server" AutoGenerateColumns="false" CssClass="table" style="margin-bottom:0;" GridLines="None">
                            <Columns>
                                    <asp:TemplateField ItemStyle-Width="50px" HeaderText="">
                                        <ItemTemplate>
                                            <div id ="new" style="padding:0px;" class="col-lg-12 col-xs-12">
                                                <!-- small box -->
                                                <div class="small-box">
                                                    <div class="inner">
                                                        <h3><%#Eval("SubmitFrom").ToString()%> From <%#Eval("CustName").ToString()%></h3>
                                                        <p><%#Eval("TicketNo").ToString()%></p>
                                                        <p><%#Eval("ProductName").ToString()%> -> <%#Eval("MenuName").ToString()%> -> <%#Eval("SubmenuName").ToString()%></p>
                                                        <p>Support by <b><%#If(Eval("stsDescription").ToString() = "NEW", " - ", Eval("supportBy").ToString())%></b></p>
                                                        <hr style="margin:0; border-color:#25a79f; border-width:2px;" />
                                                        <p><%#Eval("CaseDescription").ToString().Replace(vbLf, "<br />")%></p>
                                                    </div>
                                                    <div class="icon">
                                                        <b><%#Eval("stsDescription").ToString()%></b> 
                                                    </div>
                                                </div>
                                            </div><!-- ./col -->
                                        </ItemTemplate>
                                    </asp:TemplateField>
                             </Columns>
                             <pagerstyle cssclass="pagination-ys">
                             </pagerstyle>
                        </asp:GridView>
                      </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="Panel3" runat="server">
                    <div class="form-group" style="margin:6px;">
                        
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnStatus" runat="server" Visible="false" BackColor="White"> 
                    <div class="form-group">
                        <label for="usr" style="color:Black;">Status:</label>
                        <asp:DropDownList ID="dlUpStatus" CssClass="form-control" runat="server">
                        </asp:DropDownList>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnInputText" runat="server">
                    <div class="btn-group btn-group-justified">
                      <asp:LinkButton ID="lbDetail" CssClass="btn btn-link" runat="server" Font-Size="Small" style="padding:0 6px;"><i class="fa fa-angle-double-up" style="height:20px; font-size:20px"></i></asp:LinkButton>
                    </div>
                    <%--<div class="input-group" style="margin:3px;">
                        <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="1"></asp:TextBox>
                        <div class="input-group-btn">
                            <asp:LinkButton ID="LinkButton1" CssClass="btn btn-link" runat="server" Font-Size="Small" style="padding:0 6px;"><span class="glyphicon glyphicon-paperclip" style="height:20px;"></span></asp:LinkButton>
                            
                        </div>
                    </div>
                    <div class="form-group" style="margin:4px;">
                        
                    </div>--%>
                </asp:Panel>
            </div>
        </div>
</asp:Content>

