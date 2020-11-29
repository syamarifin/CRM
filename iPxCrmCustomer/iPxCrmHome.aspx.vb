Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Partial Class iPxCrmCustomer_iPxCrmHome
    Inherits System.Web.UI.Page
    Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Dim oCnct As SqlConnection = New SqlConnection(sCnct)
    Dim oSQLCmd As SqlCommand
    Dim oSQLReader As SqlDataReader
    Dim sSQL, BlnIni, i, BlnLalu As String
    Sub totalTicketNew()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "Select count (StatusID) as jumlahTiket from SPR_eticket where StatusID='" & "1" & "' and SPR_eticket.custID = '" & Session("sCId") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            lbNewTtl.Text = oSQLReader.Item("jumlahTiket").ToString
            Session("sCE-Ticket") = oSQLReader.Item("jumlahTiket").ToString
            oCnct.Close()
        Else
            lbNewTtl.Text = "0"
        End If
        oCnct.Close()
    End Sub
    Sub totalTicketOutstanding()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "Select count (StatusID) as jumlahTiket from SPR_eticket where StatusID <>'" & "7" & "' and StatusID<>'" & "1" & "' and StatusID<>'" & "0" & "' and SPR_eticket.custID = '" & Session("sCId") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            lbOutTtl.Text = oSQLReader.Item("jumlahTiket").ToString
            oCnct.Close()
        Else
            lbOutTtl.Text = "0"
        End If
        oCnct.Close()
    End Sub
    Sub totalTicketDone()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "Select count (StatusID) as jumlahTiket from SPR_eticket where StatusID='" & "7" & "' and SPR_eticket.custID = '" & Session("sCId") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            lbDoneTtl.Text = oSQLReader.Item("jumlahTiket").ToString
            oCnct.Close()
        Else
            lbDoneTtl.Text = "0"
        End If
        oCnct.Close()
    End Sub
    Sub totalTicketFollow()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "Select count (*) as jumlahTiket from SPR_eticketFollowUp where FollowUpCode='P' and SPR_eticketFollowUp.custID = '" & Session("sCId") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            lbTotalFollow.Text = oSQLReader.Item("jumlahTiket").ToString
            oCnct.Close()
        Else
            lbTotalFollow.Text = "0"
        End If
        oCnct.Close()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        totalTicketNew()
        totalTicketOutstanding()
        totalTicketDone()
        totalTicketFollow()
    End Sub
End Class
