Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Partial Class iPxCrmMobileUser_iPxCrmProffile
    Inherits System.Web.UI.Page
    Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Dim oCnct As SqlConnection = New SqlConnection(sCnct)
    Dim oSQLCmd As SqlCommand
    Dim oSQLReader As SqlDataReader
    Dim sSQL, Ticketno, i As String
    Dim cIpx As iPxClass
    Sub cancel()
        btnchange.Visible = True
        pnlOldPW.Visible = False
        txtnewpass.Visible = False
        txtnewpassconf.Visible = False
        btnsavepass.Visible = False
        btnCancel.Visible = False
    End Sub
    Sub dept()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_dept"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                dlDept.DataSource = dt
                dlDept.DataTextField = "DeptName"
                dlDept.DataValueField = "DeptCode"
                dlDept.DataBind()
            End Using
        End Using
    End Sub
    Sub position()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_possition"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                dlPosition.DataSource = dt
                dlPosition.DataTextField = "Possition"
                dlPosition.DataValueField = "PossitionCode"
                dlPosition.DataBind()
            End Using
        End Using
    End Sub
    Sub viewUser()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If

        sSQL = "Select * From CFG_user Where recID = '" & Session("sId") & "'"
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        oSQLReader = oSQLCmd.ExecuteReader

        If oSQLReader.Read Then
            tbName.Text = oSQLReader.Item("name")
            dlPosition.SelectedValue = oSQLReader.Item("PossitionCode")
            dlDept.SelectedValue = oSQLReader.Item("dept")
            tbEmail.Text = oSQLReader.Item("Email")
            oSQLReader.Close()
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Sorry Your Password Is Wrong !');document.getElementById('Buttonx').click()", True)
        End If
        oCnct.Close()
    End Sub
    Protected Sub btnchange_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnchange.Click
        pnlOldPW.Visible = True
        btnCancel.Visible = True
        btnchange.Visible = False
        txtoldpass.Text = Nothing
        txtoldpass.Focus()
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        cancel()
    End Sub

    Protected Sub btnCheckOldPw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCheckOldPw.Click
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If

        sSQL = "Select * From CFG_user Where recID = '" & Session("sId") & "' and passw = '" & Replace(txtoldpass.Text, "'", "''") & "' and isActive = '" & "Y" & "'"
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        oSQLReader = oSQLCmd.ExecuteReader

        If oSQLReader.Read Then
            btnCancel.Visible = True
            btnchange.Visible = False
            pnlOldPW.Visible = False
            txtnewpass.Visible = True
            txtnewpassconf.Visible = True
            btnsavepass.Visible = True
        Else
            txtoldpass.Text = ""
            txtoldpass.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('Wrong Password');", True)
        End If

        oCnct.Close()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sId") = Nothing Then
            Response.Redirect("SignIn.aspx")
        Else
            Dim cph As ContentPlaceHolder = DirectCast(Me.Master.FindControl("ContentPlaceHolder2"), ContentPlaceHolder)
            Dim LinkButton As LinkButton = DirectCast(cph.FindControl("lnkback"), LinkButton)
            Session("sIdTiket") = ""
            LinkButton.PostBackUrl = "iPxCrmHome.aspx"
            If Not Me.IsPostBack Then
                dept()
                position()
                viewUser()
            End If
        End If
    End Sub

    Protected Sub btnsavepass_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsavepass.Click
        If txtnewpass.Text = txtnewpassconf.Text Then
            If oCnct.State = ConnectionState.Closed Then
                oCnct.Open()
            End If

            oSQLCmd = New SqlCommand(sSQL, oCnct)
            sSQL = "UPDATE  CFG_user SET  "
            sSQL = sSQL & " passw='" & Replace(txtnewpassconf.Text, "'", "''") & "' WHERE recID = '" & Session("sId") & "' and Email='" & Session("sEmail") & "' "
            oSQLCmd.CommandText = sSQL
            oSQLCmd.ExecuteNonQuery()

            oCnct.Close()
            cancel()
            Response.Redirect("../login.aspx")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('Change password successful!');", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('Password is not match!');", True)


        End If
    End Sub

    Protected Sub lbAbort_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAbort.Click
        Response.Redirect("iPxCrmHome.aspx")
    End Sub

    Protected Sub lbSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSave.Click
        If tbName.Text = Nothing Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('failed to save, please complete your name !');document.getElementById('Buttonx').click()", True)
            tbName.Focus()
        ElseIf tbEmail.Text = Nothing Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('failed to save, please complete your e-mail !');document.getElementById('Buttonx').click()", True)
            tbEmail.Focus()
        Else
            If oCnct.State = ConnectionState.Closed Then
                oCnct.Open()
            End If

            oSQLCmd = New SqlCommand(sSQL, oCnct)
            sSQL = "UPDATE  CFG_user SET  "
            sSQL += "Email ='" & tbEmail.Text & "' ,name='" & tbName.Text & "',dept='" & dlDept.SelectedValue & "',PossitionCode='" & dlPosition.SelectedValue & "'"
            sSQL += " WHERE recID = '" & Session("sId") & "' "
            oSQLCmd.CommandText = sSQL
            oSQLCmd.ExecuteNonQuery()

            oCnct.Close()
            cancel()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Change proffile successful!');", True)
        End If
    End Sub
End Class
