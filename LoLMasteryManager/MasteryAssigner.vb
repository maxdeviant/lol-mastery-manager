Imports WindowsInput

Public Class MasteryAssigner

    Private Structure MasteryTreeOffsets

        Public Structure Offense

            Public Const X As Integer = 355
            Public Const Y As Integer = 165

        End Structure

        Public Structure Defense

            Public Const X As Integer = 630
            Public Const Y As Integer = 165

        End Structure

        Public Structure Utility

            Public Const X As Integer = 905
            Public Const Y As Integer = 165

        End Structure

    End Structure

    Private Structure MasteryTree

        Public Const Width As Integer = -1
        Public Const Height As Integer = -1

    End Structure

    Private Structure MasteryNode

        Public Const Width As Integer = 50
        Public Const Height As Integer = 50

        Public Structure Margin

            Public Const X As Integer = 12
            Public Const Y As Integer = 22

        End Structure

    End Structure

    Public Function Assign(ByVal masteryPage As MasteryPage) As Boolean

        Try

            Dim bResult As Boolean

            AssignMasteries(masteryPage)

            Return bResult

        Catch ex As Exception

            Throw

        End Try

    End Function

    Private Function AssignMasteries(ByVal masteryPage As MasteryPage) As Boolean

        Try

            Dim bResult As Boolean

            For Each oMastery As Mastery In masteryPage.OffenseTree

                AssignMastery(oMastery)

            Next oMastery

            For Each oMastery As Mastery In masteryPage.DefenseTree

                AssignMastery(oMastery)

            Next oMastery

            For Each oMastery As Mastery In masteryPage.UtilityTree

                AssignMastery(oMastery)

            Next oMastery

            Return bResult

        Catch ex As Exception

            Throw

        End Try

    End Function

    Private Sub AssignMastery(ByVal mastery As Mastery)

        Try

            If mastery.Ranks > 0 Then

                Dim oMasteryPosition = GetMasteryPosition(mastery)

                Mouse.Move(oMasteryPosition)

                For iRank As Integer = 0 To mastery.Ranks Step 1

                    Mouse.LeftClick()

                Next iRank

            End If

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Private Function GetMasteryPosition(ByVal mastery As Mastery) As Point

        Try

            Dim sID As String = mastery.ID.ToString

            Dim iMasteryTree As Integer = Val(sID(1))
            Dim iMasteryRow As Integer = Val(sID(2))
            Dim iMasteryColumn As Integer = Val(sID(3))

            Dim oClientPosition As Point = GetLeagueClientWindowPosition()

            Dim iMasteryTreeOffsetX As Integer
            Dim iMasteryTreeOffsetY As Integer

            Select Case iMasteryTree

                Case 1
                    iMasteryTreeOffsetX = MasteryTreeOffsets.Offense.X
                    iMasteryTreeOffsetY = MasteryTreeOffsets.Offense.Y

                Case 2
                    iMasteryTreeOffsetX = MasteryTreeOffsets.Defense.X
                    iMasteryTreeOffsetY = MasteryTreeOffsets.Defense.Y

                Case 3
                    iMasteryTreeOffsetX = MasteryTreeOffsets.Utility.X
                    iMasteryTreeOffsetY = MasteryTreeOffsets.Utility.Y

            End Select

            Dim iPositionX As Integer = oClientPosition.X + iMasteryTreeOffsetX + ((iMasteryColumn - 1) * MasteryNode.Width) + (iMasteryColumn * MasteryNode.Margin.X) + (MasteryNode.Width \ 2)
            Dim iPositionY As Integer = oClientPosition.Y + iMasteryTreeOffsetY + ((iMasteryRow - 1) * MasteryNode.Height) + (iMasteryRow * MasteryNode.Margin.Y) + (MasteryNode.Height \ 2)

            Debug.WriteLine(New Point(iPositionX, iPositionY))

            Return New Point(iPositionX, iPositionY)

        Catch ex As Exception

            Throw

        End Try

    End Function

    Private Function GetLeagueClient() As IntPtr

        Try

            Return HwndInterface.GetHwndFromTitle("PVP.net Client")

        Catch ex As Exception

            Throw

        End Try

    End Function

    Private Function GetLeagueClientWindowPosition() As Point

        Try

            Dim oLeagueWindow As IntPtr = GetLeagueClient()

            HwndInterface.ActivateWindow(GetLeagueClient())

            Return HwndInterface.GetHwndPos(oLeagueWindow)

        Catch ex As Exception

            Throw

        End Try

    End Function

End Class
