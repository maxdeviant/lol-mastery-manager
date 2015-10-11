Partial Public Module Models

    Public Structure MasteryTree

        Public Const Offense As String = "Offense"
        Public Const Defense As String = "Defense"
        Public Const Utility As String = "Utility"

    End Structure

    Public Class MasteryPage

        Public Name As String

        Public OffenseTree As New List(Of Mastery)

        Public DefenseTree As New List(Of Mastery)

        Public UtilityTree As New List(Of Mastery)

    End Class

End Module


