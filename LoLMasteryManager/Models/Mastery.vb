Imports Newtonsoft.Json

Partial Public Module Models

    <JsonObject(MemberSerialization:=MemberSerialization.OptIn)>
    Public Class Mastery

        <JsonProperty(PropertyName:="id", Required:=Required.AllowNull)>
        Public ID As Integer

        <JsonProperty(PropertyName:="tree", Required:=Required.AllowNull)>
        Public Tree As String

        <JsonProperty(PropertyName:="name", Required:=Required.AllowNull)>
        Public Name As String

        <JsonProperty(PropertyName:="ranks", Required:=Required.AllowNull)>
        Public Ranks As Integer

    End Class

End Module

