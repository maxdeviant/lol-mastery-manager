Public Class MasteryAssigner

    Private Structure ScaleFactor

        Public Structure MasteryNode

            Public Const X As Double = 1024 / 40
            Public Const Y As Double = 640 / 40

        End Structure

        Public Structure SaveMasteriesButton

            Public Const Width As Integer = 175
            Public Const Height As Integer = 25

            Public Structure ChampionSelect

                Public Structure Small

                    Public Const X As Double = 1024 / 120
                    Public Const Y As Double = 640 / 265

                End Structure

                Public Structure Medium

                    Public Const X As Double = 1280 / 145
                    Public Const Y As Double = 800 / 295

                End Structure

                Public Structure Large

                    Public Const X As Double = 1440 / 85
                    Public Const Y As Double = 900 / 285

                End Structure

            End Structure

            Public Structure Menu

                Public Const X As Double = 1024 / 105
                Public Const Y As Double = 640 / 300

            End Structure

        End Structure

        Public Structure ReturnPointsButton

            Public Const Width As Integer = 175
            Public Const Height As Integer = 25

            Public Structure ChampionSelect

                Public Structure Small

                    Public Const X As Double = 1024 / 120
                    Public Const Y As Double = 640 / 285

                End Structure

                Public Structure Medium

                    Public Const X As Double = 1280 / 145
                    Public Const Y As Double = 800 / 325

                End Structure

                Public Structure Large

                    Public Const X As Double = 1440 / 85
                    Public Const Y As Double = 900 / 320

                End Structure

            End Structure

            Public Structure Menu

                Public Const X As Double = 1024 / 105
                Public Const Y As Double = 640 / 325

            End Structure

        End Structure

        Public Structure MasteryTree

            Public Const Width As Double = 1024 / 220
            Public Const Height As Double = 640 / 380

            Public Structure ChampionSelect

                Public Const X As Double = 1024 / 290
                Public Const Y As Double = 640 / 160

            End Structure

            Public Structure Menu

                Public Const X As Double = 1024 / 275
                Public Const Y As Double = 640 / 190

            End Structure

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

            oPosition.X = oClientPosition.X + oReturnPointsButtonOffsets.X + (ScaleFactor.ReturnPointsButton.Width \ 2)
            oPosition.Y = oClientPosition.Y + oReturnPointsButtonOffsets.Y + (ScaleFactor.ReturnPointsButton.Height \ 2)

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

            oPosition.X = oClientPosition.X + oSaveMasteriesButtonOffsets.X + (ScaleFactor.SaveMasteriesButton.Width \ 2)
            oPosition.Y = oClientPosition.Y + oSaveMasteriesButtonOffsets.Y + (ScaleFactor.SaveMasteriesButton.Height \ 2)

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

            Mouse.Unlock()

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

            Dim iMasteryNodeWidth As Integer = CInt(Math.Floor(oLeagueClientWindowSize.Width / ScaleFactor.MasteryNode.X))
            Dim iMasteryNodeHeight As Integer = CInt(Math.Floor(oLeagueClientWindowSize.Height / ScaleFactor.MasteryNode.Y))

            Return New Size(iMasteryNodeWidth, iMasteryNodeHeight)

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

                    Case < 1200

                        oSaveMasteriesButtonOffsets.X = CInt(Math.Floor(oLeagueClientWindowSize.Width / ScaleFactor.SaveMasteriesButton.ChampionSelect.Small.X))
                        oSaveMasteriesButtonOffsets.Y = CInt(Math.Floor(oLeagueClientWindowSize.Height / ScaleFactor.SaveMasteriesButton.ChampionSelect.Small.Y))

                    Case >= 1200, < 1440

                        oSaveMasteriesButtonOffsets.X = CInt(Math.Floor(oLeagueClientWindowSize.Width / ScaleFactor.SaveMasteriesButton.ChampionSelect.Medium.X))
                        oSaveMasteriesButtonOffsets.Y = CInt(Math.Floor(oLeagueClientWindowSize.Height / ScaleFactor.SaveMasteriesButton.ChampionSelect.Medium.Y))

                    Case >= 1440

                        oSaveMasteriesButtonOffsets.X = CInt(Math.Floor(oLeagueClientWindowSize.Width / ScaleFactor.SaveMasteriesButton.ChampionSelect.Large.X))
                        oSaveMasteriesButtonOffsets.Y = CInt(Math.Floor(oLeagueClientWindowSize.Height / ScaleFactor.SaveMasteriesButton.ChampionSelect.Large.Y))

                End Select

            Else

                oSaveMasteriesButtonOffsets.X = CInt(Math.Floor(oLeagueClientWindowSize.Width / ScaleFactor.SaveMasteriesButton.Menu.X))
                oSaveMasteriesButtonOffsets.Y = CInt(Math.Floor(oLeagueClientWindowSize.Height / ScaleFactor.SaveMasteriesButton.Menu.Y))

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

                    Case < 1200

                        oReturnPointsButtonOffsets.X = CInt(Math.Floor(oLeagueClientWindowSize.Width / ScaleFactor.ReturnPointsButton.ChampionSelect.Small.X))
                        oReturnPointsButtonOffsets.Y = CInt(Math.Floor(oLeagueClientWindowSize.Height / ScaleFactor.ReturnPointsButton.ChampionSelect.Small.Y))

                    Case >= 1200, < 1440

                        oReturnPointsButtonOffsets.X = CInt(Math.Floor(oLeagueClientWindowSize.Width / ScaleFactor.ReturnPointsButton.ChampionSelect.Medium.X))
                        oReturnPointsButtonOffsets.Y = CInt(Math.Floor(oLeagueClientWindowSize.Height / ScaleFactor.ReturnPointsButton.ChampionSelect.Medium.Y))

                    Case >= 1440

                        oReturnPointsButtonOffsets.X = CInt(Math.Floor(oLeagueClientWindowSize.Width / ScaleFactor.ReturnPointsButton.ChampionSelect.Large.X))
                        oReturnPointsButtonOffsets.Y = CInt(Math.Floor(oLeagueClientWindowSize.Height / ScaleFactor.ReturnPointsButton.ChampionSelect.Large.Y))

                End Select

            Else

                oReturnPointsButtonOffsets.X = CInt(Math.Floor(oLeagueClientWindowSize.Width / ScaleFactor.ReturnPointsButton.Menu.X))
                oReturnPointsButtonOffsets.Y = CInt(Math.Floor(oLeagueClientWindowSize.Height / ScaleFactor.ReturnPointsButton.Menu.Y))

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

                oMasteryTreeOffsets.X = CInt(Math.Floor(oLeagueClientWindowSize.Width / ScaleFactor.MasteryTree.ChampionSelect.X) + Math.Floor(((masteryTree - 1) * (oLeagueClientWindowSize.Width / ScaleFactor.MasteryTree.Width))))
                oMasteryTreeOffsets.Y = CInt(Math.Floor(oLeagueClientWindowSize.Height / ScaleFactor.MasteryTree.ChampionSelect.Y))

            Else

                oMasteryTreeOffsets.X = CInt(Math.Floor(oLeagueClientWindowSize.Width / ScaleFactor.MasteryTree.Menu.X) + Math.Floor((masteryTree - 1) * (oLeagueClientWindowSize.Width / ScaleFactor.MasteryTree.Width)))
                oMasteryTreeOffsets.Y = CInt(Math.Floor(oLeagueClientWindowSize.Height / ScaleFactor.MasteryTree.Menu.Y))

            End If

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
