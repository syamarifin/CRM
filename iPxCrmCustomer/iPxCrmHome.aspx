<%@ Page Language="VB" MasterPageFile="~/iPxCrmCustomer/iPxCrmCustomer.master" AutoEventWireup="false" CodeFile="iPxCrmHome.aspx.vb" Inherits="iPxCrmCustomer_iPxCrmHome" title="E-Ticket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/assets/img/crm1.jpg" CssClass="img-responsive" Height="480px" Width="1100px"/>--%>
      <div class="col-md-9" style="padding-right :5px; padding-left :5px; ">
      <div id="myCarousel" class="carousel slide" data-ride="carousel">
        <!-- Indicators -->
        <ol class="carousel-indicators">
          <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
          <li data-target="#myCarousel" data-slide-to="1"></li>
          <li data-target="#myCarousel" data-slide-to="2"></li>
        </ol>

        <!-- Wrapper for slides -->
        <div class="carousel-inner">
          <div class="item active">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/assets/img/crm1.jpg" CssClass="img-responsive" Height="557px" style="width:100%;"/>
          </div>

          <div class="item">
            <asp:Image ID="Image2" runat="server" ImageUrl="~/assets/img/bgvideo.jpg" CssClass="img-responsive" Height="557px" style="width:100%;"/>
          </div>
        
          <div class="item">
            <asp:Image ID="Image3" runat="server" ImageUrl="~/assets/img/crm3.jpg" CssClass="img-responsive" Height="557px" style="width:100%;"/>
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
      </div>
        <div class="col-lg-3 col-xs-6" style="padding-right :5px; padding-left :5px;">
            <!-- small box -->
            <div class="small-box bg-aqua">
                <div class="inner">
                    <h3><asp:Label ID="lbNewTtl" runat="server" Text="0"></asp:Label></h3>
                    
                    <p>New E-Ticket</p>
                </div>
                <div class="icon">
                    <i class="fa fa-envelope-o"></i>
                </div>
                <div class="small-box-footer">&nbsp;</div>
            </div>
        </div><!-- ./col -->
        <div class="col-lg-3 col-xs-6" style="padding-right :5px; padding-left :5px;">
            <!-- small box -->
            <div class="small-box bg-aqua">
                <div class="inner">
                    <h3><asp:Label ID="lbOutTtl" runat="server" Text="0"></asp:Label></h3>
                    
                    <p>E-Ticket Process</p>
                </div>
                <div class="icon">
                    <i class="fa fa-recycle"></i>
                </div>
                <div class="small-box-footer">&nbsp;</div>
            </div>
        </div><!-- ./col -->
        <div class="col-lg-3 col-xs-6" style="padding-right :5px; padding-left :5px;">
            <!-- small box -->
            <div class="small-box bg-aqua">
                <div class="inner">
                    <h3><asp:Label ID="lbDoneTtl" runat="server" Text="0"></asp:Label></h3>
                    
                    <p>E-Ticket Done/Approved</p>
                </div>
                <div class="icon">
                    <i class="fa fa-thumbs-o-up"></i>
                </div>
                <div class="small-box-footer">&nbsp;</div>
            </div>
        </div><!-- ./col -->
        <div class="col-lg-3 col-xs-6" style="padding-right :5px; padding-left :5px;">
            <!-- small box -->
            <div class="small-box bg-aqua">
                <div class="inner">
                    <h3><asp:Label ID="lbTotalFollow" runat="server" Text="0"></asp:Label></h3>
                    
                    <p>E-Ticket Follow Up</p>
                </div>
                <div class="icon">
                    <i class="fa fa-comments-o"></i>
                </div>
                <div class="small-box-footer">&nbsp;</div>
            </div>
        </div><!-- ./col -->
</asp:Content>
<%--<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>

