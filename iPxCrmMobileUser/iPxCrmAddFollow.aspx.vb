Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Globalization
Imports System.Threading
Partial Class iPxCrmMobileUser_iPxCrmAddFollow
    Inherits System.Web.UI.Page
    Public sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Public oCnct As SqlConnection = New SqlConnection(sCnct)
    Public oSQLCmd As New SqlCommand
    Public oSQLReader As SqlDataReader
    Public sSQL, i As String
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

    Sub savedata()
        
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        Dim regDate As Date = Date.Now()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "INSERT INTO SPR_eticketFollowUp (CustID, TicketNo, FollowUpNo, FollowUpDate, FollowUpBy, FollowUpNote, FollowUpSopLink, FollowUpCode, isRead) "
        sSQL += "VALUES ('" & Session("sCustID") & "','" & Session("sTicketFollow") & "','" & tbNo.Text & "','" & regDate & "','" & Replace(tbBy.Text, "'", "''") & "','" & Replace(tbNote.Text, "'", "''") & "','" & Replace(tbLink.Text, "'", "''") & "','" & "P" & "','" & "1" & "') "
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('data send successfully !');document.getElementById('Buttonx').click()", True)
        tbBy.Text = ""
        tbLink.Text = ""
        tbNo.Text = ""
        tbNote.Text = ""
        Response.Redirect("iPxCrmDetailFollowUser.aspx")
    End Sub

    Sub updateRead()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If

        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "UPDATE SPR_eticketFollowUp SET isRead= '0'"
        sSQL = sSQL & "WHERE TicketNo = '" & Session("sTicketFollow") & "' and FollowUpCode='H'"

        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Assignment is saved successfully !');document.getElementById('Buttonx').click()", True)
    End Sub
    Sub updateStatus()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If

        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "UPDATE SPR_eticket SET StatusID= '" & dlStatus.SelectedValue & "'"
        sSQL = sSQL & "WHERE TicketNo = '" & Session("sTicketFollow") & "' "

        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Assignment is saved successfully !');document.getElementById('Buttonx').click()", True)
    End Sub
    Sub status()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_FollowupSts where StatusID >'" & "1" & "' and StatusID <>'" & "7" & "'"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                dlStatus.DataSource = dt
                dlStatus.DataTextField = "stsDescription"
                dlStatus.DataValueField = "StatusID"
                dlStatus.DataBind()
            End Using
        End Using
    End Sub
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
        sSQL += "WHERE CFG_linkSOP.SubmenuID='" & Session("sLinkSOP") & "'"

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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            status()
            dlStatus.SelectedValue = Session("sStatusTicket")
            tbBy.Text = Session("sName")
            tbNo.Text = cipx.GetCounterMBR("FP", "FP")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "openModal", "$(document).ready(function() {openModal()});", True)
        Else
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "PopUser", "clearModal()", True)
        End If
    End Sub
    Protected Sub lbSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSave.Click
        If tbBy.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "openModal", "$(document).ready(function() {openModal()});", True)
        ElseIf tbNote.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "openModal", "$(document).ready(function() {openModal()});", True)
        Else
            If oCnct.State = ConnectionState.Closed Then
                oCnct.Open()
            End If
            oSQLCmd = New SqlCommand(sSQL, oCnct)
            sSQL = "SELECT FollowUpNo FROM SPR_eticketFollowUp WHERE FollowUpNo = '" & tbNo.Text & "'"
            oSQLCmd.CommandText = sSQL
            oSQLReader = oSQLCmd.ExecuteReader

            If oSQLReader.Read Then
                oSQLReader.Close()
                'updatedata()
                updateStatus()
            Else
                oSQLReader.Close()
                updateStatus()
                savedata()
            End If
            oCnct.Close()
            If Session("sPossition") = "0" Then
                updateRead()
            End If
            Response.Redirect("iPxCrmDetailFollowUser.aspx")
        End If
    End Sub

    Protected Sub lbAbort1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAbort1.Click
        Response.Redirect("iPxCrmDetailFollowUser.aspx")
    End Sub

    Protected Sub lbFindSOP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbFindSOP.Click
        SOPlink()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showFormSOP", "$(document).ready(function() {showFormSOP()});", True)
    End Sub

    Protected Sub lbAbortSOP_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAbortSOP.Click
        tbLink.Text = ""
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "openModal", "$(document).ready(function() {openModal()});", True)
    End Sub

    Protected Sub gvSOP_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvSOP.RowCommand
        If e.CommandName = "getEdit" Then
            i = e.CommandArgument.ToString
            Dim oSQLReader As SqlDataReader
            If oCnct.State = ConnectionState.Closed Then
                oCnct.Open()
            End If
            oSQLCmd = New SqlCommand(sSQL, oCnct)
            sSQL = "SELECT * FROM CFG_linkSOP"
            sSQL += " WHERE CFG_linkSOP.SopID ='" & i & "'"
            oSQLCmd.CommandText = sSQL
            oSQLReader = oSQLCmd.ExecuteReader

            oSQLReader.Read()
            'usercode, mobileno, password, signupdate, fullname, status, quid
            If oSQLReader.HasRows Then
                tbLink.Text = oSQLReader.Item("Link").ToString
            Else
                'Refreshh()
            End If
            oCnct.Close()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "openModal", "$(document).ready(function() {openModal()});", True)
        End If
    End Sub
End Class
