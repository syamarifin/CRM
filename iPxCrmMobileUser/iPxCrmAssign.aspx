<%@ Page Language="VB" MasterPageFile="~/iPxCrmMobileUser/iPxMobileUpload.master" AutoEventWireup="false" CodeFile="iPxCrmAssign.aspx.vb" Inherits="iPxCrmMobileUser_iPxCrmAssign" title="Customer Assignment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br /><br />
    <div class="row">
        <div class="col-md-12" style="padding-left:0;">
            <div class="form-group">
                <%--<asp:LinkButton width=100px CssClass="btn btn-default" ID="btnQuery" runat="server"><h5><small><i class="fa fa-filter"></i> Query</small></h5></asp:LinkButton>--%>
                <asp:LinkButton width=100px ID="btnQuery" CssClass="btn btn-default" runat="server"><h4 style="margin:0;"><small style="color:White;"><i class="fa fa-filter"></i> Query</small></h4></asp:LinkButton>
                <asp:LinkButton width=100px ID="lbDirect" CssClass="btn btn-default" runat="server" Visible="false"><h4 style="margin:0;"><small style="color:White;"><i class="fa fa-file-text"></i> Direct</small></h4></asp:LinkButton>
            </div>
        </div>
        <div class="col-md-12" style="padding-left:0; text-align:left;">
            <asp:Panel ID="showAll" visible="true" runat="server">
                <label class="ui-checkbox">
                    <small>
                    <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged"/>
                    <span> <strong> Show All Support </strong> </span>
                    </small>
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
        <asp:GridView ShowHeader="False" ID="gvTicket" runat="server" AutoGenerateColumns="false" CssClass="table" HeaderStyle-BackColor="#0a818e" HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="true" Font-Size="Smaller" GridLines="None" AllowPaging="true" PageSize="10" OnRowDataBound="OnRowDataBound">
            <Columns>
                <asp:TemplateField ItemStyle-Width="50px" HeaderText="">
                    <ItemTemplate>
                        <div id ="new" style="padding:0px;" class="col-lg-12 col-xs-12">
                            <!-- small box -->
                            <div class='<%#if(Eval("stsDescription").ToString()="ASSIGNED","small-box bg-aqua","small-box")%>'>
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
                                <div class="icon"  style="top: 10px;">
                                    <b><%#Eval("stsDescription").ToString()%></b>
                                </div>
                                <div class="small-box-footer">
                                    <asp:LinkButton ID="lbUnduh" runat="server" CssClass="btn btn-link" style="padding:0 6px;" ForeColor="White" CommandName="getFileTiket" CommandArgument='<%# Eval("AttachFile") %>' Visible='<%# If(Eval("AttachFile").ToString() = "", "false", "True")%>'><i class="fa fa-download"></i></asp:LinkButton>
                                    <asp:LinkButton ID="lbFollow" runat="server" CssClass="btn btn-link" style='<%# "padding:0 6px; color:" + If(Eval("coment").ToString() = "1", "Red", "White") + ";" %>' CommandName="getTiketid" CommandArgument='<%# Eval("TicketNo") %>'><i class="fa fa-envelope"></i></asp:LinkButton>
                                </div>
                            </div>
                        </div><!-- ./col -->
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField ItemStyle-Width="80px" DataField="stsDescription" Visible="false" HeaderText="Status" />
             </Columns>
             <pagerstyle cssclass="pagination-ys">
             </pagerstyle>
        </asp:GridView>
</asp:Content>

