<%@ Page Language="VB" MasterPageFile="~/iPxCrmMobileUser/iPxMobileOprBck.master" AutoEventWireup="false" CodeFile="iPxCrmDetailFollowUser.aspx.vb" Inherits="iPxCrmMobileUser_iPxCrmDetailFollowUser" title="Follow Up" %>

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
           background-image:url(../assets/img/background_Chat.jpeg);
        }
        .btnPadding
        {
        	padding:0;
        	}
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br /><br />
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
                                                        <p><%#Eval("TicketNo").ToString()%> 
                                                        <asp:LinkButton ID="lbEditAssigned" CssClass ="btn btnPadding" runat="server" CommandName="getEditTicket" CommandArgument='<%# Eval("TicketNo") %>'><i class="fa fa-pencil"></i></asp:LinkButton> </p>
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
                      <asp:LinkButton ID="lbUpStatus" CssClass="btn btn-link" runat="server" Font-Size="Small" style="padding:0 6px;"><i class="fa fa-cogs"></i></asp:LinkButton>
                      <asp:LinkButton ID="lbDetail" CssClass="btn btn-link" runat="server" Font-Size="Small" style="padding:0 6px;"><i class="fa fa-angle-double-up" style="height:20px; font-size:20px"></i></asp:LinkButton>
                      <asp:LinkButton ID="lbSend" class="btn btn-link" runat="server" Font-Size="Small" style="padding:0 6px;"><span class="fa fa-send"></span></asp:LinkButton>
                    </div>
                </asp:Panel>
            </div>
        </div>
</asp:Content>

