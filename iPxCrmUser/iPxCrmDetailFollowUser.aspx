<%@ Page Language="VB" MasterPageFile="~/iPxCrmUser/iPxMasterUserUpload.master" AutoEventWireup="false" CodeFile="iPxCrmDetailFollowUser.aspx.vb" Inherits="iPxCrmUser_iPxCrmDetailFollowUser" title="Follow Up" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <style>
        .small-box {
            border-radius: 5px;
            position: relative;
            display: block;
            margin-bottom: 20px;
            box-shadow: 0 1px 1px rgba(0,0,0,0.1);
            color:White;
        }
        .bg-aqua {
            background-color: #36b3c1 !important;
        }
        .inner {
            padding: 10px;
        }
        .small-box h3 {
            font-size: 18px;
            font-weight: bold;
            margin: 0 0 10px 0;
            white-space: nowrap;
            padding: 0;
        }
        .small-box p {
            font-size: 15px;
        }
        .small-box .icon {
            -webkit-transition: all .3s linear;
            -o-transition: all .3s linear;
            transition: all .3s linear;
            position: absolute;
            top: -20px;
            right: 13px;
            z-index: 0;
            font-size: 85px;
            color: rgba(0,0,0,0.15);
        }
        .small-box-footer {
            position: relative;
            text-align: left;
            padding: 3px 10px;
            color: #fff;
            color: #000000;
            display: block;
            z-index: 10;
            background: #f8f8f8;
            text-decoration: none;
        }
        <%--=======================================================--%>
        .chat
        {
            list-style: none;
            margin: 0;
            padding: 0;
        }

        .chat li
        {
            margin-bottom: 10px;
            padding-bottom: 5px;
            border-bottom: 1px dotted #B3A9A9;
        }

        .chat li.left .chat-body
        {
            margin-left: 60px;
        }

        .chat li.right .chat-body
        {
            margin-right: 60px;
        }

        .chat li .chat-body p
        {
            margin: 0;
            color: #777777;
        }
        
        .panel .slidedown .glyphicon, .chat .glyphicon
        {
            margin-right: 5px;
        }
    </style>
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <!-- Menu Modal-->
      <div id="formMenu-modal" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-lg">
          <div class="modal-content">
            <div class="modal-header">
              <h4 id="H2" class="modal-title"><asp:Label ID="lbJudulDetail" runat="server" Text=""></asp:Label></h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                          <label for="usr">Ticket No:</label>
                            <asp:TextBox ID="tbTicketno" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="form-group">
                          <label for="usr">Hotel Name:</label>
                            <asp:TextBox ID="tbHotelName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="form-group">
                          <label for="usr">Contact:</label>
                            <asp:TextBox ID="tbContact" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="usr">Date:</label><font color=red>*</font>
                            <div class="input-group date datepicker" style="padding:0;">
                                 <asp:TextBox ID="tbDate" runat="server" CssClass ="form-control" placeholder="dd-MM-yyyy"></asp:TextBox>
                                 <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <asp:Panel ID="pnDetail" runat="server">
                            <div class="form-group">
                              <label for="usr">Product Group:</label>
                                <asp:TextBox ID="tbProductGrp" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="form-group">
                              <label for="usr">Product Name:</label>
                                <asp:TextBox ID="tbProduct" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="form-group">
                              <label for="usr">Menu:</label>
                                <asp:TextBox ID="tbMenu" runat="server" CssClass ="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="form-group">
                              <label for="usr">Sub Menu:</label>
                                <asp:TextBox ID="tbSubMenu" runat="server" CssClass = "form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnEdit" runat="server">
                            <div class="form-group">
                                <label for="usr">Product Group:</label><font color=red>*</font>
                                <asp:DropDownList ID="dlPrdGrp" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="dlPrdGrp_SelectedIndexChanged">
                                </asp:DropDownList>  
                            </div>
                            <div class="form-group">
                                <label for="usr">Product Name:</label><font color=red>*</font>
                                <asp:DropDownList ID="dlProduct" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="dlProduct_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="usr">Menu:</label><font color=red>*</font>
                                <asp:DropDownList ID="dlMenu" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="dlMenu_SelectedIndexChanged">
                                </asp:DropDownList>  
                            </div>
                            <div class="form-group">
                                <label for="usr">Sub Menu:</label><font color=red>*</font>
                                <asp:DropDownList ID="dlSubMenu" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group">
                          <label for="usr">From:</label>
                            <asp:TextBox ID="tbFrom" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                          <label for="usr">Subject:</label>
                            <asp:TextBox ID="tbSubject" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group">
                          <label for="usr">Description:</label>
                            <asp:TextBox ID="tbDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Height="125px"></asp:TextBox>
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
                    <asp:LinkButton Width ="150px" ID="lbStart" CssClass ="btn btn-default" runat="server"><i class="fa fa-send"></i> Start Follow Up</asp:LinkButton>
                    <asp:LinkButton Width ="150px" ID="lbAbortStart" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
                </p>
            </div>
          </div>
        </div>
      </div>
      <!-- Menu modal end-->
        <!-- Login Modal-->
      <div id="login-modal" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-lg">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" data-dismiss="modal" aria-label="Close" class="close"><span aria-hidden="true">×</span></button>
              <h4 id="login-modalLabel" class="modal-title">Follow Up</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                          <label for="usr">No :</label>
                            <asp:TextBox ID="tbNo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="form-group">
                          <label for="usr">By :</label><font color=red>*</font>
                            <asp:TextBox ID="tbBy" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                          <label for="usr">Status :</label>
                            <asp:DropDownList ID="dlStatus" runat="server" CssClass="form-control" >
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                          <label for="usr">Link :</label>
                            <div class="input-group">
                                <asp:TextBox ID="tbLink" runat="server" CssClass="form-control" ></asp:TextBox>
                                <div class="input-group-btn">
                                    <asp:LinkButton ID="lbFindSOP" class="btn btn-default" runat="server" Font-Size="Small"><span class="glyphicon glyphicon-paperclip" style="height:20px;"></span> </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div class="form-group">
                          <label for="usr">Note :</label><font color=red>*</font>
                            <asp:TextBox ID="tbNote" runat="server" CssClass="form-control" TextMode="MultiLine" Height="125px"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <p class="text-center">
                    <asp:LinkButton Width="150px" ID="lbSave" CssClass ="btn btn-default" runat="server"><i class="fa fa-send"></i> Send</asp:LinkButton>
                    <asp:LinkButton Width="150px" ID="lbAbort1" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
                </p>
            </div>
          </div>
        </div>
      </div>
      <!-- Login modal end-->
      <!-- Form SOP Modal-->
      <div id="FormSOP-modal" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-md">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" data-dismiss="modal" aria-label="Close" class="close"><span aria-hidden="true">×</span></button>
              <h4 id="H6" class="modal-title">List SOP Link</h4>
            </div>
            <div class="modal-body">
                <asp:GridView EmptyDataText="No records has been added." ID="gvSOP" runat="server" AutoGenerateColumns="false" CssClass="table" HeaderStyle-BackColor="#36b3c1" HeaderStyle-ForeColor="White" Font-Size="Smaller" GridLines="None">
                    <Columns>
                        <%--<asp:BoundField ItemStyle-Width="100px" DataField="PrdDescription" HeaderText="Group" />
                        <asp:BoundField ItemStyle-Width="150px" DataField="ProductName" HeaderText="Product" />
                        <asp:BoundField ItemStyle-Width="150px" DataField="MenuName" HeaderText="Menu" />--%>
                        <asp:BoundField ItemStyle-Width="150px" DataField="SopID" HeaderText="SOP ID"/>
                        <asp:BoundField DataField="link" HeaderText="SOP Link" />
                        <asp:TemplateField ItemStyle-Width="50px" HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbEditSOP" CssClass="btn btn-default" runat="server" CommandName="getEdit" CommandArgument='<%# Eval("SopID") %>'><i class="fa fa-check"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div class="form-group center">
                  <div class="form-group">
                    <asp:LinkButton width="150px" ID="lbAbortSOP" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
                  </div>
                </div>            
            </div>
          </div>
        </div>
      </div>
      <!-- Form SOP modal end-->
      <!-- Form Edit Status Modal-->
      <div id="formsub-modal" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-sm">
          <div class="modal-content">
            <div class="modal-header">
              <button type="button" data-dismiss="modal" aria-label="Close" class="close"><span aria-hidden="true">×</span></button>
              <h4 id="H1" class="modal-title">Update 
                  <asp:Label ID="lbJudul" runat="server" Text="Label"></asp:Label></h4>
            </div>
            <div class="modal-body">
                <div class="form-group">
                    <label for="usr">Status:</label>
                    <asp:DropDownList ID="dlUpStatus" CssClass="form-control" runat="server">
                    </asp:DropDownList>
                </div>
                <div class="form-group center">
                  <div class="form-group">
                    <%--<asp:LinkButton width=125px CssClass="btn btn-default dropdown-toggle" data- ID="LinkButton2" runat="server"><span class="fa fa-filter "></span> Query</asp:LinkButton>--%>
                    <asp:LinkButton width="100px" ID="lbSaveupStatus" CssClass ="btn btn-default" runat="server"><i class="fa fa-save"></i> Save</asp:LinkButton>
                    <asp:LinkButton width="100px" ID="lbAbortupStatus" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
                  </div>
                </div>            
            </div>
          </div>
        </div>
      </div>
      <!-- Form Edit Status modal end-->
        <%--<div class="row">
          <div class="col-sm-4">--%>
            <div id ="new" class="col-lg-12 col-xs-12" style="padding:0px;">
                <!-- small box -->
                <div class="small-box bg-aqua">
                    <div class="inner">
                        <b><asp:Label ID="lbNamaHotel" runat="server" Text=""></asp:Label> (<asp:Label ID="lbTicketNo" runat="server" Text=""></asp:Label>)</b> <asp:LinkButton ID="lbEditAssigned" CssClass ="btn" runat="server"><i class="fa fa-pencil"></i></asp:LinkButton> <br />
                        <asp:Label ID="lbTgl" runat="server" Text=""></asp:Label><br />
                        <asp:Label ID="lbProduct" runat="server" Text=""></asp:Label> -> <asp:Label ID="lbMenu" runat="server" Text=""></asp:Label> -> <asp:Label ID="lbSubMenu" runat="server" Text=""></asp:Label> <asp:Label ID="lbLink" runat="server" Text=""></asp:Label><br />
                        Case : <asp:Label ID="lbCase" runat="server" Text="" Font-Bold="True"></asp:Label><br />
                        Support by : <asp:Label ID="lbSuportby" runat="server" Text=""></asp:Label>
                        <hr style ="margin-top :2px; margin-bottom :2px;">
                        Subject : <asp:Label ID="lbSubject" runat="server" Text="" Font-Bold="True"></asp:Label><br />
                        <asp:Label ID="lbDescription" runat="server" Text=""></asp:Label>
                                    
                    </div>
                    <div class="icon">
                        <asp:Label ID="lbStatus" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="small-box-footer">
                        <br />
                        <asp:Repeater ID="rptFollow" runat="server">
                            <HeaderTemplate>
                              <thead>
                                <table cellspacing="0" class="table">
                                    
                              </thead>      
                            </HeaderTemplate>
                            <ItemTemplate>
                              <tbody>
                                <tr bgcolor='<%# If(Eval("FollowUpCode").ToString() = "H", "#F8F8F8", "#ddd") %>'>
                                    <ul class="chat">
                                        <li class='<%# If(Eval("FollowUpCode").ToString() = "H", "right clearfix", "left clearfix") %>'><span class='<%# If(Eval("FollowUpCode").ToString() = "H", "chat-img pull-right", "chat-img pull-left") %>'>
                                            <img src='<%# If(Eval("FollowUpCode").ToString() = "H", "http://placehold.it/50/FA6F57/fff&text=H", "http://placehold.it/50/55C1E7/fff&text=P") %>' alt="User Avatar" class="img-circle" />
                                        </span>
                                            <div class="chat-body clearfix">
                                                <div class="header">
                                                    <strong class=<%# If(Eval("FollowUpCode").ToString() = "H", "pull-right primary-font", "primary-font") %>><%# Eval("FollowUpBy") %> (<%# Eval("FollowUpNo") %>)</strong> <small class='<%# If(Eval("FollowUpCode").ToString() = "H", " text-muted", "pull-right text-muted") %>'>
                                                    <span class="glyphicon glyphicon-time"></span><%# format( Eval("FollowUpDate"),"dd-MM-yyyy hh:mm:ss") %></small>
                                                </div>
                                                <p>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("FollowUpSopLink") %>' Visible="false"></asp:Label>
                                                    <%# Eval("FollowUpNote").ToString().Replace(vbLf, "<br />") %><br />
                                                    <a target="_blank" href='<%#If(Eval("FollowUpSopLink").ToString() <> "", Eval("FollowUpSopLink"), "")%>' class="text-primary">
                                                        <asp:Label visible='<%# If(Eval("FollowUpCode").ToString() = "P", "true", "false") %>' ID="Label2" runat="server" Text='<%#If(Eval("FollowUpSopLink").ToString() <> "", "Open Link <br />", "<br/>")%>'></asp:Label>
                                                    </a>
                                                    <asp:LinkButton ID="LinkButton4" CssClass="btn-link" runat="server" OnClick="GetValue"  visible='<%# If(Eval("FollowUpCode").ToString() = "H", "true", "false") %>'><%#If(Eval("FollowUpSopLink").ToString() <> "", "<i class='fa fa-paperclip'></i> Unduh File", "<br/>")%></asp:LinkButton>
                                                </p>
                                            </div>
                                        </li>
                                    </ul>
                                </tr>
                              </tbody>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <p class="text-left">
        <asp:LinkButton Width="150px" ID="lbAddFollow" CssClass ="btn btn-default" runat="server"><i class="fa fa-plus"></i> Add Follow</asp:LinkButton>
        <asp:LinkButton Width="150px" ID="lbAbort" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
        <asp:LinkButton Width="150px" ID="lbUpStatus" CssClass ="btn btn-default" runat="server"><i class="fa fa-cogs"></i> Update Status</asp:LinkButton>
    </p>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>

