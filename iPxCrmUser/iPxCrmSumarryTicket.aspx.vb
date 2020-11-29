Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Imports System
Partial Class iPxCrmUser_iPxCrmSumarryTicket
    Inherits System.Web.UI.Page
    Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Dim oCnct As SqlConnection = New SqlConnection(sCnct)
    Dim oSQLCmd As SqlCommand
    Dim oSQLReader As SqlDataReader
    Dim sSQL, Ticketno, i, tgl, y As String
    Dim x As Integer
    Dim cIpx As iPxClass
    Sub listSumarry()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        Dim transDate As Date = Date.ParseExact(lblTglFirst.Text, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture)
        Dim transDateLast As Date = Date.ParseExact(lblTglLast.Text, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture)
        y = DateDiff(DateInterval.Day, transDate, transDateLast)
        Dim a As String = Format(transDate, "dddd, dd MMMM yyyy")
        sSQL = "select 1 as Numb, '" & a & "' as Tdate, COUNT(TicketNo) as totalTiket, "
        sSQL += "(select COUNT(TicketNo) from SPR_eticket where StatusID = 1 and (Tdate between '" & transDate & " 00:00:00' and '" & transDate & " 23:59:00') ) as totalTiketNew, "
        sSQL += "(select COUNT(TicketNo) from SPR_eticket where (StatusID > 1 and StatusID < 6) and (Tdate between '" & transDate & " 00:00:00' and '" & transDate & " 23:59:00') ) as totalTiketProses, "
        sSQL += "(select COUNT(TicketNo) from SPR_eticket where StatusID = 6 and (Tdate between '" & transDate & " 00:00:00' and '" & transDate & " 23:59:00') ) as totalTiketDone, "
        sSQL += "(select COUNT(TicketNo) from SPR_eticketFollowUp where FollowUpCode='P' and (FollowUpDate between '" & transDate & " 00:00:00' and '" & transDate & " 23:59:00') ) as totalTiketFollow "
        sSQL += "from SPR_eticket where Tdate between '" & transDate & " 00:00:00' and '" & transDate & " 23:59:00' "
        x = 1
        For z As Integer = 1 To y
            x += 1
            tgl = Format(DateAdd("d", 1, transDate), "MM/dd/yyyy")
            transDate = Date.ParseExact(tgl, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture)
            a = Format(transDate, "dddd, dd MMMM yyyy")
            sSQL += "UNION "
            sSQL += "select '" & x & "' as Numb, '" & a & "' as Tdate,COUNT(TicketNo) as totalTiket, "
            sSQL += "(select COUNT(TicketNo) from SPR_eticket where StatusID = 1 and (Tdate between '" & tgl & " 00:00:00' and '" & tgl & " 23:59:00') ) as totalTiketNew, "
            sSQL += "(select COUNT(TicketNo) from SPR_eticket where (StatusID > 1 and StatusID < 6) and (Tdate between '" & tgl & " 00:00:00' and '" & tgl & " 23:59:00') ) as totalTiketProses, "
            sSQL += "(select COUNT(TicketNo) from SPR_eticket where StatusID = 6 and (Tdate between '" & tgl & " 00:00:00' and '" & tgl & " 23:59:00') ) as totalTiketDone, "
            sSQL += "(select COUNT(TicketNo) from SPR_eticketFollowUp where FollowUpCode='P' and (FollowUpDate between '" & tgl & " 00:00:00' and '" & tgl & " 23:59:00') ) as totalTiketFollow "
            sSQL += "from SPR_eticket where Tdate between '" & transDate & " 00:00:00' and '" & transDate & " 23:59:00' "
        Next
        sSQL += "order by Numb asc"
        'sSQL += " FROM CFG_Product "
        'sSQL += "INNER JOIN CFG_productGrp ON CFG_productGrp.ProductGrp = CFG_Product.ProductGrp"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                If dt.Rows.Count <> 0 Then
                    gvSummaryTicket.DataSource = dt
                    gvSummaryTicket.DataBind()
                Else
                    dt.Rows.Add(dt.NewRow())
                    gvSummaryTicket.DataSource = dt
                    gvSummaryTicket.DataBind()
                    gvSummaryTicket.Rows(0).Visible = False
                End If
            End Using
        End Using
        oCnct.Close()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            lblTglFirst.Text = "01" + Format(Now, "-MM-yyyy")
            Dim a As Date = DateSerial(Format(Now, "yyyy"), Format(Now, "MM") + 1, 0)
            lblTglLast.Text = Format(a, "dd-MM-yyy")
            listSumarry()
        End If
    End Sub

    Protected Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showFormMenu", "showFormMenu()", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "dateQuery", "$(document).ready(function() {dateQuery()});", True)
    End Sub

    Protected Sub lbAbort_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAbort.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideFormMenu", "hideFormMenu()", True)
    End Sub

    Protected Sub lbQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbQuery.Click
        Dim transDate As Date = Date.ParseExact(tbDateFrom.Text, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture)
        Dim transDateLast As Date = Date.ParseExact(tbDateUntil.Text, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture)
        y = DateDiff(DateInterval.Day, transDate, transDateLast)
        If y > 0 Then
            lblTglFirst.Text = tbDateFrom.Text
            lblTglLast.Text = tbDateUntil.Text
            listSumarry()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideFormMenu", "hideFormMenu()", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('Date periode is not valid!!');", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideFormMenu", "hideFormMenu()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showFormMenu", "showFormMenu()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "dateQuery", "$(document).ready(function() {dateQuery()});", True)
        End If
    End Sub
End Class
