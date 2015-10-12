Public Class MasteryAssigner

    Private Structure SaveMasteriesButton

        Public Const Width As Integer = 175
        Public Const Height As Integer = 28

        Public Structure Offsets

            Public Structure ChampionSelect

                Public Const X As Integer = 145
                Public Const Y As Integer = 297

            End Structure

            Public Structure Menu

                Public Const X As Integer = 135
                Public Const Y As Integer = 377

            End Structure

        End Structure

    End Structure

    Private Structure ReturnPointsButton

        Public Const Width As Integer = 175
        Public Const Height As Integer = 28

        Public Structure Offsets

            Public Structure ChampionSelect

                Public Const X As Integer = 145
                Public Const Y As Integer = 327

            End Structure

            Public Structure Menu

                Public Const X As Integer = 135
                Public Const Y As Integer = 407

            End Structure

        End Structure

    End Structure

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

        Public Structure Scale

            Public Const X As Double = 25.6
            Public Const Y As Double = 16

        End Structure

        Public Structure Margin

            Public Const X As Integer = 12
            Public Const Y As Integer = 22

        End Structure

    End Structure

    Private _Mode As Modes = Modes.ChampionSelect

    Public Sub SetMode(ByVal mode As Modes)

        _Mode = mode

    End Sub

    Public Sub Assign(ByVal masteryPage As MasteryPage)

        Try

            ReturnMasteryPoints()

            AssignMasteries(masteryPage)

            SaveMasteries()

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Private Sub ReturnMasteryPoints()

        Try

            Dim oClientPosition As Point = GetLeagueClientWindowPosition()
            Dim oReturnPointsButtonOffsets As Point = CalculateReturnPointsButtonOffsets()

            Dim oPosition As New Point

            oPosition.X = oClientPosition.X + oReturnPointsButtonOffsets.X + (ReturnPointsButton.Width \ 2)
            oPosition.Y = oClientPosition.Y + oReturnPointsButtonOffsets.Y + (ReturnPointsButton.Height \ 2)

            Mouse.Lock()

            Mouse.Move(oPosition)

            Threading.Thread.Sleep(100)

            Mouse.LeftClick()

            Threading.Thread.Sleep(100)

            Mouse.Unlock()

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Private Sub SaveMasteries()

        Try

            Dim oClientPosition As Point = GetLeagueClientWindowPosition()
            Dim oSaveMasteriesButtonOffsets As Point = CalculateSaveMasteriesButtonOffsets()

            Dim oPosition As New Point

            oPosition.X = oClientPosition.X + oSaveMasteriesButtonOffsets.X + (SaveMasteriesButton.Width \ 2)
            oPosition.Y = oClientPosition.Y + oSaveMasteriesButtonOffsets.Y + (SaveMasteriesButton.Height \ 2)

            Mouse.Lock()

            Mouse.Move(oPosition)

            Threading.Thread.Sleep(100)

            Mouse.LeftClick()

            Threading.Thread.Sleep(100)

            Mouse.Unlock()

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Private Sub AssignMasteries(ByVal masteryPage As MasteryPage)

        Try

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

        Catch ex As Exception

            Throw

        End Try

    End Sub

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
            Dim oMasteryNodeSize As Size = CalculateMasteryNodeSize()

            Dim oPosition As New Point

            oPosition.X = oClientPosition.X + oMasteryTreeOffset.X + ((iMasteryColumn - 1) * oMasteryNodeSize.Width) + (iMasteryColumn * MasteryNode.Margin.X) + (oMasteryNodeSize.Width \ 2)
            oPosition.Y = oClientPosition.Y + oMasteryTreeOffset.Y + ((iMasteryRow - 1) * oMasteryNodeSize.Height) + (iMasteryRow * MasteryNode.Margin.Y) + (oMasteryNodeSize.Height \ 2)

            Return oPosition

        Catch ex As Exception

            Throw

        End Try

    End Function

    Private Function CalculateMasteryNodeSize() As Size

        Try

            Dim oLeagueClientWindowSize As Size = GetLeagueClientWindowSize()

            Dim iMasteryNodeWidth As Integer = CInt(Math.Round(oLeagueClientWindowSize.Width / 25.6))
            Dim iMasteryNodeHeight As Integer = CInt(Math.Round(oLeagueClientWindowSize.Height / 16))

            Return New Size(iMasteryNodeWidth, iMasteryNodeHeight)

        Catch ex As Exception

            Throw

        End Try

    End Function

    Private Function CalculateSaveMasteriesButtonOffsets() As Point

        Try

            Dim oSaveMasteriesButtonOffsets As New Point

            If _Mode = Modes.ChampionSelect Then

                oSaveMasteriesButtonOffsets.X = SaveMasteriesButton.Offsets.ChampionSelect.X
                oSaveMasteriesButtonOffsets.Y = SaveMasteriesButton.Offsets.ChampionSelect.Y

            ElseIf _Mode = Modes.Menu Then

                oSaveMasteriesButtonOffsets.X = SaveMasteriesButton.Offsets.Menu.X
                oSaveMasteriesButtonOffsets.Y = SaveMasteriesButton.Offsets.Menu.Y

            End If

            Return oSaveMasteriesButtonOffsets

        Catch ex As Exception

            Throw

        End Try

    End Function

    Private Function CalculateReturnPointsButtonOffsets() As Point

        Try

            Dim oReturnPointsButtonOffsets As New Point

            If _Mode = Modes.ChampionSelect Then

                oReturnPointsButtonOffsets.X = ReturnPointsButton.Offsets.ChampionSelect.X
                oReturnPointsButtonOffsets.Y = ReturnPointsButton.Offsets.ChampionSelect.Y

            ElseIf _Mode = Modes.Menu Then

                oReturnPointsButtonOffsets.X = ReturnPointsButton.Offsets.Menu.X
                oReturnPointsButtonOffsets.Y = ReturnPointsButton.Offsets.Menu.Y

            End If

            Return oReturnPointsButtonOffsets

        Catch ex As Exception

            Throw

        End Try

    End Function

    Private Function CalculateMasteryTreeOffsets(ByVal masteryTree As Integer) As Point

        Try

            Dim oMasteryTreeOffsets As New Point

            Select Case masteryTree

                Case 1

                    If _Mode = Modes.ChampionSelect Then

                        oMasteryTreeOffsets.X = MasteryTreeOffsets.ChampionSelect.Offense.X
                        oMasteryTreeOffsets.Y = MasteryTreeOffsets.ChampionSelect.Offense.Y

                    ElseIf _Mode = Modes.Menu Then

                        oMasteryTreeOffsets.X = MasteryTreeOffsets.Menu.Offense.X
                        oMasteryTreeOffsets.Y = MasteryTreeOffsets.Menu.Offense.Y

                    End If

                Case 2

                    If _Mode = Modes.ChampionSelect Then

                        oMasteryTreeOffsets.X = MasteryTreeOffsets.ChampionSelect.Defense.X
                        oMasteryTreeOffsets.Y = MasteryTreeOffsets.ChampionSelect.Defense.Y

                    ElseIf _Mode = Modes.Menu Then

                        oMasteryTreeOffsets.X = MasteryTreeOffsets.Menu.Defense.X
                        oMasteryTreeOffsets.Y = MasteryTreeOffsets.Menu.Defense.Y

                    End If

                Case 3

                    If _Mode = Modes.ChampionSelect Then

                        oMasteryTreeOffsets.X = MasteryTreeOffsets.ChampionSelect.Utility.X
                        oMasteryTreeOffsets.Y = MasteryTreeOffsets.ChampionSelect.Utility.Y

                    ElseIf _Mode = Modes.Menu Then

                        oMasteryTreeOffsets.X = MasteryTreeOffsets.Menu.Utility.X
                        oMasteryTreeOffsets.Y = MasteryTreeOffsets.Menu.Utility.Y

                    End If

            End Select

            Return oMasteryTreeOffsets

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

            HwndInterface.ActivateWindow(oLeagueWindow)

            Return HwndInterface.GetHwndPos(oLeagueWindow)

        Catch ex As Exception

            Throw

        End Try

    End Function

    Private Function GetLeagueClientWindowSize() As Size

        Try

            Dim oLeagueWindow As IntPtr = GetLeagueClient()

            HwndInterface.ActivateWindow(oLeagueWindow)

            Return HwndInterface.GetHwndSize(oLeagueWindow)

        Catch ex As Exception

            Throw

        End Try

    End Function

End Class
