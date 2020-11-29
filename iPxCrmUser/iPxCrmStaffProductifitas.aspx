<%@ Page Language="VB" MasterPageFile="~/iPxCrmUser/iPxMasterUser.master" AutoEventWireup="false" CodeFile="iPxCrmStaffProductifitas.aspx.vb" Inherits="iPxCrmUser_iPxCrmStaffProductifitas" title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function datetimepicker1() {
            $(".datepicker1").datepicker({ format: 'dd MM yyyy', autoclose: true, todayBtn: 'linked' })
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-lg-2" style="text-align:right;">
            <label for="usr">From Date:</label>
        </div>
        <div class="col-lg-3">
            <div class="form-group">
                <div class="input-group date datepicker1" style="padding:0;">
                    <asp:TextBox ID="tbFromDate" runat="server" CssClass ="form-control" placeholder="dd MM yyyy"></asp:TextBox>
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                </div>
            </div>
        </div>
        <div class="col-lg-2" style="text-align:right;">
            <label for="usr">Until Date:</label>
        </div>
        <div class="col-lg-3">
            <div class="form-group">
                <div class="input-group date datepicker1" style="padding:0;">
                    <asp:TextBox ID="tbUntilDate" runat="server" CssClass ="form-control" placeholder="dd MM yyyy"></asp:TextBox>
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                </div>
            </div>
        </div>
        <div class="col-lg-2">
            <asp:LinkButton ID="lbView" runat="server" CssClass="btn btn-default" Width="100px"><i class="fa fa-file"></i> View</asp:LinkButton>
        </div>
    </div>
        <asp:GridView EmptyDataText="No records has been added." ID="gvUSer" runat="server" AutoGenerateColumns="false" CssClass="table" HeaderStyle-BackColor="#0a818e" HeaderStyle-ForeColor="White" GridLines="None" AllowPaging="true" PageSize="10">
            <Columns>
                <asp:TemplateField ItemStyle-Width="50px" HeaderText="No" ItemStyle-HorizontalAlign="Center"><ItemTemplate> <%#Container.DataItemIndex+1 %></ItemTemplate></asp:TemplateField>
                <asp:BoundField DataField="name" HeaderText="Name" />
                <asp:BoundField DataField="CreateTicket" HeaderText="Submit Ticket" />
                <asp:BoundField DataField="AssignTicket" HeaderText="Asignment" />
                <asp:BoundField DataField="FollowTicket" HeaderText="Follow Up" />
                <asp:BoundField DataField="OutStandingTicket" HeaderText="Outstanding" />
                <asp:BoundField DataField="DoneTicket" HeaderText="Done" />
                <asp:BoundField DataField="ResolvedTicket" HeaderText="Resolved" />
             </Columns>
             <pagerstyle cssclass="pagination-ys">
             </pagerstyle>
        </asp:GridView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>

