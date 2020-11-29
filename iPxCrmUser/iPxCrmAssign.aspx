<%@ Page Language="VB" MasterPageFile="~/iPxCrmUser/iPxMasterUserUpload.master" AutoEventWireup="false" CodeFile="iPxCrmAssign.aspx.vb" Inherits="iPxCrmUser_iPxCrmAssign" title="Customer Assignment" %>

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
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-md-9">
            <div class="form-group">
                <asp:LinkButton width=175px CssClass="btn btn-default dropdown-toggle" data- ID="btnQuery" runat="server"><span class="fa fa-filter "></span> Query</asp:LinkButton>
                <asp:LinkButton width=175px ID="lbDirect" CssClass="btn btn-default" runat="server" Visible="false"><i class="fa fa-file-text"></i> Direct Follow Up</asp:LinkButton>
            </div>
        </div>
        <div class="col-md-3" style="top:13px;text-align:right;">
            <asp:Panel ID="showAll" visible="true" runat="server">
                <label class="ui-checkbox">
                    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged"/>
                    <span> <strong> Show All Support </strong> </span>
                </label>
            </asp:Panel>
        </div>
    </div>
        <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
            <asp:Timer ID="Timer1" runat="server" OnTick="TimerTick" Interval="180000" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:GridView EmptyDataText="No records has been added." ID="gvTicket" runat="server" AutoGenerateColumns="false" CssClass="table" HeaderStyle-BackColor="#0a818e" HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="true" Font-Size="Smaller" GridLines="None" AllowPaging="true" PageSize="10"  OnRowDataBound="OnRowDataBound">
            <Columns>
                <asp:TemplateField ItemStyle-Width="50px" HeaderText="Opsi" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                      <div class="btn-group">
                        <button type="button" class='<%# If(Eval("coment").ToString() = "1", "btn btn-danger dropdown-toggle", "btn btn-default dropdown-toggle") %>' data-toggle="dropdown">
                         <span class="caret"></span></button>
                        <ul class="dropdown-menu" role="menu">
                            <li><asp:LinkButton ID="LinkButton2" style="text-align:left;" CssClass="btn btn-default" runat="server" CommandName="getFileTiket" CommandArgument='<%# Eval("AttachFile") %>' Enabled='<%# If(Eval("AttachFile").ToString() = "", "false", "True")  %>'><i class="fa fa-download"></i> Unduh</asp:LinkButton></li>
                            <li><asp:LinkButton ID="lbFollow" style="text-align:left;" runat="server" CssClass='<%# If(Eval("coment").ToString() = "1", "btn btn-danger", "btn btn-default") %>' CommandName="getTiketid" CommandArgument='<%# Eval("TicketNo") %>'><i class="fa fa-envelope"></i> Follow Up</asp:LinkButton></li>
                            <li><asp:LinkButton ID="lbDelete" OnClientClick="return confirmation();" style="text-align:left;" runat="server" CssClass="btn btn-default" CommandName="getDeleteid" CommandArgument='<%# Eval("TicketNo") %>'><i class="fa fa-trash-o"></i> Delete</asp:LinkButton></li>
                        </ul>
                      </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField ItemStyle-Width="70px" DataField="TicketNo" HeaderText="Ticket No" />
                <asp:BoundField ItemStyle-Width="60px" DataField="Tdate" DataFormatString="{0:dd/MM/yyyy hh:mm:ss}" HeaderText="Date" />
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
                        <asp:Label ID="Label1" runat="server" Text='<%# If(Eval("Subject").ToString().Length>25,Eval("Subject").ToString().Substring(0,25)+"...",Eval("Subject").ToString())%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField ItemStyle-Width="80px" DataField="stsDescription" HeaderText="Status" />
                <asp:BoundField ItemStyle-Width="50px" DataField="name" HeaderText="Support By" />
                <%--<asp:TemplateField ItemStyle-Width="50px" HeaderText="Unduh File" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" CssClass="btn btn-default" runat="server" CommandName="getFileTiket" CommandArgument='<%# Eval("AttachFile") %>' Enabled='<%# If(Eval("AttachFile").ToString() = "", "false", "True")  %>'><i class="fa fa-download"></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="50px" HeaderText="Follow" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbFollow" runat="server" CssClass='<%# If(Eval("coment").ToString() = "1", "btn btn-danger", "btn btn-default") %>' CommandName="getTiketid" CommandArgument='<%# Eval("TicketNo") %>'><i class="fa fa-envelope"></i></asp:LinkButton>
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

