<%@ Page Language="VB" MasterPageFile="~/iPxCrmCustomer/iPxCrmCustomer.master" AutoEventWireup="false" CodeFile="iPxCrmContact.aspx.vb" Inherits="iPxCrmCustomer_iPxCrmContact" title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Form Input Modal-->
      <div id="formsub-modal" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-md">
          <div class="modal-content">
            <div class="modal-header">
              <asp:LinkButton ID="lbCancelContact" runat="server" CssClass="close" aria-label="Close"><span aria-hidden="true">&times;</span></asp:LinkButton>
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
                    <%--<asp:LinkButton ID="lbCancelContact" CssClass ="btn btn-default" runat="server" Width="150px"><i class="fa fa-close"></i> Abort</asp:LinkButton>--%>
                </p>
            </div>
          </div>
        </div>
      </div>
      <!-- Form Input Modal end-->
    <%--<div class="row">
        <div class="col-lg-12">--%>
            <div class="form-group left">
                <div class="form-group">
                    <%--<asp:LinkButton width=125px CssClass="btn btn-default dropdown-toggle" data- ID="LinkButton2" runat="server"><span class="fa fa-filter "></span> Query</asp:LinkButton>--%>
                    <asp:LinkButton ID="lbInputContact" CssClass ="btn btn-default" runat="server"><i class="fa fa-plus"></i> Add Contact</asp:LinkButton>
                </div>
            </div>
            <asp:GridView EmptyDataText="No records has been added." ID="gvContact" runat="server" AutoGenerateColumns="false" CssClass="table" HeaderStyle-BackColor="#0a818e" HeaderStyle-ForeColor="White" Font-Size="Smaller" GridLines="None"  AllowPaging="true" PageSize="10">
                <Columns>
                    <asp:BoundField ItemStyle-Width="50px" DataField="NameCode" HeaderText="ID" ItemStyle-HorizontalAlign ="Center"/>
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
                            <asp:LinkButton ID="lbDelete" CssClass ="btn btn-default" Enabled='<%# If(Eval("Description").ToString() = "OWNER", "false", "true")%>' OnClientClick='<%# If(Eval("IsActive").ToString() = "N", "return confirmationRestore();", "return confirmation();")%>' runat="server" CommandName='<%# If(Eval("IsActive").ToString() = "Y", "getHapus", "getRestore")%>' CommandArgument='<%# Eval("NameCode") %>'><i class='<%# If(Eval("IsActive").ToString() = "Y", "fa fa-trash", "fa fa-history") %>'></i></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <pagerstyle cssclass="pagination-ys">
                </pagerstyle>
            </asp:GridView>
       <%-- </div>
    </div>--%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>

