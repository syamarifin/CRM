Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Globalization
Imports System.Threading
Partial Class iPxCrmCustomer_warningmsg
    Inherits System.Web.UI.Page
    Public sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Public oCnct As SqlConnection = New SqlConnection(sCnct)
    Public oSQLCmd As New SqlCommand
    Public oSQLReader As SqlDataReader
    Public sSQL As String

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
        Response.Redirect(Session("sUrlOKONLY"))
    End Sub

    Protected Sub btnYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYes.Click
        Response.Redirect(Session("sUrlYES"))
    End Sub

    Protected Sub btnNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNo.Click
        Response.Redirect(Session("sUrlNO"))
    End Sub
End Class
