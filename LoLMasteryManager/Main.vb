Imports System.Timers

Public Class Main

#If DEBUG Then

    Private WithEvents _Timer As Timer

#End If

    Private _MasteryManager As New MasteryManager

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try

#If DEBUG Then

            _Timer = New Timer(1000)

            _Timer.Enabled = False

#End If

            With cboChampion
                .AutoCompleteMode = AutoCompleteMode.SuggestAppend
                .AutoCompleteSource = AutoCompleteSource.ListItems
            End With

            With cboRole
                .AutoCompleteMode = AutoCompleteMode.SuggestAppend
                .AutoCompleteSource = AutoCompleteSource.ListItems
            End With

            _MasteryManager.PopulateChampions(cboChampion)

            _MasteryManager.PopulateRoles(cboRole)

            _MasteryManager.PopulateStats(cboStats)

        Catch ex As Exception

            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub btnAssignMasteries_Click(sender As Object, e As EventArgs) Handles btnAssignMasteries.Click

        Try

            Dim sChampionKey As String = CType(cboChampion.SelectedItem, Champion).Key
            Dim sRole As String = cboRole.SelectedItem.ToString
            Dim sStat As String = cboStats.SelectedItem.ToString

            _MasteryManager.AssignMasteries(sChampionKey, sRole, sStat)

        Catch ex As Exception

            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub chkInChampionSelect_CheckedChanged(sender As Object, e As EventArgs) Handles chkInChampionSelect.CheckedChanged

        Try

            If chkInChampionSelect.Checked Then

                _MasteryManager.SetMode(Modes.ChampionSelect)

            Else

                _MasteryManager.SetMode(Modes.Menu)

            End If


        Catch ex As Exception

            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

#If DEBUG Then

    Private Sub _Timer_Elapsed(sender As Object, e As ElapsedEventArgs) Handles _Timer.Elapsed

        Dim oLeagueWindow As IntPtr = HwndInterface.GetHwndFromTitle("PVP.net Client")

        HwndInterface.ActivateWindow(oLeagueWindow)

        Dim oLeaguePosition = HwndInterface.GetHwndPos(oLeagueWindow)

        Debug.WriteLine(New Point(Cursor.Position.X - oLeaguePosition.X, Cursor.Position.Y - oLeaguePosition.Y))

    End Sub

#End If

End Class
