<%@ Page Language="VB" MasterPageFile="~/iPxCrmUser/iPxMasterUser.master" AutoEventWireup="false" CodeFile="findCustomer.aspx.vb" Inherits="iPxCrmUser_findCustomer" title="Untitled Page" %>

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
    <div class="modal-dialog modal-lg" style=" width:630px;">
      <div class="modal-content">
        <div class="modal-header">
         
         <h4>List Hotel Name</h4>
        </div>
        <div class="modal-body">
            <div align="center" style="top: auto; width : 600px;">
                <div class="input-group">
                    <asp:TextBox ID="tbQueryHotel" runat="server" CssClass="form-control" placeholder="Hotel Name..."></asp:TextBox>
                    <div class="input-group-btn">
                        <asp:LinkButton ID="lbQuery" runat="server" CssClass="btn btn-default">
                        <i style="font-size:20px;" class="glyphicon glyphicon-search"></i>
                        </asp:LinkButton>
                    </div>
                </div>
                <br />
                <asp:GridView EmptyDataText="No records has been added." ID="gvCust" runat="server" AutoGenerateColumns="false" CssClass="table" HeaderStyle-BackColor="#0a818e" HeaderStyle-ForeColor="White" Font-Size="Smaller" GridLines="None"  AllowPaging="true" PageSize="10">
                    <Columns>
                        <asp:BoundField ItemStyle-Width="50px" DataField="CustID" HeaderText="Cust ID" />
                        <asp:BoundField ItemStyle-Width="100px" DataField="GrpName" HeaderText="Cust Group" />
                        <asp:BoundField ItemStyle-Width="120px" DataField="CustName" HeaderText="Hotel Name" />
                        <asp:TemplateField ItemStyle-Width="50px" HeaderText="Select" HeaderStyle-HorizontalAlign ="Center" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" CssClass="btn btn-default" runat="server" CommandName="getHotel" CommandArgument='<%# Eval("CustID") %>'><i class="fa fa-check"></i></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <pagerstyle cssclass="pagination-ys">
                    </pagerstyle>
                </asp:GridView> 
            </div>
        <div class="modal-footer">
            <div class="form-group center">
                <asp:LinkButton width=125px CssClass="btn btn-default dropdown-toggle" data- ID="btnExit" runat="server"><span class="fa fa-close"></span> Abort</asp:LinkButton> 
            </div>
        </div>
      </div>
    </div>
  </div>
</div>
 
 </asp:Content>



