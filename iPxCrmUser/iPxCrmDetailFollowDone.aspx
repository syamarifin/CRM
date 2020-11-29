<%@ Page Language="VB" MasterPageFile="~/iPxCrmUser/iPxMasterUserUpload.master" AutoEventWireup="false" CodeFile="iPxCrmDetailFollowDone.aspx.vb" Inherits="iPxCrmUser_iPxCrmDetailFollowDone" title="Follow Up" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function() {
            if (document.getElementById('<%=nwLatHidden.ClientID%>').value == '0,5') {
                radiobtn = document.getElementById('starhalf');
                radiobtn.checked = true;
            } else if (document.getElementById('<%=nwLatHidden.ClientID%>').value == '1') {
                radiobtn = document.getElementById('star1');
                radiobtn.checked = true;
            } else if (document.getElementById('<%=nwLatHidden.ClientID%>').value == '1,5') {
                radiobtn = document.getElementById('star1half');
                radiobtn.checked = true;
            } else if (document.getElementById('<%=nwLatHidden.ClientID%>').value == '2') {
                radiobtn = document.getElementById('star2');
                radiobtn.checked = true;
            } else if (document.getElementById('<%=nwLatHidden.ClientID%>').value == '2,5') {
                radiobtn = document.getElementById('star2half');
                radiobtn.checked = true;
            } else if (document.getElementById('<%=nwLatHidden.ClientID%>').value == '3') {
                radiobtn = document.getElementById('star3');
                radiobtn.checked = true;
            } else if (document.getElementById('<%=nwLatHidden.ClientID%>').value == '3,5') {
                radiobtn = document.getElementById('star3half');
                radiobtn.checked = true;
            } else if (document.getElementById('<%=nwLatHidden.ClientID%>').value == '4') {
                radiobtn = document.getElementById('star4');
                radiobtn.checked = true;
            } else if (document.getElementById('<%=nwLatHidden.ClientID%>').value == '4,5') {
                radiobtn = document.getElementById('star4half');
                radiobtn.checked = true;
            } else if (document.getElementById('<%=nwLatHidden.ClientID%>').value == '5') {
                radiobtn = document.getElementById('star5');
                radiobtn.checked = true;
            } else { 
                
            }
        });
    </script>
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
        .small-box .iconReting {
            -webkit-transition: all .3s linear;
            -o-transition: all .3s linear;
            transition: all .3s linear;
            position: absolute;
            top: 106px;
            right: 13px;
            z-index: 0;
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
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <%--<div class="row">
          <div class="col-sm-4">--%>
            <div id ="new" class="col-lg-12 col-xs-12" style="padding:0px;">
                <!-- small box -->
                <div class="small-box bg-aqua">
                    <div class="inner">
                        <b><asp:Label ID="lbNamaHotel" runat="server" Text=""></asp:Label> (<asp:Label ID="lbTicketNo" runat="server" Text=""></asp:Label>)</b> <br />
                        <asp:Label ID="lbTgl" runat="server" Text=""></asp:Label><br />
                        <asp:Label ID="lbProduct" runat="server" Text=""></asp:Label> -> <asp:Label ID="lbMenu" runat="server" Text=""></asp:Label> -> <asp:Label ID="lbSubMenu" runat="server" Text=""></asp:Label> <asp:Label ID="lbLink" runat="server" Text=""></asp:Label><br />
                        Case : <asp:Label ID="lbCase" runat="server" Text="" Font-Bold="True"></asp:Label><br />
                        Support by : <asp:Label ID="lbSuportby" runat="server" Text=""></asp:Label>
                        <hr style ="margin-top :2px; margin-bottom :2px;">
                        Subject : <asp:Label ID="lbSubject" runat="server" Text="" Font-Bold="True"></asp:Label><br />
                        <asp:Label ID="lbDescription" runat="server" Text=""></asp:Label>
                                    
                    </div>
                    <div class="icon">
                        <asp:Label ID="lbStatus" runat="server" Text=""></asp:Label>
                    </div>
                    <div id="Reting" class="iconReting">
                        <fieldset class="rating">
                            <input type="radio" disabled ="disabled" id="star5" name="rating" value="5"/><label class = "full" for="star5" title="Awesome - 5 stars"></label>
                            <input type="radio" disabled ="disabled" id="star4half" name="rating" value="4 and a half"/><label class="half" for="star4half" title="Pretty good - 4.5 stars"></label>
                            <input type="radio" disabled ="disabled" id="star4" name="rating" value="4"/><label class = "full" for="star4" title="Pretty good - 4 stars"></label>
                            <input type="radio" disabled ="disabled" id="star3half" name="rating" value="3 and a half"/><label class="half" for="star3half" title="Meh - 3.5 stars"></label>
                            <input type="radio" disabled ="disabled" id="star3" name="rating" value="3"/><label class = "full" for="star3" title="Meh - 3 stars"></label>
                            <input type="radio" disabled ="disabled" id="star2half" name="rating" value="2 and a half"/><label class="half" for="star2half" title="Kinda bad - 2.5 stars"></label>
                            <input type="radio" disabled ="disabled" id="star2" name="rating" value="2"/><label class = "full" for="star2" title="Kinda bad - 2 stars"></label>
                            <input type="radio" disabled ="disabled" id="star1half" name="rating" value="1 and a half"/><label class="half" for="star1half" title="Meh - 1.5 stars"></label>
                            <input type="radio" disabled ="disabled" id="star1" name="rating" value="1"/><label class = "full" for="star1" title="Sucks big time - 1 star"></label>
                            <input type="radio" disabled ="disabled" id="starhalf" name="rating" value="half"/><label class="half" for="starhalf" title="Sucks big time - 0.5 stars"></label>
                        </fieldset>
                        <asp:HiddenField ID="nwLatHidden" runat="server" Value="" />
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
                                <tr bgcolor='<%# If(Eval("FollowUpCode").ToString() = "H", "#F8F8F8", "#ddd") %>'>
                                    <ul class="chat">
                                        <li class='<%# If(Eval("FollowUpCode").ToString() = "H", "right clearfix", "left clearfix") %>'><span class='<%# If(Eval("FollowUpCode").ToString() = "H", "chat-img pull-right", "chat-img pull-left") %>'>
                                            <img src='<%# If(Eval("FollowUpCode").ToString() = "H", "http://placehold.it/50/FA6F57/fff&text=H", "http://placehold.it/50/55C1E7/fff&text=P") %>' alt="User Avatar" class="img-circle" />
                                        </span>
                                            <div class="chat-body clearfix">
                                                <div class="header">
                                                    <strong class=<%# If(Eval("FollowUpCode").ToString() = "H", "pull-right primary-font", "primary-font") %>><%# Eval("FollowUpBy") %> (<%# Eval("FollowUpNo") %>)</strong> <small class='<%# If(Eval("FollowUpCode").ToString() = "H", " text-muted", "pull-right text-muted") %>'>
                                                    <span class="glyphicon glyphicon-time"></span><%# format( Eval("FollowUpDate"),"dd-MM-yyyy hh:mm:ss") %></small>
                                                </div>
                                                <p>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("FollowUpSopLink") %>' Visible="false"></asp:Label>
                                                    <%# Eval("FollowUpNote").ToString().Replace(vbLf, "<br />") %><br />
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
    <p class="text-left">
        <asp:LinkButton Width="150px" ID="lbAbort" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
    </p>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>

