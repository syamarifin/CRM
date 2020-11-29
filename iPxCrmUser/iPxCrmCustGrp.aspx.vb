Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Imports System
Partial Class iPxCrmUser_iPxCrmCustGrp
    Inherits System.Web.UI.Page
    Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Dim oCnct As SqlConnection = New SqlConnection(sCnct)
    Dim oSQLCmd As SqlCommand
    Dim oSQLReader As SqlDataReader
    Dim sSQL As String

    Sub showdata_dropdownCustStatus()
        dlQueryStatus.Items.Insert(0, "")
        dlQueryStatus.Items.Insert(1, "All Customer Group")
        dlQueryStatus.Items.Insert(2, "Non Customer Group")
    End Sub
    Sub ListGrup()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_customerGrp where "

        If Session("sConditionGrp") <> "" Then
            sSQL = sSQL & Session("sConditionGrp")
        Else
            sSQL = sSQL & "IsActive='" & "Y" & "'"
        End If
        sSQL += " order by GrpName asc"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                If dt.Rows.Count <> 0 Then
                    gvGrup.DataSource = dt
                    gvGrup.DataBind()
                Else
                    dt.Rows.Add(dt.NewRow())
                    gvGrup.DataSource = dt
                    gvGrup.DataBind()
                    gvGrup.Rows(0).Visible = False
                End If
            End Using
        End Using
        oCnct.Close()
    End Sub
    Sub savegrup()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        Dim active As String
        If cbActive.Checked = True Then
            active = "Y"
        Else
            active = "N"
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "INSERT INTO CFG_customerGrp( GrpName,isActive) "
        sSQL = sSQL & "VALUES ('" & Replace(tbCustgrp.Text, "'", "''") & "','" & active & "') "
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        ListGrup()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Data has been saved !');document.getElementById('Buttonx').click()", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalFormSub", "hideModalFormSub()", True)
    End Sub
    Sub updategrup()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        Dim active As String
        If cbActive.Checked = True Then
            active = "Y"
        Else
            active = "N"
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "UPDATE CFG_customerGrp SET GrpName= '" & Replace(tbCustgrp.Text, "'", "''") & "', isActive='" & active & "'"
        sSQL = sSQL & "WHERE CustGrpID = '" & Session("sGid") & "' "

        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        ListGrup()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Data has been update !');document.getElementById('Buttonx').click()", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalFormSub", "hideModalFormSub()", True)
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
                ListGrup()
            End If
        End If
    End Sub
    Protected Sub OnPaging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvGrup.PageIndex = e.NewPageIndex
        Me.ListGrup()
    End Sub

    Protected Sub gvGrup_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvGrup.PageIndexChanging
        gvGrup.PageIndex = e.NewPageIndex
        ListGrup()
    End Sub

    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvGrup.PageIndex = e.NewPageIndex
        Me.ListGrup()
    End Sub

    Protected Sub lbNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbNew.Click
        tbCustgrp.Text = ""
        Session("sGid") = ""
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalFormSub", "showModalFormSub()", True)
    End Sub

    Protected Sub gvGrup_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvGrup.RowCommand
        If e.CommandName = "getEdit" Then
            Session("sGid") = e.CommandArgument.ToString
            Dim oSQLReader As SqlDataReader
            If oCnct.State = ConnectionState.Closed Then
                oCnct.Open()
            End If
            oSQLCmd = New SqlCommand(sSQL, oCnct)
            sSQL = "SELECT * FROM CFG_customerGrp WHERE CustGrpID = '" & Session("sGid") & "'"
            oSQLCmd.CommandText = sSQL
            oSQLReader = oSQLCmd.ExecuteReader

            oSQLReader.Read()
            'usercode, mobileno, password, signupdate, fullname, status, quid
            If oSQLReader.HasRows Then
                tbCustgrp.Text = oSQLReader.Item("GrpName").ToString
                If oSQLReader.Item("IsActive").ToString = "Y" Then
                    cbActive.Checked = True
                Else
                    cbActive.Checked = False
                End If
                oCnct.Close()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalFormSub", "showModalFormSub()", True)
            Else

            End If
        ElseIf e.CommandName = "getDelete" Then
            Session("sGid") = e.CommandArgument.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalDelete", "showModalDelete()", True)
        ElseIf e.CommandName = "getAddCust" Then
            Session("sGid") = e.CommandArgument.ToString
            Response.Redirect("iPxCrmInputCustomer.aspx")
        End If
    End Sub

    Protected Sub lbSaveGrup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSaveGrup.Click
        If tbCustgrp.Text = Nothing Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalFormSub", "hideModalFormSub()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('failed to save, please enter Customer Group Name !');document.getElementById('Buttonx').click()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalFormSub", "showModalFormSub()", True)
        Else
            If Session("sGid") <> "" Then
                updategrup()
            Else
                savegrup()
            End If
        End If
    End Sub

    Protected Sub lbAbort_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAbort.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalFormSub", "hideModalFormSub()", True)
    End Sub

    Protected Sub lbQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbQuery.Click
        Select Case dlQueryStatus.SelectedValue
            Case "01"
                Session("sConditionGrp") = "CFG_customerGrp.IsActive='" & "Y" & "'"
            Case "03"
                Session("sConditionGrp") = Session("sConditionGrp") & " (CFG_customerGrp.IsActive like '%" & "N" & "%') "
            Case "02"
                Session("sConditionGrp") = Session("sConditionGrp") & " (CFG_customerGrp.IsActive like '%" & "N" & "%') or (CFG_customerGrp.IsActive like '%" & "Y" & "%')"
        End Select
        If tbQueryGrp.Text.Trim <> "" Then
            Session("sConditionGrp") = Session("sConditionGrp") & " and (CFG_customerGrp.GrpName like '%" & Replace(tbQueryGrp.Text.Trim, "'", "''") & "%') "
        End If
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideFormMenu", "hideFormMenu()", True)
        ListGrup()
    End Sub

    Protected Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        Session("sConditionGrp") = ""
        tbQueryGrp.Text = Nothing
        dlQueryStatus.SelectedIndex = 0
        'showdata_dropdownCustStatus()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showFormMenu", "showFormMenu()", True)
    End Sub

    Protected Sub lbAbortQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAbortQuery.Click
        ListGrup()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideFormMenu", "hideFormMenu()", True)
    End Sub

    Protected Sub lbAbortDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAbortDelete.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalDelete", "hideModalDelete()", True)
    End Sub

    Protected Sub lbDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbDelete.Click
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "UPDATE CFG_customerGrp SET IsActive ='" & "N" & "' where CustGrpID='" & Session("sGid") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        Session("sGid") = ""
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalDelete", "hideModalDelete()", True)
        ListGrup()
    End Sub
End Class
