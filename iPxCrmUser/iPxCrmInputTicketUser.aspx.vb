Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Imports System.IO
Partial Class iPxCrmUser_iPxCrmInputTicketUser
    Inherits System.Web.UI.Page
    Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Dim oCnct As SqlConnection = New SqlConnection(sCnct)
    Dim oSQLCmd As SqlCommand
    Dim oSQLReader As SqlDataReader
    Dim sSQL, Ticketno, i, uploadname As String
    Dim cIpx As iPxClass
    Sub upload()
        Dim folderPath As String = Server.MapPath("~/UploadFile/")
        Dim file As HttpPostedFile = FileUpload1.PostedFile
        Dim iFileSize As Integer = file.ContentLength

        'Check whether Directory (Folder) exists.
        If Not Directory.Exists(folderPath) Then
            'If Directory (Folder) does not exists. Create it.
            Directory.CreateDirectory(folderPath)
        End If
        Dim fileDate As Date = Date.Now()
        If FileUpload1.FileName = "" Then
            savedata()
        Else
            If iFileSize > 10485760 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('file size exceeds the maximum limit!');", True)
            Else
                'Save the File to the Directory (Folder).
                FileUpload1.SaveAs(folderPath & tbTicketno.Text & Path.GetFileName(FileUpload1.FileName))
                uploadname = "~/UploadFile/" & tbTicketno.Text & Path.GetFileName(FileUpload1.FileName)
                savedata()
            End If
        End If
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
    Sub submitVia()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_submitVia order by SubmitVia asc"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                dlSubmitVia.DataSource = dt
                dlSubmitVia.DataTextField = "SubmitVia"
                dlSubmitVia.DataValueField = "SubmitVia"
                dlSubmitVia.DataBind()
            End Using
        End Using
    End Sub
    Sub dataHotel()
        Dim oSQLReader As SqlDataReader
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_customer where CustID = '" & Session("sCustName") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        'usercode, mobileno, password, signupdate, fullname, status, quid
        If oSQLReader.HasRows Then
            Session("sHotelname") = oSQLReader.Item("CustName").ToString
            Session("sHotelcontact") = oSQLReader.Item("Phone").ToString
            tbHotelName.Text = Session("sHotelname")
            tbContact.Text = Session("sHotelcontact")
            oCnct.Close()
        End If
    End Sub
    Sub savedata()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        'Dim regDate As Date = Date.Now()
        Dim regDate As Date = Date.ParseExact(tbDate.Text, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture)
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "INSERT INTO  SPR_eticket(CustID, TicketNo, Tdate, CreateBy, SubmitFrom, Subject, EmailFrom, SubmitVia, ProductGrp, ProductID, MenuID, SubMenuID, CaseID, CaseDescription, AttachFile, AssignTo, StatusID) "
        sSQL = sSQL & "VALUES ('" & Session("sHotelID") & "','" & tbTicketno.Text & "','" & regDate + " " + Format(Now, "hh:mm:ss") & "','" & Session("sEmail") & "','" & Replace(tbFrom.Text, "'", "''") & "','" & Replace(tbSubject.Text, "'", "''") & "','" & Replace(tbEmailFrom.Text, "'", "''") & "','" & dlSubmitVia.SelectedValue & "','" & dlPrdGrp.SelectedValue & "','" & dlProduct.SelectedValue & "','" & dlMenu.SelectedValue & "','" & dlSubMenu.SelectedValue & "', "
        sSQL = sSQL & "'" & "" & "','" & Replace(tbDescription.Text, "'", "''") & "','" & uploadname & "','" & "" & "','" & "1" & "') "
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Data has been saved !');document.getElementById('Buttonx').click()", True)
        'ListTicket()
        product()
        menu()
        submenu()
        tbFrom.Text = ""
        tbDescription.Text = ""
        Session("sIdTiket") = ""
        Response.Redirect("iPxCrmNewtiket.aspx")
    End Sub
    Sub updatedata()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        'upload()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        Dim regDate As Date = Date.ParseExact(tbDate.Text, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture)
        sSQL = "UPDATE SPR_eticket SET CustID='" & Session("sHotelID") & "',Tdate='" & regDate + " " + Format(Now, "hh:mm:ss") & "',emailfrom='" & Replace(tbEmailFrom.Text, "'", "''") & "',Subject='" & Replace(tbSubject.Text, "'", "''") & "', CaseDescription= '" & Replace(tbDescription.Text, "'", "''") & "', ProductID= '" & dlProduct.SelectedValue & "', MenuID= '" & dlMenu.SelectedValue & "', SubMenuID= '" & dlSubMenu.SelectedValue & "', SubmitFrom= '" & Replace(tbFrom.Text, "'", "''") & "', SubmitVia= '" & dlSubmitVia.SelectedValue & "'"
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
        'ListTicket()
        product()
        menu()
        submenu()
        tbFrom.Text = ""
        tbDescription.Text = ""
        Response.Redirect("iPxCrmNewtiket.aspx")
    End Sub
    Sub IdMenu()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT MAX(MenuID) as MenuID FROM CFG_ProductMenu where ProductID = '" & dlProduct.SelectedValue & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            tbMenuID.Text = Val(Mid(oSQLReader.Item("MenuID").ToString, 6, 2)) + 1
            If Len(tbMenuID.Text) = 1 Then
                tbMenuID.Text = dlProduct.SelectedValue & "-0" & tbMenuID.Text & ""
            ElseIf Len(tbMenuID.Text) = 2 Then
                tbMenuID.Text = dlProduct.SelectedValue & "-" & tbMenuID.Text & ""
            End If
        Else
            tbMenuID.Text = dlProduct.SelectedValue & "-01"
        End If
        oCnct.Close()
    End Sub
    Sub IdSubmenu()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT MAX(SubmenuID) as SubmenuID FROM CFG_ProductSubMenu where MenuID = '" & dlMenu.SelectedValue & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            tbSubID.Text = Val(Mid(oSQLReader.Item("SubmenuID").ToString, 9, 2)) + 1
            If Len(tbSubID.Text) = 1 Then
                tbSubID.Text = dlMenu.SelectedValue & "-0" & tbSubID.Text & ""
            ElseIf Len(tbSubID.Text) = 2 Then
                tbSubID.Text = dlMenu.SelectedValue & "-" & tbSubID.Text & ""
            End If
        Else
            tbSubID.Text = dlMenu.SelectedValue & "-01"
        End If
        oCnct.Close()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            If Session("sIn1") = "" Then
                If Session("sIdTiket") = "" Then
                    tbTicketno.Text = cIpx.GetCounterMBR("PX", "PX")
                    Session("sIdTiket") = tbTicketno.Text
                Else
                    tbTicketno.Text = Session("sIdTiket")
                End If
                tbDate.Text = Format(Now, "dd-MM-yyyy")
                productGrp()
                product()
                menu()
                submitVia()
                submenu()
                dataHotel()
                Session("sHotelID") = Session("sCustName")
                Session("sCustName") = ""
                tbFrom.Text = ""
                tbDescription.Text = ""
                FileUpload1.Visible = True
                lbFile.Visible = False
                lbSave.Text = "<i class='fa fa-send'></i> Send E-Ticket"
                lbDelete.Visible = False
            Else
                If Session("sPossition") <> "0" Then
                    lbDelete.Enabled = False
                Else
                    lbDelete.Enabled = True
                    Dim dateBirthday As Date = Session("sIn7")
                    tbDate.Text = dateBirthday.ToString("dd-MM-yyyy")
                    tbSubject.Text = Session("sIn8")
                    productGrp()
                    submitVia()
                    dlSubmitVia.SelectedValue = Session("sIn10")
                    lbDelete.Visible = True
                    dlPrdGrp.SelectedValue = Session("sIn9")
                    tbTicketno.Text = Session("sIn1")
                    product()
                    dlProduct.SelectedValue = Session("sIn2")
                    menu()
                    dlMenu.SelectedValue = Session("sIn3")
                    submenu()
                    dlSubMenu.SelectedValue = Session("sIn4")
                    tbDescription.Text = Session("sIn5")
                    tbFrom.Text = Session("sIn6")
                    FileUpload1.Visible = False
                    lbFile.Text = "Attachment " + Session("sIn11")
                    tbEmailFrom.Text = Session("sIn12")
                    dataHotel()
                    Session("sHotelID") = Session("sCustName")
                    'Session("sCustName") = ""
                    lbSave.Text = "<i class='fa fa-send'></i> Update E-Ticket"
                End If
            End If
        End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "date", "$(document).ready(function() {date()});", True)
    End Sub
    Protected Sub lbSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSave.Click
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
                upload()
            End If
            oCnct.Close()
        End If  'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalAdd", "hideModalAdd()", True)
    End Sub
    Protected Sub dlProduct_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlProduct.SelectedIndexChanged
        menu()
        submenu()
    End Sub

    Protected Sub dlMenu_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlMenu.SelectedIndexChanged
        submenu()
    End Sub

    Protected Sub lbCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCancel.Click
        Session("sIdTiket") = ""
        Response.Redirect("iPxCrmNewtiket.aspx")
    End Sub
    Protected Sub dlPrdGrp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlPrdGrp.SelectedIndexChanged
        product()
        menu()
        submenu()
    End Sub

    Protected Sub lbAddMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAddMenu.Click
        If Me.dlProduct.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('please choose the product first!');", True)
        Else
            IdMenu()
            tbDetailProduct.Text = dlProduct.SelectedItem.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showFormMenu", "showFormMenu()", True)
        End If
    End Sub

    Protected Sub lbAddSubmenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAddSubmenu.Click
        If dlProduct.Text = "" Or dlMenu.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('please choose the product and menu first!');", True)
        Else
            IdSubmenu()
            tbSubID.Enabled = False
            tbDetailProductSub.Text = dlProduct.SelectedItem.ToString
            tbDetailMenuSub.Text = dlMenu.SelectedItem.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalFormSub", "showModalFormSub()", True)
        End If
    End Sub

    Protected Sub lbSaveMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSaveMenu.Click
        If tbMenuID.Text = "" Then
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideFormMenu", "hideFormMenu()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('please enter the menu id!');", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showFormMenu", "showFormMenu()", True)
            'tbMenuID.Focus()
        ElseIf tbMenuName.Text = "" Then
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideFormMenu", "hideFormMenu()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('please enter the menu name!');", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showFormMenu", "showFormMenu()", True)
            'tbMenuName.Focus()
        Else
            If oCnct.State = ConnectionState.Closed Then
                oCnct.Open()
            End If
            oSQLCmd = New SqlCommand(sSQL, oCnct)
            sSQL = "INSERT INTO  CFG_productMenu(ProductID, MenuID, MenuName) "
            sSQL = sSQL & "VALUES ('" & dlProduct.SelectedValue & "','" & tbMenuID.Text & "','" & tbMenuName.Text & "') "
            oSQLCmd.CommandText = sSQL
            oSQLCmd.ExecuteNonQuery()

            oCnct.Close()
            menu()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideFormMenu", "hideFormMenu()", True)
        End If
    End Sub

    Protected Sub lbSaveSub_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSaveSub.Click
        If tbSubmenu.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('please enter the submenu name!');", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalFormSub", "showModalFormSub()", True)
        Else
            If oCnct.State = ConnectionState.Closed Then
                oCnct.Open()
            End If
            oSQLCmd = New SqlCommand(sSQL, oCnct)
            sSQL = "INSERT INTO  CFG_productSubMenu( ProductID, MenuID, SubmenuID, SubmenuName, link) "
            sSQL = sSQL & "VALUES ('" & dlProduct.SelectedValue & "','" & dlMenu.SelectedValue & "','" & tbSubID.Text & "','" & tbSubmenu.Text & "','" & Replace(tbLink.Text, "'", "''") & "') "
            oSQLCmd.CommandText = sSQL
            oSQLCmd.ExecuteNonQuery()

            oCnct.Close()
            submenu()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalFormSub", "hideModalFormSub()", True)
        End If
    End Sub

    Protected Sub lbDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbDelete.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalAdd", "showModalAdd()", True)
    End Sub

    Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton2.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalAdd", "hideModalAdd()", True)
    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        'sSQL = "UPDATE SPR_eticket SET StatusID ='" & "0" & "' where TicketNo='" & Session("sIn1") & "'"
        sSQL = "DELETE FROM SPR_eticket where TicketNo='" & Session("sIn1") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        'sSQL = "UPDATE SPR_eticket SET StatusID ='" & "0" & "' where TicketNo='" & Session("sIn1") & "'"
        sSQL = "DELETE FROM SPR_eticketFollowUp where TicketNo='" & Session("sIn1") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        Response.Redirect("iPxCrmNewtiket.aspx")
    End Sub

    Protected Sub lbFindHotel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbFindHotel.Click
        Response.Redirect("findCustomer.aspx")
    End Sub
End Class
