Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Globalization
Imports System.Threading
Partial Class iPxCrmMobile_warningmsg
    Inherits System.Web.UI.Page
    Public sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Public oCnct As SqlConnection = New SqlConnection(sCnct)
    Public oSQLCmd As New SqlCommand
    Public oSQLReader As SqlDataReader
    Public sSQL As String
    Sub deletedata()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "UPDATE SPR_eticket SET StatusID='" & "0" & "' where TicketNo='" & Session("sIdDelete") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        cekFollow()
    End Sub

    Sub cekFollow()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT TicketNo FROM SPR_eticketFollowUp WHERE TicketNo = '" & Session("sIdDelete") & "'"
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
        sSQL = sSQL & "WHERE TicketNo = '" & Session("sIdDelete") & "' "

        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "PopUser", "clearModal()", True)
        ScriptManager.RegisterStartupScript(Page, [GetType](), "hideDisplayBlock", "<script>hideDisplayBlock()</script>", False)
        If Session("sWarningID") = "0" Then
            confirmationstep1.Visible = True
            confirmationfooterokonly.Visible = True
            confirmationfooteryesno.Visible = False
        Else
            confirmationstep1.Visible = True
            confirmationfooterokonly.Visible = False
            confirmationfooteryesno.Visible = True

        End If

    End Sub

    Protected Sub btnOkonly_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOkonly.Click
        deletedata()
        Response.Redirect(Session("sUrlOKONLY"))
    End Sub

    Protected Sub btnYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYes.Click
        Response.Redirect(Session("sUrlYES"))
    End Sub

    Protected Sub btnNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNo.Click
        Response.Redirect(Session("sUrlNO"))
    End Sub

    Protected Sub btnCancelonly_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelonly.Click
        Response.Redirect(Session("sUrlCANCEL"))
    End Sub
End Class
