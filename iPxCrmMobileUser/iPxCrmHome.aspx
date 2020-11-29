<%@ Page Language="VB" MasterPageFile="~/iPxCrmMobileUser/iPxMobileOpr.master" AutoEventWireup="false" CodeFile="iPxCrmHome.aspx.vb" Inherits="iPxCrmMobileUser_iPxCrmHome" title="Customer Relationship Management" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function hideNotifNewTiket() {
            $("#newTiket").hide();
        }
        function hideNotifFollow() {
            $("#followTiket").hide();
        }
        function hideNotifDone() {
            $("#doneTiket").hide();
        }
    </script>
    <style>
        .small-box {
            border-radius: 5px;
            position: relative;
            display: block;
            margin-bottom: 20px;
            box-shadow: 0 1px 1px rgba(0,0,0,0.1);
            color:#25a79f;
            border-style: solid;
            border-color:#25a79f;
            border-width:1px;
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
            color:#25a79f;
        }
        .small-box .icon {
            -webkit-transition: all .3s linear;
            -o-transition: all .3s linear;
            transition: all .3s linear;
            position: absolute;
            top: 0px;
            right: 13px;
            z-index: 0;
            font-size: 90px;
            color: #25a79f;
        }
        .small-box .notif {
            -webkit-transition: all .3s linear;
            -o-transition: all .3s linear;
            transition: all .3s linear;
            position: absolute;
            top: 28px;
            right: 89px;
            z-index: 0;
            font-size: 20px;
        }
        .small-box-footer {
            position: relative;
            text-align: center;
            padding: 3px 0;
            color: #fff;
            color: rgba(255,255,255,0.8);
            display: block;
            z-index: 10;
            background: #25a79f;
            text-decoration: none;
        }
    </style>
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<br /><br /><br /><br />
<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <asp:Timer ID="Timer1" runat="server" OnTick="TimerTick" Interval="60000" />
    </ContentTemplate>
</asp:UpdatePanel>--%>
<%--<div class="row">--%>
    <div class="container" align="center">
        <div class="panel panel-default" style="border-color:#25a79f; border-width :1px;">
            <div class="panel-body">
                <asp:Label ID="Label5" runat="server" Text="SUPPORT HELP DESK" style="color:#25a79f; font-family: 'Times New Roman', Times, serif;" Font-Size="Larger"></asp:Label><br />
                <asp:Label ID="Label6" runat="server" Text="the solution to your application problem" style="color:#25a79f;" Font-Size="Small" ></asp:Label>
            </div>
        </div>
    </div>
        <div id ="new" class="col-lg-12 col-xs-12">
            <!-- small box -->
            <div class="small-box">
                <div class="inner">
                    <h3>New E-Ticket</h3>
                    <p>This Month &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;:
                      <asp:Label ID="lbNewBlnIni" runat="server" Text="0" ></asp:Label></p>
                    <p>Last Month &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;:
                      <asp:Label ID="lbNewBlnLalu" runat="server" Text="0"></asp:Label></p>
                    <p>All  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;:
                      <asp:Label ID="lbNewTtl" runat="server" Text="0"></asp:Label></p>
                </div>
                <div class="icon" style="top: 10px;">
                    <i class="fa fa-envelope-o"></i>
                </div>
                <div class="small-box-footer">
                    <asp:LinkButton Width="150px" ID="lbFooterNew" runat="server" ForeColor="White">
                    More info <i class="fa fa-arrow-circle-right"></i>
                    </asp:LinkButton>
                </div>
            </div>
        </div><!-- ./col -->
        <div id ="proses" class="col-lg-12 col-xs-12">
            <!-- small box -->
            <div class="small-box">
                <div class="inner">
                    <h3>E-Ticket Process</h3>
                    <p>This Month &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;:
                      <asp:Label ID="lbOutBlnIni" runat="server" Text="0"></asp:Label></p>
                    <p>Last Month &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;:
                      <asp:Label ID="lbOutBlnLalu" runat="server" Text="0"></asp:Label></p>
                    <p>All  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;:
                      <asp:Label ID="lbOutTtl" runat="server" Text="0"></asp:Label></p>
                </div>
                <div class="icon" style="top: 10px;">
                    <i class="fa fa-recycle"></i>
                </div>
                <a href="iPxCrmAssign.aspx" class="small-box-footer" >More info <i class="fa fa-arrow-circle-right"></i></a>
            </div>
        </div><!-- ./col -->
        <div id ="done" class="col-lg-12 col-xs-12">
            <!-- small box -->
            <div class="small-box">
                <div class="inner">
                    <h3>Customer Approved</h3>
                    <p>This Month &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;:
                      <asp:Label ID="lbDoneBlnIni" runat="server" Text="0"></asp:Label></p>
                    <p>Last Month &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;:
                      <asp:Label ID="lbDoneBlnLalu" runat="server" Text="0"></asp:Label></p>
                    <p>All  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;:
                      <asp:Label ID="lbDoneTtl" runat="server" Text="0"></asp:Label></p>
                </div>
                <div class="icon" style="top: 10px;">
                    <i class="fa fa-thumbs-o-up"></i>
                </div>
                <a href="iPxCrmDone.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
            </div>
        </div><!-- ./col -->
        <div id="follow" class="col-lg-12 col-xs-12">
            <!-- small box -->
            <div class="small-box">
                <div class="inner">
                    <h3>E-Ticket Follow Up</h3>
                    <p>This Month &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;:
                      <asp:Label ID="llbFolBlnIni" runat="server" Text="0"></asp:Label></p>
                    <p>Tast Month &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;:
                      <asp:Label ID="lbFolBlnLalu" runat="server" Text="0"></asp:Label></p>
                    <p>All  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;:
                      <asp:Label ID="lbTotalFollow" runat="server" Text="0"></asp:Label></p>
                </div>
                <div class="icon">
                    <i class="fa fa-comments-o"></i>
                </div>
                <div class="small-box-footer">&nbsp;</div>
            </div>
        </div><!-- ./col -->
<%--</div>--%>
<%--<asp:Image ID="Image1" runat="server" ImageUrl="~/assets/img/crm1.jpg" CssClass="img-responsive" Height="480px" style="Width:100%;"/>--%>
</asp:Content>

