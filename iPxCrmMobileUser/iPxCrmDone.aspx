<%@ Page Language="VB" MasterPageFile="~/iPxCrmMobileUser/iPxMobileOprBck.master" AutoEventWireup="false" CodeFile="iPxCrmDone.aspx.vb" Inherits="iPxCrmMobileUser_iPxCrmDone" title="E-Ticket Done" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br /><br /><br />
        <div class="form-group left">
          <div class="form-group" style="margin-left:10px;">
            <asp:LinkButton width=125px CssClass="btn btn-default dropdown-toggle" data- ID="btnQuery" runat="server"><span class="fa fa-filter "></span> Query</asp:LinkButton>
          </div>
        </div>
        <asp:GridView ShowHeader="false" EmptyDataText="No records has been added." ID="gvTicket" runat="server" AutoGenerateColumns="false" CssClass="table" HeaderStyle-BackColor="#0a818e" HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="true" Font-Size="Smaller" GridLines="None" AllowPaging="true" PageSize="10">
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
                                    <p>Support by <b><%#Eval("name").ToString()%></b></p>
                                    <p>Subject <b><%#Eval("Subject").ToString()%></b></p>
                                    <p><%#Eval("CaseDescription").ToString().Replace(vbLf, "<br />")%></p>
                                    <p style="text-align:right;"><%#Eval("Tdate", "{0:dd MMM yyyy hh:mm:ss}").ToString()%></p>
                                </div>
                                <div class="icon"  style='<%# "top: 25px; display:" + If(Eval("stsDescription").ToString() = "DONE", "none", "block") + ";" %>'>
                                    <b style="font-size:14px"><i class="fa fa-star" ></i> <%#Eval("Rating").ToString%></b>
                                </div>
                                <div class="icon"  style="top: 7px;">
                                    <b><%#Eval("stsDescription").ToString()%></b>
                                </div>
                                <div class="small-box-footer">
                                    <asp:LinkButton ID="lbFollow" runat="server" CssClass="btn btn-link" style='<%# "padding:0 6px; color:" + If(Eval("coment").ToString() = "1", "Red", "White") + ";" %>' CommandName="getTiketid" CommandArgument='<%# Eval("TicketNo") %>'><i class="fa fa-envelope"></i></asp:LinkButton>
                                </div>
                            </div>
                        </div><!-- ./col -->
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField ItemStyle-Width="70px" DataField="TicketNo" HeaderText="Ticket No" />
                <asp:BoundField ItemStyle-Width="90px" DataField="Tdate" DataFormatString="{0:dd/MM/yyyy hh:mm:ss}" HeaderText="Date" />
                <asp:BoundField ItemStyle-Width="100px" DataField="CustName" HeaderText="Hotel Name" />
                <asp:BoundField ItemStyle-Width="70px" DataField="SubmitFrom" HeaderText="From" />
                <asp:BoundField ItemStyle-Width="90px" DataField="ProductName" HeaderText="Product" />
                <asp:BoundField ItemStyle-Width="90px" DataField="MenuName" HeaderText="Menu" />
                <asp:BoundField ItemStyle-Width="90px" DataField="SubmenuName" HeaderText="Sub Menu" />
                <asp:BoundField DataField="CaseDescription" HeaderText="Description" />
                <asp:BoundField ItemStyle-Width="80px" DataField="stsDescription" HeaderText="Status" />
                <asp:TemplateField ItemStyle-Width="60px" HeaderText="Retting" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%#If(Eval("stsDescription").ToString() = "DONE", "-", Eval("Rating").ToString()&"/5")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="60px" HeaderText="Detail Follow" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbEdit" runat="server" CssClass='<%# If(Eval("coment").ToString() = "1", "btn btn-danger", "btn btn-default") %>' CommandName="getTiketid" CommandArgument='<%# Eval("TicketNo") %>'><i class="fa fa-envelope"></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>--%>
             </Columns>
             <pagerstyle cssclass="pagination-ys">
             </pagerstyle>
        </asp:GridView>
</asp:Content>

