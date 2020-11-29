Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class iPxClass
    Public Shared Function Date2IsoDate(ByVal cDt As String) As String
        Dim cIsoDate As String
        If Trim(cDt) = "" Then
            cIsoDate = "1900/01/01"
        Else
            cIsoDate = Right(cDt, 4) & "/" & Mid(cDt, 4, 2) & "/" & Left(cDt, 2)

        End If
        Date2IsoDate = cIsoDate
    End Function

    Public Shared Function GetNewBusinessID() As String
        Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
        Dim oCnct As SqlConnection = New SqlConnection(sCnct)
        Dim oSQLCmd As SqlCommand
        Dim oSQLReader As SqlDataReader
        Dim sSQL As String

        Dim nInt As Int16
        Dim cKey As String
        Dim lNext As Boolean

        Dim aRet() As String = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"}

        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If

LBL_Loop:
        lNext = True
        cKey = ""
        While lNext

            nInt = (Math.Ceiling(Rnd() * 35))
            If nInt >= 0 And nInt <= 35 Then
                cKey = cKey & aRet(nInt)
            End If
            If Len(cKey) = 6 Then
                lNext = False
            End If
        End While


        sSQL = "Select businessid FROM iPx_profile_client WHERE (businessid = '" & cKey & "')"
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        oSQLReader = oSQLCmd.ExecuteReader

        If oSQLReader.Read Then
            ' loop
            oSQLReader.Close()
            GoTo LBL_Loop
        End If
        oCnct.Close()

        GetNewBusinessID = cKey
    End Function
    Public Shared Function GetNewToken() As String
        Dim alphabets As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"

        Dim numbers As String = "1234567890"

        Dim characters As String = numbers

        characters += Convert.ToString(alphabets) & numbers

        Dim length As Integer = 6
        Dim otp As String = String.Empty
        For i As Integer = 0 To length - 1
            Dim character As String = String.Empty
            Do
                Dim index As Integer = New Random().Next(0, characters.Length)
                character = characters.ToCharArray()(index).ToString()
            Loop While otp.IndexOf(character) <> -1
            otp += character
        Next
        Dim voucherCode As String
        voucherCode = otp
        GetNewToken = otp


    End Function

    Public Shared Function GetCounterMBR(ByVal sId As String, ByVal cModule As String) As String
        Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
        Dim oCnct As SqlConnection = New SqlConnection(sCnct)
        Dim oSQLCmd As SqlCommand
        Dim oSQLReader As SqlDataReader
        Dim sSQL As String
        Dim oTransaction As SqlTransaction
        Dim cCounter As String
        Dim iCounter As Integer

        Dim iPrefix1 As Integer
        Dim iPrefix2 As Integer
        Dim iPrefix3 As Integer
        Dim cPrefix As String

        Dim aRet() As String = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"}

        ' Starting tahun 2017
        ' Hasil Bagi adalah 57 ( DIV ) - Setiap 35 Tahun Naik 1
        iPrefix1 = (Year(Now) \ 35) - 56
        iPrefix2 = Year(Now) Mod 35
        iPrefix3 = Month(Now)

        cPrefix = Trim(Str(iPrefix1)) & aRet(iPrefix2) & aRet(iPrefix3)

        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        sSQL = "select lastcounter from CFG_counter WHERE businessid='" & sId & "' and module='" & cModule & "' and prefix='" & cPrefix & "' "
        oTransaction = oCnct.BeginTransaction("GetCounter")

        oSQLCmd = New SqlCommand(sSQL, oCnct)
        oSQLCmd.Transaction = oTransaction

        oSQLReader = oSQLCmd.ExecuteReader

        If oSQLReader.Read Then
            iCounter = Val(oSQLReader.Item("lastcounter")) + 1
            oSQLReader.Close()
            oSQLCmd.CommandText = "update CFG_counter set lastcounter=lastcounter+1 WHERE businessid='" & sId & "' and module='" & cModule & "' and prefix='" & cPrefix & "' "
            oSQLCmd.ExecuteNonQuery()
        Else
            iCounter = 1
            oSQLReader.Close()
            oSQLCmd.CommandText = "insert into CFG_counter (businessid, module, prefix, lastcounter) values('" & sId & "','" & cModule & "','" & cPrefix & "',1) "
            oSQLCmd.ExecuteNonQuery()
        End If
        oTransaction.Commit()
        cCounter = cModule & cPrefix & Right("000000000" & Trim(Str(iCounter)), 9)
        GetCounterMBR = cCounter

    End Function

    Public Shared Function GetCounterSM(ByVal sId As String, ByVal cModule As String) As String
        Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
        Dim oCnct As SqlConnection = New SqlConnection(sCnct)
        Dim oSQLCmd As SqlCommand
        Dim oSQLReader As SqlDataReader
        Dim sSQL As String
        Dim oTransaction As SqlTransaction
        Dim cCounter As String
        Dim iCounter As Integer

        Dim iPrefix1 As Integer
        Dim iPrefix2 As Integer
        Dim iPrefix3 As Integer
        Dim cPrefix As String

        Dim aRet() As String = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"}

        ' Starting tahun 2017
        ' Hasil Bagi adalah 57 ( DIV ) - Setiap 35 Tahun Naik 1
        iPrefix1 = (Year(Now) \ 35) - 56
        iPrefix2 = Year(Now) Mod 35
        iPrefix3 = Month(Now)

        cPrefix = Trim(Str(iPrefix1)) & aRet(iPrefix2) & aRet(iPrefix3)

        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        sSQL = "select lastcounter from CFG_counter WHERE businessid='" & sId & "' and module='" & cModule & "' order by lastcounter desc"
        oTransaction = oCnct.BeginTransaction("GetCounter")

        oSQLCmd = New SqlCommand(sSQL, oCnct)
        oSQLCmd.Transaction = oTransaction

        oSQLReader = oSQLCmd.ExecuteReader

        If oSQLReader.Read Then
            iCounter = Val(oSQLReader.Item("lastcounter")) + 1
            oSQLReader.Close()
            oSQLCmd.CommandText = "update CFG_counter set lastcounter=lastcounter+1 WHERE businessid='" & sId & "' and module='" & cModule & "' "
            oSQLCmd.ExecuteNonQuery()
        Else
            iCounter = 1
            oSQLReader.Close()
            oSQLCmd.CommandText = "insert into CFG_counter (businessid, module, prefix, lastcounter) values('" & sId & "','" & cModule & "','" & "" & "',1) "
            oSQLCmd.ExecuteNonQuery()
        End If
        oTransaction.Commit()
        cCounter = cModule & Right("0000" & Trim(Str(iCounter)), 4)
        GetCounterSM = cCounter

    End Function

    Public Shared Function GetCounterMGR(ByVal sBusinessID As String, ByVal cModule As String) As String
        Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
        Dim oCnct As SqlConnection = New SqlConnection(sCnct)
        Dim oSQLCmd As SqlCommand
        Dim oSQLReader As SqlDataReader
        Dim sSQL As String
        Dim oTransaction As SqlTransaction
        Dim cCounter As String
        Dim iCounter As Integer

        Dim iPrefix1 As Integer
        Dim iPrefix2 As Integer
        Dim iPrefix3 As Integer
        Dim cPrefix As String

        Dim aRet() As String = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"}

        ' Starting tahun 2017
        ' Hasil Bagi adalah 57 ( DIV ) - Setiap 35 Tahun Naik 1
        iPrefix1 = (Year(Now) \ 35) - 56
        iPrefix2 = Year(Now) Mod 35
        iPrefix3 = Month(Now)

        cPrefix = Trim(Str(iPrefix1)) & aRet(iPrefix2) & aRet(iPrefix3)

        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        sSQL = "select lastcounter from iPxMGR_profile_counter WHERE businessid='" & sBusinessID & "' and module='" & cModule & "' and prefix='" & cPrefix & "' "
        oTransaction = oCnct.BeginTransaction("GetCounter")

        oSQLCmd = New SqlCommand(sSQL, oCnct)
        oSQLCmd.Transaction = oTransaction

        oSQLReader = oSQLCmd.ExecuteReader

        If oSQLReader.Read Then
            iCounter = Val(oSQLReader.Item("lastcounter")) + 1
            oSQLReader.Close()
            oSQLCmd.CommandText = "update iPxMGR_profile_counter set lastcounter=lastcounter+1 WHERE businessid='" & sBusinessID & "' and module='" & cModule & "' and prefix='" & cPrefix & "' "
            oSQLCmd.ExecuteNonQuery()
        Else
            iCounter = 1
            oSQLReader.Close()
            oSQLCmd.CommandText = "insert into iPxMGR_profile_counter (businessid, module, prefix, lastcounter) values('" & sBusinessID & "','" & cModule & "','" & cPrefix & "',1) "
            oSQLCmd.ExecuteNonQuery()
        End If
        oTransaction.Commit()
        cCounter = cModule & cPrefix & Right("00000" & Trim(Str(iCounter)), 5)
        GetCounterMGR = cCounter

    End Function
    Public Shared Function GetDefaultPassw() As String
        Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
        Dim oCnct As SqlConnection = New SqlConnection(sCnct)
        Dim oSQLCmd As SqlCommand
        Dim oSQLReader As SqlDataReader
        Dim sSQL As String

        Dim nInt As Int16
        Dim cKey As String
        Dim lNext As Boolean

        Dim aRet() As String = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"}

