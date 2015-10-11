Imports Newtonsoft.Json

Partial Public Module Models

    <JsonObject(MemberSerialization:=MemberSerialization.OptIn)>
    Public Class Role

        <JsonProperty(PropertyName:="name", Required:=Required.AllowNull)>
        Public Name As String

        <JsonProperty(PropertyName:="rate", Required:=Required.AllowNull)>
        Public Rate As Double

        Public Overrides Function ToString() As String

            Return Name

        End Function

    End Class

End Module
