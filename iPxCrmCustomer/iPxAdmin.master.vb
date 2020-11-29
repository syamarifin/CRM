
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Partial Class iPXADMIN_iPxAdmin
    Inherits System.Web.UI.MasterPage

    Public sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Public oCnct As SqlConnection = New SqlConnection(sCnct)
    Public oSQLCmd As New SqlCommand
    Public oSQLReader As SqlDataReader
    Public sSQL As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
   

    End Sub


    

End Class

