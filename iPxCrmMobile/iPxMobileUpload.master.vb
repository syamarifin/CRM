
Partial Class iPxMobile_iPxMobileUpload
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        On Error Resume Next

        If Not Page.IsPostBack Then
            'Response.Redirect("home.aspx")

            Dim cIpx As New iPxClass

        End If
        ScriptManager.RegisterStartupScript(Page, [GetType](), "move", "<script>move()</script>", False)
    End Sub
    Sub removecookie()
        'Fetch the Cookie using its Key.
        Dim userCookie As HttpCookie = Request.Cookies("user")
        Dim passCookie As HttpCookie = Request.Cookies("pass")
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

End Class

