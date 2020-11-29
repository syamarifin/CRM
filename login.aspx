<%@ Page Language="VB" AutoEventWireup="false" CodeFile="login.aspx.vb" Inherits="login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CRM Login Page</title>
    <meta name="description" content="Responsive Admin Web App">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0">

    <!-- Needs images, font... therefore can not be part of main.css -->
    <link rel="stylesheet" href="assets/css/font-awesome.css">
    <link rel="stylesheet" href="assets/css/weather-icons.css">

    <link rel="stylesheet" href="assets/css/main.css"/>
    <script src="assets/js/jquery.js" type="text/javascript"></script>
    <script src="assets/js/bootstrap.min.js" type="text/javascript"></script>
    
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
    <%--<link href="assets/css/bootstrap.css" rel="stylesheet" />--%>
    <script type="text/javascript">
        function hideModalAdd() {
            $('#login-modal').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }
        function showModalAdd() {
            $('#login-modal').modal('show');
        }
    </script>
</head>
<body style ="min-height: 90%;background-image:url(assets/img/4k-happy.png); background-size: 100% 100%; background-attachment:fixed;">
    <form id="form1" runat="server">
     <div class="lock-wrapper">
      <div class="panel lock-box" style="border-radius: 15px;">
        <div class="center"> 
            <img alt="" src="assets/img/logo3.png" />
          </div>
        <h4> PYXIS SUPPORT E-TICKET </h4>
        <p class="text-center">Please login to Access your Account</p>
        <div class="row">
          <form action="index.html" class="form-inline" role="form">
            <div class="form-group col-md-12 col-sm-12 col-xs-12">
              <div class="iconic-input right">
                <label for="signupInputEmail" class="text-muted">Email</label>
                  
                  <asp:TextBox ID="txtUsername" runat="server"  placeholder="Enter Username or Email id" type="email" class="form-control padding-horizontal-15"  >  </asp:TextBox> <i class="fa fa-envelope"></i> 
             
                <i class="fa fa-envelope"></i> 
                </div>
              <div class="iconic-input right">
                <label for="signupInputPassword" class="text-muted">Password</label>
                <asp:TextBox ID="txtPassword" runat="server"  type="password" 
                      placeholder="Password" class="form-control lock-input" 
                      TextMode="Password"></asp:TextBox>
               
                <i class="fa fa-lock"></i> </div>
              <div class="square-blue pull-left pv-15">
                <label class="ui-checkbox">
                  <%--<input name="checkbox1" id="checkbox1" type="checkbox" value="option1">--%>
                  <asp:CheckBox ID="CheckBox1" runat="server" />
                  <span> <strong> Remember Me </strong> </span> </label>
              </div>
                <asp:Button ID="submit" runat="server" Text="Sign In" class="btn btn-block btn-primary" />
                   <%-- <asp:Button ID="btnSignup" runat="server" Text="Sign Up" class="btn btn-block btn-primary" />--%>
                    <asp:Button ID="btnRequestPassword" runat="server" Text="Request Password" class="btn btn-block btn-primary" />
             
            </div>
          </form>
        </div>
      </div>
    </form>
    
</body>
</html>
