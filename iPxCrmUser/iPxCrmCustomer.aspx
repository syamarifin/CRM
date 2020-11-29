<%@ Page Language="VB" MasterPageFile="~/iPxCrmUser/iPxMasterUser.master" AutoEventWireup="false" CodeFile="iPxCrmCustomer.aspx.vb" Inherits="iPxCrmUser_iPxCrmCustomer" title="Customer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
       function confirmation() {
           if (confirm('are you sure you want to delete ?')) {
           return true;
           }else{
           return false;
           }
       }
       function confirmationRestore(){
           if (confirm('are you sure you want to restore ?')) {
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
      <!-- SubMenu Modal-->
      <div id="submenu-modal" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-lg">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" data-dismiss="modal" aria-label="Close" class="close"><span aria-hidden="true">×</span></button>
              <h4 id="login-modalLabel" class="modal-title">Data Customer Contact</h4>
            </div>
            <div class="modal-body">
                <div class="form-group left">
                  <div class="form-group">
                    <%--<asp:LinkButton width=125px CssClass="btn btn-default dropdown-toggle" data- ID="LinkButton2" runat="server"><span class="fa fa-filter "></span> Query</asp:LinkButton>--%>
                    <asp:LinkButton ID="lbInputContact" CssClass ="btn btn-default" runat="server"><i class="fa fa-plus"></i> Add Customer Contact</asp:LinkButton>
                  </div>
                </div>
                <asp:GridView EmptyDataText="No records has been added." ID="gvContact" runat="server" AutoGenerateColumns="false" CssClass="table" HeaderStyle-BackColor="#0a818e" HeaderStyle-ForeColor="White" Font-Size="Smaller" GridLines="None"  AllowPaging="true" PageSize="10">
                    <Columns>
                        <asp:BoundField ItemStyle-Width="70px" DataField="NameCode" HeaderText="ID Contact" ItemStyle-HorizontalAlign ="Center"/>
                        <asp:BoundField ItemStyle-Width="200px" DataField="CustName" HeaderText="Hotel Name" />
                        <asp:BoundField ItemStyle-Width="150px" DataField="Name" HeaderText="Name" />
                        <asp:BoundField ItemStyle-Width="200px" DataField="Description" HeaderText="position" />
                        <asp:BoundField ItemStyle-Width="100px" DataField="Phone" HeaderText="Phone" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField ItemStyle-Width="200px" DataField="Passw" HeaderText="password" />
                        <asp:BoundField ItemStyle-Width="100px" DataField="Birthday" HeaderText="Birthday" DataFormatString="{0:dd/MM/yyyy}"/>
                        <asp:TemplateField ItemStyle-Width="50px" HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbEdit" CssClass="btn btn-default" runat="server" CommandName="getEdit" CommandArgument='<%# Eval("NameCode") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="50px" HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbDelete" CssClass ="btn btn-default" OnClientClick='<%# If(Eval("IsActive").ToString() = "N", "return confirmationRestore();", "return confirmation();")%>' runat="server" CommandName='<%# If(Eval("IsActive").ToString() = "Y", "getHapus", "getRestore")%>' CommandArgument='<%# Eval("NameCode") %>'><i class='<%# If(Eval("IsActive").ToString() = "Y", "fa fa-trash", "fa fa-history") %>'></i></asp:LinkButton>
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
      <!-- Form Input Modal-->
      <div id="formsub-modal" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-md">
          <div class="modal-content">
            <div class="modal-header">
              <h3 id="H1" class="modal-title">Form Contact</h3>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="usr">Hotel Name:</label>
                            <asp:TextBox ID="tbCustName" runat="server" CssClass ="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="usr">Contact Name:</label><font color=red>*</font>
                            <asp:TextBox ID="tbNameContact" runat="server" CssClass ="form-control" onkeypress="return hanyaHuruf(event)"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="usr">Birthday:</label>
                            <div class="input-group date datepicker">
                                <asp:TextBox ID="tbBirthday" runat="server" CssClass ="form-control " placeholder="dd-MM-yyyy"></asp:TextBox>
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="usr">Contact Group:</label><font color=red>*</font>
                            <asp:DropDownList ID="dlContactGrp" runat="server" CssClass ="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="usr">Phone:</label><font color=red>*</font>
                            <asp:TextBox ID="tbPhone" runat="server" CssClass ="form-control" onkeypress="return hanyaAngka(event)"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="usr">Email:</label><font color=red>*</font>
                            <asp:TextBox ID="tbEmail" runat="server" CssClass ="form-control"></asp:TextBox>
                        </div>
                    </div>  
                </div>
            </div>
            <div class="modal-footer">
                <p class="text-center">
                    <asp:LinkButton ID="lbSaveContact" CssClass ="btn btn-default" runat="server" Width="150px"><i class="fa fa-save" ></i> Save Contact</asp:LinkButton>
                    <asp:LinkButton ID="lbCancelContact" CssClass ="btn btn-default" runat="server" Width="150px"><i class="fa fa-close"></i> Abort</asp:LinkButton>
                </p>
            </div>
          </div>
        </div>
      </div>
      <!-- Form Input Modal end-->
      <!-- Hapus Sub Modal-->
      <div id="modalHapus" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-lg">
          <div class="modal-content">
            <div class="modal-header">
              <h3 id="H2" class="modal-title">Autentifikasi</h3>
            </div>
            <div class="modal-body">
                <h4>sure you want to delete data</h4>
            </div>
            <div class="modal-footer">
                <p class="text-right">
                    <asp:LinkButton Width="125px" ID="lbDelete" CssClass ="btn btn-default" runat="server"><i class="fa fa-check"></i> Delete</asp:LinkButton>
                    <asp:LinkButton Width="125px" ID="lbCancel" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
                </p>
            </div>
          </div>
        </div>
      </div>
      <!-- Hapus Sub Modal end-->
      <!-- Hapus Modal-->
      <div id="modalHapusMenu" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-md">
          <div class="modal-content">
            <div class="modal-header">
              <h3 id="H3" class="modal-title">Autentifikasi</h3>
            </div>
            <div class="modal-body">
                <h4>sure you want to delete data</h4>
            </div>
            <div class="modal-footer">
                <p class="text-right">
                    <asp:LinkButton Width="125px" ID="LinkButton2" CssClass ="btn btn-default" runat="server"><i class="fa fa-check"></i> Delete</asp:LinkButton>
                    <asp:LinkButton Width="125px" ID="LinkButton3" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
                </p>
            </div>
          </div>
        </div>
      </div>
      <!-- Hapus Modal end-->
    <asp:Label ID="lblTitleTicket" runat="server" Text="Data Customer" Font-Bold="true" Font-Size="Large"></asp:Label>
    <hr />
    <div class="form-group left">
      <div class="form-group">
        <asp:LinkButton width=150px CssClass="btn btn-default dropdown-toggle" data- ID="btnQuery" runat="server"><span class="fa fa-filter "></span> Query</asp:LinkButton>
        <asp:LinkButton width=150px ID="lbNew" CssClass ="btn btn-default" runat="server"><i class="fa fa-plus"></i> New Customer</asp:LinkButton>
      </div>
    </div>
    <asp:GridView EmptyDataText="No records has been added." ID="gvCust" runat="server" AutoGenerateColumns="false" CssClass="table" HeaderStyle-BackColor="#0a818e" HeaderStyle-ForeColor="White" Font-Size="Smaller" GridLines="None"  AllowPaging="true" PageSize="10" OnRowDataBound="OnRowDataBound">
        <Columns>
            <asp:TemplateField ItemStyle-Width="50px" HeaderText="Detail" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                  <div class="btn-group">
                    <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown">
                     <span class="caret"></span></button>
                    <ul class="dropdown-menu" role="menu">
                      <li><asp:LinkButton ID="lbContact" CssClass="btn btn-link" runat="server" CommandName="getContact" CommandArgument='<%# Eval("CustID") %>' style="text-align:left;"><i class="fa fa-list"></i> Customer Contact</asp:LinkButton></li>
                      <li><asp:LinkButton ID="lbDraft" CssClass="btn btn-link" runat="server" CommandName="getDraft" CommandArgument='<%# Eval("CustID") %>' style="text-align:left;"><i class="fa fa-book"></i> Draft E-Ticket</asp:LinkButton></li>
                    </ul>
                  </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField ItemStyle-Width="50px" DataField="CustID" HeaderText="Cust ID" />
            <asp:BoundField ItemStyle-Width="100px" DataField="GrpName" HeaderText="Cust Group" />
            <asp:BoundField ItemStyle-Width="120px" DataField="CustName" HeaderText="Hotel Name" />
            <asp:BoundField DataField="Phone" HeaderText="Phone" />
            <asp:BoundField ItemStyle-Width="100px" DataField="Fax" HeaderText="Fax" />
            <asp:BoundField ItemStyle-Width="100px" DataField="country" HeaderText="Country" />
            <asp:BoundField ItemStyle-Width="100px" DataField="Profinsi" HeaderText="Province" />
            <asp:BoundField ItemStyle-Width="100px" DataField="city" HeaderText="City" />
            <asp:TemplateField HeaderText="Address">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# If(Eval("Address").ToString().Length>25,Eval("Address").ToString().Substring(0,25)+"...",Eval("Address").ToString())%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            <asp:BoundField ItemStyle-Width="30px" DataField="StarClass" HeaderText="Star Class" ItemStyle-HorizontalAlign ="Center"/>
            <asp:BoundField ItemStyle-Width="30px" DataField="Troom" HeaderText="Room" ItemStyle-HorizontalAlign ="Center"/>
            <%--<asp:BoundField DataField="Anniversary" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Anniversary" />--%>
            <asp:BoundField ItemStyle-Width="80px" DataField="LevelDescription" HeaderText="Cust Level" />
            <asp:TemplateField ItemStyle-Width="50px" HeaderText="Edit" ItemStyle-HorizontalAlign="Center" >
                <ItemTemplate>
                    <asp:LinkButton ID="lbEdutCust" CssClass="btn btn-default" runat="server" CommandName="getEdit" CommandArgument='<%# Eval("CustID") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="50px" HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:LinkButton ID="lbDeleteCust" CssClass ="btn btn-default" OnClientClick='<%# If(Eval("CustLevel").ToString() = "8", "return confirmationRestore();", "return confirmation();")%>' runat="server" CommandName='<%# If(Eval("CustLevel").ToString() <> "8", "getHapus", "getRestore")%>' CommandArgument='<%# Eval("CustID") %>'><i class='<%# If(Eval("CustLevel").ToString() <> "8", "fa fa-trash", "fa fa-history") %>'></i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <pagerstyle cssclass="pagination-ys">
        </pagerstyle>
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>

