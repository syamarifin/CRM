Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Partial Class iPxCrmMobile_iPxCrmViewFollow
    Inherits System.Web.UI.Page
    Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Dim oCnct As SqlConnection = New SqlConnection(sCnct)
    Dim oSQLCmd As SqlCommand
    Dim oSQLReader As SqlDataReader
    Dim sSQL, Ticketno, i, uploadname As String
    Dim cIpx As iPxClass
    Sub ListTicket()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "select a.*,(select name from CFG_user where recID = a.AssignTo)as supportBy from "
        sSQL += "(SELECT SPR_eticket.CustID, SPR_eticket.TicketNo, SPR_eticket.Tdate, SPR_eticket.CaseDescription, SPR_eticket.SubmitFrom, SPR_eticket.Subject, CFG_Product.ProductName, CFG_productMenu.MenuName, CFG_productSubMenu.SubmenuName"
        sSQL += ", CFG_caseID.Description,CFG_FollowupSts.stsDescription,SPR_eticket.AssignTo,SPR_eticket.AttachFile From SPR_eticket "
        sSQL += "INNER JOIN CFG_Product ON SPR_eticket.ProductID = CFG_Product.ProductID  INNER JOIN CFG_FollowupSts ON SPR_eticket.StatusID = CFG_FollowupSts.StatusID "
        sSQL += "INNER JOIN CFG_productMenu ON SPR_eticket.MenuID = CFG_productMenu.MenuID and CFG_Product .ProductID = CFG_productMenu .ProductID LEFT JOIN CFG_caseID ON SPR_eticket.CaseID = CFG_caseID.CaseID "
        sSQL += "INNER JOIN CFG_productSubMenu ON SPR_eticket.SubmenuID = CFG_productSubMenu.SubmenuID where SPR_eticket.TicketNo = '" & Session("sCIdview") & "') as a"

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
        sSQL += " where TicketNo = '" & Session("sCIdview") & "' order by FollowUpDate asc"

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
        'sSQL = "SELECT SPR_eticket.CustID, SPR_eticket.TicketNo, SPR_eticket.Tdate, SPR_eticket.CaseDescription, SPR_eticket.SubmitFrom, CFG_Product.ProductName, CFG_productMenu.MenuName, CFG_productSubMenu.SubmenuName"
        'sSQL += ", CFG_caseID.Description,SPR_eticket.StatusID From SPR_eticket "
        'sSQL += "INNER JOIN CFG_Product ON SPR_eticket.ProductID = CFG_Product.ProductID "
        'sSQL += "INNER JOIN CFG_productMenu ON SPR_eticket.MenuID = CFG_productMenu.MenuID INNER JOIN CFG_caseID ON SPR_eticket.CaseID = CFG_caseID.CaseID "
        'sSQL += "INNER JOIN CFG_productSubMenu ON SPR_eticket.SubmenuID = CFG_productSubMenu.SubmenuID where SPR_eticket.TicketNo = '" & Session("sIdview") & "'"
        sSQL = "SELECT SPR_eticket.StatusID From SPR_eticket where SPR_eticket.TicketNo = '" & Session("sCIdview") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        'usercode, mobileno, password, signupdate, fullname, status, quid
        If oSQLReader.HasRows Then
            Session("sCStatusTicket") = oSQLReader.Item("StatusID").ToString
            oCnct.Close()
        Else
            'Refreshh()
        End If
    End Sub
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
                FileUpload1.SaveAs(folderPath & tbNo.Text & Path.GetFileName(FileUpload1.FileName))
                uploadname = "~/UploadFile/" & tbNo.Text & Path.GetFileName(FileUpload1.FileName)
                savedata()
            End If
        End If
    End Sub
    Sub savedata()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        Dim regDate As Date = Date.Now()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "INSERT INTO SPR_eticketFollowUp (CustID, TicketNo, FollowUpNo, FollowUpDate, FollowUpBy, FollowUpNote, FollowUpSopLink, FollowUpCode, isRead) "
        sSQL += "VALUES ('" & Session("sCId") & "','" & Session("sCIdview") & "','" & Session("sCNoFollow") & "','" & regDate & "','" & Session("sCName") & "','" & Replace(tbNote.Text, "'", "''") & "','" & uploadname & "','" & "H" & "','" & "1" & "') "
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('data send successfully !');document.getElementById('Buttonx').click()", True)
        ListTicket()
        ListFollow()
        tbNote.Text = ""
    End Sub
    Sub updateStatus()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        Dim nwLat As String
        nwLat = nwLatHidden.Value
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "UPDATE SPR_eticket SET StatusID= '" & "7" & "', Rating = '" & nwLat & "'"
        sSQL = sSQL & " WHERE TicketNo = '" & Session("sCIdview") & "' "

        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Assignment is saved successfully !');document.getElementById('Buttonx').click()", True)
    End Sub
    Sub cekFollow()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT TicketNo FROM SPR_eticketFollowUp WHERE TicketNo = '" & Session("sCIdview") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        If oSQLReader.Read Then
            oSQLReader.Close()
            updateRead()
        Else
            oSQLReader.Close()
        End If
        oCnct.Close()
    End Sub
    Sub updateRead()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If

        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "UPDATE SPR_eticketFollowUp SET isRead= '0' "
        sSQL = sSQL & "WHERE TicketNo = '" & Session("sCIdview") & "' and FollowUpCode = 'P' "

        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Assignment is saved successfully !');document.getElementById('Buttonx').click()", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sCId") = Nothing Then
            Response.Redirect("SignIn.aspx")
        Else
            Dim cph As ContentPlaceHolder = DirectCast(Me.Master.FindControl("ContentPlaceHolder2"), ContentPlaceHolder)
            Dim LinkButton As LinkButton = DirectCast(cph.FindControl("lnkback"), LinkButton)

            LinkButton.PostBackUrl = "iPxTicketCustomer.aspx"
            If Not Me.IsPostBack Then
                ListTicket()
                'ListFollow()
                ListStatus()
                cekFollow()
            End If
            ListFollow()
            If Session("sCStatusTicket") = 6 Then
                LinkButton2.Visible = True
                Panel2.Visible = True
            Else
                LinkButton2.Visible = False
                Panel2.Visible = False
            End If
            'If Session("sCStatusTicket") = 7 Or Session("sCStatusTicket") = 0 Then
            '    LinkButton1.Enabled = False
            'Else
            '    LinkButton1.Enabled = True
            'End If
        End If
    End Sub

    'Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
    '    tbNote.Text = ""
    '    tbNo.Text = cIpx.GetCounterMBR("FH", "FH")
    '    tbBy.Text = Session("sCName")
    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalAdd", "showModalAdd()", True)
    'End Sub

    Protected Sub lbSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSave.Click
        If tbBy.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('please enter by !');", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalAdd", "hideModalAdd()", True)
            'tbBy.Text = Session("sName")
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalAdd", "showModalAdd()", True)
        ElseIf tbNote.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('please enter a note !');", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalAdd", "hideModalAdd()", True)
            'tbNote.Focus()
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalAdd", "showModalAdd()", True)
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
            Else
                oSQLReader.Close()
                upload()
            End If
            oCnct.Close()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalAdd", "hideModalAdd()", True)
        End If
    End Sub

    Protected Sub lbSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSend.Click
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalApproved", "showModalApproved()", True)
        If tbNote.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('please enter a note !');", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalAdd", "hideModalAdd()", True)
            'tbNote.Focus()
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalAdd", "showModalAdd()", True)
        Else
            If oCnct.State = ConnectionState.Closed Then
                oCnct.Open()
            End If
            Session("sCNoFollow") = cIpx.GetCounterMBR("FH", "FH")
            oSQLCmd = New SqlCommand(sSQL, oCnct)
            sSQL = "SELECT FollowUpNo FROM SPR_eticketFollowUp WHERE FollowUpNo = '" & Session("sCNoFollow") & "'"
            oSQLCmd.CommandText = sSQL
            oSQLReader = oSQLCmd.ExecuteReader

            If oSQLReader.Read Then
                oSQLReader.Close()
                'updatedata()
            Else
                oSQLReader.Close()
                If pnLink.Visible = True Then
                    upload()
                ElseIf pnLink.Visible = False Then
                    savedata()
                End If
            End If
                oCnct.Close()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalAdd", "hideModalAdd()", True)
        End If
    End Sub

    Protected Sub LinkButton3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton3.Click
        updateStatus()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalApproved", "hideModalApproved()", True)
        Response.Redirect("iPxTicketCustomer.aspx")
    End Sub
    'Protected Sub GetValue(ByVal sender As Object, ByVal e As EventArgs)
    '    'Reference the Repeater Item using Button.
    '    Dim item As RepeaterItem = TryCast((TryCast(sender, LinkButton)).NamingContainer, RepeaterItem)

    '    Dim filePath As String = (TryCast(item.FindControl("Label3"), Label)).Text
    '    Response.ContentType = "image/jpg"
    '    Response.AddHeader("Content-Disposition", "attachment;filename=""" & filePath & """")
    '    Response.TransmitFile(Server.MapPath(filePath))
    '    Response.[End]()
    'End Sub

    Protected Sub gvTicket_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvTicket.RowCommand
        
    End Sub

    Protected Sub gvFollow_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvFollow.RowCommand
        If e.CommandName = "getUnduh" Then
            Try
                Dim filePath As String = e.CommandArgument.ToString
                Response.ContentType = "image/jpg"
                Response.AddHeader("Content-Disposition", "attachment;filename=""" & filePath & """")
                Response.TransmitFile(Server.MapPath(filePath))
                Response.[End]()
            Catch
            End Try
        End If
    End Sub

    Protected Sub lbLink_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbLink.Click
        If pnLink.Visible = False Then
            pnLink.Visible = True
        ElseIf pnLink.Visible = True Then
            pnLink.Visible = False
        End If
    End Sub

    Protected Sub lbBackRating_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbBackRating.Click
        pnRating.Visible = False
    End Sub

    Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton2.Click
        pnRating.Visible = True
    End Sub

    Protected Sub lbDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbDetail.Click
        lbDetail.Visible = False
        pnHeader.Visible = True
    End Sub

    Protected Sub lbDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbDown.Click
        lbDetail.Visible = True
        pnHeader.Visible = False
    End Sub
End Class
