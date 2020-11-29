Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Partial Class iPxCrmUser_iPxCrmCustomerContact
    Inherits System.Web.UI.Page
    Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Dim oCnct As SqlConnection = New SqlConnection(sCnct)
    Dim oSQLCmd As SqlCommand
    Dim oSQLReader As SqlDataReader
    Dim sSQL, Ticketno, i As String
    Dim cIpx As iPxClass
    Sub hotelid()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_customer"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                dlHotelName.DataSource = dt
                dlHotelName.DataTextField = "CustName"
                dlHotelName.DataValueField = "CustID"
                dlHotelName.DataBind()
            End Using
        End Using
    End Sub
    Sub grubid()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_customerContactGrp"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                dlCustGrp.DataSource = dt
                dlCustGrp.DataTextField = "Description"
                dlCustGrp.DataValueField = "ContactGrpID"
                dlCustGrp.DataBind()
            End Using
        End Using
    End Sub
    Sub ListCustContact()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT CFG_customerContact.CustID, CFG_customer.CustName, CFG_customerContact.Name, CFG_customerContactGrp.Description, "
        sSQL += "CFG_customerContact.Birthday, CFG_customerContact.Phone, CFG_customerContact.Email FROM CFG_customerContact "
        sSQL += "INNER JOIN CFG_customer ON CFG_customerContact.CustID = CFG_customer.CustID "
        sSQL += "INNER JOIN CFG_customerContactGrp ON CFG_customerContact.ContactGrpID = CFG_customerContactGrp.ContactGrpID"

        If Session("sCondition") <> "" Then
            sSQL = sSQL & Session("sCondition")
            Session("sCondition") = ""
        Else
            sSQL = sSQL & ""
        End If
        sSQL += " order by CFG_customerContact.CustID asc"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                gvCustContact.DataSource = dt
                gvCustContact.DataBind()
            End Using
        End Using
        oCnct.Close()
    End Sub
    Sub savedata()

        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        'Dim dateBirthday As Date = Date.ParseExact(tbBirthday.Text, "yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "INSERT INTO  CFG_customerContact(CustID, ContactGrpID, Name, Phone, Email, Passw, Birthday) "
        sSQL = sSQL & "VALUES ('" & dlHotelName.SelectedValue & "','" & dlCustGrp.SelectedValue & "','" & tbName.Text & "','" & tbPhone.Text & "','" & tbEmail.Text & "','" & tbName.Text & "','" & tbBirthday.Text & "')"
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Data has been saved !');document.getElementById('Buttonx').click()", True)
        ListCustContact()
        tbBirthday.Text = ""
        tbEmail.Text = ""
        tbName.Text = ""
        tbPhone.Text = ""
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "hideModalAdd", "hideModalAdd()", True)
    End Sub
    'Sub updatedata()
    '    If oCnct.State = ConnectionState.Closed Then
    '        oCnct.Open()
    '    End If
    '    upload()
    '    oSQLCmd = New SqlCommand(sSQL, oCnct)
    '    sSQL = "UPDATE SPR_eticket SET CaseDescription= '" & tbDescription.Text & "', ProductID= '" & dlProduct.SelectedValue & "', MenuID= '" & dlMenu.SelectedValue & "', SubMenuID= '" & dlSubMenu.SelectedValue & "', SubmitFrom= '" & tbFrom.Text & "', SubmitVia= '" & dlSubmitVia.SelectedValue & "'"
    '    If uploadname = "" Then
    '        sSQL = sSQL & " "
    '    Else
    '        sSQL = sSQL & ", AttachFile= '" & uploadname & "'"
    '    End If
    '    sSQL = sSQL & "WHERE TicketNo = '" & tbTicketno.Text & "' "

    '    oSQLCmd.CommandText = sSQL
    '    oSQLCmd.ExecuteNonQuery()

    '    oCnct.Close()
    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Data has been update !');document.getElementById('Buttonx').click()", True)
    '    'ListTicket()
    '    product()
    '    menu()
    '    submenu()
    '    tbFrom.Text = ""
    '    tbDescription.Text = ""
    '    Response.Redirect("iPxCrmNewtiket.aspx")
    'End Sub
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
                ListCustContact()
            End If
        End If
    End Sub
    Protected Sub OnPaging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvCustContact.PageIndex = e.NewPageIndex
        Me.ListCustContact()
    End Sub

    Protected Sub gvCustContact_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvCustContact.PageIndexChanging
        gvCustContact.PageIndex = e.NewPageIndex
        ListCustContact()
    End Sub

    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvCustContact.PageIndex = e.NewPageIndex
        Me.ListCustContact()
    End Sub

    Protected Sub lbAddContact_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAddContact.Click
        hotelid()
        grubid()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "showModalAdd", "showModalAdd()", True)
    End Sub

    Protected Sub lbSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSave.Click
        savedata()
    End Sub

    Protected Sub gvCustContact_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvCustContact.RowCommand

    End Sub
End Class
