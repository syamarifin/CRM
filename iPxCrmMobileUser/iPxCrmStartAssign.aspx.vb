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
Partial Class iPxCrmMobileUser_iPxCrmStartAssign
    Inherits System.Web.UI.Page
    Public sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Public oCnct As SqlConnection = New SqlConnection(sCnct)
    Public oSQLCmd As New SqlCommand
    Public oSQLReader As SqlDataReader
    Public sSQL, Produkid, Menuid, SubMenuid As String
    Dim cipx As New iPxClass
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
        sSQL = "SELECT SPR_eticket.TicketNo,SPR_eticket.Tdate, SPR_eticket.Subject, CFG_customer.CustName, CFG_customer.Phone, CFG_productGrp.PrdDescription, CFG_Product.ProductName, CFG_productMenu.MenuName, CFG_productSubMenu.SubmenuName, SPR_eticket.CaseDescription, SPR_eticket.SubmitFrom, SPR_eticket.EmailFrom FROM SPR_eticket"
        sSQL += " INNER JOIN CFG_productGrp ON SPR_eticket.ProductGrp = CFG_productGrp.ProductGrp"
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
            tbProductGrp.Text = oSQLReader.Item("PrdDescription").ToString
            tbProduct.Text = oSQLReader.Item("ProductName").ToString
            tbMenu.Text = oSQLReader.Item("MenuName").ToString
            tbSubMenu.Text = oSQLReader.Item("SubmenuName").ToString
            tbDescription.Text = oSQLReader.Item("CaseDescription").ToString
            tbFrom.Text = oSQLReader.Item("SubmitFrom").ToString
            tbSubject.Text = oSQLReader.Item("Subject").ToString
            Session("sEmailfrom") = oSQLReader.Item("EmailFrom").ToString
            Dim dateBirthday As Date = oSQLReader.Item("Tdate").ToString
            tbDate.Text = dateBirthday.ToString("dd-MM-yyyy")

            oCnct.Close()
            CaseId()
        Else
            'Refreshh()
        End If
        oCnct.Close()
    End Sub
    Public Function ExecuteQuery(ByVal cmd As SqlCommand, ByVal action As String) As DataTable
        Dim conString As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ConnectionString
        Using con As New SqlConnection(conString)
            cmd.Connection = con
            Select Case action
                Case "SELECT"
                    Using sda As New SqlDataAdapter()
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            Return dt
                        End Using
                    End Using
                Case "UPDATE"
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                    Exit Select
            End Select
            Return Nothing
        End Using
    End Function

    Sub productGrp()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_ProductGrp"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                dlPrdGrp.DataSource = dt
                dlPrdGrp.DataTextField = "PrdDescription"
                dlPrdGrp.DataValueField = "ProductGrp"
                dlPrdGrp.DataBind()
                dlPrdGrp.Items.Insert(0, "")
            End Using
        End Using
    End Sub
    Sub product()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_Product where ProductGrp = '" & dlPrdGrp.SelectedValue & "'"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                dlProduct.DataSource = dt
                dlProduct.DataTextField = "ProductName"
                dlProduct.DataValueField = "ProductID"
                dlProduct.DataBind()
                dlProduct.Items.Insert(0, "")
            End Using
        End Using
    End Sub
    Sub menu()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_ProductMenu where ProductID = '" & dlProduct.SelectedValue & "'"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                dlMenu.DataSource = dt
                dlMenu.DataTextField = "MenuName"
                dlMenu.DataValueField = "MenuID"
                dlMenu.DataBind()
                dlMenu.Items.Insert(0, "")
            End Using
        End Using
    End Sub
    Sub submenu()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_productSubMenu where ProductID = '" & dlProduct.SelectedValue & "' and MenuID ='" & dlMenu.SelectedValue & "'"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                dlSubmenu.DataSource = dt
                dlSubmenu.DataTextField = "SubmenuName"
                dlSubmenu.DataValueField = "SubmenuID"
                dlSubMenu.DataBind()
                dlSubMenu.Items.Insert(0, "")
            End Using
        End Using
    End Sub
    Sub UserUpdate()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_user where isActive ='Y' order by recID asc"
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
    Sub detailAssignEdit()
        Dim oSQLReader As SqlDataReader
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT SPR_eticket.*, CFG_customer.CustName, CFG_customer.Phone, CFG_Product.ProductName, CFG_productMenu.MenuName, CFG_productSubMenu.SubmenuName FROM SPR_eticket"
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
            dlPrdGrp.SelectedValue = oSQLReader.Item("ProductGrp").ToString
            Produkid = oSQLReader.Item("ProductID").ToString
            Menuid = oSQLReader.Item("MenuID").ToString
            SubMenuid = oSQLReader.Item("SubMenuID").ToString
            tbDescription.Text = oSQLReader.Item("CaseDescription").ToString
            tbFrom.Text = oSQLReader.Item("SubmitFrom").ToString
            tbSubject.Text = oSQLReader.Item("Subject").ToString
            dlCase.SelectedValue = oSQLReader.Item("CaseID").ToString
            dlAssign.SelectedValue = oSQLReader.Item("AssignTo").ToString
            Session("sEmailfrom") = oSQLReader.Item("EmailFrom").ToString
            Dim dateBirthday As Date = oSQLReader.Item("Tdate").ToString
            tbDate.Text = dateBirthday.ToString("dd-MM-yyyy")

            oCnct.Close()
            CaseId()
        Else
            'Refreshh()
        End If
        oCnct.Close()
    End Sub
    Sub updatedata()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        'upload()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        Dim regDate As Date = Date.ParseExact(tbDate.Text, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture)
        sSQL = "UPDATE SPR_eticket SET Tdate='" & regDate & "',Subject='" & Replace(tbSubject.Text, "'", "''") & "', CaseDescription= '" & Replace(tbDescription.Text, "'", "''") & "', ProductGrp= '" & dlPrdGrp.SelectedValue & "', ProductID= '" & dlProduct.SelectedValue & "', MenuID= '" & dlMenu.SelectedValue & "', SubMenuID= '" & dlSubMenu.SelectedValue & "', SubmitFrom= '" & Replace(tbFrom.Text, "'", "''") & "', "
        sSQL += " CaseID='" & dlCase.SelectedValue & "', AssignTo='" & dlAssign.SelectedValue & "' "
        'If uploadname = " " Then
        '    sSQL = sSQL & " "
        'Else
        '    sSQL = sSQL & ", AttachFile= '" & uploadname & "'"
        'End If
        sSQL = sSQL & "WHERE TicketNo = '" & tbTicketno.Text & "' "

        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Data has been update !');document.getElementById('Buttonx').click()", True)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If Session("sStatusTicket") = "1" Then
                lbJudulAssign.Text = "Assignment Ticket"
                detailAssign()
                Userid()
                dlAssign.Enabled = False
                tbDate.Enabled = False
                tbFrom.Enabled = False
                tbDescription.Enabled = False
                tbSubject.Enabled = False
                pnDetail.Visible = True
                pnEdit.Visible = False
                lbStart.Text = "<i class='fa fa-send'></i> Start Follow Up"
            Else
                lbJudulAssign.Text = "Detail Ticket"
                pnDetail.Visible = False
                pnEdit.Visible = True
                dlAssign.Enabled = True
                tbDate.Enabled = True
                tbFrom.Enabled = True
                tbSubject.Enabled = True
                tbDescription.Enabled = True
                productGrp()
                CaseId()
                UserUpdate()
                detailAssignEdit()
                product()
                dlProduct.SelectedValue = Produkid
                menu()
                dlMenu.SelectedValue = Menuid
                submenu()
                dlSubMenu.SelectedValue = SubMenuid
                lbStart.Text = "<i class='fa fa-pencil'></i> Update Ticket"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "date", "date();", True)
            End If
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "PopUser", "clearModal()", True)
        End If
    End Sub
    Protected Sub dlPrdGrp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlPrdGrp.SelectedIndexChanged
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideFormMenu", "hideFormMenu()", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showFormMenu", "$(document).ready(function() { showFormMenu() });", True)
        product()
        menu()
        submenu()
    End Sub
    Protected Sub dlProduct_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlProduct.SelectedIndexChanged
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideFormMenu", "hideFormMenu()", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showFormMenu", "$(document).ready(function() { showFormMenu() });", True)
        menu()
        submenu()
    End Sub

    Protected Sub dlMenu_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlMenu.SelectedIndexChanged
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideFormMenu", "hideFormMenu()", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showFormMenu", "$(document).ready(function() { showFormMenu() });", True)
        submenu()
    End Sub
    Protected Sub lbStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbStart.Click
        If lbJudulAssign.Text = "Detail Ticket" Then
            If dlProduct.Text = "" Then
                dlProduct.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('please choose the product first!');", True)
            ElseIf dlMenu.Text = "" Then
                dlMenu.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('please choose the menu first!');", True)
            ElseIf dlSubMenu.Text = "" Then
                dlSubMenu.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('please choose the submenu first!');", True)
            ElseIf tbFrom.Text = Nothing Then
                tbFrom.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('please enter the ticket from!');", True)
            ElseIf tbHotelName.Text = Nothing Then
                tbHotelName.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('please enter the Hotel Name!');", True)
            ElseIf tbDescription.Text = Nothing Then
                tbDescription.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('please enter the ticket description!');", True)
            Else
                If oCnct.State = ConnectionState.Closed Then
                    oCnct.Open()
                End If
                oSQLCmd = New SqlCommand(sSQL, oCnct)
                sSQL = "SELECT TicketNo FROM SPR_eticket WHERE TicketNo = '" & tbTicketno.Text & "'"
                oSQLCmd.CommandText = sSQL
                oSQLReader = oSQLCmd.ExecuteReader

                If oSQLReader.Read Then
                    oSQLReader.Close()
                    updatedata()
                Else
                    oSQLReader.Close()
                End If
                oCnct.Close()
                Response.Redirect("iPxCrmDetailFollowUser.aspx")
            End If
        Else
            If dlCase.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('please select case first !');", True)
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideFormMenu", "hideFormMenu()", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showFormMenu", "$(document).ready(function() { showFormMenu() });", True)
            Else
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
                    Response.Redirect("iPxCrmDetailFollowUser.aspx")
                End If
            End If
        End If
    End Sub

    Protected Sub lbAbortStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAbortStart.Click
        Response.Redirect("iPxCrmAssign.aspx")
    End Sub
End Class
