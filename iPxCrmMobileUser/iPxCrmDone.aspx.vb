Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Partial Class iPxCrmMobileUser_iPxCrmDone
    Inherits System.Web.UI.Page
    Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Dim oCnct As SqlConnection = New SqlConnection(sCnct)
    Dim oSQLCmd As SqlCommand
    Dim oSQLReader As SqlDataReader
    Dim sSQL, Ticketno, i, status As String
    Dim cIpx As iPxClass
    Sub ListTicket()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT a.*,( SELECT isRead FROM SPR_eticketFollowUp where SPR_eticketFollowUp.TicketNo=a.TicketNo and FollowUpDate in ( SELECT "
        sSQL += "max(FollowUpDate) FROM SPR_eticketFollowUp where FollowUpCode ='H' group by TicketNo )) as coment from ( SELECT SPR_eticket.CustID, CFG_customer.CustName, SPR_eticket.TicketNo, SPR_eticket.Tdate, SPR_eticket.Subject, SPR_eticket.CaseDescription, SPR_eticket.SubmitFrom, "
        sSQL += "CFG_Product.ProductName, CFG_productMenu.MenuName, CFG_productSubMenu.SubmenuName"
        sSQL += ",CFG_FollowupSts.stsDescription, SPR_eticket.AssignTo, CFG_user.name, SPR_eticket.AttachFile, SPR_eticket.Rating From SPR_eticket "
        sSQL += "LEFT JOIN CFG_user ON CFG_user.recID = SPR_eticket.AssignTo "
        sSQL += "LEFT JOIN CFG_Product ON SPR_eticket.ProductID = CFG_Product.ProductID  INNER JOIN CFG_FollowupSts ON SPR_eticket.StatusID = CFG_FollowupSts.StatusID "
        sSQL += "LEFT JOIN CFG_productMenu ON SPR_eticket.MenuID = CFG_productMenu.MenuID and CFG_Product.ProductID = CFG_productMenu.ProductID INNER JOIN CFG_customer ON SPR_eticket.CustID = CFG_customer.CustID "
        sSQL += "LEFT JOIN CFG_productSubMenu ON SPR_eticket.SubmenuID = CFG_productSubMenu.SubmenuID where SPR_eticket.statusID >= '6'"
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
            sSQL = sSQL & " and SPR_eticket.assignTo = '" & Session("sId") & "'"
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
        sSQL += ") a order by a.Tdate desc"
        'Else
        '    sSQL = sSQL & ""
        'End If
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
    Sub ListStatus()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT SPR_eticket.StatusID From SPR_eticket where SPR_eticket.TicketNo = '" & Session("sTicketFollow") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            status = oSQLReader.Item("StatusID").ToString
        Else
            'Refreshh()
        End If
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
                Session("sQueryTicket") = ""
                ListTicket()
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

    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvTicket.PageIndex = e.NewPageIndex
        Me.ListTicket()
    End Sub

    Protected Sub gvTicket_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvTicket.RowCommand
        If e.CommandName = "getTiketid" Then
            Session("sTicketFollow") = e.CommandArgument.ToString
            ListStatus()
            If status = 7 Then
                Response.Redirect("iPxCrmDetailFollowDone.aspx")
            Else
                Response.Redirect("iPxCrmDetailFollowUser.aspx")
            End If

        End If
    End Sub

    Protected Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        Response.Redirect("queryFollowDone.aspx")
    End Sub
End Class
