<%@ Page Language="VB" MasterPageFile="~/iPxCrmMobileUser/iPxMobileOprBck.master" AutoEventWireup="false" CodeFile="queryTicketUser.aspx.vb" Inherits="iPxCrmMobileUser_queryTicketUser" title="Untitled Page" %>

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
         
         <h4>Query Transaction</h4>
        </div>
        <div class="modal-body">
           <div align="center" style="top: auto;">
     
           <label for="" style="text-align:right;" class="col-sm-4">E-Ticket No</label>
           <div class="col-sm-6">
               <asp:TextBox class="form-control padding-horizontal-15" ID="txtmemID"  runat="server"></asp:TextBox>
            </div>         
            <br />     
                    
           <label for="" style="text-align:right;" class="col-sm-4">Hotel Name</label>
           <div class="col-sm-6">
               <asp:TextBox class="form-control padding-horizontal-15" ID="txtQueryGuestName"  runat="server"></asp:TextBox>
            </div>         
            <br />      
                    
           <label for="" style="text-align:right;" class="col-sm-4">E-Ticket From</label>
           <div class="col-sm-6">
               <asp:TextBox class="form-control padding-horizontal-15" ID="txtFrom"  runat="server"></asp:TextBox>
            </div>         
            <br />   
                    
           <label for="" style="text-align:right;" class="col-sm-4">Subject</label>
           <div class="col-sm-6">
               <asp:TextBox class="form-control padding-horizontal-15" ID="txtSubject"  runat="server"></asp:TextBox>
            </div>
            <br />


            <label for="" style="text-align:right;" class="col-sm-4">Trans. Date</label>
            <div class="col-sm-6">
                <div class="input-group date datepicker" style="padding:0px;">
                    <asp:TextBox ID="txtTransDate" runat="server" CssClass ="form-control " placeholder="dd-MM-yyyy"></asp:TextBox>
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                </div>
            </div>      
            <br />

            <label for="" style="text-align:right;"  class="col-sm-4">Product</label>
            <div class="col-sm-6">
                <asp:TextBox ID="dlProduct" runat="server" class="form-control padding-horizontal-15" ></asp:TextBox>
               </div>         
            <br />

            <label for="" style="text-align:right;"  class="col-sm-4">Menu</label>
            <div class="col-sm-6">
               	<%--<asp:DropDownList class="form-control padding-horizontal-15" id="dlMenu" AutoPostBack="true" OnSelectedIndexChanged="dlMenu_SelectedIndexChanged" runat="server">
               	</asp:DropDownList>--%>
               	<asp:TextBox ID="dlMenu" runat="server" class="form-control padding-horizontal-15" ></asp:TextBox>
               </div>         
            <br />

            <label for="" style="text-align:right;"  class="col-sm-4">Sub Menu</label>
            <div class="col-sm-6">
               	<%--<asp:DropDownList class="form-control padding-horizontal-15" id="dlSubMenu"  runat="server">
               	</asp:DropDownList>--%>
               	<asp:TextBox ID="dlSubMenu" runat="server" class="form-control padding-horizontal-15" ></asp:TextBox>
               </div>         
            <br />

               <hr style="width:300px" />
               <label for="" style="text-align:center; font-size:medium" class="col-sm-10">By Periode</label>
                <br />

            <label for="" style="text-align:right;" class="col-sm-4">Peroide From</label>
            <div class="col-sm-6">
                <div class="input-group date datepicker" style="padding:0px;">
                    <asp:TextBox ID="txtPerFrom" runat="server" CssClass ="form-control " placeholder="dd-MM-yyyy"></asp:TextBox>
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                </div>
            </div>   
                  
            <br />

            <label for="" style="text-align:right;" class="col-sm-4">Peroide Until</label>
            <div class="col-sm-6">
                <div class="input-group date datepicker" style="padding:0px;">
                    <asp:TextBox ID="txtPerUntl" runat="server" CssClass ="form-control " placeholder="dd-MM-yyyy"></asp:TextBox>
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                </div>
            </div>         
            <br />
             
        </div>

            


        <div class="modal-footer">
 <div class="form-group center">
    <div class="form-group">  
    <label for="" class="col-sm-1"></label>
    <div class="row">
        <div class="col-sm-6">
            <asp:LinkButton width=125px CssClass="btn btn-default dropdown-toggle" data- ID="btnLogin" runat="server"><span class="fa fa-filter "></span> Query</asp:LinkButton>    
        </div>
        <div class="col-sm-6">
            <asp:LinkButton width=125px CssClass="btn btn-default dropdown-toggle" data- ID="btnExit" runat="server"><span class="fa fa-close"></span> Abort</asp:LinkButton> 
        </div>
    </div>
    </div>  
     </div>         
        

        </div>
      </div>
    </div>
  </div>
</div>
 
     </div>
</asp:Content>



