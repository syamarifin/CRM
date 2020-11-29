Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Partial Class iPxCrmUser_iPxMasterUserHome
    Inherits System.Web.UI.MasterPage
    Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Dim oCnct As SqlConnection = New SqlConnection(sCnct)
    Dim oSQLCmd As SqlCommand
    Dim oSQLReader As SqlDataReader
    Dim sSQL As String
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
    Sub logout()
        'Fetch the Cookie using its Key.
        Dim nameCookie1 As HttpCookie = Request.Cookies("cEmail")

        If nameCookie1 IsNot Nothing Then
            'Set the Expiry date to past date.
            nameCookie1.Expires = DateTime.Now.AddDays(-1)

            'Update the Cookie in Browser.
            Response.Cookies.Add(nameCookie1)
        End If
        Response.Redirect("../login.aspx")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sId") = "" Then
            Response.Redirect("../login.aspx")
        Else
            lblUser.Text = Session("sName")
            versi()
        End If
    End Sub

    Protected Sub lbLogoutSid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbLogoutSid.Click
        logout()
    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        logout()
    End Sub
End Class

