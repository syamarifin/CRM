﻿Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Globalization
Imports System.Threading
Partial Class iPxCrmUser_queryTicketUser
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

    'Sub showdata_dropdownProduct()
    '    Dim cmd As New SqlCommand("select * from CFG_Product")
    '    dlProduct.DataSource = ExecuteQuery(cmd, "SELECT")
    '    dlProduct.DataTextField = "ProductName"
    '    dlProduct.DataValueField = "ProductID"
    '    dlProduct.DataBind()
    '    dlProduct.Items.Insert(0, "")
    'End Sub
    'Sub showdata_dropdownMenu()
    '    Dim cmd As New SqlCommand("select * from CFG_ProductMenu")
    '    dlMenu.DataSource = ExecuteQuery(cmd, "SELECT")
    '    dlMenu.DataTextField = "MenuName"
    '    dlMenu.DataValueField = "MenuID"
    '    dlMenu.DataBind()
    '    dlMenu.Items.Insert(0, "")
    'End Sub
    'Sub showdata_dropdownSubMenu()
    '    Dim cmd As New SqlCommand("select * from CFG_ProductSubMenu")
    '    dlSubMenu.DataSource = ExecuteQuery(cmd, "SELECT")
    '    dlSubMenu.DataTextField = "SubmenuName"
    '    dlSubMenu.DataValueField = "SubmenuID"
    '    dlSubMenu.DataBind()
    '    dlSubMenu.Items.Insert(0, "")
    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            'showdata_dropdownProduct()
            'showdata_dropdownMenu()
            'showdata_dropdownSubMenu()
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "PopUser", "clearModal()", True)
        End If
    End Sub


    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        'iPxLoyalty Management

        If txtQueryGuestName.Text.Trim <> "" Then
            Session("sCondition") = Session("sCondition") & " AND (CFG_customer.CustName like '%" & Replace(txtQueryGuestName.Text.Trim, "'", "''") & "%') "
        End If

        If txtmemID.Text.Trim <> "" Then
            Session("sCondition") = Session("sCondition") & " AND (SPR_eticket.TicketNo like '%" & Replace(txtmemID.Text.Trim, "'", "''") & "%') "
        End If

        If txtFrom.Text.Trim <> "" Then
            Session("sCondition") = Session("sCondition") & " AND (SPR_eticket.SubmitFrom like '%" & Replace(txtFrom.Text.Trim, "'", "''") & "%') "
        End If

        If txtSubject.Text.Trim <> "" Then
            Session("sCondition") = Session("sCondition") & " AND (SPR_eticket.Subject like '%" & Replace(txtSubject.Text.Trim, "'", "''") & "%') "
        End If

        If txtTransDate.Text.Trim <> "" Then
            Dim transDate As Date = Date.ParseExact(txtTransDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
            Session("sCondition") = Session("sCondition") & " AND (SPR_eticket.TDate between '" & transDate & " 00:00:00' and '" & transDate & " 23:59:00') "
        End If

        If dlProduct.Text.Trim <> "" Then
            Session("sCondition") = Session("sCondition") & " AND (CFG_Product.ProductName like '%" & Replace(dlProduct.Text.Trim, "'", "''") & "%') "
        End If

        If dlMenu.Text.Trim <> "" Then
            Session("sCondition") = Session("sCondition") & " AND (CFG_productMenu.MenuName like '%" & Replace(dlMenu.Text.Trim, "'", "''") & "%') "
        End If

        If dlSubMenu.Text.Trim <> "" Then
            Session("sCondition") = Session("sCondition") & " AND (CFG_productSubMenu.SubmenuName like '%" & Replace(dlSubMenu.Text.Trim, "'", "''") & "%') "
        End If

        If txtPerUntl.Text.Trim <> "" Then
            Dim PerUntl As Date = Date.ParseExact(txtPerUntl.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
            Session("sCondition") = Session("sCondition") & " AND (SPR_eticket.TDate <= '" & PerUntl & " 00:00:00') "
        End If

        If txtPerFrom.Text.Trim <> "" Then
            Dim PerFrom As Date = Date.ParseExact(txtPerFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
            Session("sCondition") = Session("sCondition") & " AND (SPR_eticket.TDate >= '" & PerFrom & " 23:59:00') "
        End If


        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "PopUser", "hideModal()", True)
        'sSQL = Session("sCondition")
        Response.Redirect("iPxCrmNewtiket.aspx?sCondition=" & sSQL)
    End Sub

    Protected Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Session("sCondition") = ""
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "PopUser", "hideModal()", True)
        Response.Redirect("iPxCrmNewtiket.aspx?sCondition=")
    End Sub

    'Protected Sub dlProduct_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlProduct.SelectedIndexChanged
    '    Dim cmd As New SqlCommand("select * from CFG_ProductMenu where ProductID = '" & dlProduct.SelectedValue & "'")
    '    dlMenu.DataSource = ExecuteQuery(cmd, "SELECT")
    '    dlMenu.DataTextField = "MenuName"
    '    dlMenu.DataValueField = "MenuID"
    '    dlMenu.DataBind()
    '    dlMenu.Items.Insert(0, "")
    'End Sub

    'Protected Sub dlMenu_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlMenu.SelectedIndexChanged
    '    Dim cmd As New SqlCommand("select * from CFG_ProductSubMenu where MenuID ='" & dlMenu.SelectedValue & "'")
    '    dlSubMenu.DataSource = ExecuteQuery(cmd, "SELECT")
    '    dlSubMenu.DataTextField = "SubmenuName"
    '    dlSubMenu.DataValueField = "SubmenuID"
    '    dlSubMenu.DataBind()
    '    dlSubMenu.Items.Insert(0, "")
    'End Sub
End Class
