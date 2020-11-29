<%@ Page Language="VB" MasterPageFile="~/iPxCrmCustomer/iPxCrmCustomerUpload.master" AutoEventWireup="false" CodeFile="iPxCrmViewFollow.aspx.vb" Inherits="iPxCrmCustomer_iPxCrmViewFollow" title="E-Ticket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
          font-size: 3.25em;
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
        .small-box {
            border-radius: 5px;
            position: relative;
            display: block;
            margin-bottom: 20px;
            box-shadow: 0 1px 1px rgba(0,0,0,0.1);
            color:White;
        }
        .bg-aqua {
            background-color: #36b3c1 !important;
        }
        .inner {
            padding: 10px;
        }
        .small-box h3 {
            font-size: 18px;
            font-weight: bold;
            margin: 0 0 10px 0;
            white-space: nowrap;
            padding: 0;
        }
        .small-box p {
            font-size: 15px;
        }
        .small-box .icon {
            -webkit-transition: all .3s linear;
            -o-transition: all .3s linear;
            transition: all .3s linear;
            position: absolute;
            top: -20px;
            right: 13px;
            z-index: 0;
            font-size: 85px;
            color: rgba(0,0,0,0.15);
        }
        .small-box-footer {
            position: relative;
            text-align: left;
            padding: 3px 10px;
            color: #fff;
            color: #000000;
            display: block;
            z-index: 10;
            background: #f8f8f8;
            text-decoration: none;
        }
        <%--=======================================================--%>
        .chat
        {
            list-style: none;
            margin: 0;
            padding: 0;
        }

        .chat li
        {
            margin-bottom: 10px;
            padding-bottom: 5px;
            border-bottom: 1px dotted #B3A9A9;
        }

        .chat li.left .chat-body
        {
            margin-left: 60px;
        }

        .chat li.right .chat-body
        {
            margin-right: 60px;
        }

        .chat li .chat-body p
        {
            margin: 0;
            color: #777777;
        }
        
        .panel .slidedown .glyphicon, .chat .glyphicon
        {
            margin-right: 5px;
        }
    </style>
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <!-- Approved Modal-->
      <div id="Modal-Approved" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-lg">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" data-dismiss="modal" aria-label="Close" class="close"><span aria-hidden="true">×</span></button>
              <h4 id="H1" class="modal-title">Customer Approved</h4>
            </div>
            <div class="modal-body">
                <h3><p align="center">please rate support</p></h3>
                <table style="width:auto">
                    <tr>
                        <td style="width:35%"></td>
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
                        <td style="width:35%"></td>
                    </tr>
                </table>
                <h3>Progres is Done, the E-Ticket will not be edited again?...</h3>
                <asp:HiddenField ID="nwLatHidden" runat="server" Value="" />
                <div id="ShowApproved" style="display: none;">
                    <p class="text-right">
                        <asp:LinkButton Width="150px" ID="LinkButton3" CssClass ="btn btn-default" runat="server"><i class="fa fa-save"></i> Done</asp:LinkButton>
                    </p>
                </div>
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
              <button type="button" data-dismiss="modal" aria-label="Close" class="close"><span aria-hidden="true">×</span></button>
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
                            <asp:TextBox ID="tbNote" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:FileUpload ID="FileUpload1" runat="server" accept="image/jpeg,application/pdf,application/vnd.openxmlformats-officedocument.wordprocessingml.document,application/zip,application/rar,application/msword"/><div id="waitMessage" style="display: none; color:Red; font-size:medium;">Please wait...</div>
                            <asp:Label ID="Label1" runat="server" Text="Max Size 10MB(.docx,.jpeg,.rar,.pdf)"></asp:Label>
                        </div>
                    </div>
                </div>
                <p class="text-right">
                    <asp:LinkButton Width="150px" ID="lbSave" CssClass ="btn btn-default" runat="server" OnClientClick="showWaitingMessage()" ><i class="fa fa-send"></i> Send</asp:LinkButton>
                    <asp:LinkButton Width="150px" ID="lbAbort1" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
                </p>
            </div>
          </div>
        </div>
      </div>
      <!-- Login modal end-->
        <%--<div class="row">--%>
        <div id ="new" class="col-lg-12 col-xs-12" style="padding:0px;">
                <!-- small box -->
                <div class="small-box bg-aqua">
                    <div class="inner">
                        <b><asp:Label ID="lbNamaHotel" runat="server" Text=""></asp:Label> <asp:Label ID="lbTicketNo" runat="server" Text=""></asp:Label></b> <br />
                        <asp:Label ID="lbTgl" runat="server" Text=""></asp:Label><br />
                        <asp:Label ID="lbProduct" runat="server" Text=""></asp:Label> -> <asp:Label ID="lbMenu" runat="server" Text=""></asp:Label> -> <asp:Label ID="lbSubMenu" runat="server" Text=""></asp:Label> <br />
                        Case : <asp:Label ID="lbCase" runat="server" Text="" Font-Bold="True"></asp:Label><br />
                        Support by : <asp:Label ID="lbSuportby" runat="server" Text=""></asp:Label>
                        <hr style ="margin-top :2px; margin-bottom :2px;">
                        <asp:Label ID="lbDescription" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="icon">
                        <asp:Label ID="lbStatus" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="small-box-footer">
                        <br />
                        <asp:Repeater ID="rptFollow" runat="server">
                            <HeaderTemplate>
                              <thead>
                                <table cellspacing="0" class="table">
                                    
                              </thead>      
                            </HeaderTemplate>
                            <ItemTemplate>
                              <tbody>
                                <tr bgcolor='<%# If(Eval("FollowUpCode").ToString() = "P", "#F8F8F8", "#ddd") %>'>
                                    <ul class="chat">
                                        <li class='<%# If(Eval("FollowUpCode").ToString() = "P", "right clearfix", "left clearfix") %>'><span class='<%# If(Eval("FollowUpCode").ToString() = "P", "chat-img pull-right", "chat-img pull-left") %>'>
                                            <img src='<%# If(Eval("FollowUpCode").ToString() = "H", "http://placehold.it/50/FA6F57/fff&text=H", "http://placehold.it/50/55C1E7/fff&text=P") %>' alt="User Avatar" class="img-circle" />
                                        </span>
                                            <div class="chat-body clearfix">
                                                <div class="header">
                                                    <strong class=<%# If(Eval("FollowUpCode").ToString() = "P", "pull-right primary-font", "primary-font") %>><%# Eval("FollowUpBy") %> (<%# Eval("FollowUpNo") %>)</strong> <small class='<%# If(Eval("FollowUpCode").ToString() = "P", " text-muted", "pull-right text-muted") %>'>
                                                    <span class="glyphicon glyphicon-time"></span><%# format( Eval("FollowUpDate"),"dd-MM-yyyy hh:mm:ss") %></small>
                                                </div>
                                                <p>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("FollowUpSopLink") %>' Visible="false"></asp:Label>
                                                    <%#Eval("FollowUpNote").ToString().Replace(vbLf, "<br />")%><br />
                                                    <a target="_blank" href='<%#If(Eval("FollowUpSopLink").ToString() <> "", Eval("FollowUpSopLink"), "")%>' class="text-primary">
                                                        <asp:Label visible='<%# If(Eval("FollowUpCode").ToString() = "P", "true", "false") %>' ID="Label2" runat="server" Text='<%#If(Eval("FollowUpSopLink").ToString() <> "", "Open Link <br />", "<br/>")%>'></asp:Label>
                                                    </a>
                                                    <asp:LinkButton ID="LinkButton4" CssClass="btn-link" runat="server" OnClick="GetValue"  visible='<%# If(Eval("FollowUpCode").ToString() = "H", "true", "false") %>'><%#If(Eval("FollowUpSopLink").ToString() <> "", "<i class='fa fa-paperclip'></i> Unduh File", "<br/>")%></asp:LinkButton>
                                                </p>
                                            </div>
                                        </li>
                                    </ul>
                                </tr>
                              </tbody>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <p class="text-center">
        <asp:LinkButton Width="150" ID="LinkButton2" CssClass ="btn btn-default" runat="server"><i class="fa fa-archive"></i> Approved</asp:LinkButton>
        <asp:LinkButton Width="150" ID="LinkButton1" CssClass ="btn btn-default" runat="server"><i class="fa fa-plus"></i> Comment</asp:LinkButton>
        <asp:LinkButton Width="150" ID="lbAbort" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
    </p>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>

