Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Imports System
Partial Class iPxCrmUser_iPxCrmInputProduct
    Inherits System.Web.UI.Page
    Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Dim oCnct As SqlConnection = New SqlConnection(sCnct)
    Dim oSQLCmd As SqlCommand
    Dim oSQLReader As SqlDataReader
    Dim sSQL, Ticketno, i As String
    Dim cIpx As iPxClass

    Sub productGrp()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_productGrp"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                dlProductGrp.DataSource = dt
                dlProductGrp.DataTextField = "prdDescription"
                dlProductGrp.DataValueField = "ProductGrp"
                dlProductGrp.DataBind()
            End Using
        End Using
    End Sub
    Sub ListNameProduct()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_Product where ProductID = '" & tbPrdID.Text & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            tbPrdName.Text = oSQLReader.Item("ProductName").ToString
            oCnct.Close()
            tbPrdID.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Id already exists, please use another Id!');document.getElementById('Buttonx').click()", True)
        Else

        End If
    End Sub

    Sub saveProduct()
        If tbHight Is Nothing Then
            tbHight.Text = "0"
        End If
        If tbLow Is Nothing Then
            tbLow.Text = "0"
        End If
        If tbNormal Is Nothing Then
            tbNormal.Text = "0"
        End If
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "INSERT INTO  CFG_Product( ProductID, ProductGrp, ProductName, NormalPrice, LowPrice, HightPrice) "
        sSQL = sSQL & "VALUES ('" & Replace(UCase(tbPrdID.Text), "'", "''") & "','" & dlProductGrp.SelectedValue & "','" & Replace(UCase(tbPrdName.Text), "'", "''") & "','" & Replace(tbNormal.Text, "'", "''") & "','" & Replace(tbLow.Text, "'", "''") & "','" & Replace(tbHight.Text, "'", "''") & "') "
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Data has been saved !');document.getElementById('Buttonx').click()", True)
        Response.Redirect("iPxCrmProduct.aspx")
    End Sub
    Sub updateProduct()
        If tbHight Is Nothing Then
            tbHight.Text = "0"
        End If
        If tbLow Is Nothing Then
            tbLow.Text = "0"
        End If
        If tbNormal Is Nothing Then
            tbNormal.Text = "0"
        End If
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "UPDATE CFG_Product SET ProductGrp = '" & dlProductGrp.SelectedValue & "', ProductName = '" & Replace(UCase(tbPrdName.Text), "'", "''") & "', NormalPrice = '" & Replace(tbNormal.Text, "'", "''") & "', LowPrice = '" & Replace(tbLow.Text, "'", "''") & "', HightPrice = '" & Replace(tbHight.Text, "'", "''") & "' "
        sSQL = sSQL & "WHERE  ProductID = '" & tbPrdID.Text & "'"

        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Data has been update !');document.getElementById('Buttonx').click()", True)
        Response.Redirect("iPxCrmProduct.aspx")
    End Sub
    Sub IdProduct()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT MAX(ProductID) as ProductID FROM CFG_Product where ProductGrp = '" & dlProductGrp.SelectedValue & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            tbPrdID.Text = Val(Mid(oSQLReader.Item("ProductID").ToString, 3, 2)) + 1
            If Len(tbPrdID.Text) = 1 Then
                tbPrdID.Text = dlProductGrp.SelectedValue & "0" & tbPrdID.Text & ""
            ElseIf Len(tbPrdID.Text) = 2 Then
                tbPrdID.Text = dlProductGrp.SelectedValue & tbPrdID.Text & ""
            End If
        Else
            tbPrdID.Text = dlProductGrp.SelectedValue & "01"
        End If
        oCnct.Close()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sPossition") <> "0" Then
            Session("sMessage") = "Sorry, Your not access !| ||"
            Session("sWarningID") = "0"
            Session("sUrlOKONLY") = "iPxCrmHome.aspx"
            Session("sUrlYES") = "http://www.thepyxis.net"
            Session("sUrlNO") = "http://www.thepyxis.net"
            Response.Redirect("warningmsg.aspx")
        Else
            If Not Me.IsPostBack Then
                productGrp()
                If Session("sP1") = "" Then
                    dlProductGrp.Enabled = True
                    'tbPrdID.Text = "AL"
                    IdProduct()
                    tbHight.Text = "0"
                    tbLow.Text = "0"
                    tbNormal.Text = "0"
                    tbPrdName.Text = ""
                Else
                    tbPrdID.Enabled = False
                    dlProductGrp.Enabled = False
                    tbPrdID.Text = Session("sP1")
                    dlProductGrp.SelectedValue = Session("sP2")
                    tbPrdName.Text = Session("sP3")
                    tbNormal.Text = Session("sP4")
                    tbLow.Text = Session("sP5")
                    tbHight.Text = Session("sP6")
                End If
            End If
        End If
    End Sub
    Protected Sub cari(ByVal sender As Object, ByVal e As EventArgs)
        Me.ListNameProduct()
    End Sub
    Protected Sub lbSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSave.Click
        If Len(tbPrdID.Text) = 2 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('failed to save, please complete Product ID !');document.getElementById('Buttonx').click()", True)
            tbPrdID.Focus()
        ElseIf tbPrdName.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('failed to save, please enter Product Name !');document.getElementById('Buttonx').click()", True)
            tbPrdName.Focus()
        Else
            If oCnct.State = ConnectionState.Closed Then
                oCnct.Open()
            End If
            oSQLCmd = New SqlCommand(sSQL, oCnct)
            sSQL = "SELECT ProductID FROM CFG_Product where ProductID = '" & Replace(tbPrdID.Text, "'", "''") & "'"
            oSQLCmd.CommandText = sSQL
            oSQLReader = oSQLCmd.ExecuteReader

            If oSQLReader.Read Then
                oSQLReader.Close()
                updateProduct()
            Else
                oSQLReader.Close()
                saveProduct()
            End If
            oCnct.Close()
        End If
    End Sub

    Protected Sub dlProductGrp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlProductGrp.SelectedIndexChanged
        'tbPrdID.Text = dlProductGrp.SelectedValue
        'tbPrdID.Focus()
        IdProduct()
    End Sub

    Protected Sub lbAbort_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAbort.Click
        Response.Redirect("iPxCrmProduct.aspx")
    End Sub
End Class
