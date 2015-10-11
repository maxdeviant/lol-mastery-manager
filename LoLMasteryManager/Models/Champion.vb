Imports Newtonsoft.Json

Partial Public Module Models

    <JsonObject(MemberSerialization:=MemberSerialization.OptIn)>
    Public Class Champion

        <JsonProperty(PropertyName:="key", Required:=Required.AllowNull)>
        Public Key As String

        <JsonProperty(PropertyName:="name", Required:=Required.AllowNull)>
        Public Name As String

        <JsonProperty(PropertyName:="roles", Required:=Required.AllowNull)>
        Public Roles As List(Of Role)

        Public Overrides Function ToString() As String

            Return Name

        End Function

    End Class

End Module
