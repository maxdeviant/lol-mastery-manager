Imports Newtonsoft.Json

Partial Public Module Models

    <JsonObject(MemberSerialization:=MemberSerialization.OptIn)>
    Public Class Metadata

        <JsonProperty(PropertyName:="patch_number", Required:=Required.AllowNull)>
        Public PatchNumber As String

        <JsonProperty(PropertyName:="last_updated", Required:=Required.AllowNull)>
        Public LastUpdated As New Date

    End Class

End Module

