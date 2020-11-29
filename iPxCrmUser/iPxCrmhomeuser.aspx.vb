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
Partial Class iPxCrmUser_iPxCrmhomeuser
    Inherits System.Web.UI.Page
    Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Dim oCnct As SqlConnection = New SqlConnection(sCnct)
    Dim oSQLCmd As SqlCommand
    Dim oSQLReader As SqlDataReader
    Dim sSQL, Ticketno, i As String
    Dim cIpx As iPxClass
    Protected Function SendEmail() As Boolean
        Try
            Dim mm As New MailMessage(ConfigurationManager.AppSettings("UserName"), tbEmail.Text)
            Dim body, cBody As String

            cBody = "Hello " + tbName.Text + ","

            cBody += "<br/>Your userid/email account:"

            cBody += "<br/>Email  : <strong>" + tbEmail.Text + "</strong> "
            cBody += "<br/>Password : <strong>" + Session("sPasswordCl") + "</strong>"
            cBody += "<br /><h4><a href = 'crm.alcorsys.com/login.aspx'>Click here to login.</a></h4>"

            cBody += "<br /><br />Thank you for using CRM "
            cBody += "<br /><br /><br /><br />Thanks"
            cBody += "<br /><br /><br /><br />CRM ADMIN"

            body = File.ReadAllText(Server.MapPath("~/iPxEmailThemplate/emailclient.html"))

            body = body.Replace("{ipx_emailbody}", cBody)

            Dim fromEmail As String = ConfigurationManager.AppSettings("UserName")
            mm.[To].Add(tbEmail.Text)
            mm.From = New MailAddress(fromEmail)
            mm.Subject = "CRM user"

            mm.Body = body
            mm.IsBodyHtml = True
            Dim smtp As SmtpClient = New SmtpClient()
            smtp.Host = ConfigurationManager.AppSettings("Host")
            smtp.EnableSsl = True
            Dim NetworkCred As NetworkCredential = New NetworkCredential()
            NetworkCred.UserName = ConfigurationManager.AppSettings("UserName")
            NetworkCred.Password = ConfigurationManager.AppSettings("Password")
            smtp.UseDefaultCredentials = True
            smtp.Credentials = NetworkCred
            smtp.Port = Integer.Parse(ConfigurationManager.AppSettings("Port"))
            smtp.Send(mm)

            '=====================================================
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Data has been saved, please check your email to see the password !');document.getElementById('Buttonx').click()", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Data has been saved, !');document.getElementById('Buttonx').click()", True)
        End Try
    End Function
    Sub dept()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_dept"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                dlDept.DataSource = dt
                dlDept.DataTextField = "DeptName"
                dlDept.DataValueField = "DeptCode"
                dlDept.DataBind()
                dlDept.Items.Insert(0, "")
            End Using
        End Using
    End Sub
    Sub position()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_possition"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                dlPosition.DataSource = dt
                dlPosition.DataTextField = "Possition"
                dlPosition.DataValueField = "PossitionCode"
                dlPosition.DataBind()
                dlPosition.Items.Insert(0, "")
            End Using
        End Using
    End Sub
    Sub productAdmin()
        dlProduct.Items.Clear()
        dlProduct.Items.Insert(0, "")
        dlProduct.Items.Insert(1, "All Product")
        dlProduct.Items.Insert(2, "Alcor")
        dlProduct.Items.Insert(3, "Pyxis")
    End Sub
    Sub ListUser()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT CFG_user.recID, CFG_user.name, CFG_user.passw, CFG_user.Email, CFG_user.PossitionCode, CFG_user.ProductCode, CFG_dept.DeptName, CFG_possition.Possition, CFG_user.isActive"
        sSQL += " FROM CFG_user "
        sSQL += "LEFT JOIN CFG_possition ON CFG_user.PossitionCode = CFG_possition.PossitionCode "
        sSQL += "LEFT JOIN CFG_dept ON CFG_dept.DeptCode = CFG_user.dept where "

        If Session("sQueryTicket") = "" Then
            If Session("sCondition") = "" Then
                Session("sQueryTicket") = "CFG_user.isActive = '" & "Y" & "' "
            Else
                Session("sQueryTicket") = Session("sCondition")
            End If
        Else

        End If
        'If Session("sCondition") <> "" Then
        sSQL = sSQL & Session("sQueryTicket")
        Session("sCondition") = ""
        'Else
        '    sSQL = sSQL & ""
        'End If
        sSQL += " order by CFG_user.name asc"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                If dt.Rows.Count <> 0 Then
                    gvUSer.DataSource = dt
                    gvUSer.DataBind()
                Else
                    dt.Rows.Add(dt.NewRow())
                    gvUSer.DataSource = dt
                    gvUSer.DataBind()
                    gvUSer.Rows(0).Visible = False
                End If
            End Using
        End Using
        oCnct.Close()
    End Sub
    Sub savedata()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        Dim regDate As Date = Date.Now()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "INSERT INTO  CFG_user(businessid,Email, passw, name, dept, PossitionCode, isActive, ProductCode) "
        sSQL += "VALUES ('" & Session("sPasswordCl") & "','" & Replace(tbEmail.Text, "'", "''") & "','" & Session("sPasswordCl") & "','" & Replace(tbName.Text, "'", "''") & "','" & dlDept.SelectedValue & "','" & dlPosition.SelectedValue & "','" & "Y" & "','" & dlProduct.SelectedIndex & "') "
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        SendEmail()
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Data has been saved !');document.getElementById('Buttonx').click()", True)
        ListUser()
    End Sub
    Sub updatedata()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If

        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "UPDATE CFG_user SET Email= '" & Replace(tbEmail.Text, "'", "''") & "', name= '" & Replace(tbName.Text, "'", "''") & "', dept= '" & dlDept.SelectedValue & "', PossitionCode= '" & dlPosition.SelectedValue & "', ProductCode='" & dlProduct.SelectedIndex & "'"
        sSQL = sSQL & "WHERE recID = '" & Session("sRecID") & "' "

        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Data has been update !');document.getElementById('Buttonx').click()", True)
        ListUser()
        tbEmail.Text = ""
        tbName.Text = ""
    End Sub
    Sub deletedata()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "UPDATE CFG_user SET isActive= '" & "N" & "'"
        sSQL = sSQL & "WHERE recID = '" & i & "' "
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        ListUser()
    End Sub
    Sub restoredata()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "UPDATE CFG_user SET isActive= '" & "Y" & "'"
        sSQL = sSQL & "WHERE recID = '" & i & "' "
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        ListUser()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sPossition") <> "0" Then
            Session("sMessage") = "Sorry, Your not access !| ||"
            Session("sWarningID") = "0"
            Session("sUrlOKONLY") = "iPxCrmHome.aspx"
            Session("sUrlYES") = "http://www.thepyxis.net"
            Session("sUrlNO") = "http://www.thepyxis.net"
            Response.Redirect("warningmsg.aspx")
        Else
            If Not Me.IsPostBack Then
                Session("sQueryTicket") = ""
                ListUser()
            End If
        End If
    End Sub
    'Protected Sub cari(ByVal sender As Object, ByVal e As EventArgs)
    '    ListUser()
    'End Sub

    Protected Sub OnPaging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvUSer.PageIndex = e.NewPageIndex
        Me.ListUser()
    End Sub

    Protected Sub gvUSer_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvUSer.PageIndexChanging
        gvUSer.PageIndex = e.NewPageIndex
        ListUser()
        If Session("snotifTotalTicket") = "0" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotifTiket", "$(document).ready(function() {hideNotifTiket()});", True)
        End If
        If Session("snotifNewTicket") = "0" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotifNewTiket", "$(document).ready(function() {hideNotifNewTiket()});", True)
        End If
        If Session("snotifFollow") = "0" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotifFollow", "$(document).ready(function() {hideNotifFollow()});", True)
        End If
        If Session("snotifDone") = "0" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotifDone", "hideNotifDone();", True)
        End If
    End Sub

    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvUSer.PageIndex = e.NewPageIndex
        Me.ListUser()
    End Sub

    Protected Sub lbAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAdd.Click
        Session("sRecID") = " "
        dept()
        productAdmin()
        position()
        tbEmail.Text = ""
        tbName.Text = ""
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalAdd", "showModalAdd()", True)
    End Sub

    Protected Sub lbSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSave.Click
        If tbName.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalAdd", "hideModalAdd()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('failed to save, please enter your name !');", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalAdd", "showModalAdd()", True)
        ElseIf tbEmail.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalAdd", "hideModalAdd()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('failed to save, please enter your e-mail !');", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalAdd", "showModalAdd()", True)
        Else
            Dim regex As Regex = New Regex("^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
            Dim isValid As Boolean = regex.IsMatch(tbEmail.Text.Trim)
            If Not isValid Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalAdd", "hideModalAdd()", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('the email you entered is invalid, please enter a valid email !');", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalAdd", "showModalAdd()", True)
            Else
                If oCnct.State = ConnectionState.Closed Then
                    oCnct.Open()
                End If
                oSQLCmd = New SqlCommand(sSQL, oCnct)
                sSQL = "SELECT recID FROM CFG_user WHERE recID = '" & Session("sRecID") & "' and isActive='Y'"
                oSQLCmd.CommandText = sSQL
                oSQLReader = oSQLCmd.ExecuteReader

                If oSQLReader.Read Then
                    oSQLReader.Close()
                    updatedata()
                Else
                    oSQLReader.Close()
                    Session("sPasswordCl") = cIpx.GetDefaultPassw()
                    savedata()
                    tbEmail.Text = ""
                    tbName.Text = ""
                End If
                oCnct.Close()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalAdd", "hideModalAdd()", True)
                If Session("snotifTotalTicket") = "0" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotifTiket", "$(document).ready(function() {hideNotifTiket()});", True)
                End If
                If Session("snotifNewTicket") = "0" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotifNewTiket", "$(document).ready(function() {hideNotifNewTiket()});", True)
                End If
                If Session("snotifFollow") = "0" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotifFollow", "$(document).ready(function() {hideNotifFollow()});", True)
                End If
                If Session("snotifDone") = "0" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotifDone", "hideNotifDone();", True)
                End If
            End If
        End If
    End Sub

    Protected Sub gvUSer_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvUSer.RowCommand
        If e.CommandName = "getUserid" Then
            i = e.CommandArgument.ToString
            Session("sRecID") = i
            Dim oSQLReader As SqlDataReader
            If oCnct.State = ConnectionState.Closed Then
                oCnct.Open()
            End If
            oSQLCmd = New SqlCommand(sSQL, oCnct)
            sSQL = "SELECT * FROM CFG_user WHERE recID = '" & i & "'"
            oSQLCmd.CommandText = sSQL
            oSQLReader = oSQLCmd.ExecuteReader

            oSQLReader.Read()
            'usercode, mobileno, password, signupdate, fullname, status, quid
            If oSQLReader.HasRows Then
                tbEmail.Text = oSQLReader.Item("Email").ToString
                tbName.Text = oSQLReader.Item("name").ToString
                dlDept.SelectedValue = oSQLReader.Item("dept").ToString
                dlPosition.SelectedValue = oSQLReader.Item("PossitionCode").ToString
                productAdmin()
                dlProduct.SelectedIndex = oSQLReader.Item("ProductCode").ToString
                oCnct.Close()
                dept()
                position()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalAdd", "showModalAdd()", True)
                If Session("snotifTotalTicket") = "0" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotifTiket", "$(document).ready(function() {hideNotifTiket()});", True)
                End If
                If Session("snotifNewTicket") = "0" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotifNewTiket", "$(document).ready(function() {hideNotifNewTiket()});", True)
                End If
                If Session("snotifFollow") = "0" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotifFollow", "$(document).ready(function() {hideNotifFollow()});", True)
                End If
                If Session("snotifDone") = "0" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotifDone", "hideNotifDone();", True)
                End If
            Else
                'Refreshh()
            End If

        ElseIf e.CommandName = "deleteUserid" Then
            i = e.CommandArgument.ToString
            deletedata()
            'kode()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Delete successful !');document.getElementById('Buttonx').click()", True)
        ElseIf e.CommandName = "RestoreUserid" Then
            i = e.CommandArgument.ToString
            restoredata()
            'kode()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Restore successful !');document.getElementById('Buttonx').click()", True)
        End If
    End Sub

    Protected Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        Response.Redirect("queryUser.aspx")
    End Sub

    Protected Sub lbAbort_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAbort.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalAdd", "hideModalAdd()", True)
        If Session("snotifTotalTicket") = "0" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotifTiket", "$(document).ready(function() {hideNotifTiket()});", True)
        End If
        If Session("snotifNewTicket") = "0" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotifNewTiket", "$(document).ready(function() {hideNotifNewTiket()});", True)
        End If
        If Session("snotifFollow") = "0" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotifFollow", "$(document).ready(function() {hideNotifFollow()});", True)
        End If
        If Session("snotifDone") = "0" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotifDone", "hideNotifDone();", True)
        End If
    End Sub
End Class
