Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Configuration

Imports System.Net.Mail
Imports System.Net
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security

Imports iTextSharp.text
Imports iTextSharp.text.html.simpleparser
Imports iTextSharp.text.pdf

Imports System.Text
Imports System.Web
Imports System.Net.Mime
Imports System.Globalization

Partial Class login
    Inherits System.Web.UI.Page
    Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Dim oCnct As SqlConnection = New SqlConnection(sCnct)
    Dim oSQLCmd As SqlCommand
    Dim oSQLReader As SqlDataReader
    Dim sSQL As String
    Protected Function SendEmail() As Boolean
        Try
            Dim mm As New MailMessage(ConfigurationManager.AppSettings("UserName"), txtUsername.Text)
            Dim body, cBody As String

            cBody = "Hello " + Session("sName") + ","

            cBody += "<br/>Your userid/email account:"

            cBody += "<br/>Email  : <strong>" + txtUsername.Text + "</strong> "
            cBody += "<br/>Password : <strong>" + Session("sPassw") + "</strong>"
            cBody += "<br /><h4><a href = 'crm.alcorsys.com'>Click here to login.</a></h4>"

            cBody += "<br /><br />Thank you for using CRM "
            cBody += "<br /><br /><br /><br />Thanks"
            cBody += "<br /><br /><br /><br />CRM ADMIN"

            body = File.ReadAllText(Server.MapPath("~/iPxEmailThemplate/emailclient.html"))

            body = body.Replace("{ipx_emailbody}", cBody)

            Dim fromEmail As String = ConfigurationManager.AppSettings("UserName")
            mm.[To].Add(txtUsername.Text)
            mm.From = New MailAddress(fromEmail)
            mm.Subject = "CRM user"

            mm.Body = body
            mm.IsBodyHtml = True
            Dim smtp As SmtpClient = New SmtpClient()
            smtp.Host = ConfigurationManager.AppSettings("Host")
            smtp.EnableSsl = False
            Dim NetworkCred As NetworkCredential = New NetworkCredential()
            NetworkCred.UserName = ConfigurationManager.AppSettings("UserName")
            NetworkCred.Password = ConfigurationManager.AppSettings("Password")
            smtp.UseDefaultCredentials = True
            smtp.Credentials = NetworkCred
            smtp.Port = Integer.Parse(ConfigurationManager.AppSettings("Port"))
            smtp.Send(mm)

            '=====================================================
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('Your password has been sent, please check your email !');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('Sorry failed to send email !');document.getElementById('Buttonx').click()", True)
        End Try
    End Function
    Sub findPass()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If

        sSQL = "Select * From CFG_user Where Email = '" & Replace(txtUsername.Text, "'", "''") & "' and IsActive ='" & "Y" & "'"
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        oSQLReader = oSQLCmd.ExecuteReader

        If oSQLReader.Read Then
            
            Session("sPassw") = oSQLReader.Item("passw").ToString.Trim
            Session("sName") = oSQLReader.Item("name").ToString.Trim

            oSQLReader.Close()
            SendEmail()
        Else
            oCnct.Close()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Sorry User Is Not Registered Yet or Not Active !');document.getElementById('Buttonx').click()", True)
        End If
        oCnct.Close()
    End Sub
    Sub findPass1()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If

        sSQL = "Select * From CFG_customerContact Where Email = '" & Replace(txtUsername.Text, "'", "''") & "' and IsActive ='" & "Y" & "'"
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        oSQLReader = oSQLCmd.ExecuteReader

        If oSQLReader.Read Then

            Session("sPassw") = oSQLReader.Item("passw").ToString.Trim
            Session("sName") = oSQLReader.Item("name").ToString.Trim
            oSQLReader.Close()
            SendEmail()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('Your password has been sent, please check your email !');", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Sorry User Is Not Registered Yet or Not Active !');document.getElementById('Buttonx').click()", True)
        End If
        oCnct.Close()
    End Sub
    Sub login()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If

        sSQL = "Select * From CFG_user Where Email = '" & Replace(txtUsername.Text, "'", "''") & "' and isActive = '" & "Y" & "'"
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        oSQLReader = oSQLCmd.ExecuteReader

        If oSQLReader.Read Then
            Dim a As String = Replace(txtPassword.Text.Trim, "'", "''")
            If Trim(oSQLReader.Item("passw")) = Replace(txtPassword.Text.Trim, "'", "''") Then

                Session("sId") = oSQLReader.Item("recID").ToString.Trim
                Session("sName") = oSQLReader.Item("name")
                Session("sPossition") = oSQLReader.Item("PossitionCode")
                Session("sProductCode") = oSQLReader.Item("ProductCode")
                Session("sEmail") = txtUsername.Text

                'set Cookies
                If CheckBox1.Checked = True Then
                    'Create a Cookie with a suitable Key.
                    Dim nameCookie1 As New HttpCookie("cEmail")
                    Dim nameCookie2 As New HttpCookie("cPassw")
                    'Set the Cookie value.
                    nameCookie1.Values("cEmail") = txtUsername.Text
                    nameCookie2.Values("cPassw") = txtPassword.Text
                    'Set the Expiry date.
                    nameCookie1.Expires = DateTime.Now.AddDays(30)
                    nameCookie2.Expires = DateTime.Now.AddDays(30)
                    'Add the Cookie to Browser.
                    Response.Cookies.Add(nameCookie1)
                    Response.Cookies.Add(nameCookie2)

                    'Dim CookieChaked As New HttpCookie("cCB")
                    'CookieChaked.Values("cCB") = True
                    'CookieChaked.Expires = DateTime.Now.AddDays(30)
                    'Response.Cookies.Add(CookieChaked)

                End If

                'Dim CookieChaked1 As New HttpCookie("cCB")
                'CookieChaked1.Values("cCB") = False
                'CookieChaked1.Expires = DateTime.Now.AddDays(30)
                'Response.Cookies.Add(CookieChaked1)

                oSQLReader.Close()

                Response.Redirect("iPxCrmUser/iPxCrmHome.aspx")

            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Sorry Your Password Is Wrong !');document.getElementById('Buttonx').click()", True)
            End If
        Else
            'oCnct.Close()
            'loginCustomer()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Sorry User Is Not Registered Yet or Not Active !');document.getElementById('Buttonx').click()", True)
        End If
        oCnct.Close()
    End Sub
    Sub loginCustomer()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If

        sSQL = "Select CFG_customerContact.CustID, CFG_customer.CustName, CFG_customerContact.Name, CFG_customerContact.NameCode, CFG_customerContact.Email, "
        sSQL += " CFG_customerContact.Passw, CFG_customerContact.IsActive, CFG_customer.CustLevel"
        sSQL += " From CFG_customerContact  INNER JOIN CFG_customer ON CFG_customerContact.CustID = CFG_customer.CustID "
        sSQL += " Where CFG_customerContact.Email = '" & Replace(txtUsername.Text, "'", "''") & "' and CFG_customerContact.IsActive = '" & "Y" & "' "
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        oSQLReader = oSQLCmd.ExecuteReader

        If oSQLReader.Read Then
            If Trim(oSQLReader.Item("CustLevel")) = "7" Then
                If Trim(oSQLReader.Item("passw")) = Replace(txtPassword.Text, "'", "''") Then
                    'session login
                    Session("sId") = oSQLReader.Item("custID").ToString.Trim
                    Session("sHotelName") = oSQLReader.Item("CustName").ToString.Trim
                    Session("sName") = oSQLReader.Item("Name")
                    Session("sNameCode") = oSQLReader.Item("NameCode")
                    Session("sEmail") = txtUsername.Text

                    'set Cookies
                    If CheckBox1.Checked = True Then
                        'Create a Cookie with a suitable Key.
                        Dim nameCookie1 As New HttpCookie("cEmail")
                        Dim nameCookie2 As New HttpCookie("cPassw")
                        'Set the Cookie value.
                        nameCookie1.Values("cEmail") = txtUsername.Text
                        nameCookie2.Values("cPassw") = txtPassword.Text
                        'Set the Expiry date.
                        nameCookie1.Expires = DateTime.Now.AddDays(30)
                        nameCookie2.Expires = DateTime.Now.AddDays(30)
                        'Add the Cookie to Browser.
                        Response.Cookies.Add(nameCookie1)
                        Response.Cookies.Add(nameCookie2)

                    End If
                    oSQLReader.Close()

                    Response.Redirect("iPxCrmCustomer/iPxCrmHome.aspx")
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Sorry Your Password Is Wrong !');document.getElementById('Buttonx').click()", True)
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Sorry User Is Not Registered Yet or Not Active !');document.getElementById('Buttonx').click()", True)
            End If
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Sorry User Is Not Registered Yet or Not Active !');document.getElementById('Buttonx').click()", True)
        End If
        oCnct.Close()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Fetch the Cookie using its Key.
        Dim nameCookie1 As HttpCookie = Request.Cookies("cEmail")
        Dim nameCookie2 As HttpCookie = Request.Cookies("cPassw")

        'If Cookie exists fetch its value.
        If nameCookie1 IsNot Nothing Then
            txtUsername.Text = nameCookie1.Values("cEmail")
            txtPassword.Text = nameCookie2.Values("cPassw")
            login()
        Else
            Session.RemoveAll()
        End If
    End Sub

    Protected Sub login(ByVal sender As Object, ByVal e As System.EventArgs) Handles submit.Click
        login()
    End Sub

    Protected Sub btnRequestPassword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRequestPassword.Click
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalAdd", "showModalAdd()", True)
        If txtUsername.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('the email is still empty, please enter your email !');document.getElementById('Buttonx').click()", True)
        Else
            findPass()
        End If
    End Sub
End Class
