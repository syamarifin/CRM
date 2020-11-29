Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Partial Class iPxCrmCustomer_iPxCrmCustomerUpload
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
    Sub totalNewFollow()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "Select count (SPR_eticketFollowUp.isRead) as jumlahFollow from SPR_eticketFollowUp where SPR_eticketFollowUp.FollowUpCode= 'P' and SPR_eticketFollowUp.isRead='" & "1" & "' and SPR_eticketFollowUp.CustID='" & Session("sCId") & "'"

        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            lbltotalTicket.Text = oSQLReader.Item("jumlahFollow").ToString
            oCnct.Close()
        Else
            lbltotalTicket.Text = "0"
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sCId") = "" Then
            Response.Redirect("SigIn.aspx")
        End If
        lblUser.Text = Session("sCName")
        lblMasterTitle.Text = "E-Ticket " & Session("sCHotelName")
        totalNewFollow()
        Session("sCnotifTotalTicket") = lbltotalTicket.Text
        If lbltotalTicket.Text = "0" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotif", "hideNotif();", True)
        End If
        versi()
    End Sub
    Protected Sub lbLogoutSid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbLogoutSid.Click
        'Fetch the Cookie using its Key.
        Dim nameCookie1 As HttpCookie = Request.Cookies("cCEmail")

        If nameCookie1 IsNot Nothing Then
            'Set the Expiry date to past date.
            nameCookie1.Expires = DateTime.Now.AddDays(-1)

            'Update the Cookie in Browser.
            Response.Cookies.Add(nameCookie1)
        End If
        Response.Redirect("SigIn.aspx")
    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        'Fetch the Cookie using its Key.
        Dim nameCookie1 As HttpCookie = Request.Cookies("cCEmail")

        If nameCookie1 IsNot Nothing Then
            'Set the Expiry date to past date.
            nameCookie1.Expires = DateTime.Now.AddDays(-1)

            'Update the Cookie in Browser.
            Response.Cookies.Add(nameCookie1)
        End If
        Response.Redirect("SigIn.aspx")
    End Sub
End Class

