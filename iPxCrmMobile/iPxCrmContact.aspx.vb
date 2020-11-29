Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Imports System

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
Partial Class iPxCrmMobile_iPxCrmContact
    Inherits System.Web.UI.Page
    Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Dim oCnct As SqlConnection = New SqlConnection(sCnct)
    Dim oSQLCmd As SqlCommand
    Dim oSQLReader As SqlDataReader
    Dim sSQL, Ticketno, i As String
    Dim cIpx As iPxClass
    Sub kosong()
        tbBirthday.Text = ""
        tbCustName.Text = ""
        tbEmail.Text = ""
        tbNameContact.Text = ""
        tbPhone.Text = ""
    End Sub

    Protected Function SendEmail() As Boolean
        Try
            Dim mm As New MailMessage(ConfigurationManager.AppSettings("UserName"), tbEmail.Text)
            Dim body, cBody As String

            cBody = "Hello " + tbNameContact.Text + ","

            cBody += "<br/>Your userid/email account:"

            cBody += "<br/>Email  : <strong>" + tbEmail.Text + "</strong> "
            cBody += "<br/>Password : <strong>" + Session("sPasswordCl") + "</strong>"
            cBody += "<br /><h4><a href = 'crm.alcorsys.com/assets/apk/ALCOR CRM_1_1.0.apk'>Download mobile APPS</a>"
            cBody += "<br /><a href = 'crm.alcorsys.com/iPxCrmMobile/SignIn.aspx'>Click here to login.</a></h4>"

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
            smtp.EnableSsl = False
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
    Sub listContact()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT CFG_customer.CustName, CFG_customerContact.Name, CFG_customerContact.NameCode, CFG_customerContact.ContactGrpID, CFG_customerContactGrp.Description, CFG_customerContact.Phone, "
        sSQL += "CFG_customerContact.Email, CFG_customerContact.Passw, CFG_customerContact.Birthday, CFG_customerContact.isActive "
        sSQL += "FROM CFG_customerContact INNER JOIN "
        sSQL += "CFG_customer ON CFG_customerContact.CustID = CFG_customer.CustID INNER JOIN "
        sSQL += "CFG_customerContactGrp ON CFG_customerContact.ContactGrpID = CFG_customerContactGrp.ContactGrpID "
        sSQL += "WHERE CFG_customerContact.CustID ='" & Session("sCId") & "' "
        If Session("sCQueryTicket") = "" Then
            Session("sCQueryTicket") = Session("sCCondition")
            If Session("sCQueryTicket") <> "" Or Session("sCCondition") <> "" Then
                sSQL = sSQL & Session("sCQueryTicket")
                Session("sCCondition") = ""
            Else
                sSQL = sSQL & " and CFG_customerContact.IsActive ='" & "Y" & "' "
            End If
        Else
            sSQL = sSQL & Session("sCQueryTicket")
            Session("sCCondition") = ""
        End If
        sSQL += " order by CFG_customerContact.Name asc"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                gvContact.DataSource = dt
                gvContact.DataBind()
            End Using
        End Using
        oCnct.Close()
    End Sub

    Sub ContactGrup()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_customerContactGrp where ContactGrpID<>'1'"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                dlContactGrp.DataSource = dt
                dlContactGrp.DataTextField = "Description"
                dlContactGrp.DataValueField = "ContactGrpID"
                dlContactGrp.DataBind()
                dlContactGrp.Items.Insert(0, "")
            End Using
        End Using
    End Sub

    Sub ContactGrupQuery()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_customerContactGrp"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                dlQGroup.DataSource = dt
                dlQGroup.DataTextField = "Description"
                dlQGroup.DataValueField = "ContactGrpID"
                dlQGroup.DataBind()
                dlQGroup.Items.Insert(0, "")
            End Using
        End Using
    End Sub

    Sub saveContact()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        Dim dateBirthday As Date
        If tbBirthday.Text <> "" Then
            dateBirthday = Date.ParseExact(tbBirthday.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
        Else
            dateBirthday = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
        End If
        sSQL = "INSERT INTO CFG_customerContact(CustID, ContactGrpID, Name, Phone, Email, Passw, Birthday,IsActive) "
        sSQL = sSQL & "VALUES ('" & Session("sCId") & "','" & dlContactGrp.SelectedValue & "','" & Replace(tbNameContact.Text, "'", "''") & "','" & Replace(tbPhone.Text, "'", "''") & "','" & Replace(tbEmail.Text, "'", "''") & "','" & Session("sPasswordCl") & "','" & dateBirthday & "','" & "Y" & "') "
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        SendEmail()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalFormSub", "hideModalFormSub()", True)
        listContact()
    End Sub

    Sub updateContact()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        Dim dateBirthday As Date
        If tbBirthday.Text <> "" Then
            dateBirthday = Date.ParseExact(tbBirthday.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
        Else
            dateBirthday = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
        End If
        sSQL = "UPDATE CFG_customerContact SET ContactGrpID= '" & dlContactGrp.SelectedValue & "', Name='" & Replace(tbNameContact.Text, "'", "''") & "', Phone='" & Replace(tbPhone.Text, "'", "''") & "', Email='" & Replace(tbEmail.Text, "'", "''") & "', Birthday='" & dateBirthday & "'"
        sSQL = sSQL & "WHERE NameCode = '" & Session("sNameCode") & "'"

        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Data has been update !');document.getElementById('Buttonx').click()", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalFormSub", "hideModalFormSub()", True)
        kosong()
        listContact()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalSub", "showModalSub()", True)
    End Sub

    Sub cekEmail()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If

        sSQL = "Select * From CFG_customerContact Where Email = '" & Replace(tbEmail.Text, "'", "''") & "' and IsActive='" & "Y" & "' "
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        oSQLReader = oSQLCmd.ExecuteReader

        If oSQLReader.Read Then
            oCnct.Close()
            If Session("sNameCode") = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Sorry, the email has been used !');document.getElementById('Buttonx').click()", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalFormSub", "hideModalFormSub()", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalFormSub", "hideModalFormSub()", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "date", "date()", True)
            Else
                updateContact()
            End If
        Else
            oCnct.Close()
            If Session("sNameCode") = "" Then
                saveContact()
            Else
                'updateContact()
            End If
        End If
        oCnct.Close()
    End Sub

    Sub deletedataContact()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "UPDATE CFG_customerContact SET IsActive= '" & "N" & "' where NameCode='" & Session("sQd") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
    End Sub

    Sub editContact()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT CFG_customer.CustName, CFG_customerContact.Name, CFG_customerContact.NameCode, CFG_customerContact.ContactGrpID, CFG_customerContact.Phone, "
        sSQL += "CFG_customerContact.Email, CFG_customerContact.Passw, CFG_customerContact.Birthday "
        sSQL += "FROM CFG_customerContact INNER JOIN "
        sSQL += "CFG_customer ON CFG_customerContact.CustID = CFG_customer.CustID INNER JOIN "
        sSQL += "CFG_customerContactGrp ON CFG_customerContact.ContactGrpID = CFG_customerContactGrp.ContactGrpID "
        sSQL += "WHERE CFG_customerContact.NameCode ='" & Session("sNameCode") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        'usercode, mobileno, password, signupdate, fullname, status, quid
        If oSQLReader.HasRows Then
            tbCustName.Text = Session("sCHotelName")
            dlContactGrp.SelectedValue = oSQLReader.Item("ContactGrpID").ToString
            tbNameContact.Text = oSQLReader.Item("Name").ToString
            tbPhone.Text = oSQLReader.Item("Phone").ToString
            tbEmail.Text = oSQLReader.Item("Email").ToString
            Dim dateBirthday As Date = oSQLReader.Item("Birthday").ToString
            tbBirthday.Text = dateBirthday.ToString("dd/MM/yyyy")
            oCnct.Close()
        Else
            'Refreshh()
        End If
    End Sub

    Sub restoredataContact()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "UPDATE CFG_customerContact SET IsActive= '" & "Y" & "' where NameCode='" & Session("sQd") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
    End Sub
    Sub showdata_dropdownActive()
        dlQStatus.Items.Clear()
        dlQStatus.Items.Insert(0, "")
        dlQStatus.Items.Insert(1, "ALL")
        dlQStatus.Items.Insert(2, "NON ACTIVE")
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sCId") = Nothing Then
            Response.Redirect("SignIn.aspx")
        Else
            Dim cph As ContentPlaceHolder = DirectCast(Me.Master.FindControl("ContentPlaceHolder2"), ContentPlaceHolder)
            Dim LinkButton As LinkButton = DirectCast(cph.FindControl("lnkback"), LinkButton)

            LinkButton.PostBackUrl = "iPxCrmHomeMobile.aspx"
            If Session("sCAccesCust") <> "5" Then
                Session("sMessage") = "Sorry, Your not access !| ||"
                'Session("sWarningID") = "0"
                Session("sUrlOKONLY") = "iPxCrmHomeMobile.aspx"
                Session("sUrlYES") = "iPxCrmHomeMobile.aspx"
                Session("sUrlNO") = "iPxCrmHomeMobile.aspx"
                Response.Redirect("warningmsg.aspx")
            Else
                If Not Me.IsPostBack Then
                    Session("sCQueryTicket") = ""
                    listContact()
                End If
            End If
        End If
    End Sub
    Protected Sub OnPaging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvContact.PageIndex = e.NewPageIndex
        Me.listContact()
    End Sub

    Protected Sub gvContact_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvContact.PageIndexChanging
        gvContact.PageIndex = e.NewPageIndex
        Me.listContact()
    End Sub

    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvContact.PageIndex = e.NewPageIndex
        Me.listContact()
    End Sub
    Protected Sub lbInputContact_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbInputContact.Click
        ContactGrup()
        Session("sNameCode") = ""
        tbBirthday.Text = ""
        tbNameContact.Text = ""
        tbEmail.Text = ""
        tbPhone.Text = ""
        tbCustName.Text = Session("sCHotelName")
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalFormSub", "showModalFormSub()", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "date", "date()", True)
    End Sub

    Protected Sub lbCancelContact_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCancelContact.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalFormSub", "hideModalFormSub()", True)
    End Sub

    Protected Sub lbSaveContact_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSaveContact.Click
        If tbNameContact.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('failed to save, please enter Name Contact !');document.getElementById('Buttonx').click()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalFormSub", "hideModalFormSub()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalFormSub", "showModalFormSub()", True)
            tbCustName.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "date", "date()", True)
        ElseIf tbEmail.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('failed to save, please enter Email Contact !');document.getElementById('Buttonx').click()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalFormSub", "hideModalFormSub()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalFormSub", "showModalFormSub()", True)
            tbEmail.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "date", "date()", True)
        ElseIf dlContactGrp.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('failed to save, please select Contact Group !');document.getElementById('Buttonx').click()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalFormSub", "hideModalFormSub()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalFormSub", "showModalFormSub()", True)
            dlContactGrp.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "date", "date()", True)
        ElseIf tbPhone.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('failed to save, please enter Phone !');document.getElementById('Buttonx').click()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalFormSub", "hideModalFormSub()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalFormSub", "showModalFormSub()", True)
            tbPhone.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "date", "date()", True)
        Else
            Dim regex As Regex = New Regex("^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
            Dim isValid As Boolean = regex.IsMatch(tbEmail.Text.Trim)
            If Not isValid Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('the email you entered is invalid, please enter a valid email !');document.getElementById('Buttonx').click()", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalFormSub", "hideModalFormSub()", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalFormSub", "showModalFormSub()", True)
            Else
                If Session("sNameCode") = "" Then
                    If oCnct.State = ConnectionState.Closed Then
                        oCnct.Open()
                    End If
                    oSQLCmd = New SqlCommand(sSQL, oCnct)
                    sSQL = "SELECT CustID, ContactGrpID FROM CFG_customerContact WHERE CustID = '" & Session("sCId") & "' and ContactGrpID = '" & dlContactGrp.SelectedValue & "'"
                    oSQLCmd.CommandText = sSQL
                    oSQLReader = oSQLCmd.ExecuteReader

                    If oSQLReader.Read Then
                        oSQLReader.Close()
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalFormSub", "hideModalFormSub()", True)
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalFormSub", "showModalFormSub()", True)
                        'dlContactGrp.Focus()
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Contact Grup is duplicated !');document.getElementById('Buttonx').click()", True)
                    Else
                        oSQLReader.Close()
                        Session("sPasswordCl") = cIpx.GetDefaultPassw()
                        cekEmail()
                        'kosong()
                    End If
                    oCnct.Close()
                Else
                    If oCnct.State = ConnectionState.Closed Then
                        oCnct.Open()
                    End If
                    oSQLCmd = New SqlCommand(sSQL, oCnct)
                    sSQL = "SELECT * FROM CFG_customerContact WHERE CustID = '" & Session("sCId") & "' and ContactGrpID = '" & dlContactGrp.SelectedValue & "' and NameCode <>'" & Session("sNameCode") & "'"
                    oSQLCmd.CommandText = sSQL
                    oSQLReader = oSQLCmd.ExecuteReader

                    If oSQLReader.Read Then
                        oSQLReader.Close()
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalFormSub", "hideModalFormSub()", True)
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalFormSub", "showModalFormSub()", True)
                        'dlContactGrp.Focus()
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Contact Grup is duplicated !');document.getElementById('Buttonx').click()", True)
                    Else
                        oSQLReader.Close()
                        'updateContact()
                        cekEmail()
                        'kosong()
                    End If
                    oCnct.Close()
                End If
            End If
        End If
    End Sub

    Protected Sub gvContact_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvContact.RowCommand
        If e.CommandName = "getEdit" Then
            Session("sNameCode") = e.CommandArgument.ToString
            ContactGrup()
            editContact()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalFormSub", "showModalFormSub()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "date", "date()", True)
        ElseIf e.CommandName = "getHapus" Then
            Session("sQd") = e.CommandArgument.ToString
            deletedataContact()
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalDeleteMenu", "showModalDeleteMenu()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('delete is successfull !');document.getElementById('Buttonx').click()", True)
            listContact()
        ElseIf e.CommandName = "getRestore" Then
            Session("sQd") = e.CommandArgument.ToString
            restoredataContact()
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalDeleteMenu", "showModalDeleteMenu()", True)
            listContact()
        End If
    End Sub

    Protected Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        ContactGrupQuery()
        showdata_dropdownActive()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalFormQuery", "showModalFormQuery()", True)
    End Sub

    Protected Sub lbAbortQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAbortQuery.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalFormQuery", "hideModalFormQuery()", True)
    End Sub

    Protected Sub lblQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblQuery.Click
        Session("sCQueryTicket") = ""
        If tbQName.Text.Trim <> "" Then
            Session("sCCondition") = Session("sCCondition") & " AND (CFG_customerContact.Name like '%" & Replace(tbQName.Text.Trim, "'", "''") & "%') "
        End If

        If dlQGroup.Text.Trim <> "" Then
            Session("sCCondition") = Session("sCCondition") & " AND (CFG_customerContact.ContactGrpID = '" & dlQGroup.SelectedValue & "') "
        End If

        If tbQEmail.Text.Trim <> "" Then
            Session("sCCondition") = Session("sCCondition") & " AND (CFG_customerContact.Email like '%" & Replace(tbQEmail.Text.Trim, "'", "''") & "%') "
        End If

        If dlQStatus.SelectedIndex = 2 Then
            Session("sCCondition") = Session("sCCondition") & " AND (CFG_customerContact.IsActive <> '" & "Y" & "') "
        ElseIf dlQStatus.SelectedIndex = 1 Then
            Session("sCCondition") = Session("sCCondition") & "  "
        Else
            Session("sCCondition") = Session("sCCondition") & " AND (CFG_customerContact.IsActive = '" & "Y" & "') "
        End If
        listContact()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalFormQuery", "hideModalFormQuery()", True)
    End Sub
End Class
