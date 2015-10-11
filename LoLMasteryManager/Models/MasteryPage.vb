Imports Newtonsoft.Json

Partial Public Module Models

    Public Structure MasteryTree

        Public Const Offense As String = "Offense"
        Public Const Defense As String = "Defense"
        Public Const Utility As String = "Utility"

    End Structure

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


