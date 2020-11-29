Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Imports System

Partial Class iPxCrmUser_iPxCrmProduct
    Inherits System.Web.UI.Page
    Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Dim oCnct As SqlConnection = New SqlConnection(sCnct)
    Dim oSQLCmd As SqlCommand
    Dim oSQLReader As SqlDataReader
    Dim sSQL, Ticketno, i As String
    Dim cIpx As iPxClass
    Sub ListProduct()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT CFG_Product.ProductID, CFG_productGrp.PrdDescription, CFG_Product.ProductName, CFG_Product.NormalPrice, CFG_Product.LowPrice, CFG_Product.HightPrice"
        sSQL += " FROM CFG_Product "
        sSQL += "INNER JOIN CFG_productGrp ON CFG_productGrp.ProductGrp = CFG_Product.ProductGrp"
        If Session("sPossition") = "0" And Session("sProductCode") = "1" Then
            sSQL += " "
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "2" Then
            sSQL += " where CFG_productGrp.ProductGrp = 'Al'"
        ElseIf Session("sPossition") = "0" And Session("sProductCode") = "3" Then
            sSQL += " where CFG_productGrp.ProductGrp <> 'Al'"
        ElseIf Session("sPossition") <> "0" Then
            sSQL += " "
        End If

        If Session("sQueryTicket") = "" Then
            Session("sQueryTicket") = Session("sCondition")
        Else

        End If
        'If Session("sCondition") <> "" Then
        sSQL = sSQL & Session("sQueryTicket")
        Session("sCondition") = ""
        'Else
        '    sSQL = sSQL & ""
        'End If
        sSQL += " order by CFG_productGrp.PrdDescription asc, CFG_Product.ProductID asc "
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                If dt.Rows.Count <> 0 Then
                    gvProduct.DataSource = dt
                    gvProduct.DataBind()
                Else
                    dt.Rows.Add(dt.NewRow())
                    gvProduct.DataSource = dt
                    gvProduct.DataBind()
                    gvProduct.Rows(0).Visible = False
                End If
            End Using
        End Using
        oCnct.Close()
    End Sub
    Sub deletedata()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "DELETE FROM CFG_Product where ProductID ='" & Session("sQPd") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Data has been delete !');document.getElementById('Buttonx').click()", True)
        ListProduct()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalDeleteProduct", "hideModalDeleteProduct()", True)
    End Sub
    Sub editProduct()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * From CFG_Product where ProductID = '" & i & "' "
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            Session("sP1") = oSQLReader.Item("ProductID").ToString
            Session("sP2") = oSQLReader.Item("ProductGrp").ToString
            Session("sP3") = oSQLReader.Item("ProductName").ToString
            Session("sP4") = String.Format("{0:N2}", (oSQLReader.Item("NormalPrice"))).ToString
            Session("sP5") = String.Format("{0:N2}", (oSQLReader.Item("LowPrice"))).ToString
            Session("sP6") = String.Format("{0:N2}", (oSQLReader.Item("HightPrice"))).ToString
            oCnct.Close()
            Response.Redirect("iPxCrmInputProduct.aspx")
        Else

        End If
    End Sub
    'Menu Product=========================================================================================
    Sub ListMenu()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT CFG_productMenu.MenuID, CFG_productGrp.productGrp, CFG_productGrp.PrdDescription, CFG_Product.ProductName, CFG_productMenu.MenuName"
        sSQL += " FROM CFG_productMenu "
        sSQL += "INNER JOIN CFG_Product ON CFG_productMenu.ProductID = CFG_Product.ProductID "
        sSQL += "INNER JOIN CFG_productGrp ON CFG_productGrp.ProductGrp = CFG_Product.ProductGrp "
        sSQL += "where CFG_productMenu.ProductID = '" & Session("sGetMenu") & "' "

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
                If dt.Rows.Count <> 0 Then
                    gvMenu.DataSource = dt
                    gvMenu.DataBind()
                Else
                    dt.Rows.Add(dt.NewRow())
                    gvMenu.DataSource = dt
                    gvMenu.DataBind()
                    gvMenu.Rows(0).Visible = False
                End If
            End Using
        End Using
        oCnct.Close()
    End Sub
    Sub IdMenu()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT MAX(MenuID) as MenuID FROM CFG_ProductMenu where ProductID = '" & Session("sGetMenu") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            tbMenuID.Text = Val(Mid(oSQLReader.Item("MenuID").ToString, 6, 2)) + 1
            If Len(tbMenuID.Text) = 1 Then
                tbMenuID.Text = Session("sGetMenu") & "-0" & tbMenuID.Text & ""
            ElseIf Len(tbMenuID.Text) = 2 Then
                tbMenuID.Text = Session("sGetMenu") & "-" & tbMenuID.Text & ""
            End If
        Else
            tbMenuID.Text = Session("sGetMenu") & "-01"
        End If
        oCnct.Close()
    End Sub
    Sub namaProduct()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT CFG_productGrp.PrdDescription, CFG_Product.* From CFG_Product"
        sSQL += " INNER JOIN CFG_productGrp ON CFG_productGrp.ProductGrp=CFG_Product.ProductGrp where ProductID = '" & Session("sGetMenu") & "' "
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            Session("sNamaProduct") = oSQLReader.Item("ProductName").ToString
            Session("sPrdGroup") = oSQLReader.Item("PrdDescription").ToString
            oCnct.Close()
        Else

        End If
    End Sub
    Sub namaMenu()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * From CFG_ProductMenu where MenuID = '" & Session("sSi") & "' "
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            Session("sNamaMenu") = oSQLReader.Item("MenuName").ToString
            oCnct.Close()
        Else

        End If
    End Sub
    Sub deletedataMenu()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "DELETE FROM CFG_productMenu where MenuID ='" & Session("sQd") & "' and ProductID='" & Session("sGetMenu") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Data has been delete !');document.getElementById('Buttonx').click()", True)
        ListMenu()
    End Sub
    Sub editProductMenu()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT CFG_productMenu.MenuID, CFG_productGrp.PrdDescription, CFG_Product.ProductName, CFG_productMenu.MenuName"
        sSQL += " FROM CFG_productMenu "
        sSQL += "INNER JOIN CFG_Product ON CFG_productMenu.ProductID = CFG_Product.ProductID "
        sSQL += "INNER JOIN CFG_productGrp ON CFG_productGrp.ProductGrp = CFG_Product.ProductGrp "
        sSQL += " where CFG_productMenu.MenuID = '" & i & "' and CFG_productMenu.ProductID='" & Session("sGetMenu") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            tbGrpMenu.Text = oSQLReader.Item("PrdDescription").ToString
            tbMenuID.Text = oSQLReader.Item("MenuID").ToString
            tbDetailProduct.Text = oSQLReader.Item("ProductName").ToString
            tbMenuName.Text = oSQLReader.Item("MenuName").ToString
            oCnct.Close()
            tbMenuID.Enabled = False
        Else
            'Refreshh()
        End If
    End Sub
    Sub saveMenu()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "INSERT INTO  CFG_productMenu(ProductID, MenuID, MenuName) "
        sSQL = sSQL & "VALUES ('" & Session("sGetMenu") & "','" & Replace(UCase(tbMenuID.Text), "'", "''") & "','" & Replace(UCase(tbMenuName.Text), "'", "''") & "') "
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
    End Sub
    Sub updateMenu()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "UPDATE CFG_productMenu SET MenuName = '" & Replace(UCase(tbMenuName.Text), "'", "''") & "' "
        sSQL = sSQL & "WHERE  MenuID = '" & tbMenuId.Text & "'"

        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Data has been update !');document.getElementById('Buttonx').click()", True)
    End Sub
    'End Menu Product=====================================================================================
    'Sub Menu Product=====================================================================================
    Sub ListSubmenu()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT CFG_productSubmenu.SubmenuID, CFG_productSubmenu.SubmenuName, CFG_productGrp.productGrp, CFG_productGrp.PrdDescription, CFG_Product.ProductName, CFG_productMenu.MenuName"
        sSQL += " FROM CFG_productSubmenu "
        sSQL += "INNER JOIN CFG_Product ON CFG_productSubmenu.ProductID = CFG_Product.ProductID "
        sSQL += "INNER JOIN CFG_productMenu ON CFG_productSubMenu.ProductID = CFG_productMenu.ProductID AND CFG_productSubMenu.MenuID = CFG_productMenu.MenuID "
        sSQL += "INNER JOIN CFG_productGrp ON CFG_productGrp.ProductGrp = CFG_Product.ProductGrp "
        sSQL += "WHERE CFG_productSubmenu.MenuID ='" & Session("sSi") & "'and CFG_productSubmenu.ProductID='" & Session("sGetMenu") & "'"

        sSQL += " order by CFG_productSubmenu.SubmenuName asc"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                If dt.Rows.Count <> 0 Then
                    gvSubmenu.DataSource = dt
                    gvSubmenu.DataBind()
                Else
                    dt.Rows.Add(dt.NewRow())
                    gvSubmenu.DataSource = dt
                    gvSubmenu.DataBind()
                    gvSubmenu.Rows(0).Visible = False
                End If
            End Using
        End Using
        oCnct.Close()
    End Sub
    Sub namaSubmenu()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * From CFG_ProductSubMenu where SubmenuID = '" & Session("sQSOP") & "' "
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            Session("sNamaSubMenu") = oSQLReader.Item("SubmenuName").ToString
            oCnct.Close()
        Else

        End If
    End Sub
    Sub IdSubmenu()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT MAX(SubmenuID) as SubmenuID FROM CFG_ProductSubMenu where MenuID = '" & Session("sSi") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            tbSubID.Text = Val(Mid(oSQLReader.Item("SubmenuID").ToString, 9, 2)) + 1
            If Len(tbSubID.Text) = 1 Then
                tbSubID.Text = Session("sSi") & "-0" & tbSubID.Text & ""
            ElseIf Len(tbSubID.Text) = 2 Then
                tbSubID.Text = Session("sSi") & "-" & tbSubID.Text & ""
            End If
        Else
            tbSubID.Text = Session("sSi") & "-01"
        End If
        oCnct.Close()
    End Sub
    Sub editSubMenu()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT CFG_productSubmenu.SubmenuID, CFG_productSubmenu.SubmenuName, CFG_productSubmenu.link, CFG_productGrp.productGrp, CFG_productGrp.PrdDescription, CFG_Product.ProductName, CFG_productMenu.MenuName"
        sSQL += " FROM CFG_productSubmenu "
        sSQL += "INNER JOIN CFG_Product ON CFG_productSubmenu.ProductID = CFG_Product.ProductID "
        sSQL += "INNER JOIN CFG_ProductMenu ON CFG_productSubmenu.MenuID = CFG_ProductMenu.MenuID "
        sSQL += "INNER JOIN CFG_productGrp ON CFG_productGrp.ProductGrp = CFG_Product.ProductGrp "
        sSQL += "WHERE CFG_productSubmenu.SubmenuID ='" & i & "' and CFG_productSubmenu.ProductID='" & Session("sGetMenu") & "' and CFG_productSubmenu.MenuID='" & Session("sSi") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()

        If oSQLReader.HasRows Then
            tbGroupSub.Text = oSQLReader.Item("PrdDescription").ToString
            tbSubID.Text = oSQLReader.Item("SubmenuID").ToString
            tbSubmenu.Text = oSQLReader.Item("SubmenuName").ToString
            tbDetailProductSub.Text = oSQLReader.Item("ProductName").ToString
            tbDetailMenuSub.Text = oSQLReader.Item("MenuName").ToString
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
        sSQL = sSQL & "VALUES ('" & Session("sGetMenu") & "','" & Session("sSi") & "','" & tbSubID.Text & "','" & Replace(UCase(tbSubmenu.Text), "'", "''") & "') "
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Data has been saved !');document.getElementById('Buttonx').click()", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalFormSub", "hideModalFormSub()", True)
        ListSubmenu()
    End Sub
    Sub updateSubMenu()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "UPDATE CFG_productSubMenu SET SubmenuName= '" & Replace(UCase(tbSubmenu.Text), "'", "''") & "'"
        sSQL = sSQL & "WHERE SubmenuID = '" & tbSubID.Text & "' and ProductID='" & Session("sGetMenu") & "' and MenuID='" & Session("sSi") & "'"

        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Data has been update !');document.getElementById('Buttonx').click()", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalFormSub", "hideModalFormSub()", True)
        ListSubmenu()
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
        ListSubmenu()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalDelete", "hideModalDelete()", True)
    End Sub
    'End Sub Menu Product=================================================================================
    'SOP Link=============================================================================================
    Sub SOPlink()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT CFG_linkSOP.SubmenuID, CFG_linkSOP.Link, CFG_linkSOP.SopID, CFG_productGrp.productGrp, CFG_productGrp.PrdDescription, "
        sSQL += "CFG_Product.ProductName, CFG_productMenu.MenuName,CFG_productSubMenu.SubmenuName"
        sSQL += " FROM CFG_linkSOP "
        sSQL += "INNER JOIN CFG_Product ON CFG_linkSOP.ProductID = CFG_Product.ProductID "
        sSQL += "INNER JOIN CFG_productMenu ON CFG_linkSOP.ProductID = CFG_productMenu.ProductID AND CFG_linkSOP.MenuID = CFG_productMenu.MenuID "
        sSQL += "INNER JOIN CFG_productSubMenu ON CFG_linkSOP.ProductID=CFG_productSubMenu.ProductID AND CFG_linkSOP.MenuID = CFG_productSubMenu.MenuID AND CFG_linkSOP.SubmenuID = CFG_productSubMenu.SubmenuID "
        sSQL += "INNER JOIN CFG_productGrp ON CFG_productGrp.ProductGrp = CFG_Product.ProductGrp "
        sSQL += "WHERE CFG_linkSOP.SubmenuID='" & Session("sQSOP") & "' and CFG_linkSOP.MenuID ='" & Session("sSi") & "'and CFG_linkSOP.ProductID='" & Session("sGetMenu") & "'"

        sSQL += " order by CFG_linkSOP.SopID asc"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                If dt.Rows.Count <> 0 Then
                    gvSOP.DataSource = dt
                    gvSOP.DataBind()
                Else
                    dt.Rows.Add(dt.NewRow())
                    gvSOP.DataSource = dt
                    gvSOP.DataBind()
                    gvSOP.Rows(0).Visible = False
                End If
            End Using
        End Using
        oCnct.Close()
    End Sub
    Sub IdSOPLink()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT MAX(SopID) as SopID FROM CFG_linkSOP where SopID = '" & Session("sQSOP") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            tbSubID.Text = Val(Mid(oSQLReader.Item("SopID").ToString, 9, 2)) + 1
            If Len(tbSubID.Text) = 1 Then
                tbSopId.Text = Session("sQSOP") & "-0" & tbSubID.Text & ""
            ElseIf Len(tbSubID.Text) = 2 Then
                tbSopId.Text = Session("sQSOP") & "-" & tbSubID.Text & ""
            End If
        Else
            tbSopId.Text = Session("sQSOP") & "-01"
        End If
        oCnct.Close()
    End Sub
    Sub editSOP()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT CFG_linkSOP.SopID, CFG_linkSOP.SubmenuID, CFG_linkSOP.Link, CFG_productGrp.productGrp, CFG_productGrp.PrdDescription, "
        sSQL += "CFG_Product.ProductName, CFG_productMenu.MenuName,CFG_productSubMenu.SubmenuName"
        sSQL += " FROM CFG_linkSOP "
        sSQL += "INNER JOIN CFG_Product ON CFG_linkSOP.ProductID = CFG_Product.ProductID "
        sSQL += "INNER JOIN CFG_productMenu ON CFG_linkSOP.ProductID = CFG_productMenu.ProductID AND CFG_linkSOP.MenuID = CFG_productMenu.MenuID "
        sSQL += "INNER JOIN CFG_productSubMenu ON CFG_linkSOP.ProductID=CFG_productSubMenu.ProductID AND CFG_linkSOP.MenuID = CFG_productSubMenu.MenuID AND CFG_linkSOP.SubmenuID = CFG_productSubMenu.SubmenuID "
        sSQL += "INNER JOIN CFG_productGrp ON CFG_productGrp.ProductGrp = CFG_Product.ProductGrp "
        sSQL += "WHERE CFG_linkSOP.SopID ='" & i & "' and CFG_linkSOP.SubmenuID='" & Session("sQSOP") & "' and CFG_linkSOP.ProductID='" & Session("sGetMenu") & "' and CFG_linkSOP.MenuID='" & Session("sSi") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()

        If oSQLReader.HasRows Then
            tbGrpSOP.Text = oSQLReader.Item("PrdDescription").ToString
            tbSopId.Text = oSQLReader.Item("SopID").ToString
            tbSubmenuSOP.Text = oSQLReader.Item("SubmenuName").ToString
            tbProductSOP.Text = oSQLReader.Item("ProductName").ToString
            tbMenuSOP.Text = oSQLReader.Item("MenuName").ToString
            tbLinkSOP.Text = oSQLReader.Item("Link").ToString
            oCnct.Close()
        Else
            'Refreshh()
        End If
    End Sub
    Sub saveSOPLink()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "INSERT INTO  CFG_linkSOP( ProductID, MenuID, SubmenuID, SopID, Link) "
        sSQL = sSQL & "VALUES ('" & Session("sGetMenu") & "','" & Session("sSi") & "','" & Session("sQSOP") & "','" & Replace(tbSopId.Text, "'", "''") & "','" & Replace(tbLinkSOP.Text, "'", "''") & "') "
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Data has been saved !');document.getElementById('Buttonx').click()", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideFormSOP", "hideFormSOP()", True)
        SOPlink()
    End Sub
    Sub updateSOPLink()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "UPDATE CFG_linkSOP SET Link= '" & Replace(tbLinkSOP.Text, "'", "''") & "'"
        sSQL = sSQL & "WHERE SopID='" & tbSopId.Text & "' and SubmenuID = '" & Session("sQSOP") & "' and ProductID='" & Session("sGetMenu") & "' and MenuID='" & Session("sSi") & "'"

        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Data has been update !');document.getElementById('Buttonx').click()", True)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideFormSOP", "hideFormSOP()", True)
        SOPlink()
    End Sub
    'End SOP Link=========================================================================================
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sId") = "" Then
            Response.Redirect("../login.aspx")
        ElseIf Session("sPossition") <> "0" Then
            Session("sMessage") = "Sorry, Your not access !| ||"
            Session("sWarningID") = "0"
            Session("sUrlOKONLY") = "iPxCrmHome.aspx"
            Session("sUrlYES") = "http://www.thepyxis.net"
            Session("sUrlNO") = "http://www.thepyxis.net"
            Response.Redirect("warningmsg.aspx")
        Else
            If Not Me.IsPostBack Then
                Session("sQueryTicket") = ""
                ListProduct()
            End If
            If Session("sGetMenu") = Nothing And Session("sSi") = Nothing And Session("sQSOP") = Nothing Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "menuHide", "$(document).ready(function() { menuHide() });", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "subMenuHide", "$(document).ready(function() { subMenuHide() });", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SOPHide", "$(document).ready(function() { SOPHide()});", True)
            ElseIf Session("sSi") = Nothing And Session("sQSOP") = Nothing Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "subMenuHide", "$(document).ready(function() { subMenuHide() });", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SOPHide", "$(document).ready(function() { SOPHide()});", True)
                ListMenu()
            ElseIf Session("sQSOP") = Nothing Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SOPHide", "$(document).ready(function() { SOPHide()});", True)
                ListMenu()
                ListSubmenu()
            ElseIf Session("sGetMenu") <> Nothing And Session("sSi") <> Nothing And Session("sQSOP") <> Nothing Then
                ListSubmenu()
                ListMenu()
                SOPlink()
            End If
        End If
    End Sub
    Protected Sub OnPaging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvProduct.PageIndex = e.NewPageIndex
        Me.ListProduct()
    End Sub

    Protected Sub gvProduct_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvProduct.Init
        
    End Sub

    Protected Sub gvProduct_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvProduct.PageIndexChanging
        gvProduct.PageIndex = e.NewPageIndex
        ListProduct()
    End Sub

    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvProduct.PageIndex = e.NewPageIndex
        Me.ListProduct()
    End Sub

    Protected Sub lbAddproduct_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAddproduct.Click
        Session("sP1") = ""
        Response.Redirect("iPxCrmInputProduct.aspx")
    End Sub

    Protected Sub gvProduct_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvProduct.RowCommand
        If e.CommandName = "getEdit" Then
            i = e.CommandArgument.ToString
            editProduct()
        ElseIf e.CommandName = "getHapus" Then
            'i = e.CommandArgument.ToString
            'deletedata()
            Session("sQPd") = e.CommandArgument.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalDeleteProduct", "showModalDeleteProduct()", True)
        ElseIf e.CommandName = "getMenu" Then
            i = e.CommandArgument.ToString
            Session("sGetMenu") = i
            ListMenu()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showMenu", "showMenu()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "subMenuHide", "subMenuHide()", True)
            Session("sSi") = Nothing
        End If
    End Sub

    Protected Sub btnQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        Response.Redirect("queryProduct.aspx")
    End Sub

    Protected Sub lbAddmenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAddmenu.Click
        namaProduct()
        tbGrpMenu.Text = Session("sPrdGroup")
        tbDetailProduct.Text = Session("sNamaProduct")
        IdMenu()
        tbMenuName.Text = ""
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showFormMenu", "showFormMenu()", True)
    End Sub

    Protected Sub gvMenu_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvMenu.RowCommand
        If e.CommandName = "getEdit" Then
            i = e.CommandArgument.ToString
            editProductMenu()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showFormMenu", "showFormMenu()", True)
        ElseIf e.CommandName = "getHapus" Then
            Session("sQd") = e.CommandArgument.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalDeleteMenu", "showModalDeleteMenu()", True)
        ElseIf e.CommandName = "getSub" Then
            Session("sSi") = e.CommandArgument.ToString
            ListSubmenu()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showPanelSubMenu", "showPanelSubMenu()", True)
        End If
    End Sub

    Protected Sub lbSaveMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSaveMenu.Click
        If tbMenuID.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideFormMenu", "hideFormMenu()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('failed to save, please enter Product Menu ID !');document.getElementById('Buttonx').click()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showFormMenu", "showFormMenu()", True)
            tbMenuID.Focus()
        ElseIf tbMenuName.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideFormMenu", "hideFormMenu()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('failed to save, please enter Product Menu Name !');document.getElementById('Buttonx').click()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showFormMenu", "showFormMenu()", True)
            tbMenuName.Focus()
        Else
            If oCnct.State = ConnectionState.Closed Then
                oCnct.Open()
            End If
            oSQLCmd = New SqlCommand(sSQL, oCnct)
            sSQL = "SELECT * FROM CFG_productMenu where MenuID = '" & Replace(UCase(tbMenuID.Text), "'", "''") & "' and ProductID='" & Session("sGetMenu") & "'"
            oSQLCmd.CommandText = sSQL
            oSQLReader = oSQLCmd.ExecuteReader

            If oSQLReader.Read Then
                oSQLReader.Close()
                updateMenu()
            Else
                oSQLReader.Close()
                saveMenu()
            End If
            oCnct.Close()
            ListMenu()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideFormMenu", "hideFormMenu()", True)
        End If
    End Sub

    Protected Sub lbAbortMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAbortMenu.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideFormMenu", "hideFormMenu()", True)
    End Sub

    Protected Sub lbAbortDeleteMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAbortDeleteMenu.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalDeleteMenu", "hideModalDeleteMenu()", True)
    End Sub

    Protected Sub lbDeleteMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbDeleteMenu.Click
        deletedataMenu()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalDeleteMenu", "hideModalDeleteMenu()", True)
    End Sub

    Protected Sub lbCloseMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCloseMenu.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "menuHide", "menuHide()", True)
        Session("sGetMenu") = Nothing
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "subMenuHide", "subMenuHide()", True)
        Session("sSi") = Nothing
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SOPHide", "SOPHide()", True)
        Session("sQSOP") = Nothing
    End Sub

    Protected Sub lbCloseSubMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCloseSubMenu.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "subMenuHide", "subMenuHide()", True)
        Session("sSi") = Nothing
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SOPHide", "SOPHide()", True)
        Session("sQSOP") = Nothing
    End Sub

    Protected Sub lbAddSubMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAddSubMenu.Click
        IdSubmenu()
        namaProduct()
        namaMenu()
        tbDetailProductSub.Text = Session("sNamaProduct")
        tbDetailMenuSub.Text = Session("sNamaMenu")
        tbGroupSub.Text = Session("sPrdGroup")
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalFormSub", "showModalFormSub()", True)
    End Sub

    Protected Sub gvSubmenu_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvSubmenu.RowCommand
        If e.CommandName = "getEdit" Then
            i = e.CommandArgument.ToString
            tbSubID.Enabled = False
            editSubMenu()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalFormSub", "showModalFormSub()", True)
        ElseIf e.CommandName = "getHapus" Then
            Session("sQd") = e.CommandArgument.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalDelete", "showModalDelete()", True)
        ElseIf e.CommandName = "getSOP" Then
            Session("sQSOP") = e.CommandArgument.ToString
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SOPShow", "SOPShow()", True)
        End If
    End Sub

    Protected Sub lbSaveSub_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSaveSub.Click
        If tbSubmenu.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalFormSub", "hideModalFormSub()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('failed to save, please enter SubMenu Name !');document.getElementById('Buttonx').click()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalFormSub", "showModalFormSub()", True)
            tbSubmenu.Focus()
        Else
            If oCnct.State = ConnectionState.Closed Then
                oCnct.Open()
            End If
            oSQLCmd = New SqlCommand(sSQL, oCnct)
            sSQL = "SELECT * FROM CFG_productSubMenu WHERE SubmenuID = '" & tbSubID.Text & "' and ProductID='" & Session("sGetMenu") & "' and MenuID='" & Session("sSi") & "'"
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
        End If
    End Sub

    Protected Sub lbAbortSub_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAbortSub.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalFormSub", "hideModalFormSub()", True)
    End Sub

    Protected Sub lbDeleteSub_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbDeleteSub.Click
        deletedataSub()
    End Sub

    Protected Sub lbCancelSub_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCancelSub.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalDelete", "hideModalDelete()", True)
    End Sub

    Protected Sub lbDeleteProduct_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbDeleteProduct.Click
        deletedata()
    End Sub

    Protected Sub lbAbortDeleteProduct_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAbortDeleteProduct.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalDeleteProduct", "hideModalDeleteProduct()", True)
    End Sub

    Protected Sub lbCloseSOP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbCloseSOP.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "SOPHide", "SOPHide()", True)
        Session("sQSOP") = Nothing
    End Sub

    Protected Sub lbAddSOP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAddSOP.Click
        IdSOPLink()
        namaProduct()
        namaMenu()
        namaSubmenu()
        tbProductSOP.Text = Session("sNamaProduct")
        tbMenuSOP.Text = Session("sNamaMenu")
        tbGrpSOP.Text = Session("sPrdGroup")
        tbSubmenuSOP.Text = Session("sNamaSubMenu")
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showFormSOP", "showFormSOP()", True)
    End Sub

    Protected Sub lbSaveSOP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSaveSOP.Click
        If tbLinkSOP.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideFormSOP", "hideFormSOP()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('failed to save, please enter SOP Link !');document.getElementById('Buttonx').click()", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showFormSOP", "showFormSOP()", True)
            tbLinkSOP.Focus()
        Else
            If oCnct.State = ConnectionState.Closed Then
                oCnct.Open()
            End If
            oSQLCmd = New SqlCommand(sSQL, oCnct)
            sSQL = "SELECT * FROM CFG_linkSOP WHERE SopID='" & tbSopId.Text & "' and SubmenuID = '" & Session("sQSOP") & "' and ProductID='" & Session("sGetMenu") & "' and MenuID='" & Session("sSi") & "'"
            oSQLCmd.CommandText = sSQL
            oSQLReader = oSQLCmd.ExecuteReader

            If oSQLReader.Read Then
                oSQLReader.Close()
                updateSOPLink()
            Else
                oSQLReader.Close()
                saveSOPLink()
            End If
            oCnct.Close()
        End If
    End Sub

    Protected Sub lbAbortSOP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAbortSOP.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideFormSOP", "hideFormSOP()", True)
    End Sub

    Protected Sub gvSOP_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvSOP.RowCommand
        If e.CommandName = "getEdit" Then
            i = e.CommandArgument.ToString
            editSOP()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showFormSOP", "showFormSOP()", True)
        End If
    End Sub
End Class
