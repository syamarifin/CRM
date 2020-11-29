
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Globalization
Imports System.Threading
Partial Class iPxCrmUser_queryCust
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
    Sub showdata_dropdownGrp()
        Dim cmd As New SqlCommand("select * from CFG_customerGrp where isActive='Y'")
        dlCustGrp.DataSource = ExecuteQuery(cmd, "SELECT")
        dlCustGrp.DataTextField = "GrpName"
        dlCustGrp.DataValueField = "CustGrpID"
        dlCustGrp.DataBind()
        dlCustGrp.Items.Insert(0, "")
    End Sub
    Sub showdata_dropdownLevel()
        Dim cmd As New SqlCommand("select * from CFG_CustomerLevel")
        dlCustLevel.DataSource = ExecuteQuery(cmd, "SELECT")
        dlCustLevel.DataTextField = "LevelDescription"
        dlCustLevel.DataValueField = "CustLevel"
        dlCustLevel.DataBind()
        dlCustLevel.Items.Insert(0, "")
    End Sub
    Dim people, Person As String
    Sub showdata_dropdownAnniv()
        dlAnniv.Items.Insert(0, "")
        dlAnniv.Items.Insert(1, "January")
        dlAnniv.Items.Insert(2, "February")
        dlAnniv.Items.Insert(3, "March")
        dlAnniv.Items.Insert(4, "April")
        dlAnniv.Items.Insert(5, "May")
        dlAnniv.Items.Insert(6, "June")
        dlAnniv.Items.Insert(7, "July")
        dlAnniv.Items.Insert(8, "August")
        dlAnniv.Items.Insert(9, "September")
        dlAnniv.Items.Insert(10, "October")
        dlAnniv.Items.Insert(11, "November")
        dlAnniv.Items.Insert(12, "December")
    End Sub
    Sub showdata_dropdownCustStatus()
        dlCustStatus.Items.Insert(0, "")
        dlCustStatus.Items.Insert(1, "All Customer")
        dlCustStatus.Items.Insert(2, "Non Customer")
        dlCustStatus.Items.Insert(3, "Customer")
        dlCustStatus.Items.Insert(4, "by..")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            showdata_dropdownGrp()
            showdata_dropdownAnniv()
            showdata_dropdownCustStatus()
            dlCustLevel.Visible = False
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "PopUser", "clearModal()", True)
        End If
    End Sub
    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Session("sCondition") = ""
        'iPxLoyalty Management
        If dlCustStatus.SelectedIndex = 0 Then
            Session("sCondition") = Session("sCondition") & " and a.CustLevel <> '8' "
        ElseIf dlCustStatus.SelectedIndex = 1 Then
            Session("sCondition") = Session("sCondition") & " and a.CustLevel >= '1' and a.CustLevel <= '8' "
        ElseIf dlCustStatus.SelectedIndex = 2 Then
            Session("sCondition") = Session("sCondition") & " and a.CustLevel = '8'"
        ElseIf dlCustStatus.SelectedIndex = 3 Then
            Session("sCondition") = Session("sCondition") & " and a.CustLevel <> '8'"
        ElseIf dlCustStatus.SelectedIndex = 4 Then
            If dlCustLevel.SelectedValue.Trim = "" Then
                Session("sCondition") = Session("sCondition") & " and a.CustLevel = '7'"
            ElseIf dlCustLevel.SelectedValue.Trim <> "" Then
                Session("sCondition") = Session("sCondition") & " and (a.CustLevel like '%" & dlCustLevel.SelectedValue.Trim & "%') "
            End If
        End If

        If dlCustGrp.SelectedValue.Trim <> "" Then
            Session("sCondition") = Session("sCondition") & " AND (a.CustGrpID like '%" & dlCustGrp.SelectedValue.Trim & "%') "
        End If

        If txtCustName.Text.Trim <> "" Then
            Session("sCondition") = Session("sCondition") & " AND (a.CustName like '%" & Replace(txtCustName.Text.Trim, "'", "''") & "%') "
        End If

        If dlAnniv.SelectedValue.Trim <> "" Then
            Session("sCondition") = Session("sCondition") & " AND (month(a.Anniversary)='" & dlAnniv.SelectedIndex & "') "
        End If

        If tbCountry.Text.Trim <> "" Then
            Session("sCondition") = Session("sCondition") & " AND ((SELECT country FROM CFG_geog_country WHERE countryid=a.CountryID) like '%" & Replace(tbCountry.Text.Trim, "'", "''") & "%') "
        End If

        If tbProvince.Text.Trim <> "" Then
            Session("sCondition") = Session("sCondition") & " AND ((SELECT description FROM CFG_geog_province WHERE countryid=a.CountryID and provid=a.Provid) like '%" & Replace(tbProvince.Text.Trim, "'", "''") & "%') "
        End If

        If tbCity.Text.Trim <> "" Then
            Session("sCondition") = Session("sCondition") & " AND ((SELECT CITY FROM CFG_geog_city WHERE CityID=A.CityID) like '%" & Replace(tbCity.Text.Trim, "'", "''") & "%') "
        End If


        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "PopUser", "hideModal()", True)
        'sSQL = Session("sCondition")
        Response.Redirect("iPxCrmCustomer.aspx?sCondition=" & sSQL)
    End Sub

    Protected Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click

        Session("sCondition") = ""
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "PopUser", "hideModal()", True)
        Response.Redirect("iPxCrmCustomer.aspx?sCondition=")
    End Sub

    Protected Sub dlCustStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlCustStatus.SelectedIndexChanged
        If dlCustStatus.SelectedIndex = 4 Then
            showdata_dropdownLevel()
            dlCustLevel.Visible = True
        Else
            dlCustLevel.Visible = False
        End If
    End Sub
End Class
