Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Partial Class iPxCrmUser_iPxCrmInputCustomer
    Inherits System.Web.UI.Page
    Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Dim oCnct As SqlConnection = New SqlConnection(sCnct)
    Dim oSQLCmd As SqlCommand
    Dim oSQLReader As SqlDataReader
    Dim sSQL, Ticketno, i As String
    Dim cIpx As iPxClass
    Sub kode()
        Dim oSQLReader As SqlDataReader
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_customer order by CustID desc"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        'usercode, mobileno, password, signupdate, fullname, status, quid
        If oSQLReader.HasRows Then
            tbCustId.Text = Val(oSQLReader.Item("CustID").ToString) + 1
            oCnct.Close()
        Else
            tbCustId.Text = 1
        End If

    End Sub
    Sub CustGrub()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_customerGrp where IsActive='" & "Y" & "'"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                dlCustGrub.DataSource = dt
                dlCustGrub.DataTextField = "GrpName"
                dlCustGrub.DataValueField = "CustGrpID"
                dlCustGrub.DataBind()
            End Using
        End Using
    End Sub
    Sub country()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_geog_country order by country asc"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                dlCountry.DataSource = dt
                dlCountry.DataTextField = "country"
                dlCountry.DataValueField = "countryid"
                dlCountry.DataBind()
            End Using
        End Using
    End Sub
    Sub provins()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_geog_province where countryid ='" & dlCountry.SelectedValue & "' and provid<>'ALL' order by description asc"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                dlProvinsi.DataSource = dt
                dlProvinsi.DataTextField = "description"
                dlProvinsi.DataValueField = "provid"
                dlProvinsi.DataBind()
            End Using
        End Using
    End Sub
    Sub city()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_geog_city where countryid ='" & dlCountry.SelectedValue.Trim & "' and provid like '" & dlProvinsi.SelectedValue.Trim & "%' order by city asc"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                dlCity.DataSource = dt
                dlCity.DataTextField = "city"
                dlCity.DataValueField = "cityid"
                dlCity.DataBind()
            End Using
        End Using
    End Sub
    Sub provinsdefault()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_geog_province where countryid like '%" & "IDN" & "%' and provid<>'ALL' order by description asc"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                dlProvinsi.DataSource = dt
                dlProvinsi.DataTextField = "description"
                dlProvinsi.DataValueField = "provid"
                dlProvinsi.DataBind()
            End Using
        End Using
    End Sub
    Sub citydefault()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_geog_city where countryid like '%" & "IDN" & "%' and provid like '" & "DKI" & "%'  order by city asc"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                dlCity.DataSource = dt
                dlCity.DataTextField = "city"
                dlCity.DataValueField = "cityid"
                dlCity.DataBind()
            End Using
        End Using
    End Sub
    Sub CustStatus()
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_CustomerLevel order by CustLevel asc"
        Using sda As New SqlDataAdapter()
            oSQLCmd.CommandText = sSQL
            sda.SelectCommand = oSQLCmd
            Using dt As New DataTable()
                sda.Fill(dt)
                dlStatus.DataSource = dt
                dlStatus.DataTextField = "LevelDescription"
                dlStatus.DataValueField = "CustLevel"
                dlStatus.DataBind()
            End Using
        End Using
    End Sub
    Sub saveCustomer()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        Dim dateBirthday As Date = Date.ParseExact(tbAnniversery.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
        sSQL = "INSERT INTO  CFG_customer(CustID, CustGrpID, CustName, Phone, Fax, CountryID, Provid, CityID, Address, StarClass, Troom, Anniversary, Notes, CustLevel) "
        sSQL += "VALUES ('" & tbCustId.Text & "','" & dlCustGrub.SelectedValue & "','" & Replace(tbName.Text, "'", "''") & "','" & Replace(tbPhone.Text, "'", "''") & "','" & Replace(tbFax.Text, "'", "''") & "','" & Trim(dlCountry.SelectedValue) & "','" & Trim(dlProvinsi.SelectedValue) & "','" & Trim(dlCity.SelectedValue) & "'"
        sSQL += ",'" & Replace(tbAddress.Text, "'", "''") & "','" & Replace(tbStar.Text, "'", "''") & "','" & Replace(tbTroom.Text, "'", "''") & "','" & dateBirthday & "','" & Replace(tbNote.Text, "'", "''") & "','" & dlStatus.SelectedValue & "') "
        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('Data has been saved !');", True)
        'Response.Redirect("iPxCrmCustomer.aspx")
        Session("sMessage") = "Data has been saved !| ||"
        Session("sWarningID") = "0"
        Session("sUrlOKONLY") = "iPxCrmCustomer.aspx"
        Session("sUrlYES") = "http://www.thepyxis.net"
        Session("sUrlNO") = "http://www.thepyxis.net"
        Response.Redirect("warningmsg.aspx")
    End Sub

    Sub updateCustomer()
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        Dim dateBirthday As Date = Date.ParseExact(tbAnniversery.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
        sSQL = "UPDATE CFG_customer SET CustGrpID= '" & dlCustGrub.SelectedValue & "', CustName= '" & Replace(tbName.Text, "'", "''") & "', Phone= '" & Replace(tbPhone.Text, "'", "''") & "', Fax= '" & Replace(tbFax.Text, "'", "''") & "', CountryID= '" & dlCountry.SelectedValue & "'"
        sSQL += ", Provid= '" & Trim(dlProvinsi.SelectedValue) & "', CityID= '" & Trim(dlCity.SelectedValue) & "', Address= '" & Replace(tbAddress.Text, "'", "''") & "', StarClass= '" & Replace(tbStar.Text, "'", "''") & "', Troom= '" & Replace(tbTroom.Text, "'", "''") & "' "
        sSQL += ", Anniversary= '" & dateBirthday & "', Notes= '" & Replace(tbNote.Text, "'", "''") & "', CustLevel= '" & dlStatus.SelectedValue & "'"
        sSQL += "WHERE CustID = '" & tbCustId.Text & "' "

        oSQLCmd.CommandText = sSQL
        oSQLCmd.ExecuteNonQuery()

        oCnct.Close()
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('Data has been update !');document.getElementById('Buttonx').click()", True)
        'Response.Redirect("iPxCrmCustomer.aspx")
        Session("sMessage") = "Data has been update !| ||"
        Session("sWarningID") = "0"
        Session("sUrlOKONLY") = "iPxCrmCustomer.aspx"
        Session("sUrlYES") = "http://www.thepyxis.net"
        Session("sUrlNO") = "http://www.thepyxis.net"
        Response.Redirect("warningmsg.aspx")
    End Sub

    Sub viewEdit()
        Dim oSQLReader As SqlDataReader
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT * FROM CFG_customer WHERE CustID = '" & Session("sCustEdit") & "'"
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            tbCustId.Text = oSQLReader.Item("CustID").ToString
            dlCustGrub.SelectedValue = oSQLReader.Item("CustGrpID").ToString
            tbName.Text = oSQLReader.Item("CustName").ToString
            tbPhone.Text = oSQLReader.Item("Phone").ToString
            tbFax.Text = oSQLReader.Item("Fax").ToString
            dlCountry.SelectedValue = oSQLReader.Item("CountryID").ToString
            Session("sProvi") = oSQLReader.Item("Provid").ToString
            Session("sCity") = oSQLReader.Item("CityID").ToString
            tbAddress.Text = oSQLReader.Item("Address").ToString
            tbStar.Text = oSQLReader.Item("StarClass").ToString
            tbTroom.Text = oSQLReader.Item("Troom").ToString
            Dim dateBirthday As Date = oSQLReader.Item("Anniversary").ToString
            tbAnniversery.Text = dateBirthday.ToString("dd/MM/yyyy")
            tbNote.Text = oSQLReader.Item("Notes").ToString
            dlStatus.SelectedValue = oSQLReader.Item("CustLevel").ToString
            oCnct.Close()
        Else

        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            If Session("sCustEdit") = "" Then
                CustGrub()
                If Session("sGid") <> "" Then
                    dlCustGrub.SelectedValue = Session("sGid")
                End If
                country()
                provinsdefault()
                citydefault()
                CustStatus()
                'kode()
                tbCustId.Text = cIpx.GetCounterSM("C", "C")
                dlCountry.SelectedValue = "IDN"
                dlProvinsi.SelectedValue = "DKI   "
                dlCity.SelectedValue = "JKT   "
            Else
                CustGrub()
                country()
                CustStatus()
                viewEdit()
                provins()
                dlProvinsi.SelectedValue = Session("sProvi")
                city()
                dlCity.SelectedValue = Session("sCity")
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "date", "$(document).ready(function() {date()});", True)
        End If
    End Sub

    Protected Sub dlCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlCountry.SelectedIndexChanged
        provins()
        city()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "date", "$(document).ready(function() {date()});", True)
    End Sub

    Protected Sub dlProvinsi_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlProvinsi.SelectedIndexChanged
        city()
        'Dim a As String = dlProvinsi.SelectedValue.ToString
        'Dim b As String = Trim(a)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "date", "$(document).ready(function() {date()});", True)
    End Sub


    Protected Sub lbSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSave.Click
        If tbName.Text = Nothing Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('failed to save, please enter Customer Name !');document.getElementById('Buttonx').click()", True)
            tbName.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "date", "$(document).ready(function() {date()});", True)
        ElseIf tbPhone.Text = Nothing Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('failed to save, please enter Customer Phone !');document.getElementById('Buttonx').click()", True)
            tbPhone.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "date", "$(document).ready(function() {date()});", True)
        ElseIf dlProvinsi.Text = Nothing Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('failed to save, please choose a province !');document.getElementById('Buttonx').click()", True)
            dlProvinsi.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "date", "$(document).ready(function() {date()});", True)
        ElseIf dlCity.Text = Nothing Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('failed to save, please choose a city !');document.getElementById('Buttonx').click()", True)
            dlCity.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "date", "$(document).ready(function() {date()});", True)
        ElseIf tbAddress.Text = Nothing Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('failed to save, please enter Customer Address !');document.getElementById('Buttonx').click()", True)
            tbAddress.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "date", "$(document).ready(function() {date()});", True)
        ElseIf tbTroom.Text = Nothing Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('failed to save, please enter Total Room !');document.getElementById('Buttonx').click()", True)
            tbTroom.Focus()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "date", "$(document).ready(function() {date()});", True)
        ElseIf tbAnniversery.Text = Nothing Then
            tbAnniversery.Text = "01/01/1900"
        Else
            If oCnct.State = ConnectionState.Closed Then
                oCnct.Open()
            End If
            oSQLCmd = New SqlCommand(sSQL, oCnct)
            sSQL = "SELECT CustID  FROM CFG_customer WHERE CustID  = '" & tbCustId.Text & "'"
            oSQLCmd.CommandText = sSQL
            oSQLReader = oSQLCmd.ExecuteReader

            If oSQLReader.Read Then
                oSQLReader.Close()
                updateCustomer()
            Else
                oSQLReader.Close()
                saveCustomer()
            End If
            oCnct.Close()
        End If
    End Sub

    Protected Sub lbAbort_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAbort.Click
        Response.Redirect("iPxCrmCustomer.aspx")
    End Sub

    Protected Sub lbAddGroup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAddGroup.Click
        Response.Redirect("iPxCrmCustGrp.aspx")
    End Sub

    Protected Sub dlCustGrub_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlCustGrub.SelectedIndexChanged
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "date", "$(document).ready(function() {date()});", True)
    End Sub
End Class
