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
Partial Class iPxCrmUser_iPxCrmCustomer
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
    Sub ListCustomer()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT a.CustID, CFG_customerGrp.GrpName, a.CustName, a.Phone, a.Fax, "
        sSQL += "(SELECT country FROM CFG_geog_country WHERE countryid=a.CountryID) AS country, "
        sSQL += "(SELECT description FROM CFG_geog_province WHERE countryid=a.CountryID and provid=a.Provid)AS Profinsi, "
        sSQL += "(SELECT CITY FROM CFG_geog_city WHERE CityID=A.CityID) AS CITY, a.Address, a.StarClass, a.Troom, a.Anniversary, "
        sSQL += "CFG_CustomerLevel.LevelDescription, a.CustLevel FROM CFG_customer as a "
        sSQL += "INNER JOIN CFG_CustomerLevel ON a.CustLevel = CFG_CustomerLevel.CustLevel "
        sSQL += "INNER JOIN CFG_customerGrp ON CFG_customerGrp.CustGrpID = a.CustGrpID "
        sSQL += " WHERE CFG_customerGrp.isActive = 'Y' "

        If Session("sQueryTicket") = "" Then
            Session("sQueryTicket") = Session("sCondition")
            If Session("sQueryTicket") <> "" Or Session("sCondition") <> "" Then
                sSQL = sSQL & Session("sQueryTicket")
                Session("sCondition") = ""
            Else
                sSQL = sSQL & " and a.CustLevel <> '8' "
            End If
        Else
            sSQL = sSQL & Session("sQueryTicket")
            Session("sCondition") = ""
        End If
        sSQL += " order by CFG_customerGrp.GrpName asc, a.CustName asc"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                If dt.Rows.Count <> 0 Then
                    gvCust.DataSource = dt
                    gvCust.DataBind()
                Else
                    dt.Rows.Add(dt.NewRow())
                    gvCust.DataSource = dt
                    gvCust.DataBind()
                    gvCust.Rows(0).Visible = False
                End If
            End Using
        End Using
        oCnct.Close()
    End Sub
    Sub listContact()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT CFG_customer.CustName, CFG_customerContact.Name, CFG_customerContact.NameCode, CFG_customerContactGrp.Description, CFG_customerContact.Phone, "
        sSQL += "CFG_customerContact.Email, CFG_customerContact.Passw, CFG_customerContact.Birthday, CFG_customerContact.isActive "
        sSQL += "FROM CFG_customerContact INNER JOIN "
        sSQL += "CFG_customer ON CFG_customerContact.CustID = CFG_customer.CustID INNER JOIN "
        sSQL += "CFG_customerContactGrp ON CFG_customerContact.ContactGrpID = CFG_customerContactGrp.ContactGrpID "
        sSQL += "WHERE CFG_customerContact.CustID ='" & Session("sSi") & "' and CFG_customerContact.IsActive ='" & "Y" & "'"

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
        sSQL = "SELECT * FROM CFG_customerContactGrp"
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
    Sub detailContact()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT CFG_customer.CustID, CFG_customer.CustName "
        sSQL += "FROM CFG_customer "
        sSQL += "WHERE CFG_customer.CustID ='" & Session("sSi") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        'usercode, mobileno, password, signupdate, fullname, status, quid
        If oSQLReader.HasRows Then
            Session("sC1") = oSQLReader.Item("CustID").ToString
            Session("sC2") = oSQLReader.Item("CustName").ToString
            oCnct.Close()
        Else
            'Refreshh()
        End If
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
            tbCustName.Text = Session("sC2")
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
        sSQL = sSQL & "VALUES ('" & Session("sC1") & "','" & dlContactGrp.SelectedValue & "','" & Replace(tbNameContact.Text, "'", "''") & "','" & Replace(tbPhone.Text, "'", "''") & "','" & Replace(tbEmail.Text, "'", "''") & "','" & Session("sPasswordCl") & "','" & dateBirthday & "','" & "Y" & "') "
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        SendEmail()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalFormSub", "hideModalFormSub()", True)
        listContact()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalSub", "showModalSub()", True)
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
    Sub deleteCust()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "UPDATE CFG_customer SET CustLevel = '8' where CustID='" & Session("sQd") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        ListCustomer()
    End Sub
    Sub restoreCust()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "UPDATE CFG_customer SET CustLevel = '1' where CustID='" & Session("sQd") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        ListCustomer()
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
                ListCustomer()
            End If
        End If
    End Sub
    Protected Sub OnPaging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvCust.PageIndex = e.NewPageIndex
        Me.ListCustomer()
    End Sub

    Protected Sub gvCust_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvCust.PageIndexChanging
        gvCust.PageIndex = e.NewPageIndex
        ListCustomer()
    End Sub

    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvCust.PageIndex = e.NewPageIndex
        Me.ListCustomer()
    End Sub
    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim level As String = e.Row.Cells(12).Text

            For Each cell As TableCell In e.Row.Cells
                If level = "DEAL/CUSTOMER" Then
                    cell.BackColor = Color.LightGray
                ElseIf level <> "DEAL" Then
                    cell.BackColor = Color.White
                End If
            Next
        End If
    End Sub
    Protected Sub lbNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbNew.Click
        Session("sCustEdit") = ""
        Response.Redirect("iPxCrmInputCustomer.aspx")
    End Sub

    Protected Sub gvCust_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvCust.RowCommand
        If e.CommandName = "getEdit" Then
            Session("sCustEdit") = e.CommandArgument.ToString
            Response.Redirect("iPxCrmInputCustomer.aspx")
        ElseIf e.CommandName = "getHapus" Then
            Session("sQd") = e.CommandArgument.ToString
            deleteCust()
        ElseIf e.CommandName = "getRestore" Then
            Session("sQd") = e.CommandArgument.ToString
            restoreCust()
        ElseIf e.CommandName = "getContact" Then
            Session("sSi") = e.CommandArgument.ToString
            listContact()
            detailContact()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalSub", "showModalSub()", True)
        ElseIf e.CommandName = "getDraft" Then
            Session("sDraft") = e.CommandArgument.ToString
            Response.Redirect("iPxCrmDraftTicket.aspx")
        End If
    End Sub

    Protected Sub lbCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCancel.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalDelete", "hideModalDelete()", True)
    End Sub

    Protected Sub lbDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbDelete.Click
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "UPDATE CFG_customer SET CustLevel = '8' where CustID='" & Session("sQd") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        ListCustomer()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalDelete", "hideModalDelete()", True)
    End Sub

    Protected Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        Response.Redirect("queryCust.aspx")
    End Sub

    Protected Sub lbInputContact_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbInputContact.Click
        ContactGrup()
        Session("sNameCode") = ""
        tbBirthday.Text = ""
        tbNameContact.Text = ""
        tbEmail.Text = ""
        tbPhone.Text = ""
        tbCustName.Text = Session("sC2")
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalSub", "hideModalSub()", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalFormSub", "showModalFormSub()", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "date", "date()", True)
    End Sub

    Protected Sub lbCancelContact_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCancelContact.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalFormSub", "hideModalFormSub()", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalSub", "showModalSub()", True)
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
                    'If oCnct.State = ConnectionState.Closed Then
                    '    oCnct.Open()
                    'End If
                    'oSQLCmd = New SqlCommand(sSQL, oCnct)
                    'sSQL = "SELECT CustID, ContactGrpID FROM CFG_customerContact WHERE CustID = '" & Session("sC1") & "' and ContactGrpID = '" & dlContactGrp.SelectedValue & "'"
                    'oSQLCmd.CommandText = sSQL
                    'oSQLReader = oSQLCmd.ExecuteReader

                    'If oSQLReader.Read Then
                    '    oSQLReader.Close()
                    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalFormSub", "hideModalFormSub()", True)
                    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalFormSub", "showModalFormSub()", True)
                    '    'dlContactGrp.Focus()
                    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Contact Grup is duplicated !');document.getElementById('Buttonx').click()", True)
                    'Else
                    '    oSQLReader.Close()
                    Session("sPasswordCl") = cIpx.GetDefaultPassw()
                    cekEmail()
                    '    'kosong()
                    'End If
                    'oCnct.Close()
                Else
                    'If oCnct.State = ConnectionState.Closed Then
                    '    oCnct.Open()
                    'End If
                    'oSQLCmd = New SqlCommand(sSQL, oCnct)
                    'sSQL = "SELECT * FROM CFG_customerContact WHERE CustID = '" & Session("sC1") & "' and ContactGrpID = '" & dlContactGrp.SelectedValue & "' and NameCode <>'" & Session("sNameCode") & "'"
                    'oSQLCmd.CommandText = sSQL
                    'oSQLReader = oSQLCmd.ExecuteReader

                    'If oSQLReader.Read Then
                    '    oSQLReader.Close()
                    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalFormSub", "hideModalFormSub()", True)
                    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalFormSub", "showModalFormSub()", True)
                    '    'dlContactGrp.Focus()
                    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Contact Grup is duplicated !');document.getElementById('Buttonx').click()", True)
                    'Else
                    '    oSQLReader.Close()
                    '    'updateContact()
                    cekEmail()
                    '    'kosong()
                    'End If
                    'oCnct.Close()
                End If
            End If
        End If
    End Sub

    Protected Sub gvContact_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvContact.RowCommand
        If e.CommandName = "getEdit" Then
            Session("sNameCode") = e.CommandArgument.ToString
            ContactGrup()
            editContact()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalSub", "hideModalSub()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalFormSub", "showModalFormSub()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "date", "date()", True)
        ElseIf e.CommandName = "getHapus" Then
            Session("sQd") = e.CommandArgument.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalSub", "hideModalSub()", True)
            deletedataContact()
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalDeleteMenu", "showModalDeleteMenu()", True)
        ElseIf e.CommandName = "getRestore" Then
            Session("sQd") = e.CommandArgument.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalSub", "hideModalSub()", True)
            restoredataContact()
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalDeleteMenu", "showModalDeleteMenu()", True)
        End If
    End Sub

    Protected Sub LinkButton3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton3.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalDeleteMenu", "hideModalDeleteMenu()", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalSub", "showModalSub()", True)
    End Sub

    Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton2.Click
        deletedataContact()
        listContact()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalDeleteMenu", "hideModalDeleteMenu()", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalSub", "showModalSub()", True)
    End Sub

End Class
