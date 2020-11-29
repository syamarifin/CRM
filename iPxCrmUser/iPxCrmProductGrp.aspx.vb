Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Imports System
Partial Class iPxCrmUser_iPxCrmProductGrp
    Inherits System.Web.UI.Page
    Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Dim oCnct As SqlConnection = New SqlConnection(sCnct)
    Dim oSQLCmd As SqlCommand
    Dim oSQLReader As SqlDataReader
    Dim sSQL, Ticketno, i As String
    Dim cIpx As iPxClass
    Sub kosong()
        tbPrdID.Enabled = True
        tbPrdID.Text = ""
        tbPrdName.Text = ""
    End Sub

    Sub ListProductGrp()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_productGrp "

        If Session("sCondition") <> "" Then
            sSQL = sSQL & Session("sCondition")
            Session("sCondition") = ""
        Else
            sSQL = sSQL & ""
        End If
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                gvProduct.DataSource = dt
                gvProduct.DataBind()
            End Using
        End Using
        oCnct.Close()
    End Sub
    Sub saveProduct()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "INSERT INTO CFG_productGrp (ProductGrp, PrdDescription) "
        sSQL = sSQL & "VALUES ('" & Replace(tbPrdID.Text, "'", "''") & "','" & Replace(tbPrdName.Text, "'", "''") & "') "
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Data has been saved !');document.getElementById('Buttonx').click()", True)
        ListProductGrp()
        kosong()
    End Sub
    Sub updateProduct()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "UPDATE CFG_productGrp SET PrdDescription = '" & Replace(tbPrdName.Text, "'", "''") & "'"
        sSQL = sSQL & "WHERE  ProductGrp = '" & tbPrdID.Text & "'"

        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Data has been update !');document.getElementById('Buttonx').click()", True)
        ListProductGrp()
        kosong()
    End Sub
    Sub deletedata()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "DELETE FROM CFG_productGrp where ProductGrp ='" & i & "'"
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Data has been delete !');document.getElementById('Buttonx').click()", True)
        ListProductGrp()
    End Sub
    Sub editProduct()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * From CFG_productGrp where ProductGrp = '" & i & "' "
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        'usercode, mobileno, password, signupdate, fullname, status, quid
        If oSQLReader.HasRows Then
            tbPrdID.Text = oSQLReader.Item("ProductGrp").ToString
            tbPrdName.Text = oSQLReader.Item("PrdDescription").ToString
            oCnct.Close()
            tbPrdID.Enabled = False
        Else
            'Refreshh()
        End If
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
                ListProductGrp()
            End If
        End If
    End Sub

    Protected Sub lbCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCancel.Click
        kosong()
        Response.Redirect("iPxCrmHome.aspx")
    End Sub

    Protected Sub lbSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSave.Click
        If tbPrdID.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('failed to save, please enter Product Group ID !');document.getElementById('Buttonx').click()", True)
            tbPrdID.Focus()
        ElseIf tbPrdName.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('failed to save, please enter Product Name Group !');document.getElementById('Buttonx').click()", True)
            tbPrdName.Focus()
        Else
            If oCnct.State = ConnectionState.Closed Then
                oCnct.Open()
            End If
            oSQLCmd = New SqlCommand(sSQL, oCnct)
            sSQL = "SELECT ProductGrp FROM CFG_productGrp where ProductGrp = '" & tbPrdID.Text & "'"
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

    Protected Sub gvProduct_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvProduct.RowCommand
        If e.CommandName = "getEdit" Then
            i = e.CommandArgument.ToString
            editProduct()
        ElseIf e.CommandName = "getHapus" Then
            i = e.CommandArgument.ToString
            deletedata()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Delete successful !');document.getElementById('Buttonx').click()", True)
        End If
    End Sub
End Class
