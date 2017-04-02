﻿Imports System.IO
Imports Newtonsoft.Json

Public Class MasteryAssigner

    Private _MasteryCoordinatesListFile As MasteryCoordinatesFile
    Private _Mode As Modes = Modes.ChampionSelect

    Public Sub New(ByVal masteryCoordinatesPath As String)

        Dim sJson As String

        Using oStreamReader As New StreamReader(masteryCoordinatesPath)

            sJson = oStreamReader.ReadToEnd()

        End Using

        _MasteryCoordinatesListFile = JsonConvert.DeserializeObject(Of MasteryCoordinatesFile)(sJson)

    End Sub

    Public Sub SetMode(ByVal mode As Modes)

        _Mode = mode

    End Sub

    Public Sub Assign(ByVal masteryPage As MasteryPage, PatchNumber As String)

        Try

            Input.Lock()

            ReturnMasteryPoints()

            AssignMasteries(masteryPage)

            NameMasteryPage(masteryPage.Name + "-" + PatchNumber)

            SaveMasteries()

        Catch ex As Exception

            Throw

        Finally

            Input.Unlock()

        End Try

    End Sub

    Private Sub NameMasteryPage(ByVal name As String)

        Try

            FocusMasteryPageNameInputBox()

            Keyboard.Type(name)

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Private Sub FocusMasteryPageNameInputBox()

        Try

            Dim oClientPosition As Point = GetLeagueClientWindowPosition()
            Dim oMasteryPageNameInputFieldOffset As Point = CalculateMasteryPageNameInputFieldOffsets()

            Dim oPosition As New Point

            If Not _MasteryCoordinatesListFile.ReferenceCoordinatesAbsolute Then
                oPosition.X = oClientPosition.X + oMasteryPageNameInputFieldOffset.X + (ClientMasteryPageNameInputField.Width \ 2)
                oPosition.Y = oClientPosition.Y + oMasteryPageNameInputFieldOffset.Y + (ClientMasteryPageNameInputField.Height \ 2)
            Else
                'Dim oInputCoordinates = _MasteryCoordinatesListFile.MasteryCoordinatesMenu("Input")

                Dim oInputCoordinates As New Point

                Select Case _Mode

                    Case Modes.Menu

                        oInputCoordinates = _MasteryCoordinatesListFile.MasteryCoordinatesMenu("Input")

                    Case Modes.ChampionSelect

                        oInputCoordinates = _MasteryCoordinatesListFile.MasteryCoordinatesChampionSelect("Input")

                End Select

                oPosition.X = oInputCoordinates.X
                oPosition.Y = oInputCoordinates.Y
            End If


            Mouse.Move(oPosition)

            Threading.Thread.Sleep(200)

            Mouse.LeftClick()

            Threading.Thread.Sleep(200)

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Private Sub ReturnMasteryPoints()

        Try

            Dim oClientPosition As Point = GetLeagueClientWindowPosition()
            Dim oReturnPointsButtonOffsets As Point = CalculateReturnPointsButtonOffsets()

            Dim oPosition As New Point

            If Not _MasteryCoordinatesListFile.ReferenceCoordinatesAbsolute Then
                oPosition.X = oClientPosition.X + oReturnPointsButtonOffsets.X + (ClientReturnPointsButton.Width \ 2)
                oPosition.Y = oClientPosition.Y + oReturnPointsButtonOffsets.Y + (ClientReturnPointsButton.Height \ 2)
            Else
                'Dim oReturnCoordinates = _MasteryCoordinatesListFile.MasteryCoordinatesMenu("Return")

                Dim oReturnCoordinates As New Point

                Select Case _Mode

                    Case Modes.Menu

                        oReturnCoordinates = _MasteryCoordinatesListFile.MasteryCoordinatesMenu("Return")

                    Case Modes.ChampionSelect

                        oReturnCoordinates = _MasteryCoordinatesListFile.MasteryCoordinatesChampionSelect("Return")

                End Select

                oPosition.X = oReturnCoordinates.X
                oPosition.Y = oReturnCoordinates.Y
            End If



            Mouse.Move(oPosition)

            Threading.Thread.Sleep(200)

            Mouse.LeftClick()

            Threading.Thread.Sleep(200)

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Private Sub SaveMasteries()

        Try

            Dim oClientPosition As Point = GetLeagueClientWindowPosition()
            Dim oSaveMasteriesButtonOffsets As Point = CalculateSaveMasteriesButtonOffsets()

            Dim oPosition As New Point

            If Not _MasteryCoordinatesListFile.ReferenceCoordinatesAbsolute Then
                oPosition.X = oClientPosition.X + oSaveMasteriesButtonOffsets.X + (ClientSaveMasteriesButton.Width \ 2)
                oPosition.Y = oClientPosition.Y + oSaveMasteriesButtonOffsets.Y + (ClientSaveMasteriesButton.Height \ 2)
            Else
                'Dim oSaveCoordinates = _MasteryCoordinatesListFile.MasteryCoordinatesMenu("Save")

                Dim oSaveCoordinates As New Point

                Select Case _Mode

                    Case Modes.Menu

                        oSaveCoordinates = _MasteryCoordinatesListFile.MasteryCoordinatesMenu("Save")

                    Case Modes.ChampionSelect

                        oSaveCoordinates = _MasteryCoordinatesListFile.MasteryCoordinatesChampionSelect("Save")

                End Select

                oPosition.X = oSaveCoordinates.X
                oPosition.Y = oSaveCoordinates.Y
            End If

            Mouse.Move(oPosition)

            Threading.Thread.Sleep(200)

            Mouse.LeftClick()

            Threading.Thread.Sleep(200)

        Catch ex As Exception

            Throw

        End Try

    End Sub

    Private Sub AssignMasteries(ByVal masteryPage As MasteryPage)

        Try

            For Each oMastery As Mastery In masteryPage.FerocityTree

                AssignMastery(oMastery)

            Next oMastery

            For Each oMastery As Mastery In masteryPage.CunningTree

                AssignMastery(oMastery)

            Next oMastery

            For Each oMastery As Mastery In masteryPage.ResolveTree

                AssignMastery(oMastery)

            Next oMastery

        Catch ex As Exception

            Throw

        End Try

    End Sub

    ''' <summary>
    ''' Assigns a mastery in the mastery window.
    ''' </summary>
    ''' <param name="mastery">The mastery to assign.</param>
    Private Sub AssignMastery(ByVal mastery As Mastery)

        Try

            ' If the mastery has at least one point in it
            If mastery.Ranks > 0 Then

                ' Get the position of the mastery node
                Dim oMasteryPosition = GetMasteryNodePosition(mastery)

                ' Move the mouse to the mastery
                Mouse.Move(oMasteryPosition)

                ' Sleep the thread to ensure the mouse has time to move
                Threading.Thread.Sleep(200)

                ' If the mastery has all of the points for that row
                If mastery.Ranks = 5 Then

                    ' Only need to click once
                    Mouse.LeftClick()

                    ' Sleep the thread to ensure the click has time to complete
                    Threading.Thread.Sleep(200)

                Else ' Points shared between both masteries on the row

                    ' Loop through the mastery ranks
                    For iRank As Integer = 0 To mastery.Ranks - 1 Step 1

                        ' Click to assign the mastery
                        Mouse.LeftClick()

                        ' Sleep the thread to ensure the click has time to complete
                        Threading.Thread.Sleep(200)

                    Next iRank ' Loop through the mastery ranks

                End If ' If the mastery has all of the points for that row

            End If ' If the mastery has at least one point in it

        Catch ex As Exception

            ' Throw the exception
            Throw

        End Try

    End Sub

    Private Function GetMasteryNodePosition(ByVal mastery As Mastery) As Point

        Try

            Dim sID As String = mastery.ID.ToString

            Dim oLocator As New MasteryLocator()

            Dim oClientPosition As Point = GetLeagueClientWindowPosition()
            Dim oClientSize As Size = GetLeagueClientWindowSize()
            Dim oMasteryNodeSize As Size = CalculateMasteryNodeSize()

            Dim oRefererenceClientSize As Size = _MasteryCoordinatesListFile.ReferenceClientSize
            Dim oMasteryPosition As Point
            Dim oAbsoluteCoordinates As Boolean = _MasteryCoordinatesListFile.ReferenceCoordinatesAbsolute

            Select Case _Mode

                Case Modes.Menu

                    oMasteryPosition = _MasteryCoordinatesListFile.MasteryCoordinatesMenu(sID)

                Case Modes.ChampionSelect

                    oMasteryPosition = _MasteryCoordinatesListFile.MasteryCoordinatesChampionSelect(sID)

            End Select

            If Not oAbsoluteCoordinates Then

                Dim oPosition As New Point

                With oPosition
                    .X = oClientPosition.X + CInt(oMasteryPosition.X * (oClientSize.Width / oRefererenceClientSize.Width)) + (oMasteryNodeSize.Width \ 2)
                    .Y = oClientPosition.Y + CInt(oMasteryPosition.Y * (oClientSize.Height / oRefererenceClientSize.Height)) + (oMasteryNodeSize.Height \ 2)
                End With

                Return oPosition

            Else

                Dim oPosition As New Point

                With oPosition
                    .X = oMasteryPosition.X
                    .Y = oMasteryPosition.Y
                End With

                Return oPosition

            End If


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

            Dim oLeagueWindow As IntPtr = HwndInterface.GetHwndFromTitle(My.Resources.LeagueClientWindowTitle)

            If oLeagueWindow = IntPtr.Zero Then

                Return HwndInterface.GetHwndFromTitle(My.Resources.LeagueClientWindowTitleAlternative)

            Else

                Return oLeagueWindow

            End If

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

    Private Function IsClientMinimized() As Boolean

        Try

            Dim oLeagueClientWindowPosition As Point = GetLeagueClientWindowPosition()

            Return oLeagueClientWindowPosition.Y < 0

        Catch ex As Exception

            Throw

        End Try

    End Function

End Class
