Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Imports System.Web.Services
Imports System.Web.Script.Serialization
Imports System.Collections.Generic
Partial Class Default2
    Inherits System.Web.UI.Page

    <System.Web.Services.WebMethod()> _
    Public Shared Function GetUser(ByVal Possition As String, ByVal ProductCode As String) As String

        Dim Order As New List(Of Object)()
        Dim sSQL As String
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("iPxCNCT").ConnectionString
            Using cmd As New SqlCommand()
                sSQL = "Select CFG_user.*, CFG_dept.DeptName from CFG_user INNER JOIN CFG_dept ON CFG_user.dept=CFG_dept.DeptCode where isActive='Y'"
                cmd.CommandText = sSQL
                'cmd.CommandText = "SELECT COUNT(isRead) as unread FROM SPR_eticketFollowUp WHERE isRead = '1'"
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        Order.Add(New With {.email = sdr("Email"), .name = sdr("name"), .dept = sdr("DeptName")})
                    End While
                End Using
                conn.Close()
            End Using
            Return (New JavaScriptSerializer().Serialize(Order))
        End Using
    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetUnread(ByVal Possition As String, ByVal ProductCode As String) As String

        Dim Order As New List(Of Object)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("iPxCNCT").ConnectionString
            Using cmd As New SqlCommand()
                'sSQL = "Select CFG_user.*, CFG_dept.DeptName from CFG_user INNER JOIN CFG_dept ON CFG_user.dept=CFG_dept.DeptCode where isActive='Y'"
                'cmd.CommandText = sSQL
                cmd.CommandText = "SELECT * From SPR_eticketFollowUp where TicketNo = 'MB1O1000000063' order by FollowUpDate asc"
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        Dim dateBirthday As Date = sdr("FollowUpDate")
                        Dim datechat As String = dateBirthday.ToString("dd MMM yyyy hh:mm:ss")
                        Order.Add(New With {.FollowUpNo = sdr("FollowUpNo"), .FollowUpBy = sdr("FollowUpBy"), .FollowUpNote = sdr("FollowUpNote"), .FollowUpCode = sdr("FollowUpCode"), .FollowUpSopLink = sdr("FollowUpSopLink"), .FollowUpDate = datechat})
                    End While
                End Using
                conn.Close()
            End Using
            Return (New JavaScriptSerializer().Serialize(Order))
        End Using
    End Function
    <System.Web.Services.WebMethod()> _
    Public Shared Function notifTotalTicketNew(ByVal Possition As String, ByVal ProductCode As String) As String

        Dim Order As New List(Of Object)()
        Dim sSQL As String
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("iPxCNCT").ConnectionString
            Using cmd As New SqlCommand()
                sSQL = "Select count (StatusID) as jumlahTiket from SPR_eticket INNER JOIN CFG_productGrp on SPR_eticket .ProductGrp = CFG_productGrp .ProductGrp  where StatusID='" & "1" & "'"
                If Possition = "0" And ProductCode = "1" Then
                    sSQL += " "
                ElseIf Possition = "0" And ProductCode = "2" Then
                    sSQL += " and CFG_productGrp.PrdDescription = 'Alcor'"
                ElseIf Possition = "0" And ProductCode = "3" Then
                    sSQL += " and CFG_productGrp.PrdDescription <> 'Alcor'"
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
    Public Shared Function notifTotalTicketFollow(ByVal Possition As String, ByVal ProductCode As String) As String

        Dim Order As New List(Of Object)()
        Dim sSQL As String
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("iPxCNCT").ConnectionString
            Using cmd As New SqlCommand()
                sSQL = "Select count (SPR_eticketFollowUp.isRead) as jumlahFollow from SPR_eticketFollowUp INNER JOIN SPR_eticket ON SPR_eticket.TicketNo =SPR_eticketFollowUp.TicketNo where SPR_eticketFollowUp.FollowUpCode= 'H' and SPR_eticketFollowUp.isRead='" & "1" & "' and SPR_eticket.StatusID < '6' and SPR_eticket.AssignTo <> '" & "" & "'"
                If Possition = "0" And ProductCode = "1" Then
                    sSQL += " "
                ElseIf Possition = "0" And ProductCode = "2" Then
                    sSQL += " and CFG_productGrp.PrdDescription = 'Alcor'"
                ElseIf Possition = "0" And ProductCode = "3" Then
                    sSQL += " and CFG_productGrp.PrdDescription <> 'Alcor'"
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
    Public Shared Function notifTotalTicketDone(ByVal Possition As String, ByVal ProductCode As String) As String

        Dim Order As New List(Of Object)()
        Dim sSQL As String
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("iPxCNCT").ConnectionString
            Using cmd As New SqlCommand()
                sSQL = "Select count (SPR_eticketFollowUp.isRead) as jumlahFollow from SPR_eticketFollowUp INNER JOIN SPR_eticket ON SPR_eticket.TicketNo =SPR_eticketFollowUp.TicketNo where SPR_eticketFollowUp.FollowUpCode= 'H' and SPR_eticketFollowUp.isRead='" & "1" & "' and SPR_eticket.StatusID >= '6' and SPR_eticket.AssignTo <> '" & "" & "'"
                If Possition = "0" And ProductCode = "1" Then
                    sSQL += " "
                ElseIf Possition = "0" And ProductCode = "2" Then
                    sSQL += " and CFG_productGrp.PrdDescription = 'Alcor'"
                ElseIf Possition = "0" And ProductCode = "3" Then
                    sSQL += " and CFG_productGrp.PrdDescription <> 'Alcor'"
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
    'Sub notifTotalNewFollowAdmin()
    '    If oCnct.State = ConnectionState.Closed Then
    '        oCnct.Open()
    '    End If
    '    oSQLCmd = New SqlCommand(sSQL, oCnct)
    '    sSQL = "Select count (SPR_eticketFollowUp.isRead) as jumlahFollow from SPR_eticketFollowUp INNER JOIN SPR_eticket ON SPR_eticket.TicketNo =SPR_eticketFollowUp.TicketNo where SPR_eticketFollowUp.FollowUpCode= 'H' and SPR_eticketFollowUp.isRead='" & "1" & "' and SPR_eticket.StatusID < '6' and SPR_eticket.AssignTo <> '" & "" & "'"
    '    If Session("sPossition") = "0" And Session("sProductCode") = "1" Then
    '        sSQL += " "
    '    ElseIf Session("sPossition") = "0" And Session("sProductCode") = "2" Then
    '        sSQL += " and SPR_eticket.ProductGrp = 'Al'"
    '    ElseIf Session("sPossition") = "0" And Session("sProductCode") = "3" Then
    '        sSQL += " and SPR_eticket.ProductGrp <> 'Al'"
    '    End If

    '    oSQLCmd.CommandText = sSQL
    '    oSQLReader = oSQLCmd.ExecuteReader

    '    oSQLReader.Read()
    '    If oSQLReader.HasRows Then
    '        lblFollow.Text = oSQLReader.Item("jumlahFollow").ToString
    '        oCnct.Close()
    '    Else
    '        lblFollow.Text = "0"
    '    End If
    '    oCnct.Close()
    'End Sub
    'Sub notifTotalNewFollowStaff()
    '    If oCnct.State = ConnectionState.Closed Then
    '        oCnct.Open()
    '    End If
    '    oSQLCmd = New SqlCommand(sSQL, oCnct)
    '    sSQL = "Select count (SPR_eticketFollowUp.isRead) as jumlahFollow from SPR_eticketFollowUp INNER JOIN SPR_eticket ON SPR_eticket.TicketNo =SPR_eticketFollowUp.TicketNo where SPR_eticketFollowUp.FollowUpCode= 'H' and SPR_eticketFollowUp.isRead='" & "1" & "' and SPR_eticket.StatusID < '6' and SPR_eticket.AssignTo = '" & Session("sId") & "'"
    '    If Session("sPossition") = "0" And Session("sProductCode") = "1" Then
    '        sSQL += " "
    '    ElseIf Session("sPossition") = "0" And Session("sProductCode") = "2" Then
    '        sSQL += " and SPR_eticket.ProductGrp = 'Al'"
    '    ElseIf Session("sPossition") = "0" And Session("sProductCode") = "3" Then
    '        sSQL += " and SPR_eticket.ProductGrp <> 'Al'"
    '    End If

    '    oSQLCmd.CommandText = sSQL
    '    oSQLReader = oSQLCmd.ExecuteReader

    '    oSQLReader.Read()
    '    If oSQLReader.HasRows Then
    '        lblFollow.Text = oSQLReader.Item("jumlahFollow").ToString
    '        oCnct.Close()
    '    Else
    '        lblFollow.Text = "0"
    '    End If
    '    oCnct.Close()
    'End Sub
    'Sub notifTotalNewDoneAdmin()
    '    If oCnct.State = ConnectionState.Closed Then
    '        oCnct.Open()
    '    End If
    '    oSQLCmd = New SqlCommand(sSQL, oCnct)
    '    sSQL = "Select count (SPR_eticketFollowUp.isRead) as jumlahFollow from SPR_eticketFollowUp INNER JOIN SPR_eticket ON SPR_eticket.TicketNo =SPR_eticketFollowUp.TicketNo where SPR_eticketFollowUp.FollowUpCode= 'H' and SPR_eticketFollowUp.isRead='" & "1" & "' and SPR_eticket.StatusID >= '6' and SPR_eticket.AssignTo <> '" & "" & "'"
    '    If Session("sPossition") = "0" And Session("sProductCode") = "1" Then
    '        sSQL += " "
    '    ElseIf Session("sPossition") = "0" And Session("sProductCode") = "2" Then
    '        sSQL += " and SPR_eticket.ProductGrp = 'Al'"
    '    ElseIf Session("sPossition") = "0" And Session("sProductCode") = "3" Then
    '        sSQL += " and SPR_eticket.ProductGrp <> 'Al'"
    '    End If

    '    oSQLCmd.CommandText = sSQL
    '    oSQLReader = oSQLCmd.ExecuteReader

    '    oSQLReader.Read()
    '    If oSQLReader.HasRows Then
    '        lblDone.Text = oSQLReader.Item("jumlahFollow").ToString
    '        oCnct.Close()
    '    Else
    '        lblDone.Text = "0"
    '    End If
    'End Sub
    'Sub notifTotalNewDoneStaff()
    '    If oCnct.State = ConnectionState.Closed Then
    '        oCnct.Open()
    '    End If
    '    oSQLCmd = New SqlCommand(sSQL, oCnct)
    '    sSQL = "Select count (SPR_eticketFollowUp.isRead) as jumlahFollow from SPR_eticketFollowUp INNER JOIN SPR_eticket ON SPR_eticket.TicketNo =SPR_eticketFollowUp.TicketNo where SPR_eticketFollowUp.FollowUpCode= 'H' and SPR_eticketFollowUp.isRead='" & "1" & "' and SPR_eticket.StatusID >= '6' and SPR_eticket.AssignTo = '" & Session("sId") & "'"
    '    If Session("sPossition") = "0" And Session("sProductCode") = "1" Then
    '        sSQL += " "
    '    ElseIf Session("sPossition") = "0" And Session("sProductCode") = "2" Then
    '        sSQL += " and SPR_eticket.ProductGrp = 'Al'"
    '    ElseIf Session("sPossition") = "0" And Session("sProductCode") = "3" Then
    '        sSQL += " and SPR_eticket.ProductGrp <> 'Al'"
    '    End If

    '    oSQLCmd.CommandText = sSQL
    '    oSQLReader = oSQLCmd.ExecuteReader

    '    oSQLReader.Read()
    '    If oSQLReader.HasRows Then
    '        lblDone.Text = oSQLReader.Item("jumlahFollow").ToString
    '        oCnct.Close()
    '    Else
    '        lblDone.Text = "0"
    '    End If
    'End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Protected Sub Unduh(ByVal sender As Object, ByVal e As EventArgs)
        Response.Redirect("SignIn.aspx")
    End Sub
End Class
