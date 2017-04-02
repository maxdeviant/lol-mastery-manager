Imports Newtonsoft.Json

Partial Public Module Models

    <JsonObject(MemberSerialization:=MemberSerialization.OptIn)>
    Public Class MasteryPage

        <JsonProperty(PropertyName:="champion_key", Required:=Required.AllowNull)>
        Public ChampionKey As String

        <JsonProperty(PropertyName:="champion_name", Required:=Required.AllowNull)>
        Public ChampionName As String

        <JsonProperty(PropertyName:="role", Required:=Required.AllowNull)>
        Public Role As String

        <JsonProperty(PropertyName:="stat", Required:=Required.AllowNull)>
        Public Stat As Stats

        <JsonProperty(PropertyName:="ferocity", Required:=Required.AllowNull)>
        Public FerocityTree As New List(Of Mastery)

        <JsonProperty(PropertyName:="resolve", Required:=Required.AllowNull)>
        Public ResolveTree As New List(Of Mastery)

        <JsonProperty(PropertyName:="cunning", Required:=Required.AllowNull)>
        Public CunningTree As New List(Of Mastery)

        Public ReadOnly Property Name As String
            Get
                ' String.Format("Patch {0}", _MasteryManager.PatchNumber)
                Return String.Format("[{0}][{2}]{1}", Stat.GetStatAbbreviation(), ChampionName, Role.GetRoleAbbreviation())

            End Get
        End Property

        Public ReadOnly Property FileName As String
            Get

                Return GenerateFileName(ChampionKey, Role, Stat)

            End Get
        End Property

        Friend Shared Function GenerateFileName(ByVal championKey As String, ByVal role As String, ByVal stat As Stats) As String

            Return String.Format("[{0}] {1} - {2}", stat.GetStatAbbreviation(), championKey, role)

        End Function

    End Class

End Module


