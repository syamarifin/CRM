<%@ Page Language="VB" MasterPageFile="~/iPxCrmUser/iPxMasterUser.master" AutoEventWireup="false" CodeFile="iPxCrmCustGrp.aspx.vb" Inherits="iPxCrmUser_iPxCrmCustGrp" title="Customer Group" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <!-- Hapus Modal-->
      <div id="modalHapus" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-md">
          <div class="modal-content">
            <div class="modal-header">
              <%--<button type="button" data-dismiss="modal" aria-label="Close" class="close"><span aria-hidden="true">×</span></button>--%>
              <h3 id="H3" class="modal-title">Autentifikasi</h3>
            </div>
            <div class="modal-body">
                <h4>sure you want to delete data</h4>
            </div>
            <div class="modal-footer">
                <p class="text-right">
                    <asp:LinkButton Width="150px" ID="lbDelete" CssClass ="btn btn-default" runat="server"><i class="fa fa-trash"></i> Delete</asp:LinkButton>
                    <asp:LinkButton Width="150px" ID="lbAbortDelete" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
                </p>
            </div>
          </div>
        </div>
      </div>
      <!-- Hapus modal end-->
      <!-- Form Menu Modal-->
      <div id="formMenu-modal" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-sm">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" data-dismiss="modal" aria-label="Close" class="close"><span aria-hidden="true">×</span></button>
              <h4 id="H2" class="modal-title">Query Customer Group</h4>
            </div>
            <div class="modal-body">
                <div class="form-group">
                    <label for="usr">Group Name:</label>
                    <asp:TextBox ID="tbQueryGrp" runat="server" CssClass ="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="usr">Status:</label>
                    <asp:DropDownList ID="dlQueryStatus" runat="server" CssClass ="form-control">
                        <asp:ListItem Text="" Value="01"></asp:ListItem>
                        <asp:ListItem Text="All Customer Group" Value="02"></asp:ListItem>
                        <asp:ListItem Text="Non Customer Group" Value="03"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group center">
                  <div class="form-group">
                    <%--<asp:LinkButton width=125px CssClass="btn btn-default dropdown-toggle" data- ID="LinkButton2" runat="server"><span class="fa fa-filter "></span> Query</asp:LinkButton>--%>
                    <asp:LinkButton width="100px" ID="lbQuery" CssClass ="btn btn-default" runat="server"><i class="fa fa-filter"></i> Query</asp:LinkButton>
                    <asp:LinkButton width="100px" ID="lbAbortQuery" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
                  </div>
                </div>            
            </div>
          </div>
        </div>
      </div>
      <!-- Form Menu modal end-->
      <!-- Form Input Modal-->
      <div id="formsub-modal" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-sm">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" data-dismiss="modal" aria-label="Close" class="close"><span aria-hidden="true">×</span></button>
              <h3 id="H1" class="modal-title">Form Cust Group</h3>
            </div>
            <div class="modal-body">
                <div class="form-group">
                    <label for="usr">Group Name:</label><font color=red>*</font>
                    <asp:TextBox ID="tbCustgrp" runat="server" CssClass ="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:CheckBox ID="cbActive" runat="server" /><label for="usr"> Active</label>
                </div>
            </div>
            <div class="modal-footer">
                <p class="text-center">
                    <asp:LinkButton ID="lbSaveGrup" CssClass ="btn btn-default" runat="server" Width="110px"><i class="fa fa-save" ></i> Save Grup</asp:LinkButton>
                    <asp:LinkButton Width="110px" ID="lbAbort" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
                </p>
            </div>
          </div>
        </div>
      </div>
      <!-- Form Input Modal end-->
    <asp:Label ID="lblTitleTicket" runat="server" Text="Customer Group" Font-Bold="true" Font-Size="Large"></asp:Label>
    <hr />
    <div class="form-group left">
      <div class="form-group">
        <asp:LinkButton width=150px CssClass="btn btn-default dropdown-toggle" data- ID="btnQuery" runat="server"><span class="fa fa-filter "></span> Query</asp:LinkButton>
        <asp:LinkButton width=150px ID="lbNew" CssClass ="btn btn-default" runat="server"><i class="fa fa-plus"></i> New Group</asp:LinkButton>
      </div>
    </div>
    <asp:GridView EmptyDataText="No records has been added." ID="gvGrup" runat="server" Width="600px" AutoGenerateColumns="false" CssClass="table" HeaderStyle-BackColor="#0a818e" HeaderStyle-ForeColor="White" Font-Size="Smaller" GridLines="None"  AllowPaging="true" PageSize="10">
        <Columns>
            <asp:BoundField ItemStyle-Width="50px" DataField="CustGrpID" HeaderText="ID" />
            <asp:BoundField DataField="GrpName" HeaderText="Customer Group" />
            <asp:TemplateField ItemStyle-Width="50px" HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" CssClass="btn btn-default" runat="server" CommandName="getEdit" CommandArgument='<%# Eval("CustGrpID") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:TemplateField ItemStyle-Width="50px" HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:LinkButton ID="lbDeleteGrp" CssClass="btn btn-default" runat="server" CommandName="getDelete" CommandArgument='<%# Eval("CustGrpID") %>'><i class="fa fa-trash"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField ItemStyle-Width="50px" HeaderText="Cust" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:LinkButton ID="lbAddCust" Enabled='<%# If(Eval("isActive").ToString() = "N", "false", "true") %>' CssClass="btn btn-default" runat="server" CommandName="getAddCust" CommandArgument='<%# Eval("CustGrpID") %>' Font-Size="Smaller"><i class="fa fa-plus"></i> Add Customer</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:TemplateField ItemStyle-Width="50px" HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:LinkButton ID="lbEdit" CssClass="btn btn-default" runat="server" CommandName="getHapus" CommandArgument='<%# Eval("CustGrpID") %>'><i class="fa fa-trash-o"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>--%>
        </Columns>
        <pagerstyle cssclass="pagination-ys">
        </pagerstyle>
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>

