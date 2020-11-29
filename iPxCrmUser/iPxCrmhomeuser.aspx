<%@ Page Language="VB" MasterPageFile="~/iPxCrmUser/iPxMasterUser.master" AutoEventWireup="false" CodeFile="iPxCrmhomeuser.aspx.vb" Inherits="iPxCrmUser_iPxCrmhomeuser" title="User" %>

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
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Login Modal-->
      <div id="login-modal" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-sm">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" data-dismiss="modal" aria-label="Close" class="close"><span aria-hidden="true">×</span></button>
              <h4 id="login-modalLabel" class="modal-title">Detail</h4>
            </div>
            <div class="modal-body">
                <div class="form-group">
                    <label for="usr">Name:</label><font color="red">*</font>
                    <asp:TextBox ID="tbName" runat="server" CssClass="form-control" onkeypress="return hanyaHuruf(event)"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="usr">Email:</label><font color="red">*</font>
                    <asp:TextBox ID="tbEmail" runat="server" CssClass="form-control" ></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="usr">Dept:</label>
                    <asp:DropDownList ID="dlDept" runat="server" CssClass="form-control" >
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <label for="usr">Position:</label>
                    <asp:DropDownList ID="dlPosition" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <label for="usr">Product:</label>
                    <asp:DropDownList ID="dlProduct" runat="server" CssClass="form-control" >
                    </asp:DropDownList>
                </div>
            </div>
            <div class="modal-footer">
                <p class="text-center">
                    <asp:LinkButton Width="100px" ID="lbSave" CssClass ="btn btn-default" runat="server"><i class="fa fa-save"></i> Save</asp:LinkButton>
                    <asp:LinkButton Width="100px" ID="lbAbort" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
                </p>
            </div>
          </div>
        </div>
      </div>
      <!-- Login modal end-->
          <div class="form-group left">
           <div class="input-group">
              <p class="text-left">
                <asp:LinkButton width=125px CssClass="btn btn-default dropdown-toggle" data- ID="btnQuery" runat="server"><span class="fa fa-filter "></span> Query</asp:LinkButton>
                <asp:LinkButton width=125px ID="lbAdd" CssClass ="btn btn-default" runat="server"><i class="fa fa-plus"></i> New User</asp:LinkButton>
              </p>
           </div>
          </div>
        <asp:GridView EmptyDataText="No records has been added." ID="gvUSer" runat="server" AutoGenerateColumns="false" CssClass="table" HeaderStyle-BackColor="#0a818e" HeaderStyle-ForeColor="White" GridLines="None" AllowPaging="true" PageSize="10">
            <Columns>
                <asp:TemplateField ItemStyle-Width="50px" HeaderText="No" ItemStyle-HorizontalAlign="Center"><ItemTemplate> <%#Container.DataItemIndex+1 %></ItemTemplate></asp:TemplateField>
                <asp:BoundField DataField="name" HeaderText="Name" />
                <asp:BoundField ItemStyle-Width="150px" DataField="DeptName" HeaderText="Dept" />
                <asp:TemplateField ItemStyle-Width="120px" HeaderText="Position">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# If(Eval("Possition").ToString() = "Admin", Eval("Possition").ToString()&If(Eval("ProductCode").ToString()="1"," ", If(Eval("ProductCode").ToString()="2"," Alcor"," Pyxis")), Eval("Possition").ToString()) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField ItemStyle-Width="150px" DataField="Email" HeaderText="Email" />
                <%--<asp:BoundField ItemStyle-Width="120px" DataField="passw" HeaderText="Password" />--%>
                <asp:TemplateField ItemStyle-Width="120px" HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbEdit" CssClass ="btn btn-default" runat="server" CommandName="getUserid" CommandArgument='<%# Eval("recID") %>'><i class="fa fa-pencil"></i></asp:LinkButton> &nbsp;
                        <asp:LinkButton ID="lbDelete" CssClass ="btn btn-default" OnClientClick='<%# If(Eval("isActive").ToString() = "Y", "return confirmation();", "return confirmationRestore();")%>' runat="server" CommandName='<%# If(Eval("isActive").ToString() = "Y", "deleteUserid", "RestoreUserid")%>' CommandArgument='<%# Eval("recID") %>' Enabled='<%# If(Eval("PossitionCode").ToString() = "0", "false", "true") %>'><i class='<%# If(Eval("isActive").ToString() = "Y", "fa fa-trash", "fa fa-history") %>'></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
             </Columns>
             <pagerstyle cssclass="pagination-ys">
             </pagerstyle>
        </asp:GridView>
</asp:Content>

