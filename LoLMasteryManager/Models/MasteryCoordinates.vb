Imports Newtonsoft.Json

Partial Public Module Models

    <JsonObject(MemberSerialization:=MemberSerialization.OptIn)>
    Public Class MasteryCoordinatesFile

        <JsonProperty(PropertyName:="reference_client_size", Required:=Required.AllowNull)>
        Public ReferenceClientSize As Size

        <JsonProperty(PropertyName:="mastery_coordinates_menu", Required:=Required.AllowNull)>
        Public MasteryCoordinatesMenu As New Dictionary(Of String, Point)

        <JsonProperty(PropertyName:="mastery_coordinates_champion_select", Required:=Required.AllowNull)>
        Public MasteryCoordinatesChampionSelect As New Dictionary(Of String, Point)

        <JsonProperty(PropertyName:="mastery_coordinates_champion_select_old", Required:=Required.AllowNull)>
        Public MasteryCoordinatesChampionSelect_Old As New Dictionary(Of String, Point)

    End Class

End Module
