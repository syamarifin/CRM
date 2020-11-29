Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Imports System.Web.Services
Imports System.Web.Script.Serialization
Imports System.Collections.Generic
Partial Class iPxCrmMobileUser_iPxCrmHome
    Inherits System.Web.UI.Page
    Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Dim oCnct As SqlConnection = New SqlConnection(sCnct)
    Dim oSQLCmd As SqlCommand
    Dim oSQLReader As SqlDataReader
    Dim sSQL, BlnIni, i, BlnLalu As String
    'notifRealtime=======================================================================================================================================
    <System.Web.Services.WebMethod()> _
    Public Shared Function notifTotalTicketNew(ByVal Possition As String, ByVal ProductCode As String, ByVal idUSer As String) As String

        Dim Order As New List(Of Object)()
        Dim sSQL As String
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("iPxCNCT").ConnectionString
            Using cmd As New SqlCommand()
                If Possition = "0" Then
                    sSQL = "Select count (StatusID) as jumlahTiket from SPR_eticket INNER JOIN CFG_productGrp on SPR_eticket .ProductGrp = CFG_productGrp .ProductGrp  where StatusID='" & "1" & "'"
                    If Possition = "0" And ProductCode = "1" Then
                        sSQL += " "
                    ElseIf Possition = "0" And ProductCode = "2" Then
                        sSQL += " and CFG_productGrp.PrdDescription = 'Alcor'"
                    ElseIf Possition = "0" And ProductCode = "3" Then
                        sSQL += " and CFG_productGrp.PrdDescription <> 'Alcor'"
                    End If
                Else
                    sSQL = "Select count (StatusID) as jumlahTiket from SPR_eticket INNER JOIN CFG_productGrp on SPR_eticket .ProductGrp = CFG_productGrp .ProductGrp  where StatusID='" & "1" & "'"
                End If
                cmd.CommandText = sSQL
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        Order.Add(New With {.jumlahTiket = sdr("jumlahTiket")})
                    End While
                End Using
                conn.Close()
            End Using
            Return (New JavaScriptSerializer().Serialize(Order))
        End Using
    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function notifTotalTicketFollow(ByVal Possition As String, ByVal ProductCode As String, ByVal idUSer As String) As String

        Dim Order As New List(Of Object)()
        Dim sSQL As String
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("iPxCNCT").ConnectionString
            Using cmd As New SqlCommand()
                If Possition = "0" Then
                    sSQL = "Select count (SPR_eticketFollowUp.isRead) as jumlahFollow from SPR_eticketFollowUp INNER JOIN SPR_eticket ON SPR_eticket.TicketNo =SPR_eticketFollowUp.TicketNo where SPR_eticketFollowUp.FollowUpCode= 'H' and SPR_eticketFollowUp.isRead='" & "1" & "' and SPR_eticket.StatusID < '6' and SPR_eticket.AssignTo <> '" & "" & "'"
                    If Possition = "0" And ProductCode = "1" Then
                        sSQL += " "
                    ElseIf Possition = "0" And ProductCode = "2" Then
                        sSQL += " and CFG_productGrp.PrdDescription = 'Alcor'"
                    ElseIf Possition = "0" And ProductCode = "3" Then
                        sSQL += " and CFG_productGrp.PrdDescription <> 'Alcor'"
                    End If
                Else
                    sSQL = "Select count (SPR_eticketFollowUp.isRead) as jumlahFollow from SPR_eticketFollowUp INNER JOIN SPR_eticket ON SPR_eticket.TicketNo =SPR_eticketFollowUp.TicketNo where SPR_eticketFollowUp.FollowUpCode= 'H' and SPR_eticketFollowUp.isRead='" & "1" & "' and SPR_eticket.StatusID < '6' and SPR_eticket.AssignTo = '" & idUSer & "'"
                End If
                cmd.CommandText = sSQL
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        Order.Add(New With {.jumlahFollow = sdr("jumlahFollow")})
                    End While
                End Using
                conn.Close()
            End Using
            Return (New JavaScriptSerializer().Serialize(Order))
        End Using
    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function notifTotalTicketDone(ByVal Possition As String, ByVal ProductCode As String, ByVal idUSer As String) As String

        Dim Order As New List(Of Object)()
        Dim sSQL As String
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("iPxCNCT").ConnectionString
            Using cmd As New SqlCommand()
                If Possition = "0" Then
                    sSQL = "Select count (SPR_eticketFollowUp.isRead) as jumlahFollow from SPR_eticketFollowUp INNER JOIN SPR_eticket ON SPR_eticket.TicketNo =SPR_eticketFollowUp.TicketNo where SPR_eticketFollowUp.FollowUpCode= 'H' and SPR_eticketFollowUp.isRead='" & "1" & "' and SPR_eticket.StatusID >= '6' and SPR_eticket.AssignTo <> '" & "" & "'"
                    If Possition = "0" And ProductCode = "1" Then
                        sSQL += " "
                    ElseIf Possition = "0" And ProductCode = "2" Then
                        sSQL += " and CFG_productGrp.PrdDescription = 'Alcor'"
                    ElseIf Possition = "0" And ProductCode = "3" Then
                        sSQL += " and CFG_productGrp.PrdDescription <> 'Alcor'"
                    End If
                Else
                    sSQL = "Select count (SPR_eticketFollowUp.isRead) as jumlahFollow from SPR_eticketFollowUp INNER JOIN SPR_eticket ON SPR_eticket.TicketNo =SPR_eticketFollowUp.TicketNo where SPR_eticketFollowUp.FollowUpCode= 'H' and SPR_eticketFollowUp.isRead='" & "1" & "' and SPR_eticket.StatusID >= '6' and SPR_eticket.AssignTo = '" & idUSer & "'"
                End If
                cmd.CommandText = sSQL
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        Order.Add(New With {.jumlahFollow = sdr("jumlahFollow")})
                    End While
                End Using
                conn.Close()
            End Using
            Return (New JavaScriptSerializer().Serialize(Order))
        End Using
    End Function
    'end notifRealtime===================================================================================================================================
    'New Ticket==========================================================================================================================================
    Sub TicketNewBlnIni()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "Select count (StatusID) as jumlahTiket from SPR_eticket where StatusID='" & "1" & "' and month(Tdate)='" & Format(Now, "MM") & "' and year(Tdate)='" & Format(Now, "yyy") & "'"
        If Session("sPossition") = "0" And Session("sProductCode") = "1" Then
            sSQL += " "
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "2" Then
            sSQL += " and SPR_eticket.ProductGrp = 'Al'"
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "3" Then
            sSQL += " and SPR_eticket.ProductGrp <> 'Al'"
        ElseIf Session("sPossition") <> "0" Then
            sSQL += " "
        End If

        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            lbNewBlnIni.Text = oSQLReader.Item("jumlahTiket").ToString
            oCnct.Close()
        Else
            lbNewBlnIni.Text = "0"
        End If
        oCnct.Close()
    End Sub
    Sub TicketNewBlnLalu()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        Dim bln, thn As String
        If Format(Now, "MM") = 1 Then
            bln = "12"
            thn = Format(Now, "yyy") - 1
        Else
            bln = Format(Now, "MM") - 1
            thn = Format(Now, "yyy")
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "Select count (StatusID) as jumlahTiket from SPR_eticket where StatusID='" & "1" & "' and month(Tdate)='" & bln & "' and year(Tdate)='" & thn & "'"
        If Session("sPossition") = "0" And Session("sProductCode") = "1" Then
            sSQL += " "
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "2" Then
            sSQL += " and SPR_eticket.ProductGrp = 'Al'"
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "3" Then
            sSQL += " and SPR_eticket.ProductGrp <> 'Al'"
        ElseIf Session("sPossition") <> "0" Then
            sSQL += " "
        End If

        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            lbNewBlnLalu.Text = oSQLReader.Item("jumlahTiket").ToString
            oCnct.Close()
        Else
            lbNewBlnLalu.Text = "0"
        End If
        oCnct.Close()
    End Sub
    Sub totalTicketNew()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "Select count (StatusID) as jumlahTiket from SPR_eticket where StatusID='" & "1" & "'"
        If Session("sPossition") = "0" And Session("sProductCode") = "1" Then
            sSQL += " "
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "2" Then
            sSQL += " and SPR_eticket.ProductGrp = 'Al'"
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "3" Then
            sSQL += " and SPR_eticket.ProductGrp <> 'Al'"
        ElseIf Session("sPossition") <> "0" Then
            sSQL += " "
        End If

        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            lbNewTtl.Text = oSQLReader.Item("jumlahTiket").ToString
            Session("sE-Ticket") = oSQLReader.Item("jumlahTiket").ToString
            oCnct.Close()
        Else
            lbNewTtl.Text = "0"
        End If
        oCnct.Close()
    End Sub
    'End New Ticket======================================================================================================================================
    'Outstanding Ticket==================================================================================================================================
    Sub TicketOutstandingBlnIni()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "Select count (StatusID) as jumlahTiket from SPR_eticket where StatusID <'6' and StatusID >'1' and month(Tdate)='" & Format(Now, "MM") & "' and year(Tdate)='" & Format(Now, "yyy") & "'"
        If Session("sPossition") = "0" And Session("sProductCode") = "1" Then
            sSQL += " "
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "2" Then
            sSQL += " and SPR_eticket.ProductGrp = 'Al'"
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "3" Then
            sSQL += " and SPR_eticket.ProductGrp <> 'Al'"
        ElseIf Session("sPossition") <> "0" Then
            sSQL += " "
        End If

        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            lbOutBlnIni.Text = oSQLReader.Item("jumlahTiket").ToString
            oCnct.Close()
        Else
            lbOutBlnIni.Text = "0"
        End If
        oCnct.Close()
    End Sub
    Sub TicketOutstandingBlnLalu()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        Dim bln, thn As String
        If Format(Now, "MM") = 1 Then
            bln = "12"
            thn = Format(Now, "yyy") - 1
        Else
            bln = Format(Now, "MM") - 1
            thn = Format(Now, "yyy")
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "Select count (StatusID) as jumlahTiket from SPR_eticket where StatusID <'6'  and StatusID >'1' and month(Tdate)='" & bln & "' and year(Tdate)='" & thn & "'"
        If Session("sPossition") = "0" And Session("sProductCode") = "1" Then
            sSQL += " "
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "2" Then
            sSQL += " and SPR_eticket.ProductGrp = 'Al'"
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "3" Then
            sSQL += " and SPR_eticket.ProductGrp <> 'Al'"
        ElseIf Session("sPossition") <> "0" Then
            sSQL += " "
        End If

        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            lbOutBlnLalu.Text = oSQLReader.Item("jumlahTiket").ToString
            oCnct.Close()
        Else
            lbOutBlnLalu.Text = "0"
        End If
        oCnct.Close()
    End Sub
    Sub totalTicketOutstanding()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "Select count (StatusID) as jumlahTiket from SPR_eticket where StatusID <'6' and StatusID >'1'"
        If Session("sPossition") = "0" And Session("sProductCode") = "1" Then
            sSQL += " "
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "2" Then
            sSQL += " and SPR_eticket.ProductGrp = 'Al'"
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "3" Then
            sSQL += " and SPR_eticket.ProductGrp <> 'Al'"
        ElseIf Session("sPossition") <> "0" Then
            sSQL += " "
        End If

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
    'End Outstanding Ticket==============================================================================================================================
    'Done Ticket=========================================================================================================================================
    Sub TicketDoneBlnIni()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "Select count (StatusID) as jumlahTiket from SPR_eticket where StatusID>='6' and month(Tdate)='" & Format(Now, "MM") & "' and year(Tdate)='" & Format(Now, "yyy") & "'"
        If Session("sPossition") = "0" And Session("sProductCode") = "1" Then
            sSQL += " "
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "2" Then
            sSQL += " and SPR_eticket.ProductGrp = 'Al'"
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "3" Then
            sSQL += " and SPR_eticket.ProductGrp <> 'Al'"
        ElseIf Session("sPossition") <> "0" Then
            sSQL += " "
        End If

        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            lbDoneBlnIni.Text = oSQLReader.Item("jumlahTiket").ToString
            oCnct.Close()
        Else
            lbDoneBlnIni.Text = "0"
        End If
        oCnct.Close()
    End Sub
    Sub TicketDoneBlnLalu()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        Dim bln, thn As String
        If Format(Now, "MM") = 1 Then
            bln = "12"
            thn = Format(Now, "yyy") - 1
        Else
            bln = Format(Now, "MM") - 1
            thn = Format(Now, "yyy")
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "Select count (StatusID) as jumlahTiket from SPR_eticket where StatusID>='6' and month(Tdate)='" & bln & "' and year(Tdate)='" & thn & "'"
        If Session("sPossition") = "0" And Session("sProductCode") = "1" Then
            sSQL += " "
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "2" Then
            sSQL += " and SPR_eticket.ProductGrp = 'Al'"
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "3" Then
            sSQL += " and SPR_eticket.ProductGrp <> 'Al'"
        ElseIf Session("sPossition") <> "0" Then
            sSQL += " "
        End If

        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            lbDoneBlnLalu.Text = oSQLReader.Item("jumlahTiket").ToString
            oCnct.Close()
        Else
            lbDoneBlnLalu.Text = "0"
        End If
        oCnct.Close()
    End Sub
    Sub totalTicketDone()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "Select count (StatusID) as jumlahTiket from SPR_eticket where StatusID>='6'"
        If Session("sPossition") = "0" And Session("sProductCode") = "1" Then
            sSQL += " "
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "2" Then
            sSQL += " and SPR_eticket.ProductGrp = 'Al'"
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "3" Then
            sSQL += " and SPR_eticket.ProductGrp <> 'Al'"
        ElseIf Session("sPossition") <> "0" Then
            sSQL += " "
        End If

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
    'End Done Ticket=====================================================================================================================================
    'Follow Ticket=======================================================================================================================================
    Sub TicketFollowBlnIni()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "select count (*) as jumlahTiket from SPR_eticketFollowUp where FollowUpCode='P' and month(FollowUpDate)='" & Format(Now, "MM") & "' and year(FollowUpDate)='" & Format(Now, "yyy") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            llbFolBlnIni.Text = oSQLReader.Item("jumlahTiket").ToString
            oCnct.Close()
        Else
            llbFolBlnIni.Text = "0"
        End If
        oCnct.Close()
    End Sub
    Sub TicketFollowBlnLalu()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        Dim bln, thn As String
        If Format(Now, "MM") = 1 Then
            bln = "12"
            thn = Format(Now, "yyy") - 1
        Else
            bln = Format(Now, "MM") - 1
            thn = Format(Now, "yyy")
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "Select count (*) as jumlahTiket from SPR_eticketFollowUp where FollowUpCode='P' and  month(FollowUpDate)='" & bln & "' and year(FollowUpDate)='" & thn & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            lbFolBlnLalu.Text = oSQLReader.Item("jumlahTiket").ToString
            oCnct.Close()
        Else
            lbFolBlnLalu.Text = "0"
        End If
        oCnct.Close()
    End Sub
    Sub totalTicketFollow()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "Select count (*) as jumlahTiket from SPR_eticketFollowUp where FollowUpCode='P'"
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
    'End Follow Ticket===================================================================================================================================
    'New Ticket==========================================================================================================================================
    Sub TicketNewBlnIniSupport()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "Select count (StatusID) as jumlahTiket from SPR_eticket where StatusID='" & "2" & "' and AssignTo='" & Session("sId") & "' and month(Tdate)='" & Format(Now, "MM") & "' and year(Tdate)='" & Format(Now, "yyy") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            lbNewBlnIni.Text = oSQLReader.Item("jumlahTiket").ToString
            oCnct.Close()
        Else
            lbNewBlnIni.Text = "0"
        End If
        oCnct.Close()
    End Sub
    Sub TicketNewBlnLaluSupport()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        Dim bln, thn As String
        If Format(Now, "MM") = 1 Then
            bln = "12"
            thn = Format(Now, "yyy") - 1
        Else
            bln = Format(Now, "MM") - 1
            thn = Format(Now, "yyy")
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "Select count (StatusID) as jumlahTiket from SPR_eticket where StatusID='" & "2" & "' and AssignTo='" & Session("sId") & "' and month(Tdate)='" & bln & "' and year(Tdate)='" & thn & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            lbNewBlnLalu.Text = oSQLReader.Item("jumlahTiket").ToString
            oCnct.Close()
        Else
            lbNewBlnLalu.Text = "0"
        End If
        oCnct.Close()
    End Sub
    Sub totalTicketNewSupport()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "Select count (StatusID) as jumlahTiket from SPR_eticket where StatusID='" & "2" & "' and AssignTo='" & Session("sId") & "' "
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            lbNewTtl.Text = oSQLReader.Item("jumlahTiket").ToString
            Session("sE-Ticket") = oSQLReader.Item("jumlahTiket").ToString
            oCnct.Close()
        Else
            lbNewTtl.Text = "0"
        End If
        oCnct.Close()
    End Sub
    'End New Ticket======================================================================================================================================
    'Outstanding Ticket==================================================================================================================================
    Sub TicketOutstandingBlnIniSupport()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "Select count (StatusID) as jumlahTiket from SPR_eticket where StatusID <'6'  and StatusID>'" & "2" & "' and AssignTo='" & Session("sId") & " ' and month(Tdate)='" & Format(Now, "MM") & "' and year(Tdate)='" & Format(Now, "yyy") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            lbOutBlnIni.Text = oSQLReader.Item("jumlahTiket").ToString
            oCnct.Close()
        Else
            lbOutBlnIni.Text = "0"
        End If
        oCnct.Close()
    End Sub
    Sub TicketOutstandingBlnLaluSupport()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        Dim bln, thn As String
        If Format(Now, "MM") = 1 Then
            bln = "12"
            thn = Format(Now, "yyy") - 1
        Else
            bln = Format(Now, "MM") - 1
            thn = Format(Now, "yyy")
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "Select count (StatusID) as jumlahTiket from SPR_eticket where StatusID <'6' and StatusID >'" & "2" & "' and AssignTo='" & Session("sId") & " ' and month(Tdate)='" & bln & "' and year(Tdate)='" & thn & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            lbOutBlnLalu.Text = oSQLReader.Item("jumlahTiket").ToString
            oCnct.Close()
        Else
            lbOutBlnLalu.Text = "0"
        End If
        oCnct.Close()
    End Sub
    Sub totalTicketOutstandingSupport()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "Select count (StatusID) as jumlahTiket from SPR_eticket where StatusID <'6'  and StatusID>'" & "2" & "' and AssignTo='" & Session("sId") & " '"
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
    'End Outstanding Ticket==============================================================================================================================
    'Done Ticket=========================================================================================================================================
    Sub TicketDoneBlnIniSupport()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "Select count (StatusID) as jumlahTiket from SPR_eticket where StatusID>='6' and month(Tdate)='" & Format(Now, "MM") & "' and year(Tdate)='" & Format(Now, "yyy") & "' and AssignTo='" & Session("sId") & " '"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            lbDoneBlnIni.Text = oSQLReader.Item("jumlahTiket").ToString
            oCnct.Close()
        Else
            lbDoneBlnIni.Text = "0"
        End If
        oCnct.Close()
    End Sub
    Sub TicketDoneBlnLaluSupport()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        Dim bln, thn As String
        If Format(Now, "MM") = 1 Then
            bln = "12"
            thn = Format(Now, "yyy") - 1
        Else
            bln = Format(Now, "MM") - 1
            thn = Format(Now, "yyy")
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "Select count (StatusID) as jumlahTiket from SPR_eticket where AssignTo='" & Session("sId") & "' and StatusID>='6' and month(Tdate)='" & bln & "' and year(Tdate)='" & thn & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            lbDoneBlnLalu.Text = oSQLReader.Item("jumlahTiket").ToString
            oCnct.Close()
        Else
            lbDoneBlnLalu.Text = "0"
        End If
        oCnct.Close()
    End Sub
    Sub totalTicketDoneSupport()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "Select count (StatusID) as jumlahTiket from SPR_eticket where StatusID>='6' and AssignTo='" & Session("sId") & " '"
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
    'End Done Ticket=====================================================================================================================================
    'Follow Ticket=======================================================================================================================================
    Sub TicketFollowBlnIniSupport()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "select count (*) as jumlahTiket from SPR_eticketFollowUp INNER JOIN SPR_eticket ON SPR_eticketFollowUp .TicketNo = SPR_eticket .TicketNo where SPR_eticket.AssignTo='" & Session("sId") & "' and FollowUpCode='P' and month(FollowUpDate)='" & Format(Now, "MM") & "' and year(FollowUpDate)='" & Format(Now, "yyy") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            llbFolBlnIni.Text = oSQLReader.Item("jumlahTiket").ToString
            oCnct.Close()
        Else
            llbFolBlnIni.Text = "0"
        End If
        oCnct.Close()
    End Sub
    Sub TicketFollowBlnLaluSupport()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        Dim bln, thn As String
        If Format(Now, "MM") = 1 Then
            bln = "12"
            thn = Format(Now, "yyy") - 1
        Else
            bln = Format(Now, "MM") - 1
            thn = Format(Now, "yyy")
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "Select count (*) as jumlahTiket from SPR_eticketFollowUp  INNER JOIN SPR_eticket ON SPR_eticketFollowUp .TicketNo = SPR_eticket .TicketNo where SPR_eticket.AssignTo='" & Session("sId") & "' and FollowUpCode='P' and  month(FollowUpDate)='" & bln & "' and year(FollowUpDate)='" & thn & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            lbFolBlnLalu.Text = oSQLReader.Item("jumlahTiket").ToString
            oCnct.Close()
        Else
            lbFolBlnLalu.Text = "0"
        End If
        oCnct.Close()
    End Sub
    Sub totalTicketFollowSupport()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "Select count (*) as jumlahTiket from SPR_eticketFollowUp  INNER JOIN SPR_eticket ON SPR_eticketFollowUp .TicketNo = SPR_eticket .TicketNo where SPR_eticket.AssignTo='" & Session("sId") & "' and FollowUpCode='P'"
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
    'End Follow Ticket===================================================================================================================================

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sId") = Nothing Then
            Response.Redirect("SignIn.aspx")
        Else
            If Session("sPossition") <> "0" Then
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hidePanelNew", "$(document).ready(function() {hidePanelNew()});", True)
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hidePanelProses", "$(document).ready(function() {hidePanelProses()});", True)
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hidePanelDone", "$(document).ready(function() {hidePanelDone()});", True)
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hidePanelFollow", "$(document).ready(function() {hidePanelFollow()});", True)
                TicketNewBlnIniSupport()
                TicketNewBlnLaluSupport()
                totalTicketNewSupport()
                TicketOutstandingBlnIniSupport()
                TicketOutstandingBlnLaluSupport()
                totalTicketOutstandingSupport()
                TicketDoneBlnIniSupport()
                TicketDoneBlnLaluSupport()
                totalTicketDoneSupport()
                TicketFollowBlnIniSupport()
                TicketFollowBlnLaluSupport()
                totalTicketFollowSupport()
            Else
                BlnIni = Format(Now, "yyy-MM-")
                i = Format(Now, "MM") - 1
                BlnLalu = Format(Now, "yyy-") & i & "-"
                'totalTicketFollow()
                TicketFollowBlnIni()
                TicketFollowBlnLalu()
                lbTotalFollow.Text = Val(lbFolBlnLalu.Text) + Val(llbFolBlnIni.Text)
                totalTicketNew()
                TicketNewBlnIni()
                TicketNewBlnLalu()
                totalTicketOutstanding()
                TicketOutstandingBlnIni()
                TicketOutstandingBlnLalu()
                totalTicketDone()
                TicketDoneBlnIni()
                TicketDoneBlnLalu()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showPanelNew", "$(document).ready(function() {showPanelNew()});", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showPanelProses", "$(document).ready(function() {showPanelProses()});", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showPanelDone", "$(document).ready(function() {showPanelDone()});", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showPanelFollow", "$(document).ready(function() {showPanelFollow()});", True)
            End If
        End If
    End Sub
    'Protected Sub TimerTick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
    '    If Session("sPossition") <> "0" Then
    '        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hidePanelNew", "$(document).ready(function() {hidePanelNew()});", True)
    '        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hidePanelProses", "$(document).ready(function() {hidePanelProses()});", True)
    '        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hidePanelDone", "$(document).ready(function() {hidePanelDone()});", True)
    '        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hidePanelFollow", "$(document).ready(function() {hidePanelFollow()});", True)
    '        TicketNewBlnIniSupport()
    '        TicketNewBlnLaluSupport()
    '        totalTicketNewSupport()
    '        TicketOutstandingBlnIniSupport()
    '        TicketOutstandingBlnLaluSupport()
    '        totalTicketOutstandingSupport()
    '        TicketDoneBlnIniSupport()
    '        TicketDoneBlnLaluSupport()
    '        totalTicketDoneSupport()
    '        TicketFollowBlnIniSupport()
    '        TicketFollowBlnLaluSupport()
    '        totalTicketFollowSupport()
    '    Else
    '        BlnIni = Format(Now, "yyy-MM-")
    '        i = Format(Now, "MM") - 1
    '        BlnLalu = Format(Now, "yyy-") & i & "-"
    '        'totalTicketFollow()
    '        TicketFollowBlnIni()
    '        TicketFollowBlnLalu()
    '        lbTotalFollow.Text = Val(lbFolBlnLalu.Text) + Val(llbFolBlnIni.Text)
    '        totalTicketNew()
    '        TicketNewBlnIni()
    '        TicketNewBlnLalu()
    '        totalTicketOutstanding()
    '        TicketOutstandingBlnIni()
    '        TicketOutstandingBlnLalu()
    '        totalTicketDone()
    '        TicketDoneBlnIni()
    '        TicketDoneBlnLalu()
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showPanelNew", "$(document).ready(function() {showPanelNew()});", True)
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showPanelProses", "$(document).ready(function() {showPanelProses()});", True)
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showPanelDone", "$(document).ready(function() {showPanelDone()});", True)
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showPanelFollow", "$(document).ready(function() {showPanelFollow()});", True)
    '    End If

    '    If Session("snotifTotalTicket") = "0" Then
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotifTiket", "$(document).ready(function() {hideNotifTiket()});", True)
    '    End If
    '    If Session("snotifNewTicket") = "0" Then
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotifNewTiket", "$(document).ready(function() {hideNotifNewTiket()});", True)
    '    End If
    '    If Session("snotifFollow") = "0" Then
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotifFollow", "$(document).ready(function() {hideNotifFollow()});", True)
    '    End If
    '    If Session("snotifDone") = "0" Then
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotifDone", "hideNotifDone();", True)
    '    End If
    'End Sub

    Protected Sub lbFooterNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbFooterNew.Click
        If Session("sPossition") = "0" Then
            Response.Redirect("iPxCrmNewtiket.aspx")
        Else
            Response.Redirect("iPxCrmAssign.aspx")
        End If
    End Sub
End Class
