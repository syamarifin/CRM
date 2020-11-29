
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing

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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        businessname()
        lbInfoUser.Text = Session("sCName") & " / " & Session("sCHotelName")
        versi()
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
        Dim userCookie As HttpCookie = Request.Cookies("cCEmail")
        Dim passCookie As HttpCookie = Request.Cookies("cCPassw")
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

