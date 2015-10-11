Imports Newtonsoft.Json

Partial Public Module Models

    <JsonObject(MemberSerialization:=MemberSerialization.OptIn)>
    Public Class MasteryPage

        <JsonProperty(PropertyName:="name", Required:=Required.AllowNull)>
        Public Name As String

        <JsonProperty(PropertyName:="offense", Required:=Required.AllowNull)>
        Public OffenseTree As New List(Of Mastery)

        <JsonProperty(PropertyName:="defense", Required:=Required.AllowNull)>
        Public DefenseTree As New List(Of Mastery)

        <JsonProperty(PropertyName:="utility", Required:=Required.AllowNull)>
        Public UtilityTree As New List(Of Mastery)

    End Class

End Module


