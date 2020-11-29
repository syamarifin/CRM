Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Partial Class iPxCrmCustomer_iPxCrmTicketProses
    Inherits System.Web.UI.Page
    Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Dim oCnct As SqlConnection = New SqlConnection(sCnct)
    Dim oSQLCmd As SqlCommand
    Dim oSQLReader As SqlDataReader
    Dim sSQL, Ticketno, i As String
    Dim cIpx As iPxClass

    Sub ListTicket()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT SPR_eticket.CustID, SPR_eticket.TicketNo, SPR_eticket.Tdate, SPR_eticket.SubmitFrom, CFG_Product.ProductName, CFG_productMenu.MenuName, CFG_productSubMenu.SubmenuName"
        sSQL += " From SPR_eticket "
        sSQL += "INNER JOIN CFG_Product ON SPR_eticket.ProductID = CFG_Product.ProductID "
        sSQL += "INNER JOIN CFG_productMenu ON SPR_eticket.MenuID = CFG_productMenu.MenuID "
        sSQL += "INNER JOIN CFG_productSubMenu ON SPR_eticket.SubmenuID = CFG_productSubMenu.SubmenuID where SPR_eticket.custID = '" & Session("sCId") & "' and SPR_eticket.statusID <> '" & "1" & "' and SPR_eticket.statusID <> '" & "6" & "' "

        If Not String.IsNullOrEmpty(tbSearch.Text.Trim()) Then
            sSQL += " and SPR_eticket.TicketNo LIKE '%' + @Search + '%'"
            sSQL += " or CFG_Product.ProductName LIKE '%' + @Search + '%'"
            sSQL += " or CFG_productMenu.MenuName LIKE '%' + @Search + '%'"
            oSQLCmd.Parameters.AddWithValue("@Search", tbSearch.Text.Trim())
        End If
        sSQL += " order by SPR_eticket.TicketNo asc"

        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                gvTicket.DataSource = dt
                gvTicket.DataBind()
            End Using
        End Using
        oCnct.Close()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            ListTicket()
        End If
    End Sub
    Protected Sub cari(ByVal sender As Object, ByVal e As EventArgs)
        ListTicket()
    End Sub
    Protected Sub OnPaging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvTicket.PageIndex = e.NewPageIndex
        Me.ListTicket()
    End Sub

    Protected Sub gvTicket_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvTicket.PageIndexChanging
        gvTicket.PageIndex = e.NewPageIndex
        ListTicket()
    End Sub

    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvTicket.PageIndex = e.NewPageIndex
        Me.ListTicket()
    End Sub
End Class
