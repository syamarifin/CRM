<%@ Page Language="VB" MasterPageFile="~/iPxCrmMobile/iPxMobileUpload.master" AutoEventWireup="false" CodeFile="iPxCrmViewFollow.aspx.vb" Inherits="iPxCrmMobile_iPxCrmViewFollow" title="Untitled Page" %>
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
<script>
    function showWaitingMessage() {
        document.getElementById("waitMessage").style.display = "block";
    }
</script>
<script type="text/javascript">
    function rdiohalfClick() {
        document.getElementById("ShowApproved").style.display = "block";
        document.getElementById('<%=nwLatHidden.ClientID%>').value = '0,5';
    }
    function rdio1Click() {
        document.getElementById("ShowApproved").style.display = "block";
        document.getElementById('<%=nwLatHidden.ClientID%>').value = '1';
    }
    function rdio1halfClick() {
        document.getElementById("ShowApproved").style.display = "block";
        document.getElementById('<%=nwLatHidden.ClientID%>').value = '1,5';
    }
    function rdio2Click() {
        document.getElementById("ShowApproved").style.display = "block";
        document.getElementById('<%=nwLatHidden.ClientID%>').value = '2';
    }
    function rdio2halfClick() {
        document.getElementById("ShowApproved").style.display = "block";
        document.getElementById('<%=nwLatHidden.ClientID%>').value = '2,5';
    }
    function rdio3Click() {
        document.getElementById("ShowApproved").style.display = "block";
        document.getElementById('<%=nwLatHidden.ClientID%>').value = '3';
    }
    function rdio3halfClick() {
        document.getElementById("ShowApproved").style.display = "block";
        document.getElementById('<%=nwLatHidden.ClientID%>').value = '3,5';
    }
    function rdio4Click() {
        document.getElementById("ShowApproved").style.display = "block";
        document.getElementById('<%=nwLatHidden.ClientID%>').value = '4';
    }
    function rdio4halfClick() {
        document.getElementById("ShowApproved").style.display = "block";
        document.getElementById('<%=nwLatHidden.ClientID%>').value = '4,5';
    }
    function rdio5Click() {
        document.getElementById("ShowApproved").style.display = "block";
        document.getElementById('<%=nwLatHidden.ClientID%>').value = '5';
    }
