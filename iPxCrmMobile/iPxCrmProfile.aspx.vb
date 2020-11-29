Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Drawing
Imports System.Configuration
Partial Class iPxCrmMobile_iPxCrmProfile
    Inherits System.Web.UI.Page
    Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
    Dim oCnct As SqlConnection = New SqlConnection(sCnct)
    Dim oSQLCmd As SqlCommand
    Dim oSQLReader As SqlDataReader
    Dim sSQL, Ticketno, i As String
    Sub listProfile()
        Dim oSQLReader As SqlDataReader
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        sSQL = "SELECT CFG_customerContact.CustID, CFG_customerContact.Name, CFG_customerContactGrp.Description, CFG_customerContact.Phone, "
        sSQL += " CFG_customerContact.Email, CFG_customerContact.Birthday, CFG_customer.CustName, CFG_customer.Address, CFG_geog_country.country, "
        sSQL += " CFG_geog_province.description, CFG_geog_city.city, CFG_customer.StarClass "
        sSQL += " FROM CFG_customerContact INNER JOIN CFG_customer ON CFG_customerContact.CustID = CFG_customer.CustID INNER JOIN CFG_geog_country ON CFG_geog_country.countryid = CFG_customer.CountryID "
        sSQL += " INNER JOIN CFG_customerContactGrp ON CFG_customerContact.ContactGrpID = CFG_customerContactGrp.ContactGrpID "
        sSQL += " INNER JOIN CFG_geog_province ON CFG_geog_province.provid = CFG_customer.provid INNER JOIN CFG_geog_city ON CFG_geog_city.cityid = CFG_customer.CityID "
        sSQL += " WHERE NameCode ='" & Session("sCNameCode") & "' "
        oSQLCmd.CommandText = sSQL
        oSQLReader = oSQLCmd.ExecuteReader

        oSQLReader.Read()
        If oSQLReader.HasRows Then
            Label1.Text = oSQLReader.Item("CustID").ToString
            Label2.Text = oSQLReader.Item("Name").ToString
            Label3.Text = oSQLReader.Item("Description").ToString
            Label4.Text = oSQLReader.Item("Phone").ToString
            Label5.Text = oSQLReader.Item("Email").ToString
            Dim dateBirthday As Date = oSQLReader.Item("Birthday").ToString
            Label6.Text = dateBirthday.ToString("dd/MM/yyyy")
            Label7.Text = oSQLReader.Item("CustName").ToString
            Label8.Text = oSQLReader.Item("Address").ToString
            Label9.Text = oSQLReader.Item("country").ToString
            Label10.Text = oSQLReader.Item("description").ToString
            Label11.Text = oSQLReader.Item("city").ToString
            Label12.Text = oSQLReader.Item("StarClass").ToString
            oCnct.Close()
        Else
            'Refreshh()
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("sCId") = Nothing Then
            Response.Redirect("SignIn.aspx")
        Else
            Dim cph As ContentPlaceHolder = DirectCast(Me.Master.FindControl("ContentPlaceHolder2"), ContentPlaceHolder)
            Dim LinkButton As LinkButton = DirectCast(cph.FindControl("lnkback"), LinkButton)

            LinkButton.PostBackUrl = "iPxCrmHomeMobile.aspx"
            If Not Me.IsPostBack Then
                listProfile()
            End If
        End If
    End Sub

    Protected Sub lbSunting_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbSunting.Click
        Response.Redirect("iPxCrmProffile.aspx")
    End Sub
End Class
