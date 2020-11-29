<%@ Page Language="VB" MasterPageFile="~/iPxCrmMobile/iPxMobileOpr.master" AutoEventWireup="false" CodeFile="iPxCrmHomeMobile.aspx.vb" Inherits="iPxCrmMobile_iPxCrmHomeMobile" title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        var myVar = setInterval (function(){ ShowCurrentTime() },1000);
        function ShowCurrentTime() {
            var CustID = '<%=Server.HtmlEncode(Session("sCId"))%>';
            $.ajax({
                type: "POST",
                url: "iPxCrmHomeMobile.aspx/newComent",
                data: '{CustID: "' + CustID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function(response) {
                    alert(response.d);
                }
            });
        }
        
        function OnSuccess(response) {
             data = JSON.parse(response.d);
             var unread = '';
             $(data).each(function (key, value) {
                    unread += value.unread;
             });
             $('#newComent').html(unread);

             if (unread == "0") {
                 $("#notif").hide();
                 $("#nonNotif").show();
             } else {
                 $("#notif").show();
                 $("#nonNotif").hide();
             }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br /><br />
      <div id="myCarousel" class="carousel slide" data-ride="carousel">
        <!-- Indicators -->
        <ol class="carousel-indicators">
          <li data-target="#myCarousel" data-slide-to="0"></li>
          <li data-target="#myCarousel" data-slide-to="1" class="active"></li>
          <li data-target="#myCarousel" data-slide-to="2"></li>
        </ol>

        <!-- Wrapper for slides -->
        <div class="carousel-inner">
          <div class="item">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/assets/img/crm2.jpg" CssClass="img-responsive" Height="20%" style="width : 100%;"/>
          </div>

          <div class="item active">
            <asp:Image ID="Image2" runat="server" ImageUrl="~/assets/img/bgvideo.jpg" CssClass="img-responsive" Height="20%" style="width : 100%;"/>
          </div>
        
          <div class="item">
            <asp:Image ID="Image3" runat="server" ImageUrl="~/assets/img/crm3.jpg" CssClass="img-responsive" Height="20%" style="width : 100%;"/>
          </div>
        </div>

        <!-- Left and right controls -->
        <a class="left carousel-control" href="#myCarousel" data-slide="prev">
          <span class="glyphicon glyphicon-chevron-left"></span>
          <span class="sr-only">Previous</span>
        </a>
        <a class="right carousel-control" href="#myCarousel" data-slide="next">
          <span class="glyphicon glyphicon-chevron-right"></span>
          <span class="sr-only">Next</span>
        </a>
      </div>
    <%--</div>--%>
    <br />
    <div class="container" align="center">
        <div class="panel panel-default" style="border-color:#25a79f;">
            <div class="panel-body">
                <asp:Label ID="Label5" runat="server" Text="ALCOR CRM" style="color:#25a79f; font-family: 'Times New Roman', Times, serif;" Font-Size="Larger"></asp:Label><br />
                <asp:Label ID="Label6" runat="server" Text="Customer Relationship Management" style="color:#25a79f;" Font-Size="Small" ></asp:Label>
            </div>
        </div>
        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-block" style="padding:0px 0px;">
        <div class="panel panel-default" style="border-color:#25a79f;">
            <div class="panel-body">
                <div id="notif">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="fa fa-envelope" style="font-size:48px; color:#25a79f;"></span>
                <span class="badge" id="newComent" style="top:-27px; left:-15px; background-color:#F44336;" ></span><br />
                </div>
                <div id="nonNotif">
                <span class="fa fa-envelope" style="font-size:48px; color:#25a79f;"></span>
                </div>
                <asp:Label ID="Label1" runat="server" Text="Support Help Desk" style="color:#25a79f; font-family: 'Times New Roman', Times, serif;" Font-Size="Medium"></asp:Label><br />
                <asp:Label ID="Label3" runat="server" Text="the solution to your application problem" style="color:#25a79f;" Font-Size="Small" ></asp:Label>
            </div>
        </div>
        </asp:LinkButton>
        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-block" style="padding:0px 0px;">
        <div class="panel panel-default" style="border-color:#25a79f;">
            <div class="panel-body">
                <span class="fa fa-user" style="font-size:48px; color:#25a79f;"></span><br />
                <asp:Label ID="Label2" runat="server" Text="Profile" style="color:#25a79f; font-family: 'Times New Roman', Times, serif;" Font-Size="Medium"></asp:Label><br />
                <asp:Label ID="Label4" runat="server" Text="view your profile" style="color:#25a79f;" Font-Size="Small" ></asp:Label>
            </div>
        </div>
        </asp:LinkButton>
    </div>
</asp:Content>

