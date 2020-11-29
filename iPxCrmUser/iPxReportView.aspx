<%@ Page Language="VB" MasterPageFile="~/iPxCrmUser/iPxMasterUser.master" AutoEventWireup="false" CodeFile="iPxReportView.aspx.vb" Inherits="iPxCrmUser_iPxReportView" title="Pyxis Support E-Ticket" %>
<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
   <div class="col-sm-12">  
        <asp:Panel ID="Panel1" BackColor="White" runat="server" >
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" DisplayGroupTree="False" HasCrystalLogo="False" />
        </asp:Panel>
   </div>
</asp:Content>

<asp:content id="Content4" contentplaceholderid="ContentPlaceHolder2" runat="Server">
<div id="footer">
    <div style="padding-left:20px">
        <asp:linkbutton width="150px" cssclass="btn btn-default" PostBackUrl="iPxReportList.aspx"  data- id="btnCxld" runat="server"><span class="fa fa-close"></span> Abort</asp:linkbutton>
    </div>
</div>
</asp:content>