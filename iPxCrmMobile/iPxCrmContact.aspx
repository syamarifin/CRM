<%@ Page Language="VB" MasterPageFile="~/iPxCrmMobile/iPxMobileOprBck.master" AutoEventWireup="false" CodeFile="iPxCrmContact.aspx.vb" Inherits="iPxCrmMobile_iPxCrmContact" title="Untitled Page" %>

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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br /><br /><br />
    <!-- Form Input Modal-->
      <div id="formsub-modal" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-md">
          <div class="modal-content">
            <div class="modal-header">
              <asp:LinkButton ID="lbCancelContact" runat="server" CssClass="close" aria-label="Close"><span aria-hidden="true">&times;</span></asp:LinkButton>
              <h3 id="H1" class="modal-title">Form Contact</h3>
            </div>
            <div class="modal-body">
                <div class="row" style="margin:0;">
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
    <!-- Form Input Modal-->
      <div id="form-query" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-sd">
          <div class="modal-content">
            <div class="modal-header">
              <asp:LinkButton ID="lbAbortQuery" runat="server" CssClass="close" aria-label="Close"><span aria-hidden="true">&times;</span></asp:LinkButton>
              <h3 id="H2" class="modal-title">Form Query</h3>
            </div>
            <div class="modal-body">
                <div class="row" style="margin:0;">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label for="usr">Contact Name:</label>
                            <asp:TextBox ID="tbQName" runat="server" CssClass ="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="usr">Contact Group:</label>
                            <asp:DropDownList ID="dlQGroup" runat="server" CssClass ="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="usr">Email:</label>
                            <asp:TextBox ID="tbQEmail" runat="server" CssClass ="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="usr">Status:</label>
                            <asp:DropDownList ID="dlQStatus" runat="server" CssClass ="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>  
                </div>
            </div>
            <div class="modal-footer">
                <p class="text-center">
                    <asp:LinkButton ID="lblQuery" CssClass ="btn btn-default" runat="server" Width="100px"><i class="fa fa-filter" ></i> Query</asp:LinkButton>
                    <%--<asp:LinkButton ID="lbCancelContact" CssClass ="btn btn-default" runat="server" Width="150px"><i class="fa fa-close"></i> Abort</asp:LinkButton>--%>
                </p>
            </div>
          </div>
        </div>
      </div>
    <!-- Form Input Modal end-->
    <%--<div class="row">
        <div class="col-lg-12">--%>
        <div class="col-sm-12" style="overflow:auto">
         <div class="form-group left">
          <div class="form-group">
            <asp:LinkButton width=100px CssClass="btn btn-default dropdown-toggle" data- ID="btnQuery" runat="server"><h4 style="margin:0;"><small style="color:White;"><span class="fa fa-filter "></span> Query</small></h4></asp:LinkButton>
            <asp:LinkButton ID="lbInputContact" CssClass ="btn btn-default" runat="server"><i class="fa fa-plus"></i> Add Contact</asp:LinkButton>
          </div>
        </div>
        </div>
        <div class="row" style=" margin:0;">
          <div class="col-sm-12" style="padding:0; overflow-x:scroll;">
                <asp:GridView HeaderStyle-BackColor="#0a818e" HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="true" ShowHeader="false" ID="gvContact" runat="server" CssClass="table" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" OnPageIndexChanging="OnPaging" PageSize="10" Width="100%" Font-Size="Smaller" GridLines="None">
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="50px" HeaderText="">
                            <ItemTemplate>
                                <div id ="new" style="padding:0px;" class="col-lg-12 col-xs-12">
                                    <!-- small box -->
                                    <div class="small-box">
                                        <div class="inner">
                                            <h3><%#Eval("Name").ToString()%> (<%#Eval("Description").ToString()%>)</h3>
                                            <%--<p><%#Eval("Name").ToString()%></p>--%>
                                            <p>Email <b><%#Eval("Email").ToString()%> </b></p>
                                            <p>Password <b><%#Eval("Passw").ToString()%> </b></p>
                                            <hr style="margin:0; border-color:#25a79f; border-width:2px;" />
                                            <p>No Telp <%#Eval("Phone").ToString().Replace(vbLf, "<br />")%></p>
                                            <p>Birthday <%#Eval("Birthday", "{0:dd MMM yyyy}").ToString()%></p>
                                        </div>
                                        <%--<div class="icon"  style="top: 10px;">
                                            <b><%#Eval("stsDescription").ToString()%></b>
                                        </div>--%>
                                        <div class="small-box-footer">
                                            <asp:LinkButton ID="lbDelete" CssClass ="btn btn-default" Visible='<%# If(Eval("Description").ToString() = "OWNER", "false", "true")%>' OnClientClick='<%# If(Eval("IsActive").ToString() = "N", "return confirmationRestore();", "return confirmation();")%>' runat="server" CommandName='<%# If(Eval("IsActive").ToString() = "Y", "getHapus", "getRestore")%>' CommandArgument='<%# Eval("NameCode") %>'><i class='<%# If(Eval("IsActive").ToString() = "Y", "fa fa-trash", "fa fa-history") %>'></i></asp:LinkButton>
                                            <asp:LinkButton ID="lbEdit" CssClass="btn btn-default" runat="server" CommandName="getEdit" CommandArgument='<%# Eval("NameCode") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div><!-- ./col -->
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <pagerstyle cssclass="pagination-ys">
                    </pagerstyle>
                </asp:GridView>
            </div>
        </div>
            <%--<asp:GridView EmptyDataText="No records has been added." ID="gvContact" runat="server" AutoGenerateColumns="false" CssClass="table" HeaderStyle-BackColor="#0a818e" HeaderStyle-ForeColor="White" Font-Size="Smaller" GridLines="None"  AllowPaging="true" PageSize="10">
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
            </asp:GridView>--%>
            
       <%-- </div>
    </div>--%>
</asp:Content>
<%--<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>--%>

