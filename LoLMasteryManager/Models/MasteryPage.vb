Public Structure MasteryTree

    Public Shared Offense As String = "Offense"
    Public Shared Defense As String = "Defense"
    Public Shared Utility As String = "Utility"

End Structure

Public Class MasteryPage

    Public Name As String

    Public OffenseTree As New List(Of Mastery)

    Public DefenseTree As New List(Of Mastery)

    Public UtilityTree As New List(Of Mastery)

End Class
