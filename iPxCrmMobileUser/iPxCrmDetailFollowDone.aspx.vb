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

Partial Class iPxCrmMobileUser_iPxCrmDetailFollowDone
    Inherits System.Web.UI.Page
    Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Dim oCnct As SqlConnection = New SqlConnection(sCnct)
    Dim oSQLCmd As SqlCommand
    Dim oSQLReader As SqlDataReader
    Dim sSQL, Ticketno, i As String
    Dim cIpx As iPxClass

    Protected Function SendEmailSupport(ByVal cbody As String, ByVal email As String, ByVal subject As String) As Boolean

        Try
            Dim mm As New MailMessage(ConfigurationManager.AppSettings("UserName"), email)

            Dim body As String
            body = File.ReadAllText(Server.MapPath("~/iPxEmailThemplate/emailclient.html"))

            body = body.Replace("{ipx_emailbody}", cbody)

            Dim fromEmail As String = ConfigurationManager.AppSettings("UserName")
            mm.[To].Add(email)
            mm.From = New MailAddress(fromEmail)
            mm.Subject = subject

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
        Catch ex As Exception
        End Try
    End Function

    Sub emailcc()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT email, name FROM CFG_user where recID ='" & dlAssign.SelectedValue & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        'usercode, mobileno, password, signupdate, fullname, status, quid
        If oSQLReader.HasRows Then
            Session("sEmailcc") = oSQLReader.Item("email").ToString
            Session("sNameSupport") = oSQLReader.Item("name").ToString

            oCnct.Close()
        Else
            'Refreshh()
        End If
    End Sub
    Sub ListTicket()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "select a.*,(select name from CFG_user where recID = a.AssignTo)as supportBy from "
        sSQL += "(SELECT SPR_eticket.CustID, CFG_customer.CustName, SPR_eticket.TicketNo, SPR_eticket.Tdate, SPR_eticket.CaseDescription, SPR_eticket.SubmitFrom, CFG_Product.ProductName, CFG_productMenu.MenuName, CFG_productSubMenu.SubmenuName"
        sSQL += ", CFG_caseID.Description,CFG_FollowupSts.stsDescription, AssignTo From SPR_eticket "
        sSQL += "INNER JOIN CFG_Product ON SPR_eticket.ProductID = CFG_Product.ProductID INNER JOIN CFG_FollowupSts ON SPR_eticket.StatusID = CFG_FollowupSts.StatusID "
        sSQL += "INNER JOIN CFG_productMenu ON SPR_eticket.MenuID = CFG_productMenu.MenuID and CFG_Product.ProductID = CFG_productMenu.ProductID LEFT JOIN CFG_caseID ON SPR_eticket.CaseID = CFG_caseID.CaseID "
        sSQL += "INNER JOIN CFG_customer ON SPR_eticket.CustID = CFG_customer.CustID "
        sSQL += "INNER JOIN CFG_productSubMenu ON SPR_eticket.SubmenuID = CFG_productSubMenu.SubmenuID where SPR_eticket.TicketNo = '" & Session("sTicketFollow") & "') as a"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                gvTicket.DataSource = dt
                gvTicket.DataBind()
            End Using
        End Using
        oCnct.Close()
    End Sub

    Sub ListFollow()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * "
        sSQL += " From SPR_eticketFollowUp "
        sSQL += " where TicketNo = '" & Session("sTicketFollow") & "' order by FollowUpDate asc"

        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                gvFollow.DataSource = dt
                gvFollow.DataBind()
            End Using
        End Using
        oCnct.Close()
    End Sub
    Sub ListStatus()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT SPR_eticket.CustID, SPR_eticket.TicketNo, SPR_eticket.Tdate, SPR_eticket.CaseDescription, SPR_eticket.SubmitFrom, CFG_Product.ProductName, CFG_productMenu.MenuName, CFG_productSubMenu.SubmenuName"
        sSQL += ", CFG_caseID.Description,SPR_eticket.StatusID From SPR_eticket "
        sSQL += "INNER JOIN CFG_Product ON SPR_eticket.ProductID = CFG_Product.ProductID "
        sSQL += "INNER JOIN CFG_productMenu ON SPR_eticket.MenuID = CFG_productMenu.MenuID INNER JOIN CFG_caseID ON SPR_eticket.CaseID = CFG_caseID.CaseID "
        sSQL += "INNER JOIN CFG_productSubMenu ON SPR_eticket.SubmenuID = CFG_productSubMenu.SubmenuID where SPR_eticket.TicketNo = '" & Session("sTicketFollow") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        'usercode, mobileno, password, signupdate, fullname, status, quid
        If oSQLReader.HasRows Then
            Session("sStatusTicket") = oSQLReader.Item("StatusID").ToString
            Session("sCustID") = oSQLReader.Item("CustID").ToString
            oCnct.Close()
        Else
            'Refreshh()
        End If
    End Sub
    Sub status()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_FollowupSts where StatusID >'" & "1" & "' and StatusID <>'" & "7" & "'"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                dlUpStatus.DataSource = dt
                dlUpStatus.DataTextField = "stsDescription"
                dlUpStatus.DataValueField = "StatusID"
                dlUpStatus.DataBind()
            End Using
        End Using
    End Sub

    Sub updateStatus1()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If

        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "UPDATE SPR_eticket SET StatusID= '" & dlUpStatus.SelectedValue & "'"
        sSQL = sSQL & "WHERE TicketNo = '" & Session("sTicketFollow") & "' "

        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Assignment is saved successfully !');document.getElementById('Buttonx').click()", True)
    End Sub
    Sub updateRead()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If

        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "UPDATE SPR_eticketFollowUp SET isRead= '0'"
        sSQL = sSQL & "WHERE TicketNo = '" & Session("sTicketFollow") & "' and FollowUpCode='H'"

        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Assignment is saved successfully !');document.getElementById('Buttonx').click()", True)
    End Sub
    Sub Userid()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_user where recID='" & Session("sId") & "' and isActive ='Y' order by recID asc"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                dlAssign.DataSource = dt
                dlAssign.DataTextField = "name"
                dlAssign.DataValueField = "recID"
                dlAssign.DataBind()
            End Using
        End Using
    End Sub
    Sub CaseId()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_caseID"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                dlCase.DataSource = dt
                dlCase.DataTextField = "Description"
                dlCase.DataValueField = "caseID"
                dlCase.DataBind()
                dlCase.Items.Insert(0, "")
            End Using
        End Using
    End Sub
    Sub detailAssign()
        Dim oSQLReader As SqlDataReader
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT SPR_eticket.TicketNo, CFG_customer.CustName, CFG_customer.Phone, CFG_Product.ProductName, CFG_productMenu.MenuName, CFG_productSubMenu.SubmenuName, SPR_eticket.CaseDescription, SPR_eticket.SubmitFrom, SPR_eticket.EmailFrom FROM SPR_eticket"
        sSQL += " INNER JOIN CFG_productMenu ON SPR_eticket.MenuID = CFG_productMenu.MenuID"
        sSQL += " INNER JOIN CFG_Product ON SPR_eticket.ProductID = CFG_Product.ProductID"
        sSQL += " INNER JOIN CFG_productSubMenu ON SPR_eticket.SubMenuID = CFG_productSubMenu.SubMenuID INNER JOIN CFG_customer ON SPR_eticket.CustID = CFG_customer.CustID"
        sSQL += " WHERE SPR_eticket.TicketNo = '" & Session("sTicketFollow") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        'usercode, mobileno, password, signupdate, fullname, status, quid
        If oSQLReader.HasRows Then
            tbTicketno.Text = oSQLReader.Item("TicketNo").ToString
            tbHotelName.Text = oSQLReader.Item("CustName").ToString 'nama Hotel
            tbContact.Text = oSQLReader.Item("Phone").ToString 'contact Hotel
            tbProduct.Text = oSQLReader.Item("ProductName").ToString
            tbMenu.Text = oSQLReader.Item("MenuName").ToString
            tbSubMenu.Text = oSQLReader.Item("SubmenuName").ToString
            tbDescription.Text = oSQLReader.Item("CaseDescription").ToString
            tbFrom.Text = oSQLReader.Item("SubmitFrom").ToString
            Session("sEmailfrom") = oSQLReader.Item("EmailFrom").ToString

            oCnct.Close()
            CaseId()
        Else
            'Refreshh()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sId") = Nothing Then
            Response.Redirect("SignIn.aspx")
        Else
            Dim cph As ContentPlaceHolder = DirectCast(Me.Master.FindControl("ContentPlaceHolder2"), ContentPlaceHolder)
            Dim LinkButton As LinkButton = DirectCast(cph.FindControl("lnkback"), LinkButton)
            If Session("sStatusTicket") = 7 Then
                LinkButton.PostBackUrl = "iPxCrmDone.aspx"
            Else
                LinkButton.PostBackUrl = "iPxCrmAssign.aspx"
            End If
            If Not Me.IsPostBack Then
                ListTicket()
                ListFollow()
                ListStatus()
                If Session("sStatusTicket") = "1" Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showFormMenu", "$(document).ready(function() { showFormMenu() });", True)
                    detailAssign()
                    Userid()
                    dlAssign.Enabled = False
                Else
                    If Session("sPossition") <> "0" Then
                        updateRead()
                    End If
                End If
                'If Session("sStatusTicket") = 7 Then
                '    lbAddFollow.Enabled = False
                '    lbUpStatus.Enabled = False
                'Else
                '    lbAddFollow.Enabled = True
                '    lbUpStatus.Enabled = True
                'End If
            End If
        End If
    End Sub

    Protected Sub GetValue(ByVal sender As Object, ByVal e As EventArgs)
        'Reference the Repeater Item using Button.
        Try
            Dim item As RepeaterItem = TryCast((TryCast(sender, LinkButton)).NamingContainer, RepeaterItem)

            Dim filePath As String = (TryCast(item.FindControl("Label1"), Label)).Text
            Response.ContentType = "image/jpg"
            Response.AddHeader("Content-Disposition", "attachment;filename=""" & filePath & """")
            Response.TransmitFile(Server.MapPath(filePath))
            Response.[End]()
        Catch
        End Try
    End Sub

    Protected Sub lbSaveupStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSaveupStatus.Click
        updateStatus1()
        ListTicket()
        ListStatus()
        ListFollow()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideFormMenu", "hideFormMenu()", True)
        Response.Redirect("iPxCrmDetailFollowUser.aspx")
    End Sub

    Protected Sub lbAbortupStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAbortupStatus.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideFormMenu", "hideFormMenu()", True)
    End Sub

    Protected Sub lbAbortStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAbortStart.Click
        Response.Redirect("iPxCrmAssign.aspx")
    End Sub

    Protected Sub lbStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbStart.Click
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If

        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "UPDATE SPR_eticket SET CaseID= '" & dlCase.SelectedValue & "', AssignTo= '" & dlAssign.SelectedValue & "', StatusID= '" & "2" & "'"
        sSQL = sSQL & "WHERE TicketNo = '" & tbTicketno.Text & "' "

        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        'ListTicket()
        If Session("sEmailfrom") <> "" Then
            ' SendEmail()
            emailcc()
            Dim bodysupport, bodyclient As String

            'string emil for support
            bodysupport = "Hello " + Session("sNameSupport") + ","

            bodysupport += "<br/>"
            bodysupport += "<br/>Ticket No    : <strong>" + tbTicketno.Text + "</strong> "
            bodysupport += "<br/>Hotel Name   : <strong>" + tbHotelName.Text + "</strong>"
            bodysupport += "<br/>Product Name : <strong>" + tbProduct.Text + "</strong> "
            bodysupport += "<br/>Menu Name    : <strong>" + tbMenu.Text + "</strong>"
            bodysupport += "<br/>Submenu Name : <strong>" + tbSubMenu.Text + "</strong>"
            bodysupport += "<br/>Description  : <br/><strong>" + tbDescription.Text + "</strong>"
            bodysupport += "<br />please immediately follow up the ticket "

            bodysupport += "<br /><br /><br /><br />Thanks"
            bodysupport += "<br /><br /><br /><br />ADMIN SUPPORT"

            'string email for client

            bodyclient = "Hello " + tbFrom.Text + ","

            bodyclient += "<br/>Your E-Ticket:"

            bodyclient += "<br/>Ticket No    : <strong>" + tbTicketno.Text + "</strong> "
            bodyclient += "<br/>Hotel Name   : <strong>" + tbHotelName.Text + "</strong>"
            bodyclient += "<br/>Product Name : <strong>" + tbProduct.Text + "</strong> "
            bodyclient += "<br/>Menu Name    : <strong>" + tbMenu.Text + "</strong>"
            bodyclient += "<br/>Submenu Name : <strong>" + tbSubMenu.Text + "</strong>"
            bodyclient += "<br/>Description  : <br/><strong>" + tbDescription.Text + "</strong>"
            bodyclient += "<br />has been assigned to " + Session("sNameSupport")

            bodyclient += "<br /><br />Thank you for using CRM "
            bodyclient += "<br /><br /><br /><br />Thanks"
            bodyclient += "<br /><br /><br /><br />ADMIN SUPPORT"


            SendEmailSupport(bodysupport, Session("sEmailcc"), "CRM")
            SendEmailSupport(bodyclient, Session("sEmailfrom"), "PYXIS SUPPORT E-TICKET")
            ListTicket()
            ListFollow()
            ListStatus()
            updateRead()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideFormMenu", "hideFormMenu()", True)
        End If
    End Sub

    Protected Sub lbDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbDetail.Click
        If pnHeader.Visible = True Then
            pnHeader.Visible = False
            Panel3.Visible = True
            lbDetail.Text = "<i class='fa fa-angle-double-up' style='height:20px; font-size:20px'></i>"
        ElseIf pnHeader.Visible = False Then
            pnHeader.Visible = True
            Panel3.Visible = False
            lbDetail.Text = "<i class='fa fa-angle-double-down' style='height:20px; font-size:20px'></i>"
        End If
    End Sub
End Class
