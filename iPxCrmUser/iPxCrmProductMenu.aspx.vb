Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Imports System
Partial Class iPxCrmUser_iPxCrmProductMenu
    Inherits System.Web.UI.Page
    Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Dim oCnct As SqlConnection = New SqlConnection(sCnct)
    Dim oSQLCmd As SqlCommand
    Dim oSQLReader As SqlDataReader
    Dim sSQL, Ticketno, i As String
    Dim cIpx As iPxClass
    Sub kosong()
        tbSubID.Text = ""
        tbSubmenu.Text = ""
    End Sub
    Sub ListMenu()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT CFG_productMenu.MenuID, CFG_productGrp.productGrp, CFG_productGrp.PrdDescription, CFG_Product.ProductName, CFG_productMenu.MenuName"
        sSQL += " FROM CFG_productMenu "
        sSQL += "INNER JOIN CFG_Product ON CFG_productMenu.ProductID = CFG_Product.ProductID "
        sSQL += "INNER JOIN CFG_productGrp ON CFG_productGrp.ProductGrp = CFG_Product.ProductGrp"

        If Session("sCondition") <> "" Then
            sSQL = sSQL & Session("sCondition")
            Session("sCondition") = ""
        Else
            sSQL = sSQL & ""
        End If
        sSQL += " order by CFG_productMenu.MenuID asc"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                gvMenu.DataSource = dt
                gvMenu.DataBind()
            End Using
        End Using
        oCnct.Close()
    End Sub
    Sub deletedata()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "DELETE FROM CFG_productMenu where MenuID ='" & Session("sQd") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Data has been delete !');document.getElementById('Buttonx').click()", True)
        ListMenu()
    End Sub
    Sub editProduct()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT CFG_productMenu.MenuID, CFG_productGrp.productGrp, CFG_Product.ProductID, CFG_productMenu.MenuName"
        sSQL += " FROM CFG_productMenu "
        sSQL += "INNER JOIN CFG_Product ON CFG_productMenu.ProductID = CFG_Product.ProductID "
        sSQL += "INNER JOIN CFG_productGrp ON CFG_productGrp.ProductGrp = CFG_Product.ProductGrp"
        sSQL += " where CFG_productMenu.MenuID = '" & i & "' "
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        'usercode, mobileno, password, signupdate, fullname, status, quid
        If oSQLReader.HasRows Then
            Session("sM1") = oSQLReader.Item("MenuID").ToString
            Session("sM2") = oSQLReader.Item("ProductID").ToString
            Session("sM3") = oSQLReader.Item("MenuName").ToString
            Session("sM4") = oSQLReader.Item("productGrp").ToString
            oCnct.Close()
            Response.Redirect("iPxCrmInputProductmenu.aspx")
        Else
            'Refreshh()
        End If
    End Sub
    Sub ListSubmenu()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT CFG_productSubmenu.SubmenuID, CFG_productSubmenu.SubmenuName, CFG_productGrp.productGrp, CFG_productGrp.PrdDescription, CFG_Product.ProductName, CFG_productMenu.MenuName"
        sSQL += " FROM CFG_productSubmenu "
        sSQL += "INNER JOIN CFG_Product ON CFG_productSubmenu.ProductID = CFG_Product.ProductID "
        sSQL += "INNER JOIN CFG_ProductMenu ON CFG_productSubmenu.MenuID = CFG_ProductMenu.MenuID "
        sSQL += "INNER JOIN CFG_productGrp ON CFG_productGrp.ProductGrp = CFG_Product.ProductGrp "
        sSQL += "WHERE CFG_productSubmenu.MenuID ='" & Session("sSi") & "'"

        'If Session("sCondition") <> "" Then
        '    sSQL = sSQL & Session("sCondition")
        '    Session("sCondition") = ""
        'Else
        '    sSQL = sSQL & ""
        'End If
        sSQL += " order by CFG_productSubmenu.SubmenuName asc"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                gvSubmenu.DataSource = dt
                gvSubmenu.DataBind()
            End Using
        End Using
        oCnct.Close()
    End Sub
    Sub editSubMenu()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT CFG_productSubmenu.SubmenuID, CFG_productSubmenu.SubmenuName, CFG_productGrp.productGrp, CFG_productGrp.PrdDescription, CFG_Product.ProductName, CFG_productMenu.MenuName"
        sSQL += " FROM CFG_productSubmenu "
        sSQL += "INNER JOIN CFG_Product ON CFG_productSubmenu.ProductID = CFG_Product.ProductID "
        sSQL += "INNER JOIN CFG_ProductMenu ON CFG_productSubmenu.MenuID = CFG_ProductMenu.MenuID "
        sSQL += "INNER JOIN CFG_productGrp ON CFG_productGrp.ProductGrp = CFG_Product.ProductGrp "
        sSQL += "WHERE CFG_productSubmenu.SubmenuID ='" & i & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        'usercode, mobileno, password, signupdate, fullname, status, quid
        If oSQLReader.HasRows Then
            tbSubID.Text = oSQLReader.Item("SubmenuID").ToString
            tbSubmenu.Text = oSQLReader.Item("SubmenuName").ToString
            tbDetailProduct.Text = oSQLReader.Item("ProductName").ToString
            tbDetailMenu.Text = oSQLReader.Item("MenuName").ToString
            oCnct.Close()
        Else
            'Refreshh()
        End If
    End Sub
    Sub detailproduct()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT CFG_productMenu.MenuID, CFG_productGrp.productGrp, CFG_Product.ProductName, CFG_Product.ProductID, CFG_productMenu.MenuName "
        sSQL += "FROM CFG_productMenu "
        sSQL += "INNER JOIN CFG_Product ON CFG_productMenu.ProductID = CFG_Product.ProductID "
        sSQL += "INNER JOIN CFG_productGrp ON CFG_productGrp.ProductGrp = CFG_Product.ProductGrp "
        sSQL += "where CFG_productMenu.MenuID = '" & Session("sSi") & "' "
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        'usercode, mobileno, password, signupdate, fullname, status, quid
        If oSQLReader.HasRows Then
            Session("sM1") = oSQLReader.Item("MenuID").ToString
            Session("sM2") = oSQLReader.Item("ProductName").ToString
            Session("sM3") = oSQLReader.Item("MenuName").ToString
            Session("sM4") = oSQLReader.Item("ProductID").ToString
            oCnct.Close()
        Else
            'Refreshh()
        End If
    End Sub
    Sub saveSubMenu()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "INSERT INTO  CFG_productSubMenu( ProductID, MenuID, SubmenuID, SubmenuName) "
        sSQL = sSQL & "VALUES ('" & Session("sM4") & "','" & Session("sM1") & "','" & tbSubID.Text & "','" & tbSubmenu.Text & "') "
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Data has been saved !');document.getElementById('Buttonx').click()", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalFormSub", "hideModalFormSub()", True)
        kosong()
        ListSubmenu()
        detailproduct()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalSub", "showModalSub()", True)
    End Sub
    Sub updateSubMenu()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "UPDATE CFG_productSubMenu SET SubmenuName= '" & tbSubmenu.Text & "'"
        sSQL = sSQL & "WHERE SubmenuID = '" & tbSubID.Text & "'"

        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Data has been update !');document.getElementById('Buttonx').click()", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalFormSub", "hideModalFormSub()", True)
        kosong()
        ListSubmenu()
        detailproduct()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalSub", "showModalSub()", True)
    End Sub
    Sub deletedataSub()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "DELETE FROM CFG_productSubMenu where SubmenuID='" & Session("sQd") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

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
                'Session("sCondition") = ""
                ListMenu()
            End If
        End If
    End Sub
    Protected Sub OnPaging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvMenu.PageIndex = e.NewPageIndex
        Me.ListMenu()
    End Sub

    Protected Sub gvTicket_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvMenu.PageIndexChanging
        gvMenu.PageIndex = e.NewPageIndex
        ListMenu()
    End Sub

    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvMenu.PageIndex = e.NewPageIndex
        Me.ListMenu()
    End Sub
    Protected Sub lbAddmenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAddmenu.Click
        Session("sM1") = ""
        Response.Redirect("iPxCrmInputProductmenu.aspx")
    End Sub

    Protected Sub gvMenu_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMenu.RowCommand
        If e.CommandName = "getEdit" Then
            i = e.CommandArgument.ToString
            editProduct()
        ElseIf e.CommandName = "getHapus" Then
            Session("sQd") = e.CommandArgument.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalDeleteMenu", "showModalDeleteMenu()", True)
        ElseIf e.CommandName = "getSub" Then
            Session("sSi") = e.CommandArgument.ToString
            ListSubmenu()
            detailproduct()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalSub", "showModalSub()", True)
        End If
    End Sub

    Protected Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        Response.Redirect("queryProductMenu.aspx")
    End Sub

    Protected Sub lbInputSub_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbInputSub.Click
        tbSubID.Text = cIpx.GetCounterSM("SM", "SM")
        tbSubID.Enabled = False
        tbDetailProduct.Text = Session("sM2")
        tbDetailMenu.Text = Session("sM3")
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalSub", "hideModalSub()", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalFormSub", "showModalFormSub()", True)
    End Sub

    Protected Sub lbSaveSub_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSaveSub.Click
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT SubmenuID FROM CFG_productSubMenu WHERE SubmenuID = '" & tbSubID.Text & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        If oSQLReader.Read Then
            oSQLReader.Close()
            updateSubMenu()
        Else
            oSQLReader.Close()
            saveSubMenu()
        End If
        oCnct.Close()
    End Sub

    Protected Sub gvSubmenu_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvSubmenu.RowCommand
        If e.CommandName = "getEdit" Then
            i = e.CommandArgument.ToString
            tbSubID.Enabled = False
            editSubMenu()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalSub", "hideModalSub()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalFormSub", "showModalFormSub()", True)
        ElseIf e.CommandName = "getHapus" Then
            Session("sQd") = e.CommandArgument.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalSub", "hideModalSub()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalDelete", "showModalDelete()", True)
        End If
    End Sub

    Protected Sub lbCancelSub_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCancelSub.Click
        ListSubmenu()
        detailproduct()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalDelete", "hideModalDelete()", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalSub", "showModalSub()", True)
    End Sub

    'Protected Sub lbDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbDelete.Click
    '    deletedataSub()
    '    ListSubmenu()
    '    detailproduct()
    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalDelete", "hideModalDelete()", True)
    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalSub", "showModalSub()", True)
    'End Sub

    Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton2.Click
        deletedata()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalDeleteMenu", "hideModalDeleteMenu()", True)
    End Sub

    Protected Sub LinkButton3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton3.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalDeleteMenu", "hideModalDeleteMenu()", True)
    End Sub

    Protected Sub lbAbort_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAbort.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalFormSub", "hideModalFormSub()", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalSub", "showModalSub()", True)
    End Sub
End Class
