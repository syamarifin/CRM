<%@ Page Language="VB" MasterPageFile="~/iPxCrmUser/iPxMasterUser.master" AutoEventWireup="false" CodeFile="iPxCrmProduct.aspx.vb" Inherits="iPxCrmUser_iPxCrmProduct" title="Product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <!-- Hapus Product Modal-->
      <div id="modalHapusProduct" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-md">
          <div class="modal-content">
            <div class="modal-header" style="background-color :#F5F5F5;">
              <h3 id="H5" class="modal-title">Autentifikasi</h3>
            </div>
            <div class="modal-body">
                <h4>sure you want to delete data</h4>
            <%--</div>
            <div class="modal-footer">--%>
                <p class="text-right">
                    <asp:LinkButton Width="150px" ID="lbDeleteProduct" CssClass ="btn btn-default" runat="server"><i class="fa fa-check"></i> Delete</asp:LinkButton>
                    <asp:LinkButton Width="150px" ID="lbAbortDeleteProduct" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
                </p>
            </div>
          </div>
        </div>
      </div>
      <!-- Hapus Product end-->
      <!-- Hapus Menu Modal-->
      <div id="modalHapusMenu" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
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
                    <asp:LinkButton Width="150px" ID="lbDeleteMenu" CssClass ="btn btn-default" runat="server"><i class="fa fa-check"></i> Delete</asp:LinkButton>
                    <asp:LinkButton Width="150px" ID="lbAbortDeleteMenu" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
                </p>
            </div>
          </div>
        </div>
      </div>
      <!-- Hapus Menu Modal end-->
      <!-- Hapus Sub Modal-->
      <div id="modalHapus" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-md">
          <div class="modal-content">
            <div class="modal-header" style="background-color :#F5F5F5;">
              <h3 id="H4" class="modal-title">Autentifikasi</h3>
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
      <!-- Form Menu Modal-->
      <div id="formMenu-modal" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-md">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" data-dismiss="modal" aria-label="Close" class="close"><span aria-hidden="true">×</span></button>
              <h4 id="H1" class="modal-title">Form Menu</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="usr">Product Group:</label>
                            <asp:TextBox ID="tbGrpMenu" runat="server" CssClass ="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="usr">Menu ID:</label><font color=red>*</font>
                            <asp:TextBox ID="tbMenuID" runat="server" Enabled ="false" CssClass ="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="usr">Product:</label>
                            <asp:TextBox ID="tbDetailProduct" runat="server" CssClass ="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="usr">Menu Name:</label><font color=red>*</font>
                            <asp:TextBox ID="tbMenuName" runat="server" CssClass ="form-control"></asp:TextBox>
                        </div>
                    </div>  
                </div>
                <div class="form-group center">
                  <div class="form-group">
                    <%--<asp:LinkButton width=125px CssClass="btn btn-default dropdown-toggle" data- ID="LinkButton2" runat="server"><span class="fa fa-filter "></span> Query</asp:LinkButton>--%>
                    <asp:LinkButton width="150px" ID="lbSaveMenu" CssClass ="btn btn-default" runat="server"><i class="fa fa-save"></i> Save</asp:LinkButton>
                    <asp:LinkButton width="150px" ID="lbAbortMenu" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
                  </div>
                </div>            
            </div>
          </div>
        </div>
      </div>
      <!-- Form Menu modal end-->
      <!-- Form SubMenu Modal-->
      <div id="formsub-modal" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-md">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" data-dismiss="modal" aria-label="Close" class="close"><span aria-hidden="true">×</span></button>
              <h4 id="H3" class="modal-title">Form Sub Menu</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="usr">Product Group:</label>
                            <asp:TextBox ID="tbGroupSub" runat="server" CssClass ="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="usr">Product:</label>
                            <asp:TextBox ID="tbDetailProductSub" runat="server" CssClass ="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="usr">Sub Menu ID:</label>
                            <asp:TextBox ID="tbSubID" runat="server" Enabled ="false" CssClass ="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="usr">Menu:</label>
                            <asp:TextBox ID="tbDetailMenuSub" runat="server" CssClass ="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="usr">Name:</label><font color=red>*</font>
                            <asp:TextBox ID="tbSubmenu" runat="server" CssClass ="form-control"></asp:TextBox>
                        </div>
                    </div> 
                </div>
                <div class="form-group center">
                  <div class="form-group">
                    <%--<asp:LinkButton width=125px CssClass="btn btn-default dropdown-toggle" data- ID="LinkButton2" runat="server"><span class="fa fa-filter "></span> Query</asp:LinkButton>--%>
                    <asp:LinkButton width="150px" ID="lbSaveSub" CssClass ="btn btn-default" runat="server"><i class="fa fa-save"></i> Save</asp:LinkButton>
                    <asp:LinkButton width="150px" ID="lbAbortSub" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
                  </div>
                </div>            
            </div>
          </div>
        </div>
      </div>
      <!-- Form SubMenu modal end-->
      <!-- Form SOP Modal-->
      <div id="FormSOP-modal" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-md">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" data-dismiss="modal" aria-label="Close" class="close"><span aria-hidden="true">×</span></button>
              <h4 id="H6" class="modal-title">Form SOP Link</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="usr">Product Group:</label>
                            <asp:TextBox ID="tbGrpSOP" runat="server" CssClass ="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="usr">Product:</label>
                            <asp:TextBox ID="tbProductSOP" runat="server" CssClass ="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="usr">Menu:</label>
                            <asp:TextBox ID="tbMenuSOP" runat="server" CssClass ="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="usr">Sub Menu:</label>
                            <asp:TextBox ID="tbSubmenuSOP" runat="server" CssClass ="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="usr">SOP Id</label>
                            <asp:TextBox ID="tbSopId" runat="server" CssClass ="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="usr">Link SOP</label>
                            <div class="input-group">
                                <asp:TextBox ID="tbLinkSOP" runat="server" CssClass ="form-control"></asp:TextBox>
                                <span class="input-group-addon"><i class="glyphicon glyphicon-paperclip"></i></span>
                            </div>
                        </div>
                    </div>  
                </div>
                <div class="form-group center">
                  <div class="form-group">
                    <%--<asp:LinkButton width=125px CssClass="btn btn-default dropdown-toggle" data- ID="LinkButton2" runat="server"><span class="fa fa-filter "></span> Query</asp:LinkButton>--%>
                    <asp:LinkButton width="150px" ID="lbSaveSOP" CssClass ="btn btn-default" runat="server"><i class="fa fa-save"></i> Save</asp:LinkButton>
                    <asp:LinkButton width="150px" ID="lbAbortSOP" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
                  </div>
                </div>            
            </div>
          </div>
        </div>
      </div>
      <!-- Form SOP modal end-->
        <asp:Label ID="lblTitleTicket" runat="server" Text="Data Product" Font-Bold="true" Font-Size="Large"></asp:Label>
        <hr />
        <div class="form-group left">
          <div class="form-group">
            <asp:LinkButton width=125px CssClass="btn btn-default dropdown-toggle" data- ID="btnQuery" runat="server"><span class="fa fa-filter "></span> Query</asp:LinkButton>
            <asp:LinkButton width=125px ID="lbAddproduct" CssClass ="btn btn-default" runat="server"><i class="fa fa-plus"></i> Add Product</asp:LinkButton>
          </div>
        </div>
        <asp:GridView EmptyDataText="No records has been added." ID="gvProduct" runat="server" AutoGenerateColumns="false" CssClass="table" HeaderStyle-BackColor="#0a818e" HeaderStyle-ForeColor="White" Font-Size="Smaller" GridLines="None"  AllowPaging="true" PageSize="10" Width="100%">
            <Columns>
                <asp:BoundField ItemStyle-Width="100px" DataField="PrdDescription" HeaderText="Group" />
                <asp:BoundField ItemStyle-Width="70px" DataField="ProductID" HeaderText="Product ID" />
                <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                <asp:TemplateField ItemStyle-Width="70px" HeaderText="Menu">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbMenu" CssClass="btn btn-default" runat="server" CommandName="getMenu" CommandArgument='<%# Eval("ProductID") %>' Font-Size="Small"><h6 style="margin:0;"><i class="fa fa-list"></i> Menu</h6></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Normal Price" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label1" Text='<%# If(Eval("NormalPrice").ToString() = "0.0000", "0", String.Format("{0:N2}", (Eval("NormalPrice")))) %>'
                        runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Low Price" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label2" Text='<%# If(Eval("LowPrice").ToString() = "0.0000", "0", String.Format("{0:N2}", (Eval("LowPrice")))) %>'
                        runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Hight Price" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label ID="Label3" Text='<%# If(Eval("HightPrice").ToString() = "0.0000", "0", String.Format("{0:N2}", (Eval("HightPrice")))) %>'
                        runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="50px" HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" CssClass="btn btn-default" runat="server" CommandName="getEdit" CommandArgument='<%# Eval("ProductID") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--jika ingin mengaktifkan tombol hapus, ada di bawah ini!!!!--%>
                <%--<asp:TemplateField ItemStyle-Width="50px" HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbEdit" CssClass="btn btn-default" runat="server" CommandName="getHapus" CommandArgument='<%# Eval("ProductID") %>'><i class="fa fa-trash-o"></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
             </Columns>
             <pagerstyle cssclass="pagination-ys">
             </pagerstyle>
        </asp:GridView>
            <div id ="menuProduct" class="panel panel-default">
                <div class="panel-heading" style="background-color :#80CBC4;">
                    <b> Product Menu</b>
                    <asp:LinkButton ID="lbCloseMenu" CssClass="close" runat="server" aria-label="Close"><span aria-hidden="true">&times;</span></asp:LinkButton>
                    <%--<button id="Button1" type="button" class="close" aria-label="Close" >
                       
                    </button>--%>
                </div>
                <div class="panel-body">
                    <div class="form-group left">
                      <div class="form-group">
                        <%--<asp:LinkButton width=125px CssClass="btn btn-default dropdown-toggle" data- ID="LinkButton2" runat="server"><span class="fa fa-filter "></span> Query</asp:LinkButton>--%>
                        <asp:LinkButton width=125px ID="lbAddmenu" CssClass ="btn btn-default" runat="server"><i class="fa fa-plus"></i> Add Menu</asp:LinkButton>
                      </div>
                        <asp:GridView EmptyDataText="No records has been added." ID="gvMenu" runat="server" AutoGenerateColumns="false" CssClass="table" HeaderStyle-BackColor="#0a818e" HeaderStyle-ForeColor="White" Font-Size="Small" GridLines="None">
                            <Columns>
                                <asp:BoundField ItemStyle-Width="100px" DataField="PrdDescription" HeaderText="Group" >
                                </asp:BoundField>
                                <asp:BoundField ItemStyle-Width="200px" DataField="ProductName" HeaderText="Product" >
                                </asp:BoundField>
                                <asp:BoundField ItemStyle-Width="70px" DataField="MenuID" HeaderText="Menu ID" >
                                </asp:BoundField>
                                <asp:BoundField DataField="MenuName" HeaderText="Menu Name" />
                                <asp:TemplateField ItemStyle-Width="100px" HeaderText="Sub Menu">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbSub" CssClass="btn btn-default" runat="server" CommandName="getSub" CommandArgument='<%# Eval("MenuID") %>' Font-Size="Small"><h6 style="margin:0;"><i class="fa fa-list"></i> Sub Menu</h6></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="50px" HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" CssClass="btn btn-default" runat="server" CommandName="getEdit" CommandArgument='<%# Eval("MenuID") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--jika ingin mengaktifkan tombol hapus, ada di bawah ini!!!!--%>
                                <%--<asp:TemplateField ItemStyle-Width="50px" HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbEdit" CssClass="btn btn-default" runat="server" CommandName="getHapus" CommandArgument='<%# Eval("MenuID") %>'><i class="fa fa-trash-o"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                            <pagerstyle cssclass="pagination-ys">
                            </pagerstyle>
                        </asp:GridView>
                    </div>
                </div>
              </div>
              
            <div id="PanelSub" class="panel panel-info">
                <div class="panel-heading">
                    <b> Product Sub Menu</b>
                    <asp:LinkButton ID="lbCloseSubMenu" CssClass="close" runat="server" aria-label="Close"><span aria-hidden="true">&times;</span></asp:LinkButton>
                </div>
                <div class="panel-body">
                    <div class="form-group left">
                      <div class="form-group">
                        <%--<asp:LinkButton width=125px CssClass="btn btn-default dropdown-toggle" data- ID="LinkButton3" runat="server"><span class="fa fa-filter "></span> Query</asp:LinkButton>--%>
                        <asp:LinkButton width=125px ID="lbAddSubMenu" CssClass ="btn btn-default" runat="server"><i class="fa fa-plus"></i> Add Sub Menu</asp:LinkButton>
                      </div>
                        <asp:GridView EmptyDataText="No records has been added." ID="gvSubmenu" runat="server" AutoGenerateColumns="false" CssClass="table" HeaderStyle-BackColor="#36b3c1" HeaderStyle-ForeColor="White" Font-Size="Smaller" GridLines="None">
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
                                <asp:TemplateField ItemStyle-Width="100px" HeaderText="SOP Link" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="SOPLink" CssClass="btn btn-default" runat="server" CommandName="getSOP" CommandArgument='<%# Eval("SubmenuID") %>'><h6 style="margin:0;"><i class="fa fa-list"></i> SOP Link</h6></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--jika ingin mengaktifkan tombol hapus, ada di bawah ini!!!!--%>
                                <%--<asp:TemplateField ItemStyle-Width="50px" HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbEdit" CssClass="btn btn-default" runat="server" CommandName="getHapus" CommandArgument='<%# Eval("SubmenuID") %>'><i class="fa fa-trash-o"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                            <pagerstyle cssclass="pagination-ys">
                            </pagerstyle>
                        </asp:GridView>
                    </div>
                </div>
              </div>
            <div id="PanelSOP" class="panel panel-info">
                <div class="panel-heading">
                    <b> SOP Link</b>
                    <asp:LinkButton ID="lbCloseSOP" CssClass="close" runat="server" aria-label="Close"><span aria-hidden="true">&times;</span></asp:LinkButton>
                </div>
                <div class="panel-body">
                    <div class="form-group left">
                      <div class="form-group">
                        <%--<asp:LinkButton width=125px CssClass="btn btn-default dropdown-toggle" data- ID="LinkButton3" runat="server"><span class="fa fa-filter "></span> Query</asp:LinkButton>--%>
                        <asp:LinkButton width=125px ID="lbAddSOP" CssClass ="btn btn-default" runat="server"><i class="fa fa-plus"></i> Add SOP Link</asp:LinkButton>
                      </div>
                        <asp:GridView EmptyDataText="No records has been added." ID="gvSOP" runat="server" AutoGenerateColumns="false" CssClass="table" HeaderStyle-BackColor="#36b3c1" HeaderStyle-ForeColor="White" Font-Size="Smaller" GridLines="None">
                            <Columns>
                                <asp:BoundField ItemStyle-Width="100px" DataField="PrdDescription" HeaderText="Group" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="ProductName" HeaderText="Product" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="MenuName" HeaderText="Menu" />
                                <asp:BoundField ItemStyle-Width="150px" DataField="SubmenuName" HeaderText="Submenu"/>
                                <asp:BoundField DataField="link" HeaderText="SOP Link" />
                                <asp:TemplateField ItemStyle-Width="50px" HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbEditSOP" CssClass="btn btn-default" runat="server" CommandName="getEdit" CommandArgument='<%# Eval("SopID") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--jika ingin mengaktifkan tombol hapus, ada di bawah ini!!!!--%>
                                <%--<asp:TemplateField ItemStyle-Width="50px" HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbEdit" CssClass="btn btn-default" runat="server" CommandName="getHapus" CommandArgument='<%# Eval("SubmenuID") %>'><i class="fa fa-trash-o"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
              </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>

