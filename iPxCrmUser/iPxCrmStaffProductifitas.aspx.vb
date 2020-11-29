Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Imports System
Partial Class iPxCrmUser_iPxCrmStaffProductifitas
    Inherits System.Web.UI.Page
    Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Dim oCnct As SqlConnection = New SqlConnection(sCnct)
    Dim oSQLCmd As SqlCommand
    Dim oSQLReader As SqlDataReader
    Dim sSQL, Ticketno, i As String
    Dim cIpx As iPxClass

    Sub ListUser()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        Dim PerFrom As Date = Date.ParseExact(tbFromDate.Text, "dd MMMM yyyy", System.Globalization.CultureInfo.InvariantCulture)
        Dim PerUntl As Date = Date.ParseExact(tbUntilDate.Text, "dd MMMM yyyy", System.Globalization.CultureInfo.InvariantCulture)
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT a.recID, a.name, a.Email, "
        sSQL += "(select count(TicketNo) from SPR_eticket where CreateBy=a.Email and Tdate>='" & PerFrom & " 00:00:00' and Tdate<='" & PerUntl & " 23:59:00' ) as CreateTicket, "
        sSQL += "(select count(TicketNo) from SPR_eticket where AssignTo=a.recID and Tdate>='" & PerFrom & " 00:00:00' and Tdate<='" & PerUntl & " 23:59:00') as AssignTicket, "
        sSQL += "(select count(TicketNo) from SPR_eticketFollowUp where FollowUpBy=a.name and FollowUpDate>='" & PerFrom & " 00:00:00' and FollowUpDate<='" & PerUntl & " 23:59:00') as FollowTicket, "
        sSQL += "(select count(TicketNo) from SPR_eticket where AssignTo=a.recID and StatusID>'1' and StatusID<'6' and Tdate>='" & PerFrom & " 00:00:00' and Tdate<='" & PerUntl & " 23:59:00') as OutStandingTicket, "
        sSQL += "(select count(TicketNo) from SPR_eticket where AssignTo=a.recID and StatusID='6' and Tdate>='" & PerFrom & " 00:00:00' and Tdate<='" & PerUntl & " 23:59:00') as DoneTicket, "
        sSQL += "(select count(TicketNo) from SPR_eticket where AssignTo=a.recID and StatusID='7' and Tdate>='" & PerFrom & " 00:00:00' and Tdate<='" & PerUntl & " 23:59:00') as ResolvedTicket "
        sSQL += "FROM CFG_user as a where a.isActive = '" & "Y" & "' "
        sSQL += "order by a.name asc"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                If dt.Rows.Count <> 0 Then
                    gvUSer.DataSource = dt
                    gvUSer.DataBind()
                Else
                    dt.Rows.Add(dt.NewRow())
                    gvUSer.DataSource = dt
                    gvUSer.DataBind()
                    gvUSer.Rows(0).Visible = False
                End If
            End Using
        End Using
        oCnct.Close()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sId") = "" Then
            Response.Redirect("../login.aspx")
        ElseIf Session("sPossition") <> "0" Then
            Session("sMessage") = "Sorry, Your not access !| ||"
            Session("sWarningID") = "0"
            Session("sUrlOKONLY") = "iPxCrmHome.aspx"
            Session("sUrlYES") = "http://www.thepyxis.net"
            Session("sUrlNO") = "http://www.thepyxis.net"
            Response.Redirect("warningmsg.aspx")
        Else
            If Not Me.IsPostBack Then
                tbFromDate.Enabled = False
                tbUntilDate.Enabled = False
                Session("sQueryTicket") = ""
                Dim i As String = Format(Now, "MM")
                Dim j As String = Format(Now, "yyy")
                Dim x As String = Date.DaysInMonth(j, i)
                tbFromDate.Text = "01 " + Format(Now, "MMMM yyy")
                tbUntilDate.Text = x + Format(Now, " MMMM yyy")
                ListUser()
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "tanggal", "$(document).ready(function() {datetimepicker1()});", True)
        End If
    End Sub

    Protected Sub lbView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbView.Click
        Dim PerFrom As Date = Date.ParseExact(tbFromDate.Text, "dd MMMM yyyy", System.Globalization.CultureInfo.InvariantCulture)
        Dim PerUntl As Date = Date.ParseExact(tbUntilDate.Text, "dd MMMM yyyy", System.Globalization.CultureInfo.InvariantCulture)
        If PerFrom > PerUntl Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Range Date tidak sesuai !');document.getElementById('Buttonx').click()", True)
        Else
            ListUser()
        End If
    End Sub

    Protected Sub OnPaging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvUSer.PageIndex = e.NewPageIndex
        Me.ListUser()
    End Sub

    Protected Sub gvUSer_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvUSer.PageIndexChanging
        gvUSer.PageIndex = e.NewPageIndex
        ListUser()
    End Sub

    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvUSer.PageIndex = e.NewPageIndex
        Me.ListUser()
    End Sub
End Class
