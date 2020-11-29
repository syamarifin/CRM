Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Partial Class iPxCrmMobileUser_iPxCrmAssign
    Inherits System.Web.UI.Page
    Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Dim oCnct As SqlConnection = New SqlConnection(sCnct)
    Dim oSQLCmd As SqlCommand
    Dim oSQLReader As SqlDataReader
    Dim sSQL, Ticketno, i As String
    Dim cIpx As iPxClass
    Sub ListStatus()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT SPR_eticket.CustID, SPR_eticket.TicketNo, SPR_eticket.Tdate, SPR_eticket.CaseDescription, SPR_eticket.SubmitFrom, CFG_Product.ProductName, CFG_productMenu.MenuName, CFG_productSubMenu.SubmenuName"
        sSQL += ", CFG_caseID.Description,SPR_eticket.StatusID From SPR_eticket "
        sSQL += "LEFT JOIN CFG_Product ON SPR_eticket.ProductID = CFG_Product.ProductID "
        sSQL += "LEFT JOIN CFG_productMenu ON SPR_eticket.MenuID = CFG_productMenu.MenuID LEFT JOIN CFG_caseID ON SPR_eticket.CaseID = CFG_caseID.CaseID "
        sSQL += "LEFT JOIN CFG_productSubMenu ON SPR_eticket.SubmenuID = CFG_productSubMenu.SubmenuID where SPR_eticket.TicketNo = '" & Session("sTicketFollow") & "'"
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
    Sub ListTicket()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT a.*,( SELECT isRead FROM SPR_eticketFollowUp where SPR_eticketFollowUp.TicketNo=a.TicketNo and FollowUpDate in ( SELECT "
        sSQL += "max(FollowUpDate) FROM SPR_eticketFollowUp where FollowUpCode ='H' group by TicketNo )) as coment from ( SELECT SPR_eticket.CustID, CFG_customer.CustName, SPR_eticket.TicketNo, SPR_eticket.Tdate, SPR_eticket.Subject, SPR_eticket.CaseDescription, SPR_eticket.SubmitFrom, "
        sSQL += "CFG_Product.ProductName, CFG_productMenu.MenuName, CFG_productSubMenu.SubmenuName"
        sSQL += ",CFG_FollowupSts.stsDescription, SPR_eticket.AssignTo, CFG_user.name, SPR_eticket.AttachFile From SPR_eticket "
        sSQL += "LEFT JOIN CFG_productGrp on SPR_eticket .ProductGrp = CFG_productGrp .ProductGrp LEFT JOIN CFG_user ON CFG_user.recID = SPR_eticket.AssignTo "
        sSQL += "LEFT JOIN CFG_Product ON SPR_eticket.ProductID = CFG_Product.ProductID  INNER JOIN CFG_FollowupSts ON SPR_eticket.StatusID = CFG_FollowupSts.StatusID "
        sSQL += "LEFT JOIN CFG_productMenu ON SPR_eticket.MenuID = CFG_productMenu.MenuID and CFG_Product.ProductID = CFG_productMenu.ProductID INNER JOIN CFG_customer ON SPR_eticket.CustID = CFG_customer.CustID "
        sSQL += "LEFT JOIN CFG_productSubMenu ON SPR_eticket.SubmenuID = CFG_productSubMenu.SubmenuID where SPR_eticket.statusID <> '1' and SPR_eticket.statusID <> '6' and SPR_eticket.statusID <> '7'"
        If Session("sPossition") = "0" And Session("sProductCode") = "1" Then
            sSQL += " "
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "2" Then
            sSQL += " and SPR_eticket.ProductGrp = 'Al'"
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "3" Then
            sSQL += " and SPR_eticket.ProductGrp <> 'Al'"
        ElseIf Session("sPossition") <> "0" Then
            sSQL += " "
        End If

        If Session("sPossition") <> "0" Then
            If CheckBox1.Checked = True Then
                sSQL = sSQL & ""
            ElseIf CheckBox1.Checked = False Then
                sSQL = sSQL & " and SPR_eticket.assignTo = '" & Session("sId") & "' "
            End If
        Else
            sSQL = sSQL & ""
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
        sSQL += ") a order by a.Tdate desc"
        'sSQL += " order by SPR_eticket.TicketNo asc"
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

    Sub ListTicketDirect()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT a.*,( SELECT isRead FROM SPR_eticketFollowUp where SPR_eticketFollowUp.TicketNo=a.TicketNo and FollowUpDate in "
        sSQL += "( SELECT max(FollowUpDate) FROM SPR_eticketFollowUp where FollowUpCode ='H' group by TicketNo )) as coment from "
        sSQL += "(SELECT SPR_eticket.CustID, CFG_customer.CustName, SPR_eticket.TicketNo, SPR_eticket.Tdate, SPR_eticket.Subject, SPR_eticket.SubmitFrom, SPR_eticket.SubmitVia, CFG_Product.ProductName, CFG_productMenu.MenuName, CFG_productSubMenu.SubmenuName"
        sSQL += ", CFG_FollowupSts.stsDescription, SPR_eticket.AttachFile, CFG_user.name, SPR_eticket.CaseDescription From SPR_eticket "
        sSQL += "LEFT JOIN CFG_productGrp on SPR_eticket .ProductGrp = CFG_productGrp .ProductGrp INNER JOIN CFG_FollowupSts ON SPR_eticket.StatusID = CFG_FollowupSts.StatusID "
        sSQL += "LEFT JOIN CFG_Product ON SPR_eticket.ProductID = CFG_Product.ProductID INNER JOIN CFG_customer ON SPR_eticket.CustID = CFG_customer.CustID "
        sSQL += "LEFT JOIN CFG_productMenu ON SPR_eticket.MenuID = CFG_productMenu.MenuID and CFG_Product.ProductID = CFG_productMenu.ProductID "
        sSQL += "LEFT JOIN CFG_user ON CFG_user.recID = SPR_eticket.AssignTo "
        sSQL += "INNER JOIN CFG_productSubMenu ON SPR_eticket.SubmenuID = CFG_productSubMenu.SubmenuID where SPR_eticket.statusID = '" & "1" & "' "

        If Session("sPossition") = "0" And Session("sProductCode") = "1" Then
            sSQL += " "
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "2" Then
            sSQL += " and CFG_productGrp.PrdDescription = 'Alcor'"
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "3" Then
            sSQL += " and CFG_productGrp.PrdDescription <> 'Alcor'"
        End If

        If Session("sQueryTicketDirect") = "" Then
            Session("sQueryTicketDirect") = Session("sConditionDirect")
        Else

        End If

        'If Session("sCondition") <> "" Then
        sSQL = sSQL & Session("sQueryTicketDirect")
        Session("sConditionDirect") = ""
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
        If Session("sId") = Nothing Then
            Response.Redirect("SignIn.aspx")
        Else
            Dim cph As ContentPlaceHolder = DirectCast(Me.Master.FindControl("ContentPlaceHolder2"), ContentPlaceHolder)
            Dim LinkButton As LinkButton = DirectCast(cph.FindControl("lnkback"), LinkButton)

            LinkButton.PostBackUrl = "iPxCrmHome.aspx"
            If Not Me.IsPostBack Then
                If Session("sPossition") = "0" Then
                    'lbDirect.Visible = False
                    showAll.Visible = False
                Else
                    'lbDirect.Visible = True
                    showAll.Visible = True
                End If
                Session("sQueryTicket") = ""
                Session("sQueryTicketDirect") = ""
                If Session("Direct") = "Direct" Then
                    ListTicketDirect()
                Else
                    If Session("sChaked") = "true" Then
                        CheckBox1.Checked = True
                    ElseIf Session("sChaked") = "false" Then
                        CheckBox1.Checked = False
                    End If
                    ListTicket()
                End If
                Session("Direct") = "NoDirect"
            End If
        End If
    End Sub

    Protected Sub OnPaging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvTicket.PageIndex = e.NewPageIndex
        Me.ListTicket()
    End Sub

    Protected Sub gvTicket_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvTicket.PageIndexChanging
        gvTicket.PageIndex = e.NewPageIndex
        ListTicket()
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

    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim level As String = e.Row.Cells(1).Text

            For Each cell As TableCell In e.Row.Cells
                If level = "ASSIGNED" Then
                    cell.BackColor = Color.LightGray
                End If
                If level <> "ASSIGNED" Then
                    cell.BackColor = Color.White
                End If
            Next
        End If
    End Sub
    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvTicket.PageIndex = e.NewPageIndex
        Me.ListTicket()
    End Sub

    Protected Sub TimerTick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
        If Session("sChaked") = "true" Then
            CheckBox1.Checked = True
        ElseIf Session("sChaked") = "false" Then
            CheckBox1.Checked = False
        End If
        ListTicket()
        If Session("snotifTotalTicket") = "0" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotifTiket", "$(document).ready(function() {hideNotifTiket()});", True)
        End If
        If Session("snotifNewTicket") = "0" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotifNewTiket", "$(document).ready(function() {hideNotifNewTiket()});", True)
        End If
        If Session("snotifFollow") = "0" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotifFollow", "$(document).ready(function() {hideNotifFollow()});", True)
        End If
    End Sub
    Protected Sub cari(ByVal sender As Object, ByVal e As EventArgs)
        ListTicket()
    End Sub

    Protected Sub gvTicket_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvTicket.RowCommand
        If e.CommandName = "getTiketid" Then
            Session("sTicketFollow") = e.CommandArgument.ToString
            ListStatus()
            If Session("sStatusTicket") = "1" Then
                Response.Redirect("iPxCrmStartAssign.aspx")
            Else
                Response.Redirect("iPxCrmDetailFollowUser.aspx")
            End If
        ElseIf e.CommandName = "getFileTiket" Then
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

    Protected Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        'If lbDirect.Visible = False Then
        Response.Redirect("queryFollow.aspx")
        'Else
        'If showAll.Visible = True Then
        '    Session("Direct") = "NoDirect"
        '    Response.Redirect("queryFollow.aspx")
        'Else
        '    Session("Direct") = "Direct"
        '    Response.Redirect("queryFollowDirect.aspx")
        'End If
        'End If
    End Sub

    Protected Sub lbDirect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbDirect.Click
        If Me.lbDirect.Text = "<h4 style='margin:0;'><small style='color:White;'><i class='fa fa-file-text'></i> Undirect</small></h4>" Then
            ListTicket()
            lbDirect.Text = "<h4 style='margin:0;'><small style='color:White;'><i class='fa fa-file-text'></i> Direct</small></h4>"
            showAll.Visible = True
        Else
            ListTicketDirect()
            lbDirect.Text = "<h4 style='margin:0;'><small style='color:White;'><i class='fa fa-file-text'></i> Undirect</small></h4>"
            showAll.Visible = False
        End If
    End Sub

    Protected Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            Session("sChaked") = "true"
        ElseIf CheckBox1.Checked = False Then
            Session("sChaked") = "false"
        End If
        ListTicket()
    End Sub
End Class
