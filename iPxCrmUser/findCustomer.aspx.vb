Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Globalization
Imports System.Threading
Partial Class iPxCrmUser_findCustomer
    Inherits System.Web.UI.Page
    Public sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Public oCnct As SqlConnection = New SqlConnection(sCnct)
    Public oSQLCmd As New SqlCommand
    Public oSQLReader As SqlDataReader
    Public sSQL As String
    Dim cipx As New iPxClass

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

    Sub Hotel()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT CFG_customer.CustID, CFG_customerGrp.GrpName, CFG_customer.CustName "
        sSQL += "FROM CFG_customer "
        sSQL += "INNER JOIN CFG_CustomerLevel ON CFG_customer.CustLevel = CFG_CustomerLevel.CustLevel "
        sSQL += "INNER JOIN CFG_customerGrp ON CFG_customerGrp.CustGrpID = CFG_customer.CustGrpID "
        sSQL += " WHERE CFG_customerGrp.isActive = 'Y' and CFG_customer.CustLevel = '7' "
        If Session("sQueryTicket") = "" Then
            Session("sQueryTicket") = Session("sCondition")
            If Session("sQueryTicket") <> "" Or Session("sCondition") <> "" Then
                sSQL = sSQL & Session("sQueryTicket")
                Session("sCondition") = ""
            Else
                sSQL = sSQL & " "
            End If
        Else
            sSQL = sSQL & Session("sQueryTicket")
            Session("sCondition") = ""
        End If
        sSQL += " order by CFG_customerGrp.GrpName asc, CFG_customer.CustName asc"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                If dt.Rows.Count <> 0 Then
                    gvCust.DataSource = dt
                    gvCust.DataBind()
                Else
                    dt.Rows.Add(dt.NewRow())
                    gvCust.DataSource = dt
                    gvCust.DataBind()
                    gvCust.Rows(0).Visible = False
                End If
            End Using
        End Using
        oCnct.Close()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Session("sQueryTicket") = ""
            Hotel()
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "PopUser", "clearModal()", True)
        End If
    End Sub

    Protected Sub OnPaging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvCust.PageIndex = e.NewPageIndex
        Me.Hotel()
    End Sub

    Protected Sub gvCust_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvCust.PageIndexChanging
        gvCust.PageIndex = e.NewPageIndex
        Hotel()
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
        gvCust.PageIndex = e.NewPageIndex
        Me.Hotel()
     End Sub

    Protected Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        'Session("sCustName") = ""
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "PopUser", "hideModal()", True)
        Response.Redirect("iPxCrmInputTicketUser.aspx?sCustName=")
    End Sub

    Protected Sub gvCust_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvCust.RowCommand
        If e.CommandName = "getHotel" Then
            Session("sCustName") = e.CommandArgument.ToString
            Response.Redirect("iPxCrmInputTicketUser.aspx")
        End If
    End Sub

    Protected Sub lbQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbQuery.Click
        Session("sQueryTicket") = ""
        If tbQueryHotel.Text.Trim <> "" Then
            Session("sCondition") = Session("sCondition") & " and (CFG_customer.CustName like '%" & Replace(tbQueryHotel.Text.Trim, "'", "''") & "%') "
        End If
        Hotel()
    End Sub
End Class
