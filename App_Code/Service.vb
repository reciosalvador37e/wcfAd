Imports System.DirectoryServices

Namespace Services
    Public Class Service
        Implements IService

        Const username As String = "searchUserAd"
        Const pwd As String = "passSearchUserAd"
        Const strPath As String = "LDAP://active.directory.es/DC=active,DC=directory,DC=es"
        Const strDomain As String = "ACTIVE"
        ReadOnly domainAndUsername As String

        Public Sub New()
            domainAndUsername = strDomain & "\" & username
        End Sub

        Public Function AutenticacionLDAP(ByVal user As String, ByVal pass As String) As UserData Implements IService.AuthenticationAD
            Dim i As Integer = 0
            Dim sw As Integer = 0
            Dim domainAndUsername As String = strDomain & "\" & user
            Dim entry As DirectoryEntry = New DirectoryEntry(strPath, user, pass)

            Try
                Dim obj As Object = entry.NativeObject
                Dim search As DirectorySearcher = New DirectorySearcher(entry)
                search.Filter = "(&(objectCategory=user)(objectClass=user)(SAMAccountName=" & user & "))"
                search.PropertiesToLoad.Add("memberOf")
                Dim result As SearchResultCollection = search.FindAll()
                Dim name As String = ""
                Dim expires As String = ""
                Dim sn As String = ""
                Dim givenName As String = ""
                For Each resent In search.FindAll
                    name = resent.GetDirectoryEntry.Properties("name").Value().ToString
                    expires = resent.GetDirectoryEntry.Properties("userAccountControl").Value().ToString
                    givenName = resent.GetDirectoryEntry.Properties("givenName").Value().ToString
                    sn = resent.GetDirectoryEntry.Properties("sn").Value().ToString
                Next
                Dim du As UserData = New UserData With {
                    .UserName = name,
                    .AccountExpire = expires,
                    .GivenName = givenName,
                    .Sn = sn
                }
                Return du
            Catch ex As Exception
                Dim du As UserData = New UserData With {
                    .UserName = ex.Message,
                    .AccountExpire = "",
                    .GivenName = "",
                    .Sn = ""
                }
                Return du
            End Try
        End Function


    End Class

End Namespace

