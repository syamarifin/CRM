Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Configuration

Imports System.Net.Mail
Imports System.Net
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security

Imports iTextSharp.text
Imports iTextSharp.text.html.simpleparser
Imports iTextSharp.text.pdf

Imports System.Text
Imports System.Web
Imports System.Net.Mime
Imports System.Globalization

Partial Class iPxCrmMobileUser_iPxCrmDetailFollowUser
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
        sSQL = "select a.*,(select name from CFG_user where recID = a.AssignTo)as supportBy from "
        sSQL += "(SELECT SPR_eticket.CustID, CFG_customer.CustName, SPR_eticket.TicketNo, SPR_eticket.Tdate, SPR_eticket.CaseDescription, SPR_eticket.SubmitFrom, CFG_Product.ProductName, CFG_productMenu.MenuName, CFG_productSubMenu.SubmenuName"
        sSQL += ", CFG_caseID.Description,CFG_FollowupSts.stsDescription, AssignTo From SPR_eticket "
        sSQL += "INNER JOIN CFG_Product ON SPR_eticket.ProductID = CFG_Product.ProductID INNER JOIN CFG_FollowupSts ON SPR_eticket.StatusID = CFG_FollowupSts.StatusID "
        sSQL += "INNER JOIN CFG_productMenu ON SPR_eticket.MenuID = CFG_productMenu.MenuID and CFG_Product.ProductID = CFG_productMenu.ProductID LEFT JOIN CFG_caseID ON SPR_eticket.CaseID = CFG_caseID.CaseID "
        sSQL += "INNER JOIN CFG_customer ON SPR_eticket.CustID = CFG_customer.CustID "
        sSQL += "INNER JOIN CFG_productSubMenu ON SPR_eticket.SubmenuID = CFG_productSubMenu.SubmenuID where SPR_eticket.TicketNo = '" & Session("sTicketFollow") & "') as a"
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

    Sub ListFollow()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * "
        sSQL += " From SPR_eticketFollowUp "
        sSQL += " where TicketNo = '" & Session("sTicketFollow") & "' order by FollowUpDate asc"

        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                gvFollow.DataSource = dt
                gvFollow.DataBind()
            End Using
        End Using
        oCnct.Close()
    End Sub
    Sub ListStatus()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT SPR_eticket.CustID, SPR_eticket.TicketNo, SPR_eticket.Tdate, SPR_eticket.CaseDescription, SPR_eticket.SubmitFrom, CFG_Product.ProductName, CFG_productMenu.MenuName, CFG_productSubMenu.SubmenuName"
        sSQL += ", CFG_caseID.Description,SPR_eticket.StatusID,SPR_eticket.SubMenuID From SPR_eticket "
        sSQL += "INNER JOIN CFG_Product ON SPR_eticket.ProductID = CFG_Product.ProductID "
        sSQL += "INNER JOIN CFG_productMenu ON SPR_eticket.MenuID = CFG_productMenu.MenuID INNER JOIN CFG_caseID ON SPR_eticket.CaseID = CFG_caseID.CaseID "
        sSQL += "INNER JOIN CFG_productSubMenu ON SPR_eticket.SubmenuID = CFG_productSubMenu.SubmenuID where SPR_eticket.TicketNo = '" & Session("sTicketFollow") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        'usercode, mobileno, password, signupdate, fullname, status, quid
        If oSQLReader.HasRows Then
            Session("sStatusTicket") = oSQLReader.Item("StatusID").ToString
            Session("sCustID") = oSQLReader.Item("CustID").ToString
            Session("sLinkSOP") = oSQLReader.Item("SubMenuID").ToString.Trim
            oCnct.Close()
        Else
            oCnct.Close()
            'Refreshh()
        End If
    End Sub
    Sub status()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_FollowupSts where StatusID >'" & "1" & "' and StatusID <>'" & "7" & "'"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                dlUpStatus.DataSource = dt
                dlUpStatus.DataTextField = "stsDescription"
                dlUpStatus.DataValueField = "StatusID"
                dlUpStatus.DataBind()
            End Using
        End Using
    End Sub
    
    Sub updateStatus1()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If

        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "UPDATE SPR_eticket SET StatusID= '" & dlUpStatus.SelectedValue & "'"
        sSQL = sSQL & "WHERE TicketNo = '" & Session("sTicketFollow") & "' "

        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Assignment is saved successfully !');document.getElementById('Buttonx').click()", True)
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sId") = Nothing Then
            Response.Redirect("SignIn.aspx")
        Else
            Dim cph As ContentPlaceHolder = DirectCast(Me.Master.FindControl("ContentPlaceHolder2"), ContentPlaceHolder)
            Dim LinkButton As LinkButton = DirectCast(cph.FindControl("lnkback"), LinkButton)
            ListStatus()
            If Session("sStatusTicket") = 6 Then
                LinkButton.PostBackUrl = "iPxCrmDone.aspx"
            Else
                LinkButton.PostBackUrl = "iPxCrmAssign.aspx"
            End If
            If Not Me.IsPostBack Then
                ListTicket()
                ListFollow()
                
                If Session("sPossition") <> "0" Then
                    updateRead()
                End If
                Dim chk As LinkButton = DirectCast(gvTicket.Rows(i).Cells(0).FindControl("lbEditAssigned"), LinkButton)
                If Session("sPossition") = "0" Then
                    chk.Visible = True
                Else
                    chk.Visible = False
                End If
                'If Session("sStatusTicket") = 7 Then
                '    lbAddFollow.Enabled = False
                '    lbUpStatus.Enabled = False
                'Else
                '    lbAddFollow.Enabled = True
                '    lbUpStatus.Enabled = True
                'End If
            End If
        End If
    End Sub

    'Protected Sub lbAddFollow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAddFollow.Click
    '    status()
    '    dlStatus.SelectedValue = Session("sStatusTicket")
    '    tbBy.Text = Session("sName")
    '    tbNo.Text = cIpx.GetCounterMBR("FP", "FP")
    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalAdd", "showModalAdd()", True)
    'End Sub

    
    Protected Sub GetValue(ByVal sender As Object, ByVal e As EventArgs)
        'Reference the Repeater Item using Button.
        Try
            Dim item As RepeaterItem = TryCast((TryCast(sender, LinkButton)).NamingContainer, RepeaterItem)

            Dim filePath As String = (TryCast(item.FindControl("Label1"), Label)).Text
            Response.ContentType = "image/jpg"
            Response.AddHeader("Content-Disposition", "attachment;filename=""" & filePath & """")
            Response.TransmitFile(Server.MapPath(filePath))
            Response.[End]()
        Catch
        End Try
    End Sub
    Protected Sub lbDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbDetail.Click
        If pnHeader.Visible = True Then
            pnHeader.Visible = False
            Panel3.Visible = True
            lbDetail.Text = "<i class='fa fa-angle-double-up' style='height:20px; font-size:20px'></i>"
        ElseIf pnHeader.Visible = False Then
            pnHeader.Visible = True
            pnStatus.Visible = False
            Panel3.Visible = False
            lbDetail.Text = "<i class='fa fa-angle-double-down' style='height:20px; font-size:20px'></i>"
        End If
    End Sub

    Protected Sub lbUpStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbUpStatus.Click
        If pnStatus.Visible = False Then
            pnStatus.Visible = True
            pnHeader.Visible = False
            status()
        ElseIf pnStatus.Visible = True Then
            pnStatus.Visible = False
        End If
    End Sub

    Protected Sub lbSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSend.Click
        Response.Redirect("iPxCrmAddFollow.aspx")
    End Sub

    Protected Sub gvTicket_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvTicket.RowCommand
        If e.CommandName = "getEditTicket" Then
            Session("sTicketFollow") = e.CommandArgument
            Response.Redirect("iPxCrmStartAssign.aspx")
        End If
    End Sub
End Class
