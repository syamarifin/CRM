<%@ Page Language="VB" MasterPageFile="~/iPxCrmMobileUser/iPxMobileMain.master" AutoEventWireup="false" CodeFile="SignIn.aspx.vb" Inherits="iPxCrmMobileUser_SignIn" title="CRM Mobile Login Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="lock-wrapper">
      <div class="panel lock-box">
        <div class="center"> 
            <img alt="" src="../assets/img/logo3.png" />
          </div>
        <h4> ALCOR CUSTOMER RELATIONSHIP MANAGEMENT </h4>
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
                  <%--<input name="checkbox1" type="checkbox" value="option1">--%>
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
</asp:Content>

