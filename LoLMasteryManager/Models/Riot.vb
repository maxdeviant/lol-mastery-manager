Imports Newtonsoft.Json

<JsonObject(MemberSerialization:=MemberSerialization.OptIn)>
Public Class RiotChampionListFile

    <JsonProperty(PropertyName:="type", Required:=Required.AllowNull)>
    Public Type As String

    <JsonProperty(PropertyName:="version", Required:=Required.AllowNull)>
    Public Version As String

    <JsonProperty(PropertyName:="data", Required:=Required.AllowNull)>
    Public Champions As Dictionary(Of String, RiotChampion)

End Class

<JsonObject(MemberSerialization:=MemberSerialization.OptIn)>
Public Class RiotChampion

    <JsonProperty(PropertyName:="id", Required:=Required.AllowNull)>
    Public ID As Integer

    <JsonProperty(PropertyName:="key", Required:=Required.AllowNull)>
    Public Key As String

    <JsonProperty(PropertyName:="name", Required:=Required.AllowNull)>
    Public Name As String

    <JsonProperty(PropertyName:="title", Required:=Required.AllowNull)>
    Public Title As String

    <JsonProperty(PropertyName:="info", Required:=Required.AllowNull)>
    Public Info As RiotChampionInfo

    Public Overrides Function ToString() As String

        Return Name

    End Function

End Class

<JsonObject(MemberSerialization:=MemberSerialization.OptIn)>
Public Class RiotChampionInfo

    <JsonProperty(PropertyName:="attack", Required:=Required.AllowNull)>
    Public Attack As Integer

    <JsonProperty(PropertyName:="defense", Required:=Required.AllowNull)>
    Public Defense As Integer

    <JsonProperty(PropertyName:="magic", Required:=Required.AllowNull)>
    Public Magic As Integer

    <JsonProperty(PropertyName:="difficulty", Required:=Required.AllowNull)>
    Public Difficulty As Integer

End Class

<JsonObject(MemberSerialization:=MemberSerialization.OptIn)>
Public Class RiotMasteryListFile

    <JsonProperty(PropertyName:="type", Required:=Required.AllowNull)>
    Public Type As String

    <JsonProperty(PropertyName:="version", Required:=Required.AllowNull)>
    Public Version As String

    <JsonProperty(PropertyName:="tree", Required:=Required.AllowNull)>
    Public Tree As RiotMasteryTreeList

    <JsonProperty(PropertyName:="data", Required:=Required.AllowNull)>
    Public Masteries As Dictionary(Of String, RiotMastery)

End Class

<JsonObject(MemberSerialization:=MemberSerialization.OptIn)>
Public Class RiotMastery

    <JsonProperty(PropertyName:="id", Required:=Required.AllowNull)>
    Public ID As Integer

    '<JsonProperty(PropertyName:="masteryTree", Required:=Required.AllowNull)>
    'Public Tree As String

    <JsonProperty(PropertyName:="name", Required:=Required.AllowNull)>
    Public Name As String

    <JsonProperty(PropertyName:="description", Required:=Required.AllowNull)>
    Public Description As List(Of String)

    '<JsonProperty(PropertyName:="image", Required:=Required.AllowNull)>
    'Public Image As String

    <JsonProperty(PropertyName:="ranks", Required:=Required.AllowNull)>
    Public Ranks As Integer

    <JsonProperty(PropertyName:="prereq", Required:=Required.AllowNull)>
    Public Prereq As String

End Class

<JsonObject(MemberSerialization:=MemberSerialization.OptIn)>
Public Class RiotMasteryTreeList

    <JsonProperty(PropertyName:="Ferocity", Required:=Required.AllowNull)>
    Public Ferocity As List(Of List(Of RiotMasteryIdPrereq))

    <JsonProperty(PropertyName:="Cunning", Required:=Required.AllowNull)>
    Public Cunning As List(Of List(Of RiotMasteryIdPrereq))

    <JsonProperty(PropertyName:="Resolve", Required:=Required.AllowNull)>
    Public Resolve As List(Of List(Of RiotMasteryIdPrereq))

End Class

<JsonObject(MemberSerialization:=MemberSerialization.OptIn)>
Public Class RiotMasteryIdPrereq

    <JsonProperty(PropertyName:="masteryId", Required:=Required.AllowNull)>
    Public MasteryId As String

    <JsonProperty(PropertyName:="prereq", Required:=Required.AllowNull)>
    Public Prereq As String

End Class