<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/iPxCrmCustomer/iPxCrmCustomer.master" CodeFile="warningmsg.aspx.vb" Inherits="iPxCrmCustomer_warningmsg" Title="Message" %>

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
   
        $('#getuser').modal({backdrop:'static',
        keyboard: false},'show');
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
    <script type="text/javascript">
        function hideDisplayBlock() {
            $('#getuser').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }
</script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="container">
  <!-- Modal -->
  <div class="modal fade"  id="getuser" role="dialog">
    <div class="modal-dialog modal-lg" style=" width:550px;">
      <div class="modal-content">
        <div class="modal-header">
         

        </div>

        <div class="modal-body">
             
       
      <asp:Panel class="" ID="confirmationstep1" runat="server" >
     <div class="lock-wrapper"> 
      <div class=" lock-box">
     
           <h3> <p class="text-center"><span class="img-circle fa fa-info-circle"></span> Message</p></h3>
            <br />
            <br />
               <%  Dim aMsg() As String
                   Dim i As Integer
                   aMsg = Split(Session("sMessage"), "|")
                   For i = 0 To UBound(aMsg) - 1
                       Response.Write("<h4 class=""center"">" & aMsg(i) & "</h4>")
                   Next
               %>
         </div>
       </div> 
    </asp:Panel>  
    
    <asp:Panel class="" ID="confirmationfooteryesno" runat="server" >
    <div class="lock-wrapper">
    <div class=" lock-box">
    <div class="row" >
    <div class="form-group col-md-12 col-sm-12 col-xs-12">
             <asp:Button ID="btnYes" runat="server" class="btn btn-block btn-warning " 
                Text="Yes" />                         
            <asp:Button ID="btnNo" runat="server" class="btn btn-block btn-warning" 
                Text="No" />
    </div>
    </div> 
    </div>
    </div> 
    </asp:Panel>  
    
     <asp:Panel class="" ID="confirmationfooterokonly" runat="server" style="overflow:auto;">
    <div class="lock-wrapper">
    <div class=" lock-box">
    <div class="row" >
    <div class="form-group col-md-12 col-sm-12 col-xs-12">
 
            <asp:Button ID="btnOkonly" runat="server" class="btn btn-block btn-warning" 
                Text="OK" />
    </div>
    </div> 
    </div>
    </div> 
    </asp:Panel>  
        </div>
        <div class="modal-footer">

        

        </div>
      </div>
    </div>
  </div>
</div>
 
 </asp:Content>
