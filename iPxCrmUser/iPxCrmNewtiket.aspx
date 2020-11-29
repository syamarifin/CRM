<%@ Page Language="VB" MasterPageFile="~/iPxCrmUser/iPxMasterUserUpload.master" AutoEventWireup="false" CodeFile="iPxCrmNewtiket.aspx.vb" Inherits="iPxCrmUser_iPxCrmNewtiket" title="E-Ticket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <!-- Login Modal-->
      <div id="login-modal" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-md">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" data-dismiss="modal" aria-label="Close" class="close"><span aria-hidden="true">×</span></button>
              <h4 id="login-modalLabel" class="modal-title">Detail</h4>
            </div>
            <div class="modal-body">
                <div class="row" style="margin:0;">
                    <div class="col-md-6">
                        <div class="form-group" style="margin:0;">
                          <label for="usr">Ticket No:</label>
                            <asp:TextBox ID="tbTicketno" runat="server" CssClass="form-control" 
                                ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="form-group" style="margin:0;">
                          <label for="usr">Hotel Name:</label>
                            <asp:TextBox ID="tbHotelName" runat="server" CssClass="form-control" 
                                ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="form-group" style="margin:0;">
                          <label for="usr">Contact:</label>
                            <asp:TextBox ID="tbContact" runat="server" CssClass="form-control" 
                                ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group" style="margin:0;">
                          <label for="usr">Product Name:</label>
                            <asp:TextBox ID="tbProduct" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="form-group" style="margin:0;">
                          <label for="usr">Menu:</label>
                            <asp:TextBox ID="tbMenu" runat="server" CssClass ="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="form-group" style="margin:0;">
                          <label for="usr">Sub Menu:</label>
                            <asp:TextBox ID="tbSubMenu" runat="server" CssClass = "form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group" style="margin:0;">
                          <label for="usr">From:</label>
                            <asp:TextBox ID="tbFrom" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="form-group" style="margin:0;">
                          <label for="usr">Subject:</label>
                            <asp:TextBox ID="tbSubject" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="form-group" style="margin:0;">
                          <label for="usr">Description:</label>
                            <asp:TextBox ID="tbDescription" runat="server" CssClass="form-control" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                          <label for="usr">Case:</label>
                            <asp:DropDownList ID="dlCase" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
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
          <div class="form-group">
            <asp:LinkButton width=125px CssClass="btn btn-default dropdown-toggle" data- ID="btnQuery" runat="server"><span class="fa fa-filter "></span> Query</asp:LinkButton>
            <asp:LinkButton width=125px ID="lbAddTicket" CssClass ="btn btn-default" runat="server"><i class="fa fa-plus"></i> New Ticket</asp:LinkButton>
          </div>
        </div>
        <asp:GridView ShowHeaderWhenEmpty="true" EmptyDataText="No records has been added." ID="gvTicket" runat="server" AutoGenerateColumns="false" CssClass="table" HeaderStyle-BackColor="#0a818e" HeaderStyle-ForeColor="White" Font-Size="Smaller" GridLines="None" AllowPaging="true" PageSize="10">
            <Columns>
                <asp:TemplateField ItemStyle-Width="50px" HeaderText="Opsi" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                      <div class="btn-group">
                        <button type="button" class='<%# If(Eval("coment").ToString() = "1", "btn btn-danger dropdown-toggle", "btn btn-default dropdown-toggle") %>' data-toggle="dropdown">
                         <span class="caret"></span></button>
                        <ul class="dropdown-menu" role="menu">
                            <li><asp:LinkButton ID="LinkButton2" style="text-align:left;" CssClass="btn btn-default" runat="server" CommandName="getFileTiket" CommandArgument='<%# Eval("AttachFile") %>' Enabled='<%# If(Eval("AttachFile").ToString() = "", "false", "True")  %>'><i class="fa fa-download"></i> Unduh</asp:LinkButton></li>
                            <li><asp:LinkButton ID="LinkButton1" style="text-align:left;" CssClass="btn btn-default" runat="server" CommandName="getEditTiket" CommandArgument='<%# Eval("TicketNo") %>'><i class="fa fa-edit"></i> Edit</asp:LinkButton></li>
                            <li><asp:LinkButton ID="lbEdit" style="text-align:left;" CssClass='<%# If(Eval("coment").ToString() = "1", "btn btn-danger", "btn btn-default") %>' runat="server" CommandName="getTiketid" CommandArgument='<%# Eval("TicketNo") %>'><i class="fa fa-mail-forward"></i> Assigned</asp:LinkButton></li>
                        </ul>
                      </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField ItemStyle-Width="80px" DataField="TicketNo" HeaderText="Ticket No" />
                <asp:BoundField ItemStyle-Width="130px" DataField="Tdate" DataFormatString="{0:dd/MM/yyyy hh:mm:ss}" HeaderText="Date" />
                <asp:TemplateField ItemStyle-Width="100px" HeaderText="Hotel Name">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# If(Eval("CustName").ToString().Length>15,Eval("CustName").ToString().Substring(0,15)+"...",Eval("CustName").ToString())%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField ItemStyle-Width="100px" DataField="SubmitFrom" HeaderText="From" />
                <asp:BoundField ItemStyle-Width="100px" DataField="ProductName" HeaderText="Product" />
                <asp:BoundField ItemStyle-Width="100px" DataField="MenuName" HeaderText="Menu" />
                <asp:BoundField ItemStyle-Width="100px" DataField="SubmenuName" HeaderText="Sub Menu" />
                <%--<asp:BoundField DataField="CaseDescription" HeaderText="Description" />--%>
                <asp:TemplateField HeaderText="Subject">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# If(Eval("Subject").ToString().Length>25,Eval("Subject").ToString().Substring(0,25)+"...",Eval("Subject").ToString())%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField ItemStyle-Width="50px" HeaderText="Unduh File" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" CssClass="btn btn-default" runat="server" CommandName="getFileTiket" CommandArgument='<%# Eval("AttachFile") %>' Enabled='<%# If(Eval("AttachFile").ToString() = "", "false", "True")  %>'><i class="fa fa-download"></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="50px" HeaderText="Detail" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" CssClass="btn btn-default" runat="server" CommandName="getEditTiket" CommandArgument='<%# Eval("TicketNo") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="50px" HeaderText="Assign To" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbEdit" CssClass='<%# If(Eval("coment").ToString() = "1", "btn btn-danger", "btn btn-default") %>' runat="server" CommandName="getTiketid" CommandArgument='<%# Eval("TicketNo") %>'><i class="fa fa-mail-forward"></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
             </Columns>
             <pagerstyle cssclass="pagination-ys">
             </pagerstyle>
        </asp:GridView>
        <br />
        <br />
        <br />
</asp:Content>

