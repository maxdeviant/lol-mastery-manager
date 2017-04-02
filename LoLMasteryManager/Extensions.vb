Imports System.Runtime.CompilerServices

Public Module Extensions

    ''' <summary>
    ''' Gets the human-readable name for the given stat.
    ''' </summary>
    ''' <param name="stat">The stat value to get the name of.</param>
    ''' <returns>The human-readable name for the given stat.</returns>
    <Extension>
    Public Function GetName(ByRef stat As Stats) As String

        Try

            Dim sAbbreviation As String

            Select Case stat

                Case Stats.MostFrequent

                    sAbbreviation = "Most Frequent"

                Case Stats.HighestWin

                    sAbbreviation = "Highest Win"

                Case Else

                    Throw New ArgumentException(String.Format("Unrecognized stat type '{0}'.", stat))

            End Select

            Return sAbbreviation

        Catch ex As Exception

            Throw

        End Try

    End Function

    ''' <summary>
    ''' Gets the abbreviation for the given stat.
    ''' </summary>
    ''' <param name="stat">The stat value to get the abbreviation for.</param>
    ''' <returns>The abbreviation for the given stat.</returns>
    <Extension>
    Public Function GetStatAbbreviation(ByRef stat As Stats) As String

        Try

            Dim sAbbreviation As String

            Select Case stat

                Case Stats.MostFrequent

                    sAbbreviation = "MF"

                Case Stats.HighestWin

                    sAbbreviation = "HW"

                Case Else

                    Throw New ArgumentException(String.Format("Unrecognized stat type '{0}'.", stat))

            End Select

            Return sAbbreviation

        Catch ex As Exception

            Throw

        End Try

    End Function

    <Extension>
    Public Function GetRoleAbbreviation(ByRef role As String) As String

        Try

            Dim sAbbreviation As String

            Select Case role

                Case "Middle"

                    sAbbreviation = "MID"

                Case "Support"

                    sAbbreviation = "SUP"

                Case "Jungle"

                    sAbbreviation = "JNG"


                Case Else

                    sAbbreviation = role

            End Select

            Return sAbbreviation

        Catch ex As Exception

            Throw

        End Try
    End Function

End Module
