Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Configuration

Partial Class iPxCrmUser_iPxCrmDetailFollowDone
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
        sSQL += "(SELECT SPR_eticket.CustID, SPR_eticket.TicketNo, SPR_eticket.Tdate, SPR_eticket.CaseDescription, SPR_eticket.SubmitFrom, CFG_Product.ProductName, CFG_productMenu.MenuName, CFG_productSubMenu.SubmenuName"
        sSQL += ", CFG_caseID.Description,CFG_FollowupSts.stsDescription, AssignTo, Subject, CFG_productSubMenu.link From SPR_eticket "
        sSQL += "INNER JOIN CFG_Product ON SPR_eticket.ProductID = CFG_Product.ProductID INNER JOIN CFG_FollowupSts ON SPR_eticket.StatusID = CFG_FollowupSts.StatusID "
        sSQL += "INNER JOIN CFG_productMenu ON SPR_eticket.MenuID = CFG_productMenu.MenuID and CFG_Product.ProductID = CFG_productMenu.ProductID LEFT JOIN CFG_caseID ON SPR_eticket.CaseID = CFG_caseID.CaseID "
        sSQL += "INNER JOIN CFG_productSubMenu ON SPR_eticket.SubmenuID = CFG_productSubMenu.SubmenuID where SPR_eticket.TicketNo = '" & Session("sTicketFollow") & "') as a"
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        oSQLReader = oSQLCmd.ExecuteReader

        If oSQLReader.Read Then

            'lbNamaHotel.Text = oSQLReader.Item("CustName").ToString.Trim
            lbTicketNo.Text = oSQLReader.Item("TicketNo").ToString.Trim
            Dim dateBirthday As Date = oSQLReader.Item("Tdate").ToString
            lbTgl.Text = dateBirthday.ToString("dd MMMM yyyy hh:mm:ss")
            lbProduct.Text = UCase(oSQLReader.Item("ProductName").ToString.Trim)
            lbMenu.Text = UCase(oSQLReader.Item("MenuName").ToString.Trim)
            lbSubMenu.Text = UCase(oSQLReader.Item("SubmenuName").ToString.Trim)
            lbSubject.Text = oSQLReader.Item("Subject").ToString.Trim
            lbCase.Text = oSQLReader.Item("Description").ToString.Trim
            Dim a As String = oSQLReader.Item("CaseDescription").ToString.Trim
            lbDescription.Text = Replace(a, vbLf, "<br />")
            lbStatus.Text = StrConv(oSQLReader.Item("stsDescription").ToString.Trim, VbStrConv.ProperCase)
            lbSuportby.Text = oSQLReader.Item("supportBy").ToString.Trim
            If oSQLReader.Item("link").ToString.Trim = "" Then
                lbLink.Text = ""
            Else
                lbLink.Text = "<a target='_blank' href='" & oSQLReader.Item("link").ToString.Trim & "' style='color:White;'>( Open SOP )</a> "
            End If
            oSQLReader.Close()
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Sorry User Is Not Registered Yet or Not Active !');document.getElementById('Buttonx').click()", True)
        End If
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
                rptFollow.DataSource = dt
                rptFollow.DataBind()
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
        sSQL += ", CFG_caseID.Description,SPR_eticket.StatusID,SPR_eticket.Rating From SPR_eticket "
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
            nwLatHidden.Value = oSQLReader.Item("Rating").ToString
            oCnct.Close()
        Else
            'Refreshh()
        End If
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
            Response.Redirect("../login.aspx")
        End If
        If Not Me.IsPostBack Then
            ListTicket()
            ListFollow()
            ListStatus()
        End If
        If Session("sPossition") <> "0" Then
            updateRead()
        End If
    End Sub
    Protected Sub lbAbort_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAbort.Click
        Response.Redirect("iPxCrmDone.aspx")
    End Sub
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
End Class
