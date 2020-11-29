<%@ Page Language="VB" MasterPageFile="~/iPxCrmUser/iPxMasterUser.master" AutoEventWireup="false" CodeFile="iPxCrmProductMenu.aspx.vb" Inherits="iPxCrmUser_iPxCrmProductMenu" title="Product Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <!-- SubMenu Modal-->
      <div id="submenu-modal" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-lg">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" data-dismiss="modal" aria-label="Close" class="close"><span aria-hidden="true">×</span></button>
              <h4 id="login-modalLabel" class="modal-title">Data Sub Menu</h4>
            </div>
            <div class="modal-body">
                <div class="form-group left">
                  <div class="form-group">
                    <%--<asp:LinkButton width=125px CssClass="btn btn-default dropdown-toggle" data- ID="LinkButton2" runat="server"><span class="fa fa-filter "></span> Query</asp:LinkButton>--%>
                    <asp:LinkButton width=125px ID="lbInputSub" CssClass ="btn btn-default" runat="server"><i class="fa fa-plus"></i> Add Sub Menu</asp:LinkButton>
                  </div>
                </div>
                <asp:GridView EmptyDataText="No records has been added." ID="gvSubmenu" runat="server" AutoGenerateColumns="false" CssClass="table" HeaderStyle-BackColor="Silver" Font-Size="Smaller" GridLines="None"  AllowPaging="true" PageSize="10">
                    <Columns>
                        <asp:BoundField ItemStyle-Width="100px" DataField="PrdDescription" HeaderText="Group" />
                        <asp:BoundField ItemStyle-Width="200px" DataField="ProductName" HeaderText="Product" />
                        <asp:BoundField ItemStyle-Width="200px" DataField="MenuName" HeaderText="Menu" />
                        <asp:BoundField ItemStyle-Width="70px" DataField="SubmenuID" HeaderText="Submenu ID" ItemStyle-HorizontalAlign ="Center"/>
                        <asp:BoundField DataField="SubmenuName" HeaderText="Submenu" />
                        <asp:TemplateField ItemStyle-Width="50px" HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" CssClass="btn btn-default" runat="server" CommandName="getEdit" CommandArgument='<%# Eval("SubmenuID") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="50px" HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbEdit" CssClass="btn btn-default" runat="server" CommandName="getHapus" CommandArgument='<%# Eval("SubmenuID") %>'><i class="fa fa-trash-o"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <pagerstyle cssclass="pagination-ys">
                    </pagerstyle>
                </asp:GridView>
            </div>
          </div>
        </div>
      </div>
      <!-- SubMenu modal end-->
      <!-- Form SubMenu Modal-->
      <div id="formsub-modal" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-md">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" data-dismiss="modal" aria-label="Close" class="close"><span aria-hidden="true">×</span></button>
              <h4 id="H1" class="modal-title">Form Sub Menu</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="usr">Product:</label>
                            <asp:TextBox ID="tbDetailProduct" runat="server" CssClass ="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="usr">Sub Menu ID:</label>
                            <asp:TextBox ID="tbSubID" runat="server" CssClass ="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="usr">Menu:</label>
                            <asp:TextBox ID="tbDetailMenu" runat="server" CssClass ="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="usr">Name:</label>
                            <asp:TextBox ID="tbSubmenu" runat="server" CssClass ="form-control"></asp:TextBox>
                        </div>
                    </div>  
                </div>
                <div class="form-group center">
                  <div class="form-group">
                    <%--<asp:LinkButton width=125px CssClass="btn btn-default dropdown-toggle" data- ID="LinkButton2" runat="server"><span class="fa fa-filter "></span> Query</asp:LinkButton>--%>
                    <asp:LinkButton width="150px" ID="lbSaveSub" CssClass ="btn btn-default" runat="server"><i class="fa fa-save"></i> Save</asp:LinkButton>
                    <asp:LinkButton width="150px" ID="lbAbort" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
                  </div>
                </div>            
            </div>
          </div>
        </div>
      </div>
      <!-- Form SubMenu modal end-->
      <!-- Hapus Sub Modal-->
      <div id="modalHapus" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-md">
          <div class="modal-content">
            <div class="modal-header" style="background-color :#F5F5F5;">
              <h3 id="H2" class="modal-title">Autentifikasi</h3>
            </div>
            <div class="modal-body">
                <h4>sure you want to delete data</h4>
            <%--</div>
            <div class="modal-footer">--%>
                <p class="text-right">
                    <asp:LinkButton Width="150px" ID="lbDeleteSub" CssClass ="btn btn-default" runat="server"><i class="fa fa-check"></i> Delete</asp:LinkButton>
                    <asp:LinkButton Width="150px" ID="lbCancelSub" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
                </p>
            </div>
          </div>
        </div>
      </div>
      <!-- Hapus Sub Modal end-->
      <!-- Hapus Modal-->
      <div id="modalHapusMenu" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-lg">
          <div class="modal-content">
            <div class="modal-header">
              <h3 id="H3" class="modal-title">Autentifikasi</h3>
            </div>
            <div class="modal-body">
                <h4>sure you want to delete data</h4>
            </div>
            <div class="modal-footer">
                <p class="text-right">
                    <asp:LinkButton ID="LinkButton2" CssClass ="btn btn-success" runat="server"><i class="fa fa-check"></i> Delete</asp:LinkButton>
                    <asp:LinkButton ID="LinkButton3" CssClass ="btn btn-danger" runat="server"><i class="fa fa-close"></i> Cancel</asp:LinkButton>
                </p>
            </div>
          </div>
        </div>
      </div>
      <!-- Hapus Modal end-->
    <asp:Label ID="lblTitleTicket" runat="server" Text="Data Product Menu" Font-Bold="true" Font-Size="Large"></asp:Label>
    <hr />
    <div class="form-group left">
      <div class="form-group">
        <asp:LinkButton width=125px CssClass="btn btn-default dropdown-toggle" data- ID="btnQuery" runat="server"><span class="fa fa-filter "></span> Query</asp:LinkButton>
        <asp:LinkButton width=125px ID="lbAddmenu" CssClass ="btn btn-default" runat="server"><i class="fa fa-plus"></i> Add Menu</asp:LinkButton>
      </div>
    </div>
    <asp:GridView EmptyDataText="No records has been added." ID="gvMenu" runat="server" AutoGenerateColumns="false" CssClass="table" HeaderStyle-BackColor="Silver" Font-Size="Small" GridLines="None" AllowPaging="true" PageSize="10">
        <Columns>
            <asp:BoundField ItemStyle-Width="100px" DataField="PrdDescription" HeaderText="Group" >
                <ItemStyle Width="100px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField ItemStyle-Width="200px" DataField="ProductName" HeaderText="Product" >
                <ItemStyle Width="200px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField ItemStyle-Width="70px" DataField="MenuID" HeaderText="Menu ID" >
                <ItemStyle Width="70px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="MenuName" HeaderText="Menu Name" />
            <asp:TemplateField ItemStyle-Width="150px" HeaderText="Sub Menu">
                <ItemTemplate>
                    <asp:LinkButton ID="lbSub" CssClass="btn btn-default" runat="server" CommandName="getSub" CommandArgument='<%# Eval("MenuID") %>' Font-Size="Smaller"><i class="fa fa-list"></i> Sub Menu</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="150px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="50px" HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" CssClass="btn btn-default" runat="server" CommandName="getEdit" CommandArgument='<%# Eval("MenuID") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="50px" HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:LinkButton ID="lbEdit" CssClass="btn btn-default" runat="server" CommandName="getHapus" CommandArgument='<%# Eval("MenuID") %>'><i class="fa fa-trash-o"></i></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
            </asp:TemplateField>
        </Columns>
        <pagerstyle cssclass="pagination-ys">
        </pagerstyle>

<HeaderStyle BackColor="Silver"></HeaderStyle>
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>

