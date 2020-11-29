<%@ Page Language="VB" MasterPageFile="~/iPxCrmUser/iPxMasterUser.master" AutoEventWireup="false" CodeFile="iPxCrmDraftTicket.aspx.vb" Inherits="iPxCrmUser_iPxCrmDraftTicket" title="Draft E-Ticket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="lblTitleTicket" runat="server" Text="Draft" Font-Bold="true" Font-Size="Large"></asp:Label>
    <hr />
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
                        <asp:LinkButton ID="LinkButton3" CssClass ="btn btn-default" runat="server" CommandName="getViewid" CommandArgument='<%# Eval("TicketNo") %>' Enabled ='<%# If(Eval("stsDescription").ToString() = "NEW", "false", "true") %>'><i class="fa fa-eye"></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <pagerstyle cssclass="pagination-ys">
            </pagerstyle>
        </asp:GridView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:LinkButton ID="lbAbort" CssClass="btn btn-default" runat="server" Width="200px"><i class="fa fa-close"></i> Abort</asp:LinkButton>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>

