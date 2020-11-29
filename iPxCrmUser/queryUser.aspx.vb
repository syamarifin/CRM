
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Globalization
Imports System.Threading
Partial Class iPxCrmUser_queryUser
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
    Sub showdata_dropdownStatus()
        dlStatusActive.Items.Insert(0, "")
        dlStatusActive.Items.Insert(1, "ALL")
        dlStatusActive.Items.Insert(2, "NOT ACTIVE")
    End Sub
    Sub showdata_dropdownDept()
        Dim cmd As New SqlCommand("select * from CFG_dept")
        dlDepartemen.DataSource = ExecuteQuery(cmd, "SELECT")
        dlDepartemen.DataTextField = "DeptName"
        dlDepartemen.DataValueField = "DeptCode"
        dlDepartemen.DataBind()
        dlDepartemen.Items.Insert(0, "")
    End Sub
    Sub showdata_dropdownPostion()
        Dim cmd As New SqlCommand("select * from CFG_possition")
        dlPosition.DataSource = ExecuteQuery(cmd, "SELECT")
        dlPosition.DataTextField = "Possition"
        dlPosition.DataValueField = "PossitionCode"
        dlPosition.DataBind()
        dlPosition.Items.Insert(0, "")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            showdata_dropdownDept()
            showdata_dropdownPostion()
            showdata_dropdownStatus()
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "PopUser", "clearModal()", True)
        End If
    End Sub


    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        'iPxLoyalty Management

        If dlStatusActive.SelectedIndex = 2 Then
            Session("sCondition") = Session("sCondition") & " (CFG_user.IsActive = '" & "N" & "') "
        ElseIf dlStatusActive.SelectedIndex = 1 Then
            Session("sCondition") = Session("sCondition") & " (CFG_user.IsActive <> '" & "N" & "') or (CFG_user.IsActive = '" & "N" & "') "
        Else
            Session("sCondition") = Session("sCondition") & " (CFG_user.IsActive <> '" & "N" & "') "
        End If

        If txtName.Text.Trim <> "" Then
            Session("sCondition") = Session("sCondition") & " AND (CFG_user.name like '%" & Replace(txtName.Text.Trim, "'", "''") & "%') "
        End If

        If tbEmail.Text.Trim <> "" Then
            Session("sCondition") = Session("sCondition") & " AND (CFG_user.Email like '%" & Replace(tbEmail.Text.Trim, "'", "''") & "%') "
        End If

        If dlDepartemen.SelectedValue.Trim <> "" Then
            Session("sCondition") = Session("sCondition") & " AND (CFG_dept.DeptCode like '%" & dlDepartemen.SelectedValue.Trim & "%') "
        End If

        If dlPosition.SelectedValue.Trim <> "" Then
            Session("sCondition") = Session("sCondition") & " AND (CFG_possition.PossitionCode like '%" & dlPosition.SelectedValue.Trim & "%') "
        End If


        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "PopUser", "hideModal()", True)
        'sSQL = Session("sCondition")
        Response.Redirect("iPxCrmhomeuser.aspx?sCondition=" & sSQL)
    End Sub

    Protected Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click

        Session("sCondition") = ""
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "PopUser", "hideModal()", True)
        Response.Redirect("iPxCrmhomeuser.aspx?sCondition=")
    End Sub

    'Protected Sub btnCal1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCal1.Click
    '    Calendar1.Visible = True
    'End Sub

    'Protected Sub btncal2_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btncal2.Click
    '    Calendar2.Visible = True
    'End Sub

    'Protected Sub btncal3_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btncal3.Click
    '    Calendar3.Visible = True
    'End Sub

    'Protected Sub Calendar1_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Calendar1.SelectionChanged
    '    txtTransDate.Text = Format(Calendar1.SelectedDate, "yyy-MM-dd")
    '    Calendar1.Visible = False
    'End Sub

    'Protected Sub Calendar2_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Calendar2.SelectionChanged
    '    txtPerFrom.Text = Format(Calendar2.SelectedDate, "yyy-MM-dd")
    '    Calendar2.Visible = False
    'End Sub

    'Protected Sub Calendar3_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Calendar3.SelectionChanged
    '    txtPerUntl.Text = Format(Calendar3.SelectedDate, "yyy-MM-dd")
    '    Calendar3.Visible = False
    'End Sub

End Class
