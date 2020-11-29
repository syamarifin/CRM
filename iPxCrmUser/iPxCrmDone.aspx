<%@ Page Language="VB" MasterPageFile="~/iPxCrmUser/iPxMasterUser.master" AutoEventWireup="false" CodeFile="iPxCrmDone.aspx.vb" Inherits="iPxCrmUser_iPxCrmDone" title="E-Ticket Done" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
       function confirmation() {
           if (confirm('are you sure you want to delete ?')) {
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
        <div class="form-group left">
          <div class="form-group">
            <asp:LinkButton width="125px" CssClass="btn btn-default" ID="btnQuery" runat="server"><span class="fa fa-filter "></span> Query</asp:LinkButton>
          </div>
        </div>
        <asp:GridView EmptyDataText="No records has been added." ID="gvTicket" runat="server" AutoGenerateColumns="false" CssClass="table" HeaderStyle-BackColor="#0a818e" HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="true" Font-Size="Smaller" GridLines="None" AllowPaging="true" PageSize="10">
            <Columns>
                <asp:TemplateField ItemStyle-Width="50px" HeaderText="Opsi" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                      <div class="btn-group">
                        <button type="button" class='<%# If(Eval("coment").ToString() = "1", "btn btn-danger dropdown-toggle", "btn btn-default dropdown-toggle") %>' data-toggle="dropdown">
                         <span class="caret"></span></button>
                        <ul class="dropdown-menu" role="menu">
                            <li><asp:LinkButton ID="lbEdit" runat="server" style="text-align:left;" CssClass='<%# If(Eval("coment").ToString() = "1", "btn btn-danger", "btn btn-default") %>' CommandName="getTiketid" CommandArgument='<%# Eval("TicketNo") %>'><i class="fa fa-envelope"></i> Detail Follow</asp:LinkButton></li>
                            <li><asp:LinkButton ID="lbDelete" OnClientClick="return confirmation();" runat="server" style="text-align:left;" CssClass="btn btn-default" CommandName="getDeleteid" CommandArgument='<%# Eval("TicketNo") %>'><i class="fa fa-trash-o"></i> Delete</asp:LinkButton></li>
                        </ul>
                      </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField ItemStyle-Width="70px" DataField="TicketNo" HeaderText="Ticket No" />
                <asp:BoundField ItemStyle-Width="90px" DataField="Tdate" DataFormatString="{0:dd/MM/yyyy hh:mm:ss}" HeaderText="Date" />
                <asp:TemplateField ItemStyle-Width="100px" HeaderText="Hotel Name">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# If(Eval("CustName").ToString().Length>15,Eval("CustName").ToString().Substring(0,15)+"...",Eval("CustName").ToString())%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField ItemStyle-Width="70px" DataField="SubmitFrom" HeaderText="From" />
                <asp:BoundField ItemStyle-Width="90px" DataField="ProductName" HeaderText="Product" />
                <asp:BoundField ItemStyle-Width="90px" DataField="MenuName" HeaderText="Menu" />
                <asp:BoundField ItemStyle-Width="90px" DataField="SubmenuName" HeaderText="Sub Menu" />
                <asp:TemplateField HeaderText="Subject">
                    <ItemTemplate>
                        <asp:Label ID="lblSubject" runat="server" Text='<%# If(Eval("Subject").ToString().Length>=25,Eval("Subject").ToString().Substring(0,24)+"...",Eval("Subject").ToString())%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField ItemStyle-Width="80px" DataField="stsDescription" HeaderText="Status" />
                <asp:TemplateField ItemStyle-Width="50px" HeaderText="Rating" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%#If(Eval("stsDescription").ToString() = "DONE", "-", Eval("Rating").ToString()&"/5")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField ItemStyle-Width="60px" HeaderText="Detail Follow" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbEdit" runat="server" CssClass='<%# If(Eval("coment").ToString() = "1", "btn btn-danger", "btn btn-default") %>' CommandName="getTiketid" CommandArgument='<%# Eval("TicketNo") %>'><i class="fa fa-envelope"></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="50px" HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbDelete" runat="server" CssClass="btn btn-default" CommandName="getDeleteid" CommandArgument='<%# Eval("TicketNo") %>'><i class="fa fa-trash-o"></i></asp:LinkButton>
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
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>

