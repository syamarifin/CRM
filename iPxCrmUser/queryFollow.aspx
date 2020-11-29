<%@ Page Language="VB" MasterPageFile="~/iPxCrmUser/iPxMasterUser.master" AutoEventWireup="false" CodeFile="queryFollow.aspx.vb" Inherits="iPxCrmUser_queryFollow" title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
         $('.modal-backdrop').hide();
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
<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="container">
  <!-- Modal -->
  <div class="modal fade"  id="getuser" role="dialog">
    <div class="modal-dialog modal-lg" style=" width:500px;">
      <div class="modal-content">
        <div class="modal-header">
         
         <h4>Query Transaction</h4>
        </div>
        <div class="modal-body">
           <div align="center" style="top: auto; width : 500px;">
     
           <label for="" style="text-align:right;" class="col-sm-4">E-Ticket No</label>
           <div class="col-sm-6">
               <asp:TextBox class="form-control padding-horizontal-15" ID="txtmemID"  runat="server"></asp:TextBox>
            </div>         
            <br />
            <br />      
                    
           <label for="" style="text-align:right;" class="col-sm-4">Hotel Name</label>
           <div class="col-sm-6">
               <asp:TextBox class="form-control padding-horizontal-15" ID="txtQueryGuestName"  runat="server"></asp:TextBox>
            </div>         
            <br />
            <br />      
                    
           <label for="" style="text-align:right;" class="col-sm-4">E-Ticket From</label>
           <div class="col-sm-6">
               <asp:TextBox class="form-control padding-horizontal-15" ID="txtFrom"  runat="server"></asp:TextBox>
            </div>         
            <br />
            <br />     
                    
           <label for="" style="text-align:right;" class="col-sm-4">Subject</label>
           <div class="col-sm-6">
               <asp:TextBox class="form-control padding-horizontal-15" ID="txtSubject"  runat="server"></asp:TextBox>
            </div>         
            <br />
            <br />


            <label for="" style="text-align:right;" class="col-sm-4">Trans. Date</label>
            <div class="col-sm-6">
                <div class="input-group date datepicker" style="padding:0px;">
                    <asp:TextBox ID="txtTransDate" runat="server" CssClass ="form-control " placeholder="dd-MM-yyyy"></asp:TextBox>
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                </div>
            </div>      
            <br />
            <br />

            <label for="" style="text-align:right;"  class="col-sm-4">Product Group</label>
            <div class="col-sm-6">
               	<asp:DropDownList class="form-control padding-horizontal-15" id="dlProductGrp" AutoPostBack="true" OnSelectedIndexChanged="dlProduct_SelectedIndexChanged" runat="server">
               	</asp:DropDownList>
               </div>         
            <br />
            <br />

            <label for="" style="text-align:right;"  class="col-sm-4">Product</label>
            <div class="col-sm-6">
               	<asp:DropDownList class="form-control padding-horizontal-15" id="dlProduct" AutoPostBack="true" OnSelectedIndexChanged="dlProduct_SelectedIndexChanged" runat="server">
               	</asp:DropDownList>
               </div>         
            <br />
            <br />

            <label for="" style="text-align:right;"  class="col-sm-4">Menu</label>
            <div class="col-sm-6">
               	<asp:DropDownList class="form-control padding-horizontal-15" id="dlMenu" AutoPostBack="true" OnSelectedIndexChanged="dlMenu_SelectedIndexChanged" runat="server">
               	</asp:DropDownList>
               </div>         
            <br />
            <br />

            <label for="" style="text-align:right;"  class="col-sm-4">Sub Menu</label>
            <div class="col-sm-6">
               	<asp:DropDownList class="form-control padding-horizontal-15" id="dlSubMenu"  runat="server">
               	</asp:DropDownList>
               </div>         
            <br />
            <br />

            <label for="" style="text-align:right;"  class="col-sm-4">Status</label>
            <div class="col-sm-6">
               	<asp:DropDownList class="form-control padding-horizontal-15" id="dlStatus"  runat="server">
               	</asp:DropDownList>
               </div>         
            <br />
            <br />

            <label for="" style="text-align:right;"  class="col-sm-4">Support by</label>
            <div class="col-sm-6">
               	<asp:TextBox class="form-control padding-horizontal-15" ID="tbQSupport"  runat="server"></asp:TextBox>
            </div>         
            <br />
            <br />
            
            <label for="" style="text-align:right;"  class="col-sm-4"></label>
            <div class="col-sm-6" style="text-align:left;">
                <label class="ui-checkbox">
                  <%--<input name="checkbox1" id="checkbox1" type="checkbox" value="option1">--%>
                  <asp:CheckBox ID="cbComment" runat="server" />
                  <span> <strong> Comment </strong> </span> </label>
            </div>         
            <br />
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
            <br />

            <label for="" style="text-align:right;" class="col-sm-4">Peroide Until</label>
            <div class="col-sm-6">
                <div class="input-group date datepicker" style="padding:0px;">
                    <asp:TextBox ID="txtPerUntl" runat="server" CssClass ="form-control " placeholder="dd-MM-yyyy"></asp:TextBox>
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                </div>
            </div>         
            <br />
            <br />
             
        </div>

            


        <div class="modal-footer">
 <div class="form-group center">
    <div class="form-group">  
    <label for="" class="col-sm-1"></label>
              <asp:LinkButton width=125px CssClass="btn btn-default dropdown-toggle" data- ID="btnLogin" runat="server"><span class="fa fa-filter "></span> Query</asp:LinkButton>    
      
      <asp:LinkButton width=125px CssClass="btn btn-default dropdown-toggle" data- ID="btnExit" runat="server"><span class="fa fa-close"></span> Abort</asp:LinkButton> 
      

    </div>  
     </div>         
        

        </div>
      </div>
    </div>
  </div>
</div>
 
</asp:Content>
<%--<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">
</asp:Content>--%>

