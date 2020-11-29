<%@ Page Language="VB" MasterPageFile="~/iPxCrmUser/iPxMasterUser.master" AutoEventWireup="false" CodeFile="iPxCrmProductGrp.aspx.vb" Inherits="iPxCrmUser_iPxCrmProductGrp" title="Product Group" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Label ID="lblTitleTicket" runat="server" Text="Data Product Group" Font-Bold="true" Font-Size="Large"></asp:Label>
<div class="row">
    <div class="col-sm-8">
        <br />
        <asp:GridView EmptyDataText="No records has been added." ID="gvProduct" runat="server" AutoGenerateColumns="false" CssClass="table" HeaderStyle-BackColor="#0a818e" HeaderStyle-ForeColor="White" Font-Size="Smaller" GridLines="None"  AllowPaging="true" PageSize="10">
            <Columns>
                <asp:BoundField ItemStyle-Width="115px" DataField="ProductGrp" HeaderText="Product Group ID" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="PrdDescription" HeaderText="Product Group Name" />
                <asp:TemplateField ItemStyle-Width="50px" HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" CssClass="btn btn-default" runat="server" CommandName="getEdit" CommandArgument='<%# Eval("ProductGrp") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="50px" HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbEdit" CssClass="btn btn-default" runat="server" CommandName="getHapus" CommandArgument='<%# Eval("ProductGrp") %>'><i class="fa fa-trash-o"></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
             </Columns>
             <pagerstyle cssclass="pagination-ys">
             </pagerstyle>
        </asp:GridView>
    </div>
    <div class="col-sm-4">
        <div class="form-group">
            <label for="usr">Product Group ID:</label><font color=red>*</font>
            <asp:TextBox ID="tbPrdID" runat="server" CssClass ="form-control" onkeypress="return hanyaHuruf(event)"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="usr">Product Group Name:</label><font color=red>*</font>
            <asp:TextBox ID="tbPrdName" runat="server" CssClass ="form-control" onkeypress="return hanyaHuruf(event)"></asp:TextBox>
        </div>
        <p class="text-center">
            <asp:LinkButton ID="lbSave" CssClass ="btn btn-default" runat="server" Width="150px"><i class="fa fa-save"></i> Save Product</asp:LinkButton>
            <asp:LinkButton ID="lbCancel" CssClass ="btn btn-default" runat="server" Width="150px"><i class="fa fa-close"></i> Abort</asp:LinkButton>
        </p>
    </div>
</div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>

