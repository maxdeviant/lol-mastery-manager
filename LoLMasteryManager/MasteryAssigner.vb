Public Class MasteryAssigner

    Private Structure MasteryTreeOffsets

        Public Structure ChampionSelect

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

        Public Structure Menu

            Public Structure Offense

                Public Const X As Integer = 345
                Public Const Y As Integer = 245

            End Structure

            Public Structure Defense

                Public Const X As Integer = 620
                Public Const Y As Integer = 245

            End Structure

            Public Structure Utility

                Public Const X As Integer = 895
                Public Const Y As Integer = 245

            End Structure

        End Structure

    End Structure

    Private Structure MasteryNode

        Public Const Width As Integer = 50
        Public Const Height As Integer = 50

        Public Structure Margin

            Public Const X As Integer = 12
            Public Const Y As Integer = 22

        End Structure

    End Structure

    Private _Mode As Modes = Modes.ChampionSelect

    Public Sub SetMode(ByVal mode As Modes)

        _Mode = mode

    End Sub

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

            Mouse.Lock()

            For Each oMastery As Mastery In masteryPage.OffenseTree

                AssignMastery(oMastery)

            Next oMastery

            For Each oMastery As Mastery In masteryPage.DefenseTree

                AssignMastery(oMastery)

            Next oMastery

            For Each oMastery As Mastery In masteryPage.UtilityTree

                AssignMastery(oMastery)

            Next oMastery

            Mouse.Unlock()

            Return bResult

        Catch ex As Exception

            Throw

        End Try

    End Function

    Private Sub AssignMastery(ByVal mastery As Mastery)

        Try

            If mastery.Ranks > 0 Then

                Dim oMasteryPosition = GetMasteryNodePosition(mastery)

                Mouse.Move(oMasteryPosition)

                Threading.Thread.Sleep(100)

                For iRank As Integer = 0 To mastery.Ranks - 1 Step 1

                    Mouse.LeftClick()

                    Threading.Thread.Sleep(100)

                Next iRank

            End If

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Private Function GetMasteryNodePosition(ByVal mastery As Mastery) As Point

        Try

            Dim sID As String = mastery.ID.ToString

            Dim iMasteryTree As Integer = Val(sID(1))
            Dim iMasteryRow As Integer = Val(sID(2))
            Dim iMasteryColumn As Integer = Val(sID(3))

            Dim oClientPosition As Point = GetLeagueClientWindowPosition()
            Dim oMasteryTreeOffset As Point = CalculateMasteryTreeOffsets(iMasteryTree)

            Dim iPositionX As Integer = oClientPosition.X + oMasteryTreeOffset.X + ((iMasteryColumn - 1) * MasteryNode.Width) + (iMasteryColumn * MasteryNode.Margin.X) + (MasteryNode.Width \ 2)
            Dim iPositionY As Integer = oClientPosition.Y + oMasteryTreeOffset.Y + ((iMasteryRow - 1) * MasteryNode.Height) + (iMasteryRow * MasteryNode.Margin.Y) + (MasteryNode.Height \ 2)

            Debug.WriteLine(New Point(iPositionX, iPositionY))

            Return New Point(iPositionX, iPositionY)

        Catch ex As Exception

            Throw

        End Try

    End Function

    Private Function CalculateMasteryTreeOffsets(ByVal masteryTree As Integer) As Point

        Try

            Dim iMasteryTreeOffsetX As Integer
            Dim iMasteryTreeOffsetY As Integer

            Select Case masteryTree

                Case 1

                    If _Mode = Modes.ChampionSelect Then

                        iMasteryTreeOffsetX = MasteryTreeOffsets.ChampionSelect.Offense.X
                        iMasteryTreeOffsetY = MasteryTreeOffsets.ChampionSelect.Offense.Y

                    ElseIf _Mode = Modes.Menu Then

                        iMasteryTreeOffsetX = MasteryTreeOffsets.Menu.Offense.X
                        iMasteryTreeOffsetY = MasteryTreeOffsets.Menu.Offense.Y

                    End If

                Case 2

                    If _Mode = Modes.ChampionSelect Then

                        iMasteryTreeOffsetX = MasteryTreeOffsets.ChampionSelect.Defense.X
                        iMasteryTreeOffsetY = MasteryTreeOffsets.ChampionSelect.Defense.Y

                    ElseIf _Mode = Modes.Menu Then

                        iMasteryTreeOffsetX = MasteryTreeOffsets.Menu.Defense.X
                        iMasteryTreeOffsetY = MasteryTreeOffsets.Menu.Defense.Y

                    End If

                Case 3

                    If _Mode = Modes.ChampionSelect Then

                        iMasteryTreeOffsetX = MasteryTreeOffsets.ChampionSelect.Utility.X
                        iMasteryTreeOffsetY = MasteryTreeOffsets.ChampionSelect.Utility.Y

                    ElseIf _Mode = Modes.Menu Then

                        iMasteryTreeOffsetX = MasteryTreeOffsets.Menu.Utility.X
                        iMasteryTreeOffsetY = MasteryTreeOffsets.Menu.Utility.Y

                    End If

            End Select

            Return New Point(iMasteryTreeOffsetX, iMasteryTreeOffsetY)

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
