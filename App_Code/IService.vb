Namespace Services
    <ServiceContract()>
    Public Interface IService

        <OperationContract()>
        <WebInvoke(Method:="POST", UriTemplate:="/credentials/{user}/{pass}", RequestFormat:=WebMessageFormat.Json, ResponseFormat:=WebMessageFormat.Json)>
        Function AuthenticationAD(ByVal user As String, ByVal pass As String) As UserData



    End Interface

    <DataContract>
    Public Class UserData
        <DataMember>
        Public Property UserName As String
        <DataMember>
        Public Property AccountExpire As String
        <DataMember>
        Public Property GivenName As String
        <DataMember>
        Public Property Sn As String
    End Class



End Namespace