Imports System.Runtime.InteropServices

Public Class MasteryAssigner

    <StructLayout(LayoutKind.Sequential)>
    Public Structure Rectangle

        Public Left As Integer
        Public Top As Integer
        Public Right As Integer
        Public Bottom As Integer

    End Structure


    Public Function Assign(ByVal masteryPage As MasteryPage) As Boolean

        Try

            Dim bResult As Boolean

            GetLeagueClientWindowPosition()

            Return bResult

        Catch ex As Exception

            Throw

        End Try

    End Function

    Private Function GetLeagueClientWindowPosition() As Point

        Try

            Dim oLeagueWindow As IntPtr = HwndInterface.GetHwndFromTitle("PVP.net Client")

            HwndInterface.ActivateWindow(oLeagueWindow)

            Return HwndInterface.GetHwndPos(oLeagueWindow)

        Catch ex As Exception

            Throw

        End Try

    End Function

End Class
