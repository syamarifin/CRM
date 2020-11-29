<%@ Page Language="VB" MasterPageFile="~/iPxCrmCustomer/iPxCrmCustomer.master" AutoEventWireup="false" CodeFile="iPxCrmTicketProses.aspx.vb" Inherits="iPxCrmCustomer_iPxCrmTicketProses" title="E-Ticket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="row">
          <div class="col-sm-4">
           <div class="input-group">
              <span class="input-group-addon"><i class="glyphicon glyphicon-search"></i></span>
              <asp:TextBox ID="tbSearch" runat="server" CssClass="form-control" OnTextChanged="cari" AutoPostBack="true" placeholder="Search..."></asp:TextBox>
           </div>
          </div>
        </div><br />
    <asp:GridView EmptyDataText="No records has been added." ID="gvTicket" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" OnPageIndexChanging="OnPaging" PageSize="10" Width="100%">
            <Columns>
                <asp:BoundField ItemStyle-Width="100px" DataField="TicketNo" HeaderText="Ticket No" />
                <asp:BoundField ItemStyle-Width="150px" DataField="Tdate" HeaderText="Date" />
                <asp:BoundField ItemStyle-Width="150px" DataField="SubmitFrom" HeaderText="From" />
                <asp:BoundField ItemStyle-Width="150px" DataField="ProductName" HeaderText="Product" />
                <asp:BoundField ItemStyle-Width="150px" DataField="MenuName" HeaderText="Menu" />
                <asp:BoundField ItemStyle-Width="150px" DataField="SubmenuName" HeaderText="Sub Menu" />
            </Columns>
        <pagerstyle cssclass="pagination-ys">
            </pagerstyle>
    </asp:GridView>
</asp:Content>
<%--<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>

