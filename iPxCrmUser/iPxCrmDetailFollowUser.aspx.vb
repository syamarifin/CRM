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

Partial Class iPxCrmUser_iPxCrmDetailFollowUser
    Inherits System.Web.UI.Page
    Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Dim oCnct As SqlConnection = New SqlConnection(sCnct)
    Dim oSQLCmd As SqlCommand
    Dim oSQLReader As SqlDataReader
    Dim sSQL, Ticketno, i, Produkid, Menuid, SubMenuid As String
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
        sSQL = "select a.*,(select name from CFG_user where recID = a.AssignTo)as supportBy,(select CustName from CFG_customer  where CFG_customer.CustID = a.CustID)as HotelName from "
        sSQL += "(SELECT SPR_eticket.CustID, SPR_eticket.TicketNo, SPR_eticket.Tdate, SPR_eticket.CaseDescription, SPR_eticket.SubmitFrom, CFG_Product.ProductName, CFG_productMenu.MenuName, CFG_productSubMenu.SubmenuName"
        sSQL += ", CFG_caseID.Description,CFG_FollowupSts.stsDescription, AssignTo, Subject, SPR_eticket.SubMenuID From SPR_eticket "
        sSQL += "INNER JOIN CFG_Product ON SPR_eticket.ProductID = CFG_Product.ProductID INNER JOIN CFG_FollowupSts ON SPR_eticket.StatusID = CFG_FollowupSts.StatusID "
        sSQL += "INNER JOIN CFG_productMenu ON SPR_eticket.MenuID = CFG_productMenu.MenuID and CFG_Product.ProductID = CFG_productMenu.ProductID LEFT JOIN CFG_caseID ON SPR_eticket.CaseID = CFG_caseID.CaseID "
        sSQL += "INNER JOIN CFG_productSubMenu ON SPR_eticket.SubmenuID = CFG_productSubMenu.SubmenuID where SPR_eticket.TicketNo = '" & Session("sTicketFollow") & "') as a"
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        oSQLReader = oSQLCmd.ExecuteReader

        If oSQLReader.Read Then

            lbNamaHotel.Text = oSQLReader.Item("HotelName").ToString.Trim
            lbTicketNo.Text = oSQLReader.Item("TicketNo").ToString.Trim
            Dim dateBirthday As Date = oSQLReader.Item("Tdate").ToString
            lbTgl.Text = dateBirthday.ToString("dd MMMM yyyy hh:mm:ss")
            lbProduct.Text = UCase(oSQLReader.Item("ProductName").ToString.Trim)
            lbMenu.Text = UCase(oSQLReader.Item("MenuName").ToString.Trim)
            lbSubMenu.Text = UCase(oSQLReader.Item("SubmenuName").ToString.Trim)
            lbSubject.Text = oSQLReader.Item("Subject").ToString.Trim
            lbCase.Text = oSQLReader.Item("Description").ToString.Trim
            Dim a As String = oSQLReader.Item("CaseDescription").ToString.Trim
            lbDescription.Text = Replace(a, vbLf, "<br />")
            lbStatus.Text = StrConv(oSQLReader.Item("stsDescription").ToString.Trim, VbStrConv.ProperCase)
            lbSuportby.Text = oSQLReader.Item("supportBy").ToString.Trim
            Session("sLinkSOP") = oSQLReader.Item("SubMenuID").ToString.Trim
            
            oSQLReader.Close()
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Sorry User Is Not Registered Yet or Not Active !');document.getElementById('Buttonx').click()", True)
        End If
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
                rptFollow.DataSource = dt
                rptFollow.DataBind()
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
        sSQL += "INNER JOIN CFG_productMenu ON SPR_eticket.MenuID = CFG_productMenu.MenuID LEFT JOIN CFG_caseID ON SPR_eticket.CaseID = CFG_caseID.CaseID "
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
            oCnct.Close()
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
                dlStatus.DataSource = dt
                dlStatus.DataTextField = "stsDescription"
                dlStatus.DataValueField = "StatusID"
                dlStatus.DataBind()
                dlUpStatus.DataSource = dt
                dlUpStatus.DataTextField = "stsDescription"
                dlUpStatus.DataValueField = "StatusID"
                dlUpStatus.DataBind()
            End Using
        End Using
    End Sub
    Sub statusAll()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_FollowupSts where StatusID <>'" & "7" & "'"
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
    Sub savedata()
        
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        Dim regDate As Date = Date.Now()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "INSERT INTO SPR_eticketFollowUp (CustID, TicketNo, FollowUpNo, FollowUpDate, FollowUpBy, FollowUpNote, FollowUpSopLink, FollowUpCode, isRead) "
        sSQL += "VALUES ('" & Session("sCustID") & "','" & Session("sTicketFollow") & "','" & tbNo.Text & "','" & regDate & "','" & Replace(tbBy.Text, "'", "''") & "','" & Replace(tbNote.Text, "'", "''") & "','" & Replace(tbLink.Text, "'", "''") & "','" & "P" & "','" & "1" & "') "
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('data send successfully !');document.getElementById('Buttonx').click()", True)
        ListTicket()
        ListFollow()
        tbBy.Text = ""
        tbLink.Text = ""
        tbNo.Text = ""
        tbNote.Text = ""
    End Sub
    Sub updateStatus()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If

        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "UPDATE SPR_eticket SET StatusID= '" & dlStatus.SelectedValue & "'"
        sSQL = sSQL & "WHERE TicketNo = '" & Session("sTicketFollow") & "' "

        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Assignment is saved successfully !');document.getElementById('Buttonx').click()", True)
    End Sub
    Sub updateStatus1()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        If dlUpStatus.SelectedValue = "1" Then
            oSQLCmd = New SqlCommand(sSQL, oCnct)
            sSQL = "UPDATE SPR_eticket SET StatusID= '" & dlUpStatus.SelectedValue & "', CaseID='', AssignTo='0'"
            sSQL = sSQL & "WHERE TicketNo = '" & Session("sTicketFollow") & "' "
        Else
            oSQLCmd = New SqlCommand(sSQL, oCnct)
            sSQL = "UPDATE SPR_eticket SET StatusID= '" & dlUpStatus.SelectedValue & "'"
            sSQL = sSQL & "WHERE TicketNo = '" & Session("sTicketFollow") & "' "
        End If

        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Assignment is saved successfully !');document.getElementById('Buttonx').click()", True)
    End Sub
    Sub updateSupportby()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If

        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "UPDATE SPR_eticket SET AssignTo= '" & dlUpStatus.SelectedValue & "'"
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
        sSQL = "SELECT * FROM CFG_user where Email='" & Session("sEmail") & "' and isActive ='Y' order by recID asc"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                dlAssign.DataSource = dt
                dlAssign.DataTextField = "name"
                dlAssign.DataValueField = "recID"
                dlAssign.DataBind()

                dlUpStatus.DataSource = dt
                dlUpStatus.DataTextField = "name"
                dlUpStatus.DataValueField = "recID"
                dlUpStatus.DataBind()
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
        sSQL = "SELECT SPR_eticket.TicketNo,SPR_eticket.Subject,SPR_eticket.Tdate, CFG_customer.CustName, CFG_customer.Phone,CFG_productGrp.PrdDescription , CFG_Product.ProductName, CFG_productMenu.MenuName, CFG_productSubMenu.SubmenuName, SPR_eticket.CaseDescription, SPR_eticket.SubmitFrom, SPR_eticket.EmailFrom FROM SPR_eticket"
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
    Sub totalFollow()
        Dim oSQLReader As SqlDataReader
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT count(*) as total FROM SPR_eticketFollowUp where TicketNo = '" & Session("sTicketFollow") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        'usercode, mobileno, password, signupdate, fullname, status, quid
        If oSQLReader.HasRows Then
            Dim a As String = oSQLReader.Item("total").ToString
            oCnct.Close()
            If a <> "0" Then
                status()
            Else
                statusAll()
            End If
        End If
    End Sub
    Sub SOPlink()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT CFG_linkSOP.SubmenuID, CFG_linkSOP.Link, CFG_linkSOP.SopID, CFG_productGrp.productGrp, CFG_productGrp.PrdDescription, "
        sSQL += "CFG_Product.ProductName, CFG_productMenu.MenuName,CFG_productSubMenu.SubmenuName"
        sSQL += " FROM CFG_linkSOP "
        sSQL += "INNER JOIN CFG_Product ON CFG_linkSOP.ProductID = CFG_Product.ProductID "
        sSQL += "INNER JOIN CFG_productMenu ON CFG_linkSOP.ProductID = CFG_productMenu.ProductID AND CFG_linkSOP.MenuID = CFG_productMenu.MenuID "
        sSQL += "INNER JOIN CFG_productSubMenu ON CFG_linkSOP.ProductID=CFG_productSubMenu.ProductID AND CFG_linkSOP.MenuID = CFG_productSubMenu.MenuID AND CFG_linkSOP.SubmenuID = CFG_productSubMenu.SubmenuID "
        sSQL += "INNER JOIN CFG_productGrp ON CFG_productGrp.ProductGrp = CFG_Product.ProductGrp "
        sSQL += "WHERE CFG_linkSOP.SubmenuID='" & Session("sLinkSOP") & "'"

        sSQL += " order by CFG_linkSOP.SopID asc"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                If dt.Rows.Count <> 0 Then
                    gvSOP.DataSource = dt
                    gvSOP.DataBind()
                Else
                    dt.Rows.Add(dt.NewRow())
                    gvSOP.DataSource = dt
                    gvSOP.DataBind()
                    gvSOP.Rows(0).Visible = False
                End If
            End Using
        End Using
        oCnct.Close()
    End Sub
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
        If Session("sId") = Nothing Then
            Response.Redirect("../login.aspx")
        End If
        If Not Me.IsPostBack Then
            ListTicket()
            ListFollow()
            ListStatus()
            If Session("sStatusTicket") = "1" Then
                lbJudulDetail.Text = "Detail e-Ticket"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showFormMenu", "$(document).ready(function() { showFormMenu() });", True)
                detailAssign()
                Userid()
                pnDetail.Visible = True
                pnEdit.Visible = False
                tbSubject.Enabled = False
                dlAssign.Enabled = False
                tbTicketno.Enabled = False
                tbHotelName.Enabled = False
                tbContact.Enabled = False
                tbFrom.Enabled = False
                tbDescription.Enabled = False
                tbDate.Enabled = False
                lbStart.Text = "<i class='fa fa-send'></i> Start Follow Up"
            Else
                If Session("sPossition") <> "0" Then
                    updateRead()
                End If
            End If
            If Session("sStatusTicket") = 7 Then
                lbAddFollow.Enabled = False
                lbUpStatus.Enabled = False
            Else
                lbAddFollow.Enabled = True
                lbUpStatus.Enabled = True
            End If
            If Session("sPossition") = "0" Then
                lbEditAssigned.Visible = True
            Else
                lbEditAssigned.Visible = False
            End If
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

    Protected Sub lbAddFollow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAddFollow.Click
        status()
        dlStatus.SelectedValue = Session("sStatusTicket")
        tbBy.Text = Session("sName")
        tbNo.Text = cIpx.GetCounterMBR("FP", "FP")
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalAdd", "showModalAdd()", True)
    End Sub

    Protected Sub lbSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSave.Click
        If tbBy.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('please enter by !');", True)
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalAdd", "hideModalAdd()", True)
            tbBy.Text = Session("sName")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalAdd", "showModalAdd()", True)
        ElseIf tbNote.Text = "" Then
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalAdd", "hideModalAdd()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('please enter Note !');", True)
            'tbNote.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalAdd", "showModalAdd()", True)
        Else
            If oCnct.State = ConnectionState.Closed Then
                oCnct.Open()
            End If
            oSQLCmd = New SqlCommand(sSQL, oCnct)
            sSQL = "SELECT FollowUpNo FROM SPR_eticketFollowUp WHERE FollowUpNo = '" & tbNo.Text & "'"
            oSQLCmd.CommandText = sSQL
            oSQLReader = oSQLCmd.ExecuteReader

            If oSQLReader.Read Then
                oSQLReader.Close()
                'updatedata()
                updateStatus()
            Else
                oSQLReader.Close()
                updateStatus()
                savedata()
            End If
            oCnct.Close()
            If Session("sPossition") = "0" Then
                updateRead()
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalAdd", "hideModalAdd()", True)
            Response.Redirect("iPxCrmDetailFollowUser.aspx")
        End If
    End Sub

    Protected Sub lbAbort_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAbort.Click
        If Session("sStatusTicket") = 6 Then
            Response.Redirect("iPxCrmDone.aspx")
        Else
            Response.Redirect("iPxCrmAssign.aspx")
        End If
    End Sub

    Protected Sub lbAbort1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAbort1.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalAdd", "hideModalAdd()", True)
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

    Protected Sub lbUpStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbUpStatus.Click
        lbJudul.Text = "Status e-Ticket"
        'status()
        totalFollow()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalFormSub", "showModalFormSub()", True)
    End Sub

    Protected Sub lbSaveupStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSaveupStatus.Click
        If lbJudul.Text = "Status e-Ticket" Then
            updateStatus1()
            If dlUpStatus.SelectedValue = "1" Then
                Response.Redirect("iPxCrmAssign.aspx")
            End If
        ElseIf lbJudul.Text = "Support by" Then
            updateSupportby()
        End If
        ListTicket()
        ListStatus()
        ListFollow()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideFormMenu", "hideFormMenu()", True)
        'Response.Redirect("iPxCrmDetailFollowUser.aspx")
    End Sub

    Protected Sub lbAbortupStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAbortupStatus.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalFormSub", "hideFormMenu()", True)
    End Sub

    Protected Sub lbAbortStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAbortStart.Click
        If lbJudulDetail.Text = "Edit e-Ticket" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideFormMenu", "hideFormMenu()", True)
        Else
            Response.Redirect("iPxCrmAssign.aspx")
        End If
    End Sub

    Protected Sub lbStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbStart.Click
        If lbJudulDetail.Text = "Edit e-Ticket" Then
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
                ListTicket()
                ListFollow()
                ListStatus()
                updateRead()
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
                    ListTicket()
                    ListFollow()
                    ListStatus()
                    updateRead()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideFormMenu", "hideFormMenu()", True)
                End If
            End If
        End If
    End Sub

    Protected Sub lbFindSOP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbFindSOP.Click
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalAdd", "hideModalAdd()", True)
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalAdd", "showModalAdd()", True)
        SOPlink()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showFormSOP", "showFormSOP()", True)
    End Sub

    Protected Sub lbAbortSOP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAbortSOP.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalAdd", "showModalAdd()", True)
    End Sub

    Protected Sub gvSOP_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvSOP.RowCommand
        If e.CommandName = "getEdit" Then
            i = e.CommandArgument.ToString
            Dim oSQLReader As SqlDataReader
            If oCnct.State = ConnectionState.Closed Then
                oCnct.Open()
            End If
            oSQLCmd = New SqlCommand(sSQL, oCnct)
            sSQL = "SELECT * FROM CFG_linkSOP"
            sSQL += " WHERE CFG_linkSOP.SopID ='" & i & "'"
            oSQLCmd.CommandText = sSQL
            oSQLReader = oSQLCmd.ExecuteReader

            oSQLReader.Read()
            'usercode, mobileno, password, signupdate, fullname, status, quid
            If oSQLReader.HasRows Then
                tbLink.Text = oSQLReader.Item("Link").ToString
            Else
                'Refreshh()
            End If
            oCnct.Close()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalAdd", "showModalAdd()", True)
        End If
    End Sub

    Protected Sub lbEditAssigned_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbEditAssigned.Click
        lbJudulDetail.Text = "Edit e-Ticket"
        productGrp()
        pnDetail.Visible = False
        pnEdit.Visible = True
        tbSubject.Enabled = True
        tbTicketno.Enabled = True
        tbHotelName.Enabled = True
        tbContact.Enabled = True
        tbFrom.Enabled = True
        tbDate.Enabled = True
        tbDescription.Enabled = True
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
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showFormMenu", "showFormMenu();", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "date", "date();", True)
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showFormMenu", "$(document).ready(function() { showFormMenu() });", True)
    End Sub
End Class
