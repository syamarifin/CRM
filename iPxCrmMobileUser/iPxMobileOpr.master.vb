
Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Imports System.Web.Services
Imports System.Web.Script.Serialization
Imports System.Collections.Generic

Partial Class iPxMobile_iPxMobileOpr
    Inherits System.Web.UI.MasterPage

    Public sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Public oCnct As SqlConnection = New SqlConnection(sCnct)
    Public oSQLCmd As New SqlCommand
    Public oSQLReader As SqlDataReader
    Public sSQL As String
    Sub versi()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "select * from iPx_general_ver where id = (select MAX(iPx_general_verDTL.verid) from iPx_general_verDTL where iPx_general_verDTL .active='Y')"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            lbVersi.Text = "v. " + oSQLReader.Item("version").ToString
            oCnct.Close()
        Else
            lbVersi.Text = "v. 1.0.0"
        End If
    End Sub
    <System.Web.Services.WebMethod()> _
    Public Shared Function GetUnread(ByVal Possition As String, ByVal ProductCode As String) As String
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
                'cmd.CommandText = "SELECT COUNT(isRead) as unread FROM SPR_eticketFollowUp WHERE isRead = '1'"
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        Order.Add(New With {.unread = sdr("jumlahTiket")})
                    End While
                End Using
                conn.Close()
            End Using
            Return (New JavaScriptSerializer().Serialize(Order))
        End Using
    End Function
    'Sub notifTotalTicketNew()
    '    If oCnct.State = ConnectionState.Closed Then
    '        oCnct.Open()
    '    End If
    '    oSQLCmd = New SqlCommand(sSQL, oCnct)
    '    sSQL = "Select count (StatusID) as jumlahTiket from SPR_eticket INNER JOIN CFG_productGrp on SPR_eticket .ProductGrp = CFG_productGrp .ProductGrp  where StatusID='" & "1" & "'"
    '    If Session("sPossition") = "0" And Session("sProductCode") = "1" Then
    '        sSQL += " "
    '    ElseIf Session("sPossition") = "0" And Session("sProductCode") = "2" Then
    '        sSQL += " and CFG_productGrp.PrdDescription = 'Alcor'"
    '    ElseIf Session("sPossition") = "0" And Session("sProductCode") = "3" Then
    '        sSQL += " and CFG_productGrp.PrdDescription <> 'Alcor'"
    '    End If
    '    oSQLCmd.CommandText = sSQL
    '    oSQLReader = oSQLCmd.ExecuteReader

    '    oSQLReader.Read()
    '    If oSQLReader.HasRows Then
    '        lblNew.Text = oSQLReader.Item("jumlahTiket").ToString
    '        oCnct.Close()
    '    Else
    '        lblNew.Text = "0"
    '    End If
    '    oCnct.Close()
    'End Sub
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        businessname()
        lbInfoUser.Text = Session("sName")
        versi()
        'If Session("sPossition") <> "0" Then
        '    notifTotalNewFollowStaff()
        '    notifTotalNewDoneStaff()
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotifNewTiket", "hideNotifNewTiket();", True)
        '    If lblFollow.Text = "0" Then
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotifFollow", "hideNotifFollow();", True)
        '    End If
        '    If lblDone.Text = "0" Then
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotifDone", "hideNotifDone();", True)
        '    End If
        '    If lblFollow.Text = "0" And lblDone.Text = "0" Then
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotifHead", "hideNotifHead();", True)
        '    End If
        'Else
        '    notifTotalTicketNew()
        '    notifTotalNewFollowAdmin()
        '    notifTotalNewDoneAdmin()
        '    If lblNew.Text = "0" Then
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotifNewTiket", "hideNotifNewTiket();", True)
        '    End If
        '    If lblFollow.Text = "0" Then
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotifFollow", "hideNotifFollow();", True)
        '    End If
        '    If lblDone.Text = "0" Then
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotifDone", "hideNotifDone();", True)
        '    End If
        '    If lblNew.Text = "0" And lblFollow.Text = "0" And lblDone.Text = "0" Then
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotifHead", "hideNotifHead();", True)
        '    End If
        'End If
        'imgProfile.ImageUrl = "Handler.ashx?ID=0|" & Session("sBusinessID") & "|" & Session("sProfileID") & ""


    End Sub


    ' Protected Sub ddSite_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddSite.SelectedIndexChanged
    'Session("sSite") = ddSite.SelectedValue
    '      Response.Redirect("logoff.aspx")
    '   End Sub

    Sub businessname()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If

        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "select * from  CFG_customerContact where CustID='" & "1" & "' "
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        If oSQLReader.Read Then
            lblBusinessName.Text = oSQLReader.Item("Name")
        End If

        oCnct.Close()
    End Sub


    Sub removecookie()
        'Fetch the Cookie using its Key.
        Dim userCookie As HttpCookie = Request.Cookies("cEmail")
        Dim passCookie As HttpCookie = Request.Cookies("cPassw")
        'Set the Expiry date to past date.
        If userCookie Is Nothing Then

        Else
            userCookie.Expires = DateTime.Now.AddDays(-1)
            passCookie.Expires = DateTime.Now.AddDays(-1)
            'Update the Cookie in Browser.
            Response.Cookies.Add(userCookie)
            Response.Cookies.Add(passCookie)
        End If

    End Sub
    Protected Sub logout_ServerClick(ByVal sender As Object, ByVal e As EventArgs) Handles logout.ServerClick
        removecookie()
        Response.Redirect("SignIn.aspx")
    End Sub
End Class

