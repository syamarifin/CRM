Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Imports System.Web.Services
Imports System.Web.Script.Serialization
Imports System.Collections.Generic
Partial Class iPxCrmMobile_iPxCrmHomeMobile
    Inherits System.Web.UI.Page
    Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Dim oCnct As SqlConnection = New SqlConnection(sCnct)
    Dim oSQLCmd As SqlCommand
    Dim oSQLReader As SqlDataReader
    Dim sSQL As String
#Region "notifComent"
    <System.Web.Services.WebMethod()> _
    Public Shared Function newComent(ByVal CustID As String) As String

        Dim Order As New List(Of Object)()
        Dim sSQL As String
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("iPxCNCT").ConnectionString
            Using cmd As New SqlCommand()
                sSQL = "Select count (SPR_eticketFollowUp.isRead) as jumlahFollow from SPR_eticketFollowUp where SPR_eticketFollowUp.FollowUpCode= 'P' and SPR_eticketFollowUp.isRead='" & "1" & "' and SPR_eticketFollowUp.CustID='" & CustID & "'"

                cmd.CommandText = sSQL
                'cmd.CommandText = "SELECT COUNT(isRead) as unread FROM SPR_eticketFollowUp WHERE isRead = '1'"
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        Order.Add(New With {.unread = sdr("jumlahFollow")})
                        'If sdr("jumlahFollow") = "0" Then
                        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotif", "hideNotif();", True)
                        'Else
                        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showNotif", "showNotif();", True)
                        'End If
                    End While
                End Using
                conn.Close()
            End Using
            Return (New JavaScriptSerializer().Serialize(Order))
        End Using
    End Function
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sCId") = Nothing Then
            Response.Redirect("SignIn.aspx")
        Else
            'If lbltotalTicket.Text = "0" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideNotif", "hideNotif();", True)
            'Else
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showNotif", "showNotif();", True)
            'End If
        End If
    End Sub
    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Response.Redirect("iPxTicketCustomer.aspx")
    End Sub

    Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton2.Click
        Response.Redirect("iPxCrmProfile.aspx")
    End Sub
End Class
