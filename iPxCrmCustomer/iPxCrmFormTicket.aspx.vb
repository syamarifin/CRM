Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Imports System.IO
Partial Class iPxCrmCustomer_iPxCrmFormTicket
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
        sSQL = "SELECT * FROM CFG_productSubMenu where MenuID ='" & dlMenu.SelectedValue & "'"
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
    Sub dataHotel()
        Dim oSQLReader As SqlDataReader
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_customer where CustID = '" & Session("sCId") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        'usercode, mobileno, password, signupdate, fullname, status, quid
        If oSQLReader.HasRows Then
            Session("sCHotelname") = oSQLReader.Item("CustName").ToString
            Session("sCHotelcontact") = oSQLReader.Item("Phone").ToString
            tbHotelName.Text = Session("sCHotelname")
            tbContact.Text = Session("sCHotelcontact")
            oCnct.Close()
        End If
    End Sub
    Sub savedata()
        'Dim a As String = dlProduct.SelectedValue
        'Dim sfrom, desc As String
        'sfrom = txTicketFrom.Text
        'desc = txtTicketDescription.Text
        'upload()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        Dim regDate As Date = Date.Now()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "INSERT INTO  SPR_eticket(CustID, TicketNo, Tdate, CreateBy, SubmitFrom, EmailFrom, SubmitVia, ProductGrp, ProductID, MenuID, SubMenuID, CaseID, CaseDescription, AttachFile, AssignTo, StatusID) "
        sSQL = sSQL & "VALUES ('" & Session("sCId") & "','" & tbTicketno.Text & "','" & regDate & "','" & Session("sCEmail") & "','" & Replace(tbFrom.Text, "'", "''") & "','" & Session("sCEmail") & "','" & "ETICKET" & "','" & dlPrdGrp.SelectedValue & "','" & dlProduct.SelectedValue & "','" & dlMenu.SelectedValue & "','" & dlSubMenu.SelectedValue & "', "
        sSQL = sSQL & "'" & "" & "','" & Replace(tbDescription.Text, "'", "''") & "','" & uploadname & "','" & "" & "','" & "1" & "') "
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('data send successfully !');document.getElementById('Buttonx').click()", True)
        'ListTicket()
        product()
        menu()
        submenu()
        tbFrom.Text = ""
        tbDescription.Text = ""
        Response.Redirect("iPxTicketCustomer.aspx")
    End Sub
    Sub updatedata()
        upload()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If

        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "UPDATE SPR_eticket SET CaseDescription= '" & Replace(tbDescription.Text, "'", "''") & "', ProductID= '" & dlProduct.SelectedValue & "', MenuID= '" & dlMenu.SelectedValue & "', SubMenuID= '" & dlSubMenu.SelectedValue & "', SubmitFrom= '" & Replace(tbFrom.Text, "'", "''") & "'"
        If uploadname = "" Then
            sSQL = sSQL & " "
        Else
            sSQL = sSQL & ", AttachFile= '" & uploadname & "'"
        End If
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
        Response.Redirect("iPxTicketCustomer.aspx")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            If Session("sCIn1") = "" Then
                tbTicketno.Text = cIpx.GetCounterMBR("MB", "MB")
                productGrp()
                product()
                menu()
                submenu()
                dataHotel()
                tbFrom.Text = Session("sCName")
                tbDescription.Text = ""
            Else
                productGrp()
                dlPrdGrp.SelectedValue = Session("sCIn7")
                product()
                dlProduct.SelectedValue = Session("sCIn2")
                menu()
                dlMenu.SelectedValue = Session("sCIn3")
                submenu()
                dataHotel()
                tbTicketno.Text = Session("sCIn1")
                tbHotelName.Text = Session("sCHotelname")
                tbContact.Text = Session("sCHotelcontact")
                dlSubMenu.SelectedValue = Session("sCIn4")
                tbDescription.Text = Session("sCIn5")
                tbFrom.Text = Session("sCIn6")
            End If
        End If
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
        End If
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalAdd", "hideModalAdd()", True)
    End Sub
    Protected Sub dlProduct_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlProduct.SelectedIndexChanged
        menu()
        submenu()
    End Sub

    Protected Sub dlMenu_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlMenu.SelectedIndexChanged
        submenu()
    End Sub

    Protected Sub lbCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCancel.Click
        Response.Redirect("iPxTicketCustomer.aspx")
    End Sub

    Protected Sub dlPrdGrp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlPrdGrp.SelectedIndexChanged
        product()
        menu()
        submenu()
    End Sub
End Class
