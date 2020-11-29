Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Partial Class iPxCrmCustomer_iPxTicketCustomer
    Inherits System.Web.UI.Page
    Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Dim oCnct As SqlConnection = New SqlConnection(sCnct)
    Dim oSQLCmd As SqlCommand
    Dim oSQLReader As SqlDataReader
    Dim sSQL, Ticketno, i As String
    Dim cIpx As iPxClass

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
        sSQL += "INNER JOIN CFG_productSubMenu ON SPR_eticket.SubmenuID = CFG_productSubMenu.SubmenuID where SPR_eticket.custID = '" & Session("sCId") & "' "

        If Session("sCQueryTicket") = "" Then
            Session("sCQueryTicket") = Session("sCCondition")
            If Session("sCQueryTicket") <> "" Or Session("sCCondition") <> "" Then
                sSQL = sSQL & Session("sCQueryTicket")
                Session("sCCondition") = ""
            Else
                sSQL = sSQL & " and SPR_eticket.StatusID <> '" & "7" & "' "
            End If
        Else
            sSQL = sSQL & Session("sCQueryTicket")
            Session("sCCondition") = ""
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
    
    Sub deletedata()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "UPDATE SPR_eticket SET StatusID ='" & "0" & "' where TicketNo='" & Session("sCIdDelete") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        cekFollow()
        ListTicket()
        If Session("sCnotifTotalTicket") = "0" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotif", "hideNotif();", True)
        End If
    End Sub

    Sub cekFollow()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT TicketNo FROM SPR_eticketFollowUp WHERE TicketNo = '" & Session("sCIdDelete") & "'"
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
        sSQL = "UPDATE SPR_eticketFollowUp SET isRead= '0'"
        sSQL = sSQL & "WHERE TicketNo = '" & Session("sCIdDelete") & "' "

        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Session("sCQueryTicket") = ""
            ListTicket()
        End If
    End Sub
    Protected Sub TimerTick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
        ListTicket()
        If Session("sCnotifTotalTicket") = "0" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotif", "$(document).ready(function() {hideNotif()});", True)
        End If
    End Sub
    Protected Sub OnPaging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvTicket.PageIndex = e.NewPageIndex
        Me.ListTicket()
    End Sub

    Protected Sub gvTicket_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvTicket.PageIndexChanging
        gvTicket.PageIndex = e.NewPageIndex
        ListTicket()
        If Session("sCnotifTotalTicket") = "0" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotif", "hideNotif();", True)
        End If
    End Sub

    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvTicket.PageIndex = e.NewPageIndex
        Me.ListTicket()
    End Sub
    Protected Sub lbAddTicket_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAddTicket.Click
        'tbTicketno.Text = cIpx.GetCounterMBR(Session("sId"), "TN")
        Session("sCIn1") = ""
        Response.Redirect("iPxCrmFormTicket.aspx")
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalAdd", "showModalAdd()", True)
    End Sub

    Protected Sub gvTicket_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvTicket.RowCommand
        If e.CommandName = "getTiketid" Then
            i = e.CommandArgument.ToString
            Dim oSQLReader As SqlDataReader
            If oCnct.State = ConnectionState.Closed Then
                oCnct.Open()
            End If
            oSQLCmd = New SqlCommand(sSQL, oCnct)
            sSQL = "SELECT * FROM SPR_eticket WHERE TicketNo = '" & i & "'"
            oSQLCmd.CommandText = sSQL
            oSQLReader = oSQLCmd.ExecuteReader

            oSQLReader.Read()
            'usercode, mobileno, password, signupdate, fullname, status, quid
            If oSQLReader.HasRows Then
                Session("sCIn1") = oSQLReader.Item("TicketNo").ToString
                Session("sCIn2") = oSQLReader.Item("ProductID").ToString
                Session("sCIn3") = oSQLReader.Item("MenuID").ToString
                Session("sCIn4") = oSQLReader.Item("SubMenuID").ToString
                Session("sCIn5") = oSQLReader.Item("CaseDescription").ToString
                Session("sCIn6") = oSQLReader.Item("SubmitFrom").ToString
                Session("sCIn7") = oSQLReader.Item("ProductGrp").ToString
                oCnct.Close()
                Response.Redirect("iPxCrmFormTicket.aspx")
            Else
                'Refreshh()
            End If

        ElseIf e.CommandName = "getDeleteid" Then
            Session("sCIdDelete") = e.CommandArgument.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalDelete", "showModalDelete()", True)
            If Session("sCnotifTotalTicket") = "0" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotif", "hideNotif();", True)
            End If
        ElseIf e.CommandName = "getViewid" Then
            Session("sCIdview") = e.CommandArgument.ToString
            Response.Redirect("iPxCrmViewFollow.aspx")
        End If
    End Sub

    Protected Sub lbDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbDelete.Click
        deletedata()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalDelete", "hideModalDelete()", True)
    End Sub

    Protected Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        Response.Redirect("queryTicket.aspx")
    End Sub
End Class
