<%@ Page Language="VB" MasterPageFile="~/iPxCrmMobile/iPxMobileOprBck.master" AutoEventWireup="false" CodeFile="iPxTicketCustomer.aspx.vb" Inherits="iPxCrmMobile_iPxTicketCustomer" title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
       function confirmation() {
           if (confirm('are you sure you want to delete ?')) {
           return true;
           }else{
           return false;
           }
       }
   </script>
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <br /><br /><br /><br />
        <div class="col-sm-12" style="overflow:auto">
         <div class="form-group left">
          <div class="form-group">
            <asp:LinkButton width=100px CssClass="btn btn-default dropdown-toggle" data- ID="btnQuery" runat="server"><h4 style="margin:0;"><small style="color:White;"><span class="fa fa-filter "></span> Query</small></h4></asp:LinkButton>
            <asp:LinkButton ID="lbAddTicket" CssClass ="btn btn-default" runat="server"><h4 style="margin:0;"><small style="color:White;"><i class="fa fa-plus"></i> New Ticket</small></h4></asp:LinkButton>
          </div>
        </div>
        </div>
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>--%>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <asp:Timer ID="Timer1" runat="server" OnTick="TimerTick" Interval="180000" />
        <div class="row" style=" margin:0;">
          <div class="col-sm-12" style="padding:0; overflow-x:scroll;">
                <asp:GridView HeaderStyle-BackColor="#0a818e" HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="true" ShowHeader="false" ID="gvTicket" runat="server" CssClass="table" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" OnPageIndexChanging="OnPaging" PageSize="10" Width="100%" Font-Size="Smaller" GridLines="None">
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
                                            <p>Support by <b><%#If(Eval("stsDescription").ToString() = "NEW", " - ", Eval("supportBy").ToString())%></b></p>
                                            <hr style="margin:0; border-color:#25a79f; border-width:2px;" />
                                            <p><%#Eval("CaseDescription").ToString().Replace(vbLf, "<br />")%></p>
                                            <p style="text-align:right;"><%#Eval("Tdate", "{0:dd MMM yyyy hh:mm:ss}").ToString()%></p>
                                        </div>
                                        <div class="icon"  style="top: 10px;">
                                            <b><%#Eval("stsDescription").ToString()%></b>
                                        </div>
                                        <div class="small-box-footer">
                                            <asp:LinkButton ID="lbUnduh" runat="server" CssClass="btn btn-link" style="padding:0 6px;" ForeColor="White" CommandName="getDeleteid" CommandArgument='<%# Eval("TicketNo") %>' OnClientClick="return confirmation();" Visible='<%# If(Eval("stsDescription").ToString() <> "NEW", "false", "True")%>'><i class="fa fa-trash-o"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lbDetail" runat="server" CssClass="btn btn-link" style="padding:0 6px;" ForeColor="White" CommandName="getEditTiket" CommandArgument='<%# Eval("TicketNo") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lbAssign" runat="server" CssClass="btn btn-link" style='<%# "padding:0 6px; color:" + If(Eval("coment").ToString() = "1", "Red", "White") + ";" %>' CommandName="getViewid" CommandArgument='<%# Eval("TicketNo") %>'><i class="fa fa-eye"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div><!-- ./col -->
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField ItemStyle-Width="5px" HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbEdit" CssClass ="btn btn-default" runat="server" CommandName="getTiketid" CommandArgument='<%# Eval("TicketNo") %>' Enabled='<%# If(Eval("stsDescription").ToString() = "CANCEL", "false", "True")  %>'><i class="fa fa-edit"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>             
                        <asp:TemplateField ItemStyle-Width="50px" HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton2" CssClass ="btn btn-default" runat="server" CommandName="getDeleteid" CommandArgument='<%# Eval("TicketNo") %>' Enabled='<%# If(Eval("stsDescription").ToString() <> "NEW", "false", "True")  %>'><i class="fa fa-trash-o"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="5px" HeaderText="View" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton3" CssClass ='<%# If(Eval("coment").ToString() = "1", "btn btn-danger", "btn btn-default") %>' runat="server" CommandName="getViewid" CommandArgument='<%# Eval("TicketNo") %>'><i class="fa fa-eye"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField ItemStyle-Width="80px" DataField="TicketNo" HeaderText="Ticket No" />
                        <asp:BoundField ItemStyle-Width="90px" DataField="Tdate" DataFormatString="{0:dd/MM/yyyy hh:mm:ss}" HeaderText="Date" />
                        <asp:BoundField ItemStyle-Width="80px" DataField="SubmitFrom" HeaderText="From" />
                        <asp:BoundField ItemStyle-Width="100px" DataField="ProductName" HeaderText="Product" />
                        <asp:BoundField ItemStyle-Width="100px" DataField="MenuName" HeaderText="Menu" />
                        <asp:BoundField ItemStyle-Width="100px" DataField="SubmenuName" HeaderText="Sub Menu" />
                        <asp:BoundField DataField="CaseDescription" HeaderText="Description" />
                        <asp:BoundField ItemStyle-Width="100px" DataField="stsDescription" HeaderText="Status" />--%>
                    </Columns>
                <pagerstyle cssclass="pagination-ys">
                    </pagerstyle>
                </asp:GridView>
            </div>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>