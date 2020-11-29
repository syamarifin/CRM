<%@ Page Language="VB" MasterPageFile="~/iPxCrmCustomer/iPxCrmCustomer.master" AutoEventWireup="false" CodeFile="iPxTicketCustomer.aspx.vb" Inherits="iPxCrmCustomer_iPxTicketCustomer" title="E-Ticket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <!-- Hapus Modal-->
      <div id="modalHapus" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-lg">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" data-dismiss="modal" aria-label="Close" class="close"><span aria-hidden="true">×</span></button>
              <h3 id="H1" class="modal-title">Autentifikasi</h3>
            </div>
            <div class="modal-body">
                <h4>sure you want to delete data</h4>
            </div>
            <div class="modal-footer">
                <p class="text-right">
                    <asp:LinkButton ID="lbDelete" CssClass ="btn btn-success" runat="server"><i class="fa fa-close"></i> Delete</asp:LinkButton>
                </p>
            </div>
          </div>
        </div>
      </div>
      <!-- Hapus modal end-->
         <div class="form-group left">
          <div class="form-group">
            <asp:LinkButton width=125px CssClass="btn btn-default dropdown-toggle" data- ID="btnQuery" runat="server"><span class="fa fa-filter "></span> Query</asp:LinkButton>
            <asp:LinkButton ID="lbAddTicket" CssClass ="btn btn-default" runat="server"><i class="fa fa-plus"></i> New Ticket</asp:LinkButton>
          </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <asp:Timer ID="Timer1" runat="server" OnTick="TimerTick" Interval="180000" />
        <asp:GridView HeaderStyle-BackColor="#0a818e" HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="true" EmptyDataText="No records has been added." ID="gvTicket" runat="server" CssClass="table" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" OnPageIndexChanging="OnPaging" PageSize="10" Width="100%" Font-Size="Smaller" GridLines="None">
            <Columns>
                <asp:BoundField ItemStyle-Width="80px" DataField="TicketNo" HeaderText="Ticket No" />
                <asp:BoundField ItemStyle-Width="90px" DataField="Tdate" DataFormatString="{0:dd/MM/yyyy hh:mm:ss}" HeaderText="Date" />
                <asp:BoundField ItemStyle-Width="80px" DataField="SubmitFrom" HeaderText="From" />
                <asp:BoundField ItemStyle-Width="100px" DataField="ProductName" HeaderText="Product" />
                <asp:BoundField ItemStyle-Width="100px" DataField="MenuName" HeaderText="Menu" />
                <asp:BoundField ItemStyle-Width="100px" DataField="SubmenuName" HeaderText="Sub Menu" />
                <asp:BoundField DataField="CaseDescription" HeaderText="Description" />
                <asp:BoundField ItemStyle-Width="100px" DataField="stsDescription" HeaderText="Status" />
                <asp:TemplateField ItemStyle-Width="50px" HeaderText="View" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton3" CssClass ='<%# If(Eval("coment").ToString() = "1", "btn btn-danger", "btn btn-default") %>' runat="server" CommandName="getViewid" CommandArgument='<%# Eval("TicketNo") %>'><i class="fa fa-eye"></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>               
                <asp:TemplateField ItemStyle-Width="50px" HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbEdit" CssClass ="btn btn-default" runat="server" CommandName="getTiketid" CommandArgument='<%# Eval("TicketNo") %>' Enabled='<%# If(Eval("stsDescription").ToString() = "CANCEL", "false", "True")  %>'><i class="fa fa-edit"></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>              
                <asp:TemplateField ItemStyle-Width="50px" HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" CssClass ="btn btn-default" runat="server" CommandName="getDeleteid" CommandArgument='<%# Eval("TicketNo") %>' Enabled='<%# If(Eval("stsDescription").ToString() <> "NEW", "false", "True")  %>'><i class="fa fa-trash-o"></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <pagerstyle cssclass="pagination-ys">
            </pagerstyle>
        </asp:GridView>
        </ContentTemplate> 
        </asp:UpdatePanel>
</asp:Content>
<%--<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>

