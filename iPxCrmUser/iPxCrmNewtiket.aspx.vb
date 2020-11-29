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
Partial Class iPxCrmUser_iPxCrmNewtiket
    Inherits System.Web.UI.Page
    Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Dim oCnct As SqlConnection = New SqlConnection(sCnct)
    Dim oSQLCmd As SqlCommand
    Dim oSQLReader As SqlDataReader
    Dim sSQL, Ticketno, i, emailfrom As String
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
    
    Sub Userid()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_user where isActive ='Y' "
        If Session("sPossition") = "0" And Session("sProductCode") = "1" Then
            sSQL += " "
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "2" Then
            sSQL += " AND ProductCode = '2'"
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "3" Then
            sSQL += " AND ProductCode = '3'"
        End If
        sSQL += "order by recID asc "
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
            End Using
        End Using
    End Sub

    Sub ListTicket()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT a.*,( SELECT isRead FROM SPR_eticketFollowUp where SPR_eticketFollowUp.TicketNo=a.TicketNo and FollowUpDate in "
        sSQL += "( SELECT max(FollowUpDate) FROM SPR_eticketFollowUp where FollowUpCode ='H' group by TicketNo )) as coment from "
        sSQL += "(SELECT SPR_eticket.CustID, CFG_customer.CustName, SPR_eticket.TicketNo, SPR_eticket.Tdate, SPR_eticket.SubmitFrom, SPR_eticket.Subject, SPR_eticket.SubmitVia, CFG_Product.ProductName, CFG_productMenu.MenuName, CFG_productSubMenu.SubmenuName"
        sSQL += ", SPR_eticket.AttachFile,SPR_eticket.CaseDescription From SPR_eticket "
        sSQL += "LEFT JOIN CFG_productGrp on SPR_eticket .ProductGrp = CFG_productGrp .ProductGrp "
        sSQL += "LEFT JOIN CFG_Product ON SPR_eticket.ProductID = CFG_Product.ProductID LEFT JOIN CFG_customer ON SPR_eticket.CustID = CFG_customer.CustID "
        sSQL += "LEFT JOIN CFG_productMenu ON SPR_eticket.MenuID = CFG_productMenu.MenuID and CFG_Product.ProductID = CFG_productMenu.ProductID "
        sSQL += "LEFT JOIN CFG_productSubMenu ON SPR_eticket.SubmenuID = CFG_productSubMenu.SubmenuID where SPR_eticket.statusID = '" & "1" & "' "

        If Session("sPossition") = "0" And Session("sProductCode") = "1" Then
            sSQL += " "
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "2" Then
            sSQL += " and CFG_productGrp.PrdDescription = 'Alcor'"
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "3" Then
            sSQL += " and CFG_productGrp.PrdDescription <> 'Alcor'"
        End If

        If Session("sQueryTicket") = "" Then
            Session("sQueryTicket") = Session("sCondition")
        Else

        End If

        'If Session("sCondition") <> "" Then
        sSQL = sSQL & Session("sQueryTicket")
        Session("sCondition") = ""
        'Else
        '    sSQL = sSQL & ""
        'End If
        sSQL += " ) a order by a.Tdate desc"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                If dt.Rows.Count <> 0 Then
                    gvTicket.DataSource = dt
                    gvTicket.DataBind()
                Else
                    dt.Rows.Add(dt.NewRow())
                    gvTicket.DataSource = dt
                    gvTicket.DataBind()
                    gvTicket.Rows(0).Visible = False
                End If
            End Using
        End Using
        oCnct.Close()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session("sPossition") <> "0" Then
        '    Session("sMessage") = "Sorry, Your not access !| ||"
        '    Session("sWarningID") = "0"
        '    Session("sUrlOKONLY") = "iPxCrmHome.aspx"
        '    Session("sUrlYES") = "http://www.thepyxis.net"
        '    Session("sUrlNO") = "http://www.thepyxis.net"
        '    Response.Redirect("warningmsg.aspx")
        'Else
        If Not Me.IsPostBack Then
            Session("sQueryTicket") = ""
            ListTicket()
        End If
        'End If
    End Sub
    Protected Sub OnPaging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvTicket.PageIndex = e.NewPageIndex
        Me.ListTicket()
    End Sub

    Protected Sub gvTicket_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvTicket.PageIndexChanging
        gvTicket.PageIndex = e.NewPageIndex
        ListTicket()
    End Sub

    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvTicket.PageIndex = e.NewPageIndex
        Me.ListTicket()
    End Sub

    Protected Sub gvTicket_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvTicket.RowCommand
        If e.CommandName = "getTiketid" Then
            'If Session("sPossition") <> "0" Then
            '    Session("sMessage") = "Sorry, Your not access !| ||"
            '    Session("sWarningID") = "0"
            '    Session("sUrlOKONLY") = "iPxCrmHome.aspx"
            '    Session("sUrlYES") = "http://www.thepyxis.net"
            '    Session("sUrlNO") = "http://www.thepyxis.net"
            '    Response.Redirect("warningmsg.aspx")
            'Else
            i = e.CommandArgument.ToString
            Dim oSQLReader As SqlDataReader
            If oCnct.State = ConnectionState.Closed Then
                oCnct.Open()
            End If
            oSQLCmd = New SqlCommand(sSQL, oCnct)
            sSQL = "SELECT SPR_eticket.TicketNo, SPR_eticket.Subject, CFG_customer.CustName, CFG_customer.Phone, CFG_Product.ProductName, CFG_productMenu.MenuName, CFG_productSubMenu.SubmenuName, SPR_eticket.CaseDescription, SPR_eticket.SubmitFrom, SPR_eticket.EmailFrom FROM SPR_eticket"
            sSQL += " LEFT JOIN CFG_productMenu ON SPR_eticket.MenuID = CFG_productMenu.MenuID"
            sSQL += " LEFT JOIN CFG_Product ON SPR_eticket.ProductID = CFG_Product.ProductID"
            sSQL += " LEFT JOIN CFG_productSubMenu ON SPR_eticket.SubMenuID = CFG_productSubMenu.SubMenuID LEFT JOIN CFG_customer ON SPR_eticket.CustID = CFG_customer.CustID"
            sSQL += " WHERE SPR_eticket.TicketNo = '" & i & "'"
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
                tbSubject.Text = oSQLReader.Item("Subject").ToString
                Session("sEmailfrom") = oSQLReader.Item("EmailFrom").ToString

                oCnct.Close()
                Userid()
                CaseId()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalAdd", "showModalAdd()", True)
            Else
                'Refreshh()
            End If
            'End If
        ElseIf e.CommandName = "getEditTiket" Then
        i = e.CommandArgument.ToString
        Dim oSQLReader As SqlDataReader
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM SPR_eticket LEFT JOIN CFG_customer on CFG_customer.CustID = SPR_eticket.CustID WHERE TicketNo = '" & i & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        'usercode, mobileno, password, signupdate, fullname, status, quid
        If oSQLReader.HasRows Then
            Session("sIn1") = oSQLReader.Item("TicketNo").ToString
            Session("sIn2") = oSQLReader.Item("ProductID").ToString
            Session("sIn3") = oSQLReader.Item("MenuID").ToString
            Session("sIn4") = oSQLReader.Item("SubMenuID").ToString
            Session("sIn5") = oSQLReader.Item("CaseDescription").ToString
            Session("sIn6") = oSQLReader.Item("SubmitFrom").ToString
            Session("sIn7") = oSQLReader.Item("Tdate").ToString
            Session("sIn8") = oSQLReader.Item("Subject").ToString
            Session("sCustName") = oSQLReader.Item("CustID").ToString
            Session("sIn9") = oSQLReader.Item("ProductGrp").ToString
            Session("sIn10") = oSQLReader.Item("SubmitVia").ToString
            Session("sIn11") = oSQLReader.Item("AttachFile").ToString
            Session("sIn12") = oSQLReader.Item("EmailFrom").ToString
            oCnct.Close()
            Response.Redirect("iPxCrmInputTicketUser.aspx")
        Else
            'Refreshh()
        End If
        ElseIf e.CommandName = "getFileTiket" Then
        Try
            Dim filePath As String = e.CommandArgument.ToString
            Response.ContentType = "image/jpg,application/pdf,application/vnd.openxmlformats-officedocument.wordprocessingml.document,application/zip,application/rar,application/msword"
            Response.AddHeader("Content-Disposition", "attachment;filename=""" & filePath & """")
            Response.TransmitFile(Server.MapPath(filePath))
            Response.[End]()
        Catch
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('sorry, the data you want to download is not found !');document.getElementById('Buttonx').click()", True)
        End Try
        End If
    End Sub

    Protected Sub lbSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSave.Click
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

        End If
        Response.Redirect("iPxCrmNewtiket.aspx")
    End Sub

    Protected Sub lbAddTicket_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAddTicket.Click
        Session("sIn1") = ""
        Session("sCustName") = ""
        Response.Redirect("iPxCrmInputTicketUser.aspx")
    End Sub

    Protected Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        Response.Redirect("queryTicketUser.aspx")
    End Sub
End Class
