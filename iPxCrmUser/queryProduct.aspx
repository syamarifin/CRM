﻿<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/iPxCrmUser/iPxMasterUser.master" CodeFile="queryProduct.aspx.vb" title="Customer Relationship Management" Inherits="iPxCrmUser_queryProduct" %>


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
        $('.modal-backdrop').hide()
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
    <div class="modal-dialog modal-lg" style=" width:600px;">
      <div class="modal-content">
        <div class="modal-header">
         
         <h4>Query Transaction</h4>
        </div>
        <div class="modal-body">
           <div align="center" style="top: auto; width : 600px;">

            <label for="" style="text-align:right;"  class="col-sm-4">Product Group</label>
            <div class="col-sm-6">
               	<asp:DropDownList class="form-control padding-horizontal-15" id="dlProductGrp" runat="server">
               	</asp:DropDownList>
               </div>         
            <br />
            <br />
     
           <label for="" style="text-align:right;" class="col-sm-4">Product ID</label>
           <div class="col-sm-6">
               <asp:TextBox class="form-control padding-horizontal-15" ID="tbPrdID"  runat="server"></asp:TextBox>
            </div>         
            <br />
            <br />      
                    
           <label for="" style="text-align:right;" class="col-sm-4">Product Name</label>
           <div class="col-sm-6">
               <asp:TextBox class="form-control padding-horizontal-15" ID="tbPrdName"  runat="server"></asp:TextBox>
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

