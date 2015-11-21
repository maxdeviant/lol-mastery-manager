Imports Newtonsoft.Json

Partial Public Module Models

    <JsonObject(MemberSerialization:=MemberSerialization.OptIn)>
    Public Class MasteryCoordinateListFile

        <JsonProperty(PropertyName:="reference_client_size", Required:=Required.AllowNull)>
        Public ReferenceClientSize As Size

        <JsonProperty(PropertyName:="mastery_coordinates", Required:=Required.AllowNull)>
        Public MasteryCoordinates As New Dictionary(Of String, Point)

    End Class

End Module
