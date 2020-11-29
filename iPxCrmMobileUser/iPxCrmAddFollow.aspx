<%@ Page Language="VB" MasterPageFile="~/iPxCrmMobileUser/iPxMobileOprBck.master" AutoEventWireup="false" CodeFile="iPxCrmAddFollow.aspx.vb" Inherits="iPxCrmMobileUser_iPxCrmAddFollow" title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
         *
{
padding: 0;
        margin-left: 0px;
        margin-right: 0px;
        margin-top: 0px;
        z-index:auto; 
    }
                  body
        {
            font-family: Arial;
            font-size: 10pt;
           
        }
        td
        {
            cursor: pointer;
        }
        .hover_row
        {
            background-color: #FFFFBF;
        }
         .selected_row td
        {
            background-color: #A1DCF2;
        }
    #step1
{
top:0%;
height: 100%;
width :100%;
left :0%;
position:fixed ;
z-index:999;
}
</style> 

<script type="text/javascript">

//    $(window).load(function() {
//        hideModal();
//        $('#FormSOP-modal').modal({ backdrop: 'static',
//            keyboard: false
//        }, 'show');

//    });

    function hideModal() {
        $('#getuser').modal('hide');
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
    }    
    function openModal() {
        $('#getuser').modal({ backdrop: 'static',
            keyboard: false
        }, 'show');
    }
    function hideFormSOP() {
        $('#FormSOP-modal').modal('hide');
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
    }
    function showFormSOP() {
        $('#FormSOP-modal').modal({ backdrop: 'static',
            keyboard: false
        }, 'show');
    }
    
 
</script>
<script type="text/javascript">
//    function clearModal() {
//        $('#getuser').modal({ backdrop: 'static',
//            keyboard: false
//        }, 'hide');
//        $('.modal-backdrop').hide();
//        $('#getuser').modal({ backdrop: 'static',
//            keyboard: false
//        }, 'show');
//    }    
</script>
</asp:Content>



  
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<div class="container">
    <!-- Form SOP Modal-->
      <div id="FormSOP-modal" tabindex="-1" role="dialog" aria-labelledby="login-modalLabel" aria-hidden="true" class="modal fade">
        <div role="document" class="modal-dialog modal-sm">
          <div class="modal-content">
            <div class="modal-header">
              <h4 id="H6" class="modal-title">List SOP Link</h4>
            </div>
            <div class="modal-body">
                <asp:GridView EmptyDataText="No records has been added." ID="gvSOP" runat="server" AutoGenerateColumns="false" CssClass="table" HeaderStyle-BackColor="#36b3c1" HeaderStyle-ForeColor="White" Font-Size="Smaller" GridLines="None">
                    <Columns>
                        <%--<asp:BoundField ItemStyle-Width="100px" DataField="PrdDescription" HeaderText="Group" />
                        <asp:BoundField ItemStyle-Width="150px" DataField="ProductName" HeaderText="Product" />
                        <asp:BoundField ItemStyle-Width="150px" DataField="MenuName" HeaderText="Menu" />
                        <asp:BoundField DataField="SopID" HeaderText="SOP ID"/>--%>
                        <asp:BoundField ItemStyle-Width="150px" ItemStyle-Font-Size="XX-Small" HeaderStyle-Font-Size="XX-Small" DataField="link" HeaderText="SOP Link" />
                        <asp:TemplateField ItemStyle-Width="50px" ItemStyle-Font-Size="XX-Small" HeaderStyle-Font-Size="XX-Small" HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbEditSOP" CssClass="btn btn-default" runat="server" Font-Size="XX-Small" CommandName="getEdit" CommandArgument='<%# Eval("SopID") %>'><small><i class="fa fa-check"></i></small></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div class="form-group center">
                  <div class="form-group">
                    <asp:LinkButton width="100px" ID="lbAbortSOP" CssClass ="btn btn-default" runat="server"><small><i class="fa fa-close"></i> Abort</small></asp:LinkButton>
                  </div>
                </div>            
            </div>
          </div>
        </div>
      </div>
      <!-- Form SOP modal end-->
      
      <!-- Modal -->
      <div class="modal fade" id="getuser" role="dialog">
        <div class="modal-dialog modal-lg">
          <div class="modal-content">
            <div class="modal-header">
             
             <h4>Follow Up</h4>
            </div>
            <div class="modal-body">
                <div class="row" style="margin-left:-15px;">
                        <div class="col-md-6">
                            <div class="form-group">
                              <label for="usr">No :</label>
                                <asp:TextBox ID="tbNo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="form-group">
                              <label for="usr">By :</label>
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
                                        <asp:LinkButton ID="lbFindSOP" class="btn btn-default" style="background-color: #fff; color: #607D8B;" runat="server" Font-Size="Small"><span class="glyphicon glyphicon-paperclip" style="height:20px;"></span> </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div class="form-group">
                              <label for="usr">Note :</label><font color=red>*</font>
                                <asp:TextBox ID="tbNote" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
            <div class="modal-footer">
                <div class="form-group center">
                    <asp:LinkButton Width="100px" ID="lbSave" CssClass ="btn btn-default" runat="server"><i class="fa fa-send"></i> Send</asp:LinkButton>
                    <asp:LinkButton Width="100px" ID="lbAbort1" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
                </div>
            </div>
          </div>
        </div>
      </div>
</div>
 
     </div>
</asp:Content>