</script>
<style type="text/css">
    fieldset, label { margin: 0; padding: 0; }
    body{ margin: 20px; }
    h1 { font-size: 1.5em; margin: 10px; }

    /****** Style Star Rating Widget *****/

    .rating { 
      border: none;
      float: left;
    }

    .rating > input { display: none; } 
    .rating > label:before { 
      margin: 5px;
      font-size: 1.75em;
      font-family: FontAwesome;
      display: inline-block;
      content: "\f005";
      text-align:center;
    }

    .rating > .half:before { 
      content: "\f089";
      position: absolute;
    }

    .rating > label { 
      color: #ddd; 
     float: right; 
    }

    /***** CSS Magic to Highlight Stars on Hover *****/

    .rating > input:checked ~ label, /* show gold star when clicked */
    .rating:not(:checked) > label:hover, /* hover current star */
    .rating:not(:checked) > label:hover ~ label { color: #FFD700;  } /* hover previous stars in list */

    .rating > input:checked + label:hover, /* hover current star when changing rating */
    .rating > input:checked ~ label:hover,
    .rating > label:hover ~ input:checked ~ label, /* lighten current selection */
    .rating > input:checked ~ label:hover ~ label { color: #FFED85;  } 
</style>
<style>
    #messages {
      overflow-y: auto;
    }
</style>
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
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
      <!-- Approved Modal-->
      <div id="Modal-Approved" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-md">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" data-dismiss="modal" aria-label="Close" class="close"><span aria-hidden="true">
                  ×</span></button>
              <h4 id="H1" class="modal-title">Customer Approved</h4>
            </div>
            <div class="modal-body">
            </div>
          </div>
        </div>
      </div>
      <!-- Approved modal end-->
      <!-- Login Modal-->
        <div id="login-modal" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
            <div role="document" class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" data-dismiss="modal" aria-label="Close" class="close"><span aria-hidden="true">
                            ×</span></button>
                        <h4 id="login-modalLabel" class="modal-title">Follow Up</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="usr">No :</label>
                                    <asp:TextBox ID="tbNo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="usr">By :</label><font color="red">*</font>
                                    <asp:TextBox ID="tbBy" runat="server" CssClass="form-control" ></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <label for="usr">Note :</label><font color="red">*</font>
                                    <%--<asp:TextBox ID="tbNote" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>--%>
                                </div>
                                <div class="form-group">
                                    <label for="usr">Attactment File :</label>
                                    <%--<asp:FileUpload ID="FileUpload1" runat="server" accept="image/jpeg,application/pdf,application/vnd.openxmlformats-officedocument.wordprocessingml.document,application/zip,application/rar,application/msword"/><div id="waitMessage" style="display: none; color:Red; font-size:medium;">Please wait...</div>--%>
                                    <asp:Label ID="Label1" runat="server" Text="Max Size 10MB(.docx,.jpeg,.rar,.pdf)"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <p class="text-right">
                            <asp:LinkButton ID="lbSave" CssClass ="btn btn-success" runat="server" OnClientClick="showWaitingMessage()" ><i class="fa fa-send"></i> Send</asp:LinkButton>
                        </p>
                    </div>
                </div>
            </div>
        </div>
      <!-- Login modal end-->
      <br /><br /><br /><br />
        
        <%--<div class="row" >
            <div class="col-sm-4">
                <p class="text-left">
                    <asp:LinkButton ID="LinkButton1" Visible="false" CssClass ="btn btn-default" runat="server"><i class="fa fa-plus"></i> Comment</asp:LinkButton>
                    <asp:LinkButton ID="LinkButton2" Visible="false" CssClass ="btn btn-default" runat="server"><i class="fa fa-low-vision"></i> Approved</asp:LinkButton>
                </p>
            </div>
        </div>--%>
        <div id="messages">
            <asp:GridView HeaderStyle-BackColor="#0a818e" HeaderStyle-ForeColor="White" ShowHeader="false" HeaderStyle-Font-Bold="true" Font-Size="X-Small" EmptyDataText="there is no follow up from support" ID="gvFollow" runat="server" AutoGenerateColumns="false" CssClass="table" GridLines="None">
                <Columns>
                    <asp:TemplateField HeaderText="Follow Up">
                        <ItemTemplate>
                            <div class='<%# If(Eval("FollowUpCode").ToString() = "P", "box3 sb14 darker", "box3 sb13") %>'>
                              <img src='<%# If(Eval("FollowUpCode").ToString() = "P", "../assets/images/icon/user.png", "../assets/images/icon/userwhite.png") %>' alt="Avatar" class='<%# If(Eval("FollowUpCode").ToString() = "P", "", "right") %>' style="width:70%;">
                              <%#Eval("FollowUpBy").ToString()%><br />
                              <p style='<%#"margin-bottom:0; color:" + If(Eval("FollowUpCode").ToString() = "P", "", "")+";" %>'><%#Eval("FollowUpNote").ToString().Replace(vbLf, "<br />")%></p>
                              <asp:Panel visible='<%# If(Eval("FollowUpCode").ToString() = "P", "true", "false") %>' ID="Panel1" runat="server">
                              <a target="_blank" href='<%#If(Eval("FollowUpSopLink").ToString() <> "", Eval("FollowUpSopLink"), "")%>' class="text-primary">
                                <asp:Label ID="Label2" runat="server" Text='<%#If(Eval("FollowUpSopLink").ToString() <> "", "Open Link <br />", "<br/>")%>'></asp:Label>
                              </a>
                              </asp:Panel>
                              <asp:LinkButton ID="LinkButton4" CssClass="btn-link" runat="server" CommandName="getUnduh" CommandArgument='<%# Eval("FollowUpSopLink") %>' visible='<%# If(Eval("FollowUpCode").ToString() = "H", "true", "false") %>'><%#If(Eval("FollowUpSopLink").ToString() <> "", "<i class='fa fa-paperclip'></i> Unduh File <br/>", "<br/>")%></asp:LinkButton>
                              <span class='<%# If(Eval("FollowUpCode").ToString() = "P", "time-right", "time-left") %>'> 
                                  <asp:Label ID="Label1" runat="server" Text='<%#Eval("FollowUpDate", "{0:d MMM yyyy HH:mm}")%>'></asp:Label>
                              </span>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                 </Columns>
            </asp:GridView>
        </div>
        <asp:Panel ID="Panel2" runat="server">
            <br /><br />
        </asp:Panel>
        <br /><br />
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
                                                        <h3><%#Eval("SubmitFrom").ToString()%></h3>
                                                        <p><%#Eval("TicketNo").ToString()%></p>
                                                        <p><%#Eval("ProductName").ToString()%> -> <%#Eval("MenuName").ToString()%> -> <%#Eval("SubmenuName").ToString()%></p>
                                                        <p>Subject <b><%#Eval("Subject").ToString()%></b></p>
                                                        <p><b><%#Eval("stsDescription").ToString()%></b> Support by <b><%#If(Eval("stsDescription").ToString() = "NEW", " - ", Eval("supportBy").ToString())%></b></p>
                                                        <hr style="margin:0; border-color:#25a79f; border-width:2px;" />
                                                        <p><%#Eval("CaseDescription").ToString().Replace(vbLf, "<br />")%></p>
                                                        <p style="text-align:right;"><%#Eval("Tdate", "{0:dd MMM yyyy hh:mm:ss}").ToString()%></p>
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
                    <div class="form-group" style="margin:6px;">
                        <asp:LinkButton ID="lbDown" CssClass="btn btn-link btn-block" runat="server" style="padding:0;"><i class="fa fa-angle-double-down" style="font-size:24px"></i></asp:LinkButton>
                    </div>
                </asp:Panel>
                <asp:Panel ID="Panel3" runat="server">
                    <div class="form-group" style="margin:6px;">
                        <asp:LinkButton ID="lbDetail" CssClass="btn btn-link btn-block" runat="server" style="padding:0;"><i class="fa fa-angle-double-up" style="font-size:24px"></i></asp:LinkButton>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnRating" runat="server" BackColor="White" Visible="false">
                    <h4><p align="center">please rate support</p></h4>
                    <table style="width:auto;">
                        <tr>
                            <td style="width:25%"></td>
                            <td align="center">
                                <fieldset class="rating">
                                    <input type="radio" id="star5" name="rating" value="5" onclick="rdio5Click()"/><label class = "full" for="star5" title="Awesome - 5 stars"></label>
                                    <input type="radio" id="star4half" name="rating" value="4 and a half" onclick="rdio4halfClick()"/><label class="half" for="star4half" title="Pretty good - 4.5 stars"></label>
                                    <input type="radio" id="star4" name="rating" value="4" onclick="rdio4Click()"/><label class = "full" for="star4" title="Pretty good - 4 stars"></label>
                                    <input type="radio" id="star3half" name="rating" value="3 and a half" onclick="rdio3halfClick()"/><label class="half" for="star3half" title="Meh - 3.5 stars"></label>
                                    <input type="radio" id="star3" name="rating" value="3" onclick="rdio3Click()"/><label class = "full" for="star3" title="Meh - 3 stars"></label>
                                    <input type="radio" id="star2half" name="rating" value="2 and a half" onclick="rdio2halfClick()"/><label class="half" for="star2half" title="Kinda bad - 2.5 stars"></label>
                                    <input type="radio" id="star2" name="rating" value="2" onclick="rdio2Click()"/><label class = "full" for="star2" title="Kinda bad - 2 stars"></label>
                                    <input type="radio" id="star1half" name="rating" value="1 and a half" onclick="rdio1halfClick()"/><label class="half" for="star1half" title="Meh - 1.5 stars"></label>
                                    <input type="radio" id="star1" name="rating" value="1" onclick="rdio1Click()"/><label class = "full" for="star1" title="Sucks big time - 1 star"></label>
                                    <input type="radio" id="starhalf" name="rating" value="half" onclick="rdiohalfClick()"/><label class="half" for="starhalf" title="Sucks big time - 0.5 stars"></label>
                                </fieldset>
                            </td>
                            <td style="width:19%"></td>
                        </tr>
                    </table>
                    <h6>Progres is Done, the E-Ticket will not be edited again?...</h6>
                    <asp:HiddenField ID="nwLatHidden" runat="server" Value="" />
                    <p class="text-right">
                        <asp:LinkButton ID="lbBackRating" cssClass ="btn btn-default" runat="server"><i class="	fa fa-arrow-left"></i> Back</asp:LinkButton>
                        <div id="ShowApproved" style="display: none;">
                            <asp:LinkButton ID="LinkButton3" cssClass ="btn btn-default" runat="server"><i class="fa fa-save"></i> Done</asp:LinkButton>
                        </div>
                    </p>
                </asp:Panel>
                <asp:Panel ID="pnLink" runat="server" Visible="false">
                    <div class="form-group" style="margin: 3px;">
                        <asp:FileUpload ID="FileUpload1" CssClass="btn btn-link" runat="server" accept="image/jpeg,application/pdf,application/vnd.openxmlformats-officedocument.wordprocessingml.document,application/zip,application/rar,application/msword"/><div id="waitMessage" style="display: none; color:Red; font-size:medium;">Please wait...</div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnInputText" runat="server">
                    <div class="input-group" style="margin:3px;">
                        <asp:TextBox ID="tbNote" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="1"></asp:TextBox>
                        <div class="input-group-btn">
                            <asp:LinkButton ID="lbLink" CssClass="btn btn-link" runat="server" Font-Size="Small" style="padding:0 6px;"><span class="glyphicon glyphicon-paperclip" style="height:20px;"></span></asp:LinkButton>
                            <asp:LinkButton ID="lbSend" class="btn btn-link" runat="server" Font-Size="Small" style="padding:0 6px;"><span class="fa fa-send" style="height:20px;"></span></asp:LinkButton>
                        </div>
                    </div>
                    <div class="form-group" style="margin:6px;">
                        <asp:LinkButton ID="LinkButton2" CssClass="btn btn-default btn-block" runat="server"><i class="fa fa-database"></i> Approved</asp:LinkButton>
                    </div>
                </asp:Panel>
            </div>
        </div>
</asp:Content>