LBL_Loop:
        lNext = True
        cKey = ""
        While lNext

            nInt = (Math.Ceiling(Rnd() * 35))
            If nInt >= 0 And nInt <= 35 Then
                cKey = cKey & aRet(nInt)
            End If
            If Len(cKey) = 6 Then
                lNext = False
            End If
        End While
        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If
        sSQL = "Select Passw FROM CFG_customerContact WHERE (Passw = '" & cKey & "')"
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        oSQLReader = oSQLCmd.ExecuteReader

        If oSQLReader.Read Then
            ' loop
            oSQLReader.Close()
            GoTo LBL_Loop
        End If
        oCnct.Close()

        GetDefaultPassw = cKey
    End Function
    Public Shared Function GetUniqueNo() As String
        Dim sCnct As String = ConfigurationManager.ConnectionStrings("iPxCNCT").ToString
        Dim oCnct As SqlConnection = New SqlConnection(sCnct)
        Dim oSQLCmd As SqlCommand
        Dim oSQLReader As SqlDataReader
        Dim sSQL As String

        Dim nInt As Int16
        Dim cKey As String
        Dim lNext As Boolean

        Dim aRet() As String = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"}

        If oCnct.State = ConnectionState.Closed Then
            oCnct.Open()
        End If

LBL_Loop:
        lNext = True
        cKey = ""
        While lNext

            nInt = (Math.Ceiling(Rnd() * 35))
            If nInt >= 0 And nInt <= 35 Then
                cKey = cKey & aRet(nInt)
            End If
            If Len(cKey) = 6 Then
                lNext = False
            End If
        End While


        sSQL = "Select recID FROM CFG_user WHERE (recID = '" & cKey & "')"
        oSQLCmd = New SqlCommand(sSQL, oCnct)
        oSQLReader = oSQLCmd.ExecuteReader

        If oSQLReader.Read Then
            ' loop
            oSQLReader.Close()
            GoTo LBL_Loop
        End If
        oCnct.Close()

        GetUniqueNo = cKey
    End Function

End Class
