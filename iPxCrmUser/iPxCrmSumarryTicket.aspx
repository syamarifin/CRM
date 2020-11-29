<%@ Page Language="VB" MasterPageFile="~/iPxCrmUser/iPxMasterUser.master" AutoEventWireup="false" CodeFile="iPxCrmSumarryTicket.aspx.vb" Inherits="iPxCrmUser_iPxCrmSumarryTicket" title="Sumarry Ticket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!-- Form Menu Modal-->
      <div id="formMenu-modal" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-sm">
          <div class="modal-content">
            <div class="modal-header">
              <asp:LinkButton ID="lbAbort" runat="server" CssClass="close" aria-label="Close"><span aria-hidden="true">&times;</span></asp:LinkButton>
              <h4 id="H1" class="modal-title">Query</h4>
            </div>
            <div class="modal-body">
                <div class="row" style="margin:0;">
                    <div class="form-group">
                        <label for="usr">Periode From:</label><font color=red>*</font>
                        <div class="input-group date datepicker">
                             <asp:TextBox ID="tbDateFrom" runat="server" CssClass ="form-control" placeholder="dd-MM-yyyy"></asp:TextBox>
                             <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="usr">Periode Until:</label><font color=red>*</font>
                        <div class="input-group date datepicker">
                             <asp:TextBox ID="tbDateUntil" runat="server" CssClass ="form-control" placeholder="dd-MM-yyyy"></asp:TextBox>
                             <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        </div>
                    </div>
                </div>
                <div class="form-group center">
                  <div class="form-group">
                    <asp:LinkButton width="150px" ID="lbQuery" CssClass ="btn btn-default" runat="server"><span class="fa fa-filter "></span> Query</asp:LinkButton>
                  </div>
                </div>            
            </div>
          </div>
        </div>
      </div>
      <!-- Form Menu modal end-->
    <div style="text-align:center; font-weight:bold;">
        Summary e-Ticket<br />
        Periode Date : 
        <asp:Label ID="lblTglFirst" runat="server" Text="Label"></asp:Label> 
        s/d  
        <asp:Label ID="lblTglLast" runat="server" Text="Label"></asp:Label>
    </div>
    <div class="form-group left">
        <div class="form-group">
            <asp:LinkButton Width="125px" CssClass="btn btn-default" ID="btnQuery" runat="server"><span class="fa fa-filter "></span> Query</asp:LinkButton>
        </div>
    </div>
    <asp:GridView EmptyDataText="No records has been added." ID="gvSummaryTicket" runat="server" AutoGenerateColumns="false" CssClass="table" HeaderStyle-BackColor="#0a818e" HeaderStyle-ForeColor="White" Font-Size="Smaller" GridLines="None">
        <Columns>
            <asp:BoundField ItemStyle-Width="20px" DataField="Numb" HeaderText="No" ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataFormatString="{0:dddd, dd MMMM yyyy}"  DataField="Tdate" HeaderText="Tanggal" />
            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Total Ticket" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblTotal" runat="server" Text='<%#If(Eval("totalTiket").ToString() = "", "0", Eval("totalTiket").ToString())%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="100px" HeaderText="New Ticket" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblNew" runat="server" Text='<%#If(Eval("totalTiketNew").ToString() = "", "0", Eval("totalTiketNew").ToString())%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Ticket Proses" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblProses" runat="server" Text='<%#If(Eval("totalTiketProses").ToString() = "", "0", Eval("totalTiketProses").ToString())%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="100px" HeaderText="Ticket Done" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblDone" runat="server" Text='<%#If(Eval("totalTiketDone").ToString() = "", "0", Eval("totalTiketDone").ToString())%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:TemplateField ItemStyle-Width="100px" HeaderText="Follow Up" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblFollow" runat="server" Text='<%#If(Eval("totalTiketFollow").ToString() = "", "0", Eval("totalTiketFollow").ToString())%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%>
        </Columns>
        <pagerstyle cssclass="pagination-ys">
        </pagerstyle>
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>

