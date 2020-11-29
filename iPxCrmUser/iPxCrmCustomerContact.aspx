<%@ Page Language="VB" MasterPageFile="~/iPxCrmUser/iPxMasterUser.master" AutoEventWireup="false" CodeFile="iPxCrmCustomerContact.aspx.vb" Inherits="iPxCrmUser_iPxCrmCustomerContact" title="Customer Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Login Modal-->
      <div id="login-modal" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-lg">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" data-dismiss="modal" aria-label="Close" class="close"><span aria-hidden="true">×</span></button>
              <h4 id="login-modalLabel" class="modal-title">Detail</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                          <label for="usr">Hotel Name:</label>
                            <asp:DropDownList ID="dlHotelName" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                          <label for="usr">Name:</label>
                            <asp:TextBox ID="tbName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                          <label for="usr">Email:</label>
                            <asp:TextBox ID="tbEmail" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                          <label for="usr">Customer Group:</label>
                            <asp:DropDownList ID="dlCustGrp" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                          <label for="usr">Phone:</label>
                            <asp:TextBox ID="tbPhone" runat="server" CssClass ="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                          <label for="usr">Birthday:</label>
                            <asp:TextBox ID="tbBirthday" runat="server" CssClass="form-control" placeholder="yyyy-MM-dd"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <p class="text-center">
                    <asp:LinkButton ID="lbSave" CssClass ="btn btn-success" runat="server"><i class="fa fa-save"></i> Save Contact</asp:LinkButton>
                </p>
            </div>
          </div>
        </div>
      </div>
      <!-- Login modal end-->
        <div class="form-group left">
          <div class="form-group">
            <asp:LinkButton width=125px CssClass="btn btn-default dropdown-toggle" data- ID="btnQuery" runat="server"><span class="fa fa-filter "></span> Query</asp:LinkButton>
            <asp:LinkButton width=125px ID="lbAddContact" CssClass ="btn btn-default" runat="server"><i class="fa fa-plus"></i> New Contact</asp:LinkButton>
          </div>
        </div>
        <asp:GridView EmptyDataText="No records has been added." ID="gvCustContact" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered" HeaderStyle-BackColor="Silver" Font-Size="Smaller" GridLines="None"  AllowPaging="true" PageSize="10">
            <Columns>
                <asp:BoundField ItemStyle-Width="70px" DataField="CustID" HeaderText="Customer ID" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField ItemStyle-Width="120px" DataField="CustName" HeaderText="Hotel Name" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField ItemStyle-Width="100px" DataField="Description" HeaderText="Group" />
                <asp:BoundField ItemStyle-Width="90px" DataField="Birthday" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Birthday" />
                <asp:BoundField ItemStyle-Width="80px" DataField="Phone" HeaderText="Phone" />
                <asp:BoundField ItemStyle-Width="150px" DataField="Email" HeaderText="Email" />
                <asp:TemplateField ItemStyle-Width="50px" HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" CssClass="btn btn-default" runat="server" CommandName="getEditTiket" CommandArgument='<%# Eval("CustID") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="50px" HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbEdit" CssClass="btn btn-default" runat="server" CommandName="getHapusid" CommandArgument='<%# Eval("CustID") %>'><i class="fa fa-trash-o"></i></asp:LinkButton>
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

