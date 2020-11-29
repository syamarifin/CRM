<%@ Page Language="VB" MasterPageFile="~/iPxCrmMobileUser/iPxMobileOprBck.master" AutoEventWireup="false" CodeFile="iPxCrmStartAssign.aspx.vb" Inherits="iPxCrmMobileUser_iPxCrmStartAssign" title="Untitled Page" %>

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

    $(window).load(function() {
        hideModal();
        $('#getuser').modal({backdrop:'static',
            keyboard: false
        }, 'show');
       
    });

     
    function openModal() {
        $('#getuser').modal({ backdrop: 'static',
            keyboard: false
        }, 'show');
    }

 
</script>
<script type="text/javascript">
     function hideModal() {
         $('#getuser').modal({ backdrop: 'static',
             keyboard: false
         }, 'hide');
         $('.modal-backdrop').hide()
         $(document).ready(function() {
             $(".datepicker").datepicker({ format: 'dd/mm/yyyy', autoclose: true, todayBtn: 'linked' })
         });
    }    
</script>
<script type="text/javascript">
    function clearModal() {
        $('#getuser').modal({ backdrop: 'static',
            keyboard: false
        }, 'hide');
        $('.modal-backdrop').hide();
        $('#getuser').modal({ backdrop: 'static',
            keyboard: false
        }, 'show');
    }    
</script>
</asp:Content>



  
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<div class="container">
  <!-- Modal -->
  <div class="modal fade"  id="getuser" role="dialog">
    <div class="modal-dialog modal-lg">
      <div class="modal-content">
        <div class="modal-header">
         
         <h4><asp:Label ID="lbJudulAssign" runat="server" Text="Label"></asp:Label></h4>
        </div>
        <div class="modal-body">
            <div class="row" style="margin-left:-15px;">
                    <div class="col-md-6">
                        <div class="form-group">
                          <label for="usr">Ticket No:</label>
                            <asp:TextBox ID="tbTicketno" runat="server" CssClass="form-control" 
                                ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="form-group">
                          <label for="usr">Hotel Name:</label>
                            <asp:TextBox ID="tbHotelName" runat="server" CssClass="form-control" 
                                ReadOnly="True"></asp:TextBox>
                        </div>
                        <div class="form-group">
                          <label for="usr">Contact:</label>
                            <asp:TextBox ID="tbContact" runat="server" CssClass="form-control" 
                                ReadOnly="True"></asp:TextBox>
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
                            <asp:TextBox ID="tbDescription" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
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
        <div class="modal-footer">
            <div class="form-group center">
                <asp:LinkButton Width ="150px" ID="lbStart" CssClass ="btn btn-default" runat="server"><i class="fa fa-send"></i> Start Follow Up</asp:LinkButton>
            </div>
            <div class="form-group center">
                <asp:LinkButton Width ="150px" ID="lbAbortStart" CssClass ="btn btn-default" runat="server"><i class="fa fa-close"></i> Abort</asp:LinkButton>
            </div>
        </div>
      </div>
    </div>
  </div>
</div>
 
     </div>
</asp:Content>



