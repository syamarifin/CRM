Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Imports System
Partial Class iPxCrmUser_iPxCrmDraftTicket
    Inherits System.Web.UI.Page
    Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Dim oCnct As SqlConnection = New SqlConnection(sCnct)
    Dim oSQLCmd As SqlCommand
    Dim oSQLReader As SqlDataReader
    Dim sSQL, Ticketno, i As String
    Dim cIpx As iPxClass
    Sub NamaHotel()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If

        sSQL = "Select * From CFG_customer  Where CustID = '" & Session("sDraft") & "'  "
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        oSQLReader = oSQLCmd.ExecuteReader

        If oSQLReader.Read Then
            lblTitleTicket.Text = "Draft E-Ticket dari Hotel " + oSQLReader.Item("CustName")
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('data not found !');document.getElementById('Buttonx').click()", True)
            Response.Redirect("iPxCrmCustomer.aspx")
        End If
        oCnct.Close()
    End Sub
    Sub ListTicket()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT a.*,( SELECT isRead FROM SPR_eticketFollowUp where SPR_eticketFollowUp.TicketNo=a.TicketNo and FollowUpDate in ( SELECT "
        sSQL += "max(FollowUpDate) FROM SPR_eticketFollowUp where FollowUpCode ='P' group by TicketNo )) as coment from ( SELECT SPR_eticket.CustID, SPR_eticket.TicketNo, SPR_eticket.Tdate, SPR_eticket.SubmitFrom, SPR_eticket.CaseDescription, CFG_Product.ProductName, CFG_productMenu.MenuName, CFG_productSubMenu.SubmenuName"
        sSQL += ",CFG_FollowupSts.stsDescription From SPR_eticket "
        sSQL += "INNER JOIN CFG_Product ON SPR_eticket.ProductID = CFG_Product.ProductID INNER JOIN CFG_FollowupSts ON SPR_eticket.StatusID = CFG_FollowupSts.StatusID "
        sSQL += "INNER JOIN CFG_productMenu ON SPR_eticket.MenuID = CFG_productMenu.MenuID and CFG_Product .ProductID = CFG_productMenu .ProductID "
        sSQL += "INNER JOIN CFG_productSubMenu ON SPR_eticket.SubmenuID = CFG_productSubMenu.SubmenuID where SPR_eticket.custID = '" & Session("sDraft") & "' "

        If Session("sCondition") <> "" Then
            sSQL = sSQL & Session("sCondition")
            Session("sCondition") = ""
        Else
            sSQL = sSQL & " "
        End If
        sSQL += " ) as a order by a.Tdate desc"

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
        If Session("sPossition") <> "0" Then
            Session("sMessage") = "Sorry, Your not access !| ||"
            Session("sWarningID") = "0"
            Session("sUrlOKONLY") = "iPxCrmHome.aspx"
            Session("sUrlYES") = "http://www.thepyxis.net"
            Session("sUrlNO") = "http://www.thepyxis.net"
            Response.Redirect("warningmsg.aspx")
        Else
            If Not Me.IsPostBack Then
                NamaHotel()
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

    Protected Sub lbAbort_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAbort.Click
        Response.Redirect("iPxCrmCustomer.aspx")
    End Sub

    Protected Sub gvTicket_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvTicket.RowCommand
        If e.CommandName = "getViewid" Then
            Session("sTicketFollow") = e.CommandArgument.ToString
            Response.Redirect("iPxCrmDetailFollowUser.aspx")
        End If
    End Sub
End Class
