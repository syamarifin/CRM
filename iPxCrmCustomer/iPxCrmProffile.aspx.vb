Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Partial Class iPxCrmCustomer_iPxCrmProffile
    Inherits System.Web.UI.Page
    Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Dim oCnct As SqlConnection = New SqlConnection(sCnct)
    Dim oSQLCmd As SqlCommand
    Dim oSQLReader As SqlDataReader
    Dim sSQL, Ticketno, i As String
    Dim cIpx As iPxClass
    Sub cancel()
        btnchange.Visible = True
        pnlOldPW.Visible = False
        txtnewpass.Visible = False
        txtnewpassconf.Visible = False
        btnsavepass.Visible = False
        btnCancel.Visible = False
    End Sub
    Sub editContact()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT CFG_customer.CustName, CFG_customerContact.Name, CFG_customerContact.NameCode, CFG_customerContact.ContactGrpID, CFG_customerContact.Phone, "
        sSQL += "CFG_customerContact.Email, CFG_customerContact.Passw, CFG_customerContact.Birthday "
        sSQL += "FROM CFG_customerContact INNER JOIN "
        sSQL += "CFG_customer ON CFG_customerContact.CustID = CFG_customer.CustID INNER JOIN "
        sSQL += "CFG_customerContactGrp ON CFG_customerContact.ContactGrpID = CFG_customerContactGrp.ContactGrpID "
        sSQL += "WHERE CFG_customerContact.NameCode ='" & Session("sCNameCode") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        'usercode, mobileno, password, signupdate, fullname, status, quid
        If oSQLReader.HasRows Then
            tbCustName.Text = oSQLReader.Item("CustName").ToString
            dlContactGrp.SelectedValue = oSQLReader.Item("ContactGrpID").ToString
            tbNameContact.Text = oSQLReader.Item("Name").ToString
            tbPhone.Text = oSQLReader.Item("Phone").ToString
            tbEmail.Text = oSQLReader.Item("Email").ToString
            Dim dateBirthday As Date = oSQLReader.Item("Birthday").ToString
            tbBirthday.Text = dateBirthday.ToString("dd/MM/yyyy")
            oCnct.Close()
        Else
            'Refreshh()
        End If
    End Sub
    Sub ContactGrup()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_customerContactGrp"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                dlContactGrp.DataSource = dt
                dlContactGrp.DataTextField = "Description"
                dlContactGrp.DataValueField = "ContactGrpID"
                dlContactGrp.DataBind()
                dlContactGrp.Items.Insert(0, "")
            End Using
        End Using
    End Sub
    Protected Sub btnchange_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnchange.Click
        pnlOldPW.Visible = True
        btnCancel.Visible = True
        btnchange.Visible = False
        txtoldpass.Text = Nothing
        txtoldpass.Focus()
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        cancel()
    End Sub

    Protected Sub btnCheckOldPw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCheckOldPw.Click
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If

        sSQL = "Select * From CFG_customerContact Where CustID = '" & Session("sCId") & "' and passw = '" & Replace(txtoldpass.Text, "'", "''") & "' and NameCode ='" & Session("sCNameCode") & "'"
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        oSQLReader = oSQLCmd.ExecuteReader

        If oSQLReader.Read Then
            btnCancel.Visible = True
            btnchange.Visible = False
            pnlOldPW.Visible = False
            txtnewpass.Visible = True
            txtnewpassconf.Visible = True
            btnsavepass.Visible = True
        Else
            txtoldpass.Text = ""
            txtoldpass.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Wrong Password');", True)


        End If

        oCnct.Close()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            ContactGrup()
            editContact()
        End If
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "date", "$(document).ready(function() { date() });", True)
    End Sub

    Protected Sub btnsavepass_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsavepass.Click
        If txtnewpass.Text = txtnewpassconf.Text Then
            If oCnct.State = ConnectionState.Closed Then
                oCnct.Open()
            End If

            oSQLCmd = New SqlCommand(sSQL, oCnct)
            sSQL = "UPDATE  CFG_customerContact SET  "
            sSQL = sSQL & " passw='" & Replace(txtnewpassconf.Text, "'", "''") & "' WHERE CustID = '" & Session("sCId") & "' and Email='" & Session("sCEmail") & "' and NameCode ='" & Session("sCNameCode") & "' "
            oSQLCmd.CommandText = sSQL
            oSQLCmd.ExecuteNonQuery()

            oCnct.Close()
            cancel()
            Response.Redirect("SigIn.aspx")
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Change password successful!');", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Password is not match!');", True)


        End If
    End Sub

    Protected Sub lbAbort_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAbort.Click
        Response.Redirect("iPxCrmHome.aspx")
    End Sub

    Protected Sub lbSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSave.Click
        If tbNameContact.Text = Nothing Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('failed to save, please complete your name !');document.getElementById('Buttonx').click()", True)
            tbNameContact.Focus()
        ElseIf tbEmail.Text = Nothing Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('failed to save, please complete your e-mail !');document.getElementById('Buttonx').click()", True)
            tbEmail.Focus()
        ElseIf dlContactGrp.SelectedValue = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('failed to save, enter your Contact Group !');document.getElementById('Buttonx').click()", True)
            dlContactGrp.Focus()
        Else
            If oCnct.State = ConnectionState.Closed Then
                oCnct.Open()
            End If

            oSQLCmd = New SqlCommand(sSQL, oCnct)
            Dim dateBirthday As Date
            If tbBirthday.Text <> "" Then
                dateBirthday = Date.ParseExact(tbBirthday.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
            Else
                dateBirthday = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
            End If
            sSQL = "UPDATE  CFG_customerContact SET  "
            sSQL += "ContactGrpID='" & dlContactGrp.SelectedValue & "',Name='" & Replace(tbNameContact.Text, "'", "''") & "',Phone='" & Replace(tbPhone.Text, "'", "''") & "',Email='" & Replace(tbEmail.Text, "'", "''") & "',Birthday='" & dateBirthday & "'"
            sSQL += " WHERE CustID = '" & Session("sCId") & "' "
            oSQLCmd.CommandText = sSQL
            oSQLCmd.ExecuteNonQuery()

            oCnct.Close()
            cancel()
            ContactGrup()
            editContact()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Change proffile successful!');", True)
            Response.Redirect("iPxCrmProffile.aspx")
        End If
    End Sub
End Class
