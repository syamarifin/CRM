<%@ Page Language="VB" MasterPageFile="~/iPxCrmUser/iPxMasterUserHome.master" AutoEventWireup="false" CodeFile="iPxCrmHome.aspx.vb" Inherits="iPxCrmUser_iPxCrmHome" title="Customer Relationship Management" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <asp:Timer ID="Timer1" runat="server" OnTick="TimerTick" Interval="60000" />
    </ContentTemplate>
</asp:UpdatePanel>
<%--<div class="row">--%>
        <div id ="new" class="col-lg-3 col-xs-6">
            <!-- small box -->
            <div class="small-box bg-aqua" style="background-color:#b21f1f !important;">
                <div class="inner">
                    <h3>New </h3>
                    <h3>E-Ticket &nbsp;</h3>
                    
                    <p>This Month :
                      <asp:Label ID="lbNewBlnIni" runat="server" Text="0"></asp:Label></p>
                    <p>Last Month :
                      <asp:Label ID="lbNewBlnLalu" runat="server" Text="0"></asp:Label></p>
                    <p>All  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;:
                      <asp:Label ID="lbNewTtl" runat="server" Text="0"></asp:Label></p>
                </div>
                <div class="icon">
                    <i class="fa fa-envelope-o"></i>
                </div>
                <div class="small-box-footer">
                    <asp:LinkButton Width="150px" ID="lbFooterNew" runat="server" ForeColor="White">
                    More info <i class="fa fa-arrow-circle-right"></i>
                    </asp:LinkButton>
                </div>
            </div>
        </div><!-- ./col -->
        <div id ="proses" class="col-lg-3 col-xs-6">
            <!-- small box -->
            <div class="small-box bg-aqua" style="background-color:#f39c12 !important;">
                <div class="inner">
                    <h3>E-Ticket</h3>
                    <h3>Process</h3>
                    <p>This Month :
                      <asp:Label ID="lbOutBlnIni" runat="server" Text="0"></asp:Label></p>
                    <p>Last Month :
                      <asp:Label ID="lbOutBlnLalu" runat="server" Text="0"></asp:Label></p>
                    <p>All  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;:
                      <asp:Label ID="lbOutTtl" runat="server" Text="0"></asp:Label></p>
                </div>
                <div class="icon" style="top: 10px;">
                    <i class="fa fa-recycle"></i>
                </div>
                <a href="iPxCrmAssign.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
            </div>
        </div><!-- ./col -->
        <div id ="done" class="col-lg-3 col-xs-6">
            <!-- small box -->
            <div class="small-box bg-aqua" style="background-color:#2d8659 !important;">
                <div class="inner">
                    <h3>E-Ticket  </h3>
                    <h3>Done/Approved</h3>
                    <p>This Month :
                      <asp:Label ID="lbDoneBlnIni" runat="server" Text="0"></asp:Label></p>
                    <p>Last Month :
                      <asp:Label ID="lbDoneBlnLalu" runat="server" Text="0"></asp:Label></p>
                    <p>All  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;:
                      <asp:Label ID="lbDoneTtl" runat="server" Text="0"></asp:Label></p>
                </div>
                <div class="icon" style="top: 10px;">
                    <i class="fa fa-thumbs-o-up"></i>
                </div>
                <a href="iPxCrmDone.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
            </div>
        </div><!-- ./col -->
        <div id="follow" class="col-lg-3 col-xs-6">
            <!-- small box -->
            <div class="small-box bg-aqua">
                <div class="inner">
                    <h3>E-Ticket</h3>
                    <h3>Follow Up &nbsp;</h3>
                    <p>This Month :
                      <asp:Label ID="llbFolBlnIni" runat="server" Text="0"></asp:Label></p>
                    <p>Last Month :
                      <asp:Label ID="lbFolBlnLalu" runat="server" Text="0"></asp:Label></p>
                    <p>All  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;:
                      <asp:Label ID="lbTotalFollow" runat="server" Text="0"></asp:Label></p>
                </div>
                <div class="icon">
                    <i class="fa fa-comments-o"></i>
                </div>
                <a href="iPxCrmSumarryTicket.aspx" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
            </div>
        </div><!-- ./col -->
<%--</div>--%>
<%--<asp:Image ID="Image1" runat="server" ImageUrl="~/assets/img/crm1.jpg" CssClass="img-responsive" Height="480px" style="Width:100%;"/>--%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>

