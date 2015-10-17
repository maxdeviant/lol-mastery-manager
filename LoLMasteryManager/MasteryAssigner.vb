﻿Public Class MasteryAssigner

    Private _Mode As Modes = Modes.ChampionSelect

    Public Sub SetMode(ByVal mode As Modes)

        _Mode = mode

    End Sub

    Public Sub Assign(ByVal masteryPage As MasteryPage)

        Try

            ReturnMasteryPoints()

            AssignMasteries(masteryPage)

            NameMasteryPage(masteryPage.Name)

            SaveMasteries()

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Private Sub NameMasteryPage(ByVal name As String)

        Try

            Input.Lock()

            FocusMasteryPageNameInputBox()

            Keyboard.Type(name)

            Input.Unlock()

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Private Sub FocusMasteryPageNameInputBox()

        Try

            Dim oClientPosition As Point = GetLeagueClientWindowPosition()
            Dim oMasteryPageNameInputFieldOffset As Point = CalculateMasteryPageNameInputFieldOffsets()

            Dim oPosition As New Point

            oPosition.X = oClientPosition.X + oMasteryPageNameInputFieldOffset.X + (ClientMasteryPageNameInputField.Width \ 2)
            oPosition.Y = oClientPosition.Y + oMasteryPageNameInputFieldOffset.Y + (ClientMasteryPageNameInputField.Height \ 2)

            Input.Lock()

            Mouse.Move(oPosition)

            Threading.Thread.Sleep(100)

            Mouse.LeftClick()

            Threading.Thread.Sleep(100)

            Input.Unlock()

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Private Sub ReturnMasteryPoints()

        Try

            Dim oClientPosition As Point = GetLeagueClientWindowPosition()
            Dim oReturnPointsButtonOffsets As Point = CalculateReturnPointsButtonOffsets()

            Dim oPosition As New Point

            oPosition.X = oClientPosition.X + oReturnPointsButtonOffsets.X + (ClientReturnPointsButton.Width \ 2)
            oPosition.Y = oClientPosition.Y + oReturnPointsButtonOffsets.Y + (ClientReturnPointsButton.Height \ 2)

            Input.Lock()

            Mouse.Move(oPosition)

            Threading.Thread.Sleep(100)

            Mouse.LeftClick()

            Threading.Thread.Sleep(100)

            Input.Unlock()

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Private Sub SaveMasteries()

        Try

            Dim oClientPosition As Point = GetLeagueClientWindowPosition()
            Dim oSaveMasteriesButtonOffsets As Point = CalculateSaveMasteriesButtonOffsets()

            Dim oPosition As New Point

            oPosition.X = oClientPosition.X + oSaveMasteriesButtonOffsets.X + (ClientSaveMasteriesButton.Width \ 2)
            oPosition.Y = oClientPosition.Y + oSaveMasteriesButtonOffsets.Y + (ClientSaveMasteriesButton.Height \ 2)

            Input.Lock()

            Mouse.Move(oPosition)

            Threading.Thread.Sleep(100)

            Mouse.LeftClick()

            Threading.Thread.Sleep(100)

            Input.Unlock()

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Private Sub AssignMasteries(ByVal masteryPage As MasteryPage)

        Try

            Input.Lock()

            For Each oMastery As Mastery In masteryPage.OffenseTree

                AssignMastery(oMastery)

            Next oMastery

            For Each oMastery As Mastery In masteryPage.DefenseTree

                AssignMastery(oMastery)

            Next oMastery

            For Each oMastery As Mastery In masteryPage.UtilityTree

                AssignMastery(oMastery)

            Next oMastery

            Input.Unlock()

        Catch ex As Exception

            Input.Unlock()

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
            Dim oLeagueClientWindowSize As Size = GetLeagueClientWindowSize()
            Dim oMasteryTreeOffset As Point = CalculateMasteryTreeOffsets(iMasteryTree)
            Dim oMasteryNodeSize As Size = CalculateMasteryNodeSize()

            Dim oPosition As New Point

            If _Mode = Modes.ChampionSelect AndAlso oLeagueClientWindowSize.Width > 1200 Then

                oPosition.X = oClientPosition.X + oMasteryTreeOffset.X + ((iMasteryColumn - 1) * oMasteryNodeSize.Width) + (iMasteryColumn * CInt(oMasteryNodeSize.Width / 2.2))
                oPosition.Y = oClientPosition.Y + oMasteryTreeOffset.Y + ((iMasteryRow - 1) * oMasteryNodeSize.Height) + (iMasteryRow * CInt(oMasteryNodeSize.Height / 2.5))

            Else

                oPosition.X = oClientPosition.X + oMasteryTreeOffset.X + ((iMasteryColumn - 1) * oMasteryNodeSize.Width) + (iMasteryColumn * (oMasteryNodeSize.Width \ 2))
                oPosition.Y = oClientPosition.Y + oMasteryTreeOffset.Y + ((iMasteryRow - 1) * oMasteryNodeSize.Height) + (iMasteryRow * (oMasteryNodeSize.Height \ 2))

            End If

            Return oPosition

        Catch ex As Exception

            Throw

        End Try

    End Function

    Private Function CalculateMasteryNodeSize() As Size

        Try

            Dim oLeagueClientWindowSize As Size = GetLeagueClientWindowSize()

            Dim iMasteryNodeWidth As Integer = CInt(Math.Floor(oLeagueClientWindowSize.Width / ClientMasteryNode.X))
            Dim iMasteryNodeHeight As Integer = CInt(Math.Floor(oLeagueClientWindowSize.Height / ClientMasteryNode.Y))

            Return New Size(iMasteryNodeWidth, iMasteryNodeHeight)

        Catch ex As Exception

            Throw

        End Try

    End Function

    Private Function CalculateMasteryPageNameInputFieldOffsets() As Point

        Try

            Dim oLeagueClientWindowSize As Size = GetLeagueClientWindowSize()
            Dim oMasteryPageNameInputFieldOffsets As New Point

            If _Mode = Modes.ChampionSelect Then

                Select Case oLeagueClientWindowSize.Width

                    Case < ClientSize.Medium.Width

                        oMasteryPageNameInputFieldOffsets.X = CInt(Math.Floor(oLeagueClientWindowSize.Width / ClientMasteryPageNameInputField.ChampionSelect.Small.X))
                        oMasteryPageNameInputFieldOffsets.Y = CInt(Math.Floor(oLeagueClientWindowSize.Height / ClientMasteryPageNameInputField.ChampionSelect.Small.Y))

                    Case >= ClientSize.Medium.Width, < ClientSize.Large.Width

                        oMasteryPageNameInputFieldOffsets.X = CInt(Math.Floor(oLeagueClientWindowSize.Width / ClientMasteryPageNameInputField.ChampionSelect.Medium.X))
                        oMasteryPageNameInputFieldOffsets.Y = CInt(Math.Floor(oLeagueClientWindowSize.Height / ClientMasteryPageNameInputField.ChampionSelect.Medium.Y))

                    Case >= ClientSize.Large.Width

                        oMasteryPageNameInputFieldOffsets.X = CInt(Math.Floor(oLeagueClientWindowSize.Width / ClientMasteryPageNameInputField.ChampionSelect.Large.X))
                        oMasteryPageNameInputFieldOffsets.Y = CInt(Math.Floor(oLeagueClientWindowSize.Height / ClientMasteryPageNameInputField.ChampionSelect.Large.Y))

                End Select

            Else

                oMasteryPageNameInputFieldOffsets.X = CInt(Math.Floor(oLeagueClientWindowSize.Width / ClientMasteryPageNameInputField.Menu.X))
                oMasteryPageNameInputFieldOffsets.Y = CInt(Math.Floor(oLeagueClientWindowSize.Height / ClientMasteryPageNameInputField.Menu.Y))

            End If

            Return oMasteryPageNameInputFieldOffsets

        Catch ex As Exception

            Throw

        End Try

    End Function

    Private Function CalculateSaveMasteriesButtonOffsets() As Point

        Try

            Dim oLeagueClientWindowSize As Size = GetLeagueClientWindowSize()
            Dim oSaveMasteriesButtonOffsets As New Point

            If _Mode = Modes.ChampionSelect Then

                Select Case oLeagueClientWindowSize.Width

                    Case < ClientSize.Medium.Width

                        oSaveMasteriesButtonOffsets.X = CInt(Math.Floor(oLeagueClientWindowSize.Width / ClientSaveMasteriesButton.ChampionSelect.Small.X))
                        oSaveMasteriesButtonOffsets.Y = CInt(Math.Floor(oLeagueClientWindowSize.Height / ClientSaveMasteriesButton.ChampionSelect.Small.Y))

                    Case >= ClientSize.Medium.Width, < ClientSize.Large.Width

                        oSaveMasteriesButtonOffsets.X = CInt(Math.Floor(oLeagueClientWindowSize.Width / ClientSaveMasteriesButton.ChampionSelect.Medium.X))
                        oSaveMasteriesButtonOffsets.Y = CInt(Math.Floor(oLeagueClientWindowSize.Height / ClientSaveMasteriesButton.ChampionSelect.Medium.Y))

                    Case >= ClientSize.Large.Width

                        oSaveMasteriesButtonOffsets.X = CInt(Math.Floor(oLeagueClientWindowSize.Width / ClientSaveMasteriesButton.ChampionSelect.Large.X))
                        oSaveMasteriesButtonOffsets.Y = CInt(Math.Floor(oLeagueClientWindowSize.Height / ClientSaveMasteriesButton.ChampionSelect.Large.Y))

                End Select

            Else

                oSaveMasteriesButtonOffsets.X = CInt(Math.Floor(oLeagueClientWindowSize.Width / ClientSaveMasteriesButton.Menu.X))
                oSaveMasteriesButtonOffsets.Y = CInt(Math.Floor(oLeagueClientWindowSize.Height / ClientSaveMasteriesButton.Menu.Y))

            End If

            Return oSaveMasteriesButtonOffsets

        Catch ex As Exception

            Throw

        End Try

    End Function

    Private Function CalculateReturnPointsButtonOffsets() As Point

        Try

            Dim oLeagueClientWindowSize As Size = GetLeagueClientWindowSize()
            Dim oReturnPointsButtonOffsets As New Point

            If _Mode = Modes.ChampionSelect Then

                Select Case oLeagueClientWindowSize.Width

                    Case < ClientSize.Medium.Width

                        oReturnPointsButtonOffsets.X = CInt(Math.Floor(oLeagueClientWindowSize.Width / ClientReturnPointsButton.ChampionSelect.Small.X))
                        oReturnPointsButtonOffsets.Y = CInt(Math.Floor(oLeagueClientWindowSize.Height / ClientReturnPointsButton.ChampionSelect.Small.Y))

                    Case >= ClientSize.Medium.Width, < ClientSize.Large.Width

                        oReturnPointsButtonOffsets.X = CInt(Math.Floor(oLeagueClientWindowSize.Width / ClientReturnPointsButton.ChampionSelect.Medium.X))
                        oReturnPointsButtonOffsets.Y = CInt(Math.Floor(oLeagueClientWindowSize.Height / ClientReturnPointsButton.ChampionSelect.Medium.Y))

                    Case >= ClientSize.Large.Width

                        oReturnPointsButtonOffsets.X = CInt(Math.Floor(oLeagueClientWindowSize.Width / ClientReturnPointsButton.ChampionSelect.Large.X))
                        oReturnPointsButtonOffsets.Y = CInt(Math.Floor(oLeagueClientWindowSize.Height / ClientReturnPointsButton.ChampionSelect.Large.Y))

                End Select

            Else

                oReturnPointsButtonOffsets.X = CInt(Math.Floor(oLeagueClientWindowSize.Width / ClientReturnPointsButton.Menu.X))
                oReturnPointsButtonOffsets.Y = CInt(Math.Floor(oLeagueClientWindowSize.Height / ClientReturnPointsButton.Menu.Y))

            End If

            Return oReturnPointsButtonOffsets

        Catch ex As Exception

            Throw

        End Try

    End Function

    Private Function CalculateMasteryTreeOffsets(ByVal masteryTree As Integer) As Point

        Try

            Dim oLeagueClientWindowSize As Size = GetLeagueClientWindowSize()
            Dim oMasteryTreeOffsets As New Point

            If _Mode = Modes.ChampionSelect Then

                oMasteryTreeOffsets.X = CInt(Math.Floor(oLeagueClientWindowSize.Width / ClientMasteryTree.ChampionSelect.X) + Math.Floor(((masteryTree - 1) * (oLeagueClientWindowSize.Width / ClientMasteryTree.Width))))
                oMasteryTreeOffsets.Y = CInt(Math.Floor(oLeagueClientWindowSize.Height / ClientMasteryTree.ChampionSelect.Y))

            Else

                oMasteryTreeOffsets.X = CInt(Math.Floor(oLeagueClientWindowSize.Width / ClientMasteryTree.Menu.X) + Math.Floor((masteryTree - 1) * (oLeagueClientWindowSize.Width / ClientMasteryTree.Width)))
                oMasteryTreeOffsets.Y = CInt(Math.Floor(oLeagueClientWindowSize.Height / ClientMasteryTree.Menu.Y))

            End If

            Return oMasteryTreeOffsets

        Catch ex As Exception

            Throw

        End Try

    End Function

    Private Function GetLeagueClient() As IntPtr

        Try

            Return HwndInterface.GetHwndFromTitle(My.Resources.LeagueClientWindowTitle)

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
