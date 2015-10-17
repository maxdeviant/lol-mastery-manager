Imports System.Runtime.CompilerServices

Public Module Extensions

    <Extension>
    Public Function GetName(ByRef e As Stats) As String

        Try

            Dim sAbbreviation As String

            Select Case e

                Case Stats.MostFrequent

                    sAbbreviation = "Most Frequent"

                Case Stats.HighestWin

                    sAbbreviation = "Highest Win"

                Case Else

                    Throw New ArgumentException(String.Format("Unrecognized stat type '{0}'.", e))

            End Select

            Return sAbbreviation

        Catch ex As Exception

            Throw

        End Try

    End Function

    <Extension>
    Public Function GetAbbreviation(ByRef e As Stats) As String

        Try

            Dim sAbbreviation As String

            Select Case e

                Case Stats.MostFrequent

                    sAbbreviation = "MF"

                Case Stats.HighestWin

                    sAbbreviation = "HW"

                Case Else

                    Throw New ArgumentException(String.Format("Unrecognized stat type '{0}'.", e))

            End Select

            Return sAbbreviation

        Catch ex As Exception

            Throw

        End Try

    End Function

End Module
