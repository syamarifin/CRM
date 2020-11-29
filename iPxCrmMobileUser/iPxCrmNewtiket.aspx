<%@ Page Language="VB" MasterPageFile="~/iPxCrmMobileUser/iPxMobileUpload.master" AutoEventWireup="false" CodeFile="iPxCrmNewtiket.aspx.vb" Inherits="iPxCrmMobileUser_iPxCrmNewtiket" title="E-Ticket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br /><br />
    <!-- Login Modal-->
      <div id="login-modal" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-lg">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" data-dismiss="modal" aria-label="Close" class="close"><span aria-hidden="true">×</span></button>
              <h4 id="login-modalLabel" class="modal-title">Detail</h4>
            </div>
            <div class="modal-body">
                <div class="row" style="margin-left:-15px;">
                    <div class="col-md-12">
                        <div class="form-group" style="margin-bottom: 0;">
                          <label for="usr">Product Name:</label>
                            <asp:TextBox ID="tbProduct" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="form-group" style="margin-bottom: 0;">
                          <label for="usr">Menu:</label>
                            <asp:TextBox ID="tbMenu" runat="server" CssClass ="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="form-group" style="margin-bottom: 0;">
                          <label for="usr">Sub Menu:</label>
                            <asp:TextBox ID="tbSubMenu" runat="server" CssClass = "form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="form-group" style="margin-bottom: 0;">
                          <label for="usr">From:</label>
                            <asp:TextBox ID="tbFrom" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="form-group" style="margin-bottom: 0;">
                          <label for="usr">Description:</label>
                            <asp:TextBox ID="tbDescription" runat="server" CssClass="form-control" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group" style="margin-bottom: 0;">
                          <label for="usr">Case:</label>
                            <asp:DropDownList ID="dlCase" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6" style="margin-bottom: 0;">
                        <div class="form-group">
                          <label for="usr">Assign To:</label>
                            <asp:DropDownList ID="dlAssign" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <p class="text-center">
                    <asp:LinkButton ID="lbSave" CssClass ="btn btn-default" runat="server"><i class="fa fa-send"></i> Send Assignment</asp:LinkButton>
                </p>
            </div>
          </div>
        </div>
      </div>
      <!-- Login modal end-->
        <div class="form-group left">
          <div class="form-group" style="margin-left:10px;">
            <asp:LinkButton width=125px CssClass="btn btn-default dropdown-toggle" data- ID="btnQuery" runat="server"><span class="fa fa-filter "></span> Query</asp:LinkButton>
            <asp:LinkButton width=125px ID="lbAddTicket" CssClass ="btn btn-default" runat="server"><i class="fa fa-plus"></i> New Ticket</asp:LinkButton>
          </div>
        </div>
        <div style="padding-left:0px; overflow-x:scroll;">
        <asp:GridView ShowHeader="False" ID="gvTicket" runat="server" AutoGenerateColumns="false" CssClass="table" HeaderStyle-BackColor="#0a818e" HeaderStyle-ForeColor="White" Font-Size="Smaller" GridLines="None" AllowPaging="true" PageSize="10">
            <Columns>
                <asp:TemplateField ItemStyle-Width="50px" HeaderText="">
                    <ItemTemplate>
                        <div id ="new" style="padding:0px;" class="col-lg-12 col-xs-12">
                            <!-- small box -->
                            <div class="small-box">
                                <div class="inner">
                                    <h3><%#Eval("SubmitFrom").ToString()%> From <%#Eval("CustName").ToString()%></h3>
                                    <p><%#Eval("TicketNo").ToString()%></p>
                                    <p><%#Eval("ProductName").ToString()%> -> <%#Eval("MenuName").ToString()%> -> <%#Eval("SubmenuName").ToString()%></p>
                                    <hr style="margin:0; border-color:#25a79f; border-width:2px;" />
                                    <p>Subject <b><%#Eval("Subject").ToString()%></b></p>
                                    <p><%#Eval("CaseDescription").ToString().Replace(vbLf, "<br />")%></p>
                                    <p style="text-align:right;"><%#Eval("Tdate", "{0:dd MMM yyyy hh:mm:ss}").ToString()%></p>
                                </div>
                                <div class="small-box-footer">
                                    <asp:LinkButton ID="lbUnduh" runat="server" CssClass="btn btn-link" style="padding:0 6px;" ForeColor="White" CommandName="getFileTiket" CommandArgument='<%# Eval("AttachFile") %>' Visible='<%# If(Eval("AttachFile").ToString() = "", "false", "True")%>'><i class="fa fa-download"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lbDetail" runat="server" CssClass="btn btn-link" style="padding:0 6px;" ForeColor="White" CommandName="getEditTiket" CommandArgument='<%# Eval("TicketNo") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lbAssign" runat="server" CssClass="btn btn-link" style='<%# "padding:0 6px; color:" + If(Eval("coment").ToString() = "1", "Red", "White") + ";" %>' CommandName="getTiketid" CommandArgument='<%# Eval("TicketNo") %>'><i class="fa fa-mail-forward"></i></asp:LinkButton>
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
</asp:Content>

