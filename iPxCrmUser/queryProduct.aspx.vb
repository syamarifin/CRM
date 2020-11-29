
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Globalization
Imports System.Threading
Partial Class iPxCrmUser_queryProduct
    Inherits System.Web.UI.Page

    Public sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Public oCnct As SqlConnection = New SqlConnection(sCnct)
    Public oSQLCmd As New SqlCommand
    Public oSQLReader As SqlDataReader
    Public sSQL As String
    Dim cipx As New iPxClass

    Public Function ExecuteQuery(ByVal cmd As SqlCommand, ByVal action As String) As DataTable
        Dim conString As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ConnectionString
        Using con As New SqlConnection(conString)
            cmd.Connection = con
            Select Case action
                Case "SELECT"
                    Using sda As New SqlDataAdapter()
                        sda.SelectCommand = cmd
                        Using dt As New DataTable()
                            sda.Fill(dt)
                            Return dt
                        End Using
                    End Using
                Case "UPDATE"
                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                    Exit Select
            End Select
            Return Nothing
        End Using
    End Function

    Sub showdata_ProductGrp()

        If Session("sPossition") = "0" And Session("sProductCode") = "1" Then
            sSQL = "select * from CFG_productGrp"
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "2" Then
            sSQL = "select * from CFG_productGrp where ProductGrp = 'AL'"
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "3" Then
            sSQL = "select * from CFG_productGrp where ProductGrp <> 'AL'"
        ElseIf Session("sPossition") <> "0" Then
            sSQL = "select * from CFG_productGrp"
        End If
        Dim cmd As New SqlCommand(sSQL)
        dlProductGrp.DataSource = ExecuteQuery(cmd, "SELECT")
        dlProductGrp.DataTextField = "prdDescription"
        dlProductGrp.DataValueField = "ProductGrp"
        dlProductGrp.DataBind()
        dlProductGrp.Items.Insert(0, "")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            showdata_ProductGrp()
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "PopUser", "clearModal()", True)
        End If
    End Sub


    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        'iPxLoyalty Management
        If Session("sPossition") = "0" And Session("sProductCode") = "1" Then
            Session("sCondition") = " Where "
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "2" Then
            Session("sCondition") = " And"
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "3" Then
            Session("sCondition") = " And"
        ElseIf Session("sPossition") <> "0" Then
            Session("sCondition") = " Where"
        End If

        If tbPrdID.Text.Trim <> "" Then
            Session("sCondition") = Session("sCondition") & " (CFG_Product.ProductID like '%" & Replace(tbPrdID.Text.Trim, "'", "''") & "%') "
        End If

        If tbPrdName.Text.Trim <> "" Then
            Session("sCondition") = Session("sCondition") & " (CFG_Product.ProductName like '%" & Replace(tbPrdName.Text.Trim, "'", "''") & "%') "
        End If

        If dlProductGrp.SelectedValue.Trim <> "" Then
            Session("sCondition") = Session("sCondition") & " (CFG_Product.ProductGrp like '%" & dlProductGrp.SelectedValue.Trim & "%') "
        End If

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "PopUser", "hideModal()", True)
        'sSQL = Session("sCondition")
        Response.Redirect("iPxCrmProduct.aspx?sCondition=" & sSQL)
    End Sub

    Protected Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click

        Session("sCondition") = ""
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "PopUser", "hideModal()", True)
        Response.Redirect("iPxCrmProduct.aspx?sCondition=")
    End Sub

End Class
